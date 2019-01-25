import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Job from '../entities/job'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'
interface JobState extends ListState<Job>{
    editJob:Job
}
class JobModule extends ListModule<JobState,any,Job>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Job>(),
        loading:false,
        editJob:new Job()
    }
    actions={
        async getAll(context:ActionContext<JobState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Job/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Job>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<JobState,any>,payload:any){
            await Ajax.post('/api/services/app/Job/Create',payload.data);
        },
        async update(context:ActionContext<JobState,any>,payload:any){
            await Ajax.put('/api/services/app/Job/Update',payload.data);
        },
        async delete(context:ActionContext<JobState,any>,payload:any){
            await Ajax.delete('/api/services/app/Job/Delete?Id='+payload.data.id);
        },
        async trigger(context:ActionContext<JobState,any>,payload:any){
            await Ajax.post('/api/services/app/Job/Trigger', payload.data);
        },
        async enable(context:ActionContext<JobState,any>,payload:any){
            await Ajax.post('/api/services/app/Job/Enable', payload.data);
        },
        async disable(context:ActionContext<JobState,any>,payload:any){
            await Ajax.post('/api/services/app/Job/Disable', payload.data);
        },
        async get(context:ActionContext<JobState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Job/Get?Id='+payload.id);
            return reponse.data.result as Job;
        }
    };
    mutations={
        setCurrentPage(state:JobState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:JobState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:JobState,job:Job){
            state.editJob=job;
        }
    }
}
const jobModule=new JobModule();
export default jobModule;