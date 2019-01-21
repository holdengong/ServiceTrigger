using AutoMapper;

namespace ServiceTrigger.Projects.Dtos.LTMAutoMapper
{
    /// <summary>
    /// 配置Project的AutoMapper
    /// </summary>
    internal static class ProjectMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Project, ProjectListDto>();
            configuration.CreateMap<ProjectEditDto, Project>();
        }
    }
}