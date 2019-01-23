import Entity from './entity'
export default class Job extends Entity<number>{
    jobName:string;
    projectName:string;
    frequency:number;
    apiUrl:string;
}