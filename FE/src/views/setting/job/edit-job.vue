<template>
    <div>
        <Modal
         :title="L('EditJob')"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="jobForm"  label-position="top" :rules="jobRule" :model="job">
                  <Tabs value="detail">
                    <TabPane :label="L('JobDetails')" name="detail">
                        <FormItem :label="L('JobName')" prop="jobName">
                            <Input v-model="job.jobName" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                        <FormItem :label="L('ProjectName')" prop="projectName">
                            <Input v-model="job.projectName" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                        <FormItem :label="L('Enviroment')" prop="enviroment">
                            <Input v-model="job.enviroment" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                        <FormItem :label="L('Frequency')" prop="frequency">
                             <RadioGroup v-model="job.frequency">
                                <Radio label="10" key="每分钟">
                                    <span>每分钟</span>
                                </Radio>
                                <Radio label="20" key="每小时">
                                    <span>每小时</span>
                                </Radio>
                                <Radio label="30" key="每天">
                                    <span>每天</span>
                                </Radio>
                                <Radio label="40" key="每月">
                                    <span>每月</span>
                                </Radio>
                            </RadioGroup>
                        </FormItem>
                        <FormItem :label="L('CronFormLabel')" prop="cron">
                            <Input v-model="job.cron" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                         <FormItem :label="L('ApiUrl')" prop="apiUrl">
                            <Input v-model="job.apiUrl" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                    </TabPane>
                </Tabs>
            </Form>
            <div slot="footer">
                <Button @click="cancel">{{L('Cancel')}}</Button>
                <Button @click="save" type="primary">{{L('OK')}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '../../../lib/util'
    import AbpBase from '../../../lib/abpbase'
    import Job from '../../../store/entities/job'
    @Component
    export default class EditJob extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        job:Job=new Job();
        created(){
        }
        save(){
            (this.$refs.jobForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'job/update',
                        data:this.job
                    });
                    (this.$refs.jobForm as any).resetFields();
                    this.$emit('save-success');
                    this.$emit('input',false);
                }
            })
        }
        cancel(){
            (this.$refs.jobForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$emit('input',value);
            }else{
                this.job=Util.extend(true,{},this.$store.state.job.editJob);
            }
        }
        jobRule={
            jobName:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('JobName')),trigger: 'blur'}],
            host:[{required:true,message:this.L('FieldIsRequired',undefined,this.L('Host')),trigger: 'blur'}],
        }
    }
</script>

