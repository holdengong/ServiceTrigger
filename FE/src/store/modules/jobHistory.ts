import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import JobHistory from '../entities/jobHistory'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'
interface JobHistoryState extends ListState<JobHistory>{
    editJobHistory:JobHistory
}
class JobHistoryModule extends ListModule<JobHistoryState,any,JobHistory>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<JobHistory>(),
        loading:false,
        editJobHistory:new JobHistory()
    }
    actions={
        async getAll(context:ActionContext<JobHistoryState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/JobHistory/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<JobHistory>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        }
    };
    mutations={
        setCurrentPage(state:JobHistoryState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:JobHistoryState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:JobHistoryState,jobHistory:JobHistory){
            state.editJobHistory=jobHistory;
        }
    }
}
const jobHistoryModule=new JobHistoryModule();
export default jobHistoryModule;