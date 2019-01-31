import Entity from './entity'
export default class Job extends Entity<number>{
    jobName:string;
    projectName:string;
    enviroment:string;
    frequency:number;
    apiUrl:string;
    job:string;
    cron:string;
    timeZoneId:string;
    queue: string;
    createdAt:Date;
    lastExecution:Date;
    lastJobId:number;
    nextExecution:Date;
    cronDescription:string;
}