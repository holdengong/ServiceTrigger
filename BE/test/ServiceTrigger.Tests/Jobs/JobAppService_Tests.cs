using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Abp.Application.Services.Dto;
using ServiceTrigger.Users;
using ServiceTrigger.Users.Dto;
using ServiceTrigger.Jobs;
using ServiceTrigger.Jobs.Dtos;

namespace ServiceTrigger.Tests.Users
{
    public class JobAppService_Tests : ServiceTriggerTestBase
    {
        private readonly IJobAppService _jobAppService;

        public JobAppService_Tests()
        {
            _jobAppService = Resolve<IJobAppService>();
        }


        [Fact]
        public async Task List_Test()
        {
            // Act
            var output = await _jobAppService.GetPagedJobAsync(new GetJobInput()
            {
                MaxResultCount = 20,
                SkipCount = 0
            });

            // Assert
            output.Items.Count.ShouldBeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task Create_Test()
        {
            await _jobAppService.CreateOrUpdateJobAsync(
                new CreateOrUpdateJobInput()
                {
                    Job = new JobEditDto()
                    {
                        ApiUrl = "api/job/create",
                        Frequency = FrequencyEnum.Daily,
                        IsEnable = false,
                        JobName = "myJob",
                        ProjectId = 1,
                        Id = 0
                    }
                });

            await UsingDbContextAsync(async context =>
            {
                var job = await context.Jobs.FirstOrDefaultAsync(e => e.JobName == "myJob");
                job.ShouldNotBeNull();
            });
        }
    }
}
