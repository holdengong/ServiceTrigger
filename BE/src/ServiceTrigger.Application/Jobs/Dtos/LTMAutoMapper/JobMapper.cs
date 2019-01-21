using AutoMapper;

namespace ServiceTrigger.Jobs.Dtos.LTMAutoMapper
{
    /// <summary>
    /// 配置Job的AutoMapper
    /// </summary>
    internal static class JobMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Job, JobListDto>();
            configuration.CreateMap<JobEditDto, Job>();
        }
    }
}