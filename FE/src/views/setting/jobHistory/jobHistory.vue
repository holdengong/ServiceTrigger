<style>
    .ivu-table .demo-table-info-row td{
        background-color: #ffffff;
        color: rgb(0, 0, 0);
    }

    .ivu-table .demo-table-error-row td{
        background-color: #ffffff;
        color: red;
    }
</style>

<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                 <Form ref="queryForm" :label-width="90" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="8">
                            <FormItem :label="L('Keyword')+':'" style="width:100%">
                                <Input v-model="pagerequest.keywords" :placeholder="L('ProjectName')+'/'+L('Enviroment')+'/'+L('JobName')+'（多个关键词用空格隔开）'"></Input>
                            </FormItem>
                        </Col>
                        <Col span="6">
                            <FormItem label="执行结果:" style="width:100%">
                                <!--Select should not set :value="'All'" it may not trigger on-change when first select 'NoActive'(or 'Actived') then select 'All'-->
                                <Select :placeholder="L('Select')" @on-change="isSuccessChange">
                                    <Option value="All">全部</Option>
                                    <Option value="Success">成功</Option>
                                    <Option value="Fail">失败</Option>
                                </Select>
                            </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :row-class-name="rowClassName" :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Url from '@/lib/url'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    import Util from '@/lib/util';

    class  PageJobRequest extends PageRequest{
        keywords:string;
        isSuccess:boolean=null;//nullable
        from:Date;
        to:Date;
    }

    @Component({
        components:{}
    })
    export default class Jobs extends AbpBase{
        edit(){
            this.editModalShow=true;
        }
        //filters
        pagerequest:PageJobRequest=new PageJobRequest();
        
        creationTime:Date[]=[];

        createModalShow:boolean=false;
        editModalShow:boolean=false;
        get list(){
            return this.$store.state.jobHistory.list;
        };
        get loading(){
            return this.$store.state.jobHistory.loading;
        }

        rowClassName (row, index) {
                if (row.result) {
                    return 'demo-table-info-row';
                } else{
                    return 'demo-table-error-row';
                }
        }

        isSuccessChange(val:string){
            console.log(val);
            if(val==='Success'){
                this.pagerequest.isSuccess=true;
            }else if(val==='Fail'){
                this.pagerequest.isSuccess=false;
            }else{
                this.pagerequest.isSuccess=null;
            }
        }
        pageChange(page:number){
            this.$store.commit('jobHistory/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('jobHistory/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
          
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;
            //filters
            
            if (this.creationTime.length>0) {
                this.pagerequest.from=this.creationTime[0];
            }
            if (this.creationTime.length>1) {
                this.pagerequest.to=this.creationTime[1];
            }

            await this.$store.dispatch({
                type:'jobHistory/getAll',
                data:this.pagerequest
            })
        }
        get pageSize(){
            return this.$store.state.jobHistory.pageSize;
        }
        get totalCount(){
            return this.$store.state.jobHistory.totalCount;
        }
        get currentPage(){
            return this.$store.state.jobHistory.currentPage;
        }
        columns=[
        {
            title:this.L('ProjectName'),
            render:(h:any,params:any)=>{
                return h('span',params.row.job.project.projectName)
            }
        },
        {
            title:this.L('Enviroment'),
            render:(h:any,params:any)=>{
                return h('span',params.row.job.project.enviroment)
            }
        },
        {
            title:this.L('JobName'),
            render:(h:any,params:any)=>{
                return h('span',params.row.job.jobName)
            }
        },
        {
            title:this.L('CreationTime'),
            render:(h:any,params:any)=>{
                return h('span',new Date(params.row.creationTime).toLocaleString())
            }
        },
        {
            title:this.L('Result'),
            render:(h:any,params:any)=>{
                return h('span',params.row.result?"成功":"失败")
            }
        },
        {
            title:this.L('ErrorMsg'),
            render:(h:any,params:any)=>{
                return h('span',params.row.result ? "" : params.row.errorMsg)
            }
        },
        {
            title:this.L('HttpStatusCode'),
            key:'httpStatusCode'
        }]
        async created(){
            this.getpage();
        }
    }
</script>