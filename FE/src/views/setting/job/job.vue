<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row>
                        <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>
        <create-job v-model="createModalShow" @save-success="getpage"></create-job>
        <edit-job v-model="editModalShow" @save-success="getpage"></edit-job>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Url from '@/lib/url'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    import CreateJob from './create-job.vue'
    import EditJob from './edit-job.vue'
    import Util from '@/lib/util';

    class  PageJobRequest extends PageRequest{
        keyword:string;
        isActive:boolean=null;//nullable
        from:Date;
        to:Date;
    }

    @Component({
        components:{CreateJob,EditJob}
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
            return this.$store.state.job.list;
        };
        get loading(){
            return this.$store.state.job.loading;
        }
        create(){
            this.createModalShow=true;
        }

        isActiveChange(val:string){
            console.log(val);
            if(val==='Actived'){
                this.pagerequest.isActive=true;
            }else if(val==='NoActive'){
                this.pagerequest.isActive=false;
            }else{
                this.pagerequest.isActive=null;
            }
        }
        pageChange(page:number){
            this.$store.commit('job/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('job/setPageSize',pagesize);
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
                type:'job/getAll',
                data:this.pagerequest
            })
        }
        get pageSize(){
            return this.$store.state.job.pageSize;
        }
        get totalCount(){
            return this.$store.state.job.totalCount;
        }
        get currentPage(){
            return this.$store.state.job.currentPage;
        }
        columns=[{
            title:this.L('JobName'),
            key:'jobName'
        },
        {
            title:this.L('IsEnable'),
            render:(h:any,params:any)=>{
                return h('span',params.row.isEnable ? "已启用" : "已禁用")
            }
        },
        {
            title:this.L('ProjectName'),
            key:'projectName'
        },
        {
            title:this.L('Frequency'),
            key:'frequency'
        },
        {
            title:this.L('ApiUrl'),
            key:'apiUrl'
        },
        {
            title:this.L('Cron'),
            key:'cron'
        },
        {
            title:this.L('CreatedAt'),
            render:(h:any,params:any)=>{
                return h('span',new Date(params.row.createdAt).toLocaleString())
            }
        },
        {
            title:this.L('LastExecution'),
            render:(h:any,params:any)=>{
                return h('span',new Date(params.row.lastExecution).toLocaleString())
            }
        },
        {
            title:this.L('NextExecution'),
            render:(h:any,params:any)=>{
                return h('span',new Date(params.row.nextExecution).toLocaleString())
            }
        },
        {
            title:this.L('Actions'),
            key:'Actions',
            width:300,
            render:(h:any,params:any)=>{
                return h('div',[
                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:()=>{
                                this.$store.commit('job/edit',params.row);
                                this.edit();
                            }
                        }
                    },this.L('Edit')),

                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                             click:async()=>{
                                await this.$store.dispatch({
                                                type:'job/enable',
                                                data:params.row
                                            });

                                 this.$Notice.success({
                                    title: this.L('Success')
                                });

                                await this.getpage();
                            }
                        }
                    },this.L('Enable')),
                     h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                           click:async()=>{
                                await this.$store.dispatch({
                                                type:'job/disable',
                                                data:params.row
                                            });

                                 this.$Notice.success({
                                    title: this.L('Success')
                                });

                                await this.getpage();
                            }
                        }
                    },this.L('Disable')),
                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:async()=>{
                                await this.$store.dispatch({
                                                type:'job/trigger',
                                                data:params.row
                                            });

                                 this.$Notice.success({
                                    title: this.L('TriggerSuccess')
                                });

                                await this.getpage();
                            }
                        }
                    },this.L('Trigger')),
                    h('Button',{
                        props:{
                            type:'error',
                            size:'small'
                        },
                        on:{
                            click:async ()=>{
                                this.$Modal.confirm({
                                        title:this.L('Tips'),
                                        content:this.L('DeleteJobConfirm'),
                                        okText:this.L('Yes'),
                                        cancelText:this.L('No'),
                                        onOk:async()=>{
                                            await this.$store.dispatch({
                                                type:'job/delete',
                                                data:params.row
                                            })
                                            
                                            await this.getpage();
                                        }
                                })
                            }
                        }
                    },this.L('Delete'))
                ])
            }
        }]
        async created(){
            this.getpage();
        }
    }
</script>