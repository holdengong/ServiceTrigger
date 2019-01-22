import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Project from '../entities/project'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'
interface ProjectState extends ListState<Project>{
    editProject:Project
}
class ProjectModule extends ListModule<ProjectState,any,Project>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Project>(),
        loading:false,
        editProject:new Project()
    }
    actions={
        async getAll(context:ActionContext<ProjectState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Project/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Project>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ProjectState,any>,payload:any){
            await Ajax.post('/api/services/app/Project/Create',payload.data);
        },
        async update(context:ActionContext<ProjectState,any>,payload:any){
            await Ajax.put('/api/services/app/Project/Update',payload.data);
        },
        async delete(context:ActionContext<ProjectState,any>,payload:any){
            await Ajax.delete('/api/services/app/Project/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<ProjectState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Project/Get?Id='+payload.id);
            return reponse.data.result as Project;
        }
    };
    mutations={
        setCurrentPage(state:ProjectState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ProjectState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ProjectState,project:Project){
            state.editProject=project;
        }
    }
}
const projectModule=new ProjectModule();
export default projectModule;