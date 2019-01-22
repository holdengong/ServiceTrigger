<template>
    <div>
        <Modal
         :title="L('CreateNewProject')"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="projectForm"  label-position="top" :rules="projectRule" :model="project">
                <Tabs value="detail">
                    <TabPane :label="L('ProjectDetails')" name="detail">
                        <FormItem :label="L('ProjectName')" prop="projectName">
                            <Input v-model="project.projectName" :maxlength="32" :minlength="2"></Input>
                        </FormItem>
                        <FormItem :label="L('Host')" prop="host">
                            <Input v-model="project.Host" :maxlength="32" :minlength="2"></Input>
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
    import Project from '../../../store/entities/project'
    @Component
    export default class CreateProject extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        project:Project=new Project();
        save(){
            (this.$refs.projectForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'project/create',
                        data:this.project
                    });
                    (this.$refs.projectForm as any).resetFields();
                    this.$emit('save-success');
                    this.$emit('input',false);
                }
            })
        }
        cancel(){
            (this.$refs.projectForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
        }
        validatePassCheck = (rule:any, value:any, callback:any) => {
        };
        projectRule={
        }
    }
</script>

