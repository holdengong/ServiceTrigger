import Entity from './entity'
import Job from './job';
export default class JobHistory extends Entity<number>{
    result:boolean;
    resultString:string;
    errorMsg:number;
    httpStatusCode:string;
    job:Job;
}