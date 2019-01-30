using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
using ServiceTrigger.JobHistories;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    public class SendRequestJob : BackgroundJob<SendRequestJobArgs>, ITransientDependency
    {
        private IRepository<JobHistory> _jobHistoryRepository;
        public SendRequestJob(IRepository<JobHistory> jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public override void Execute(SendRequestJobArgs args)
        {
            HttpClient hc = new HttpClient();
            var requestUrl = args.Host.EnsureEndsWith('/') + args.ApiUrl.Trim('/');

            JobHistory jobHistory = new JobHistory()
            {
                JobId = args.JobId,
                RequestUrl = requestUrl,
                CreationTime = Clock.Now
            };

            HttpResponseMessage hrm = new HttpResponseMessage();
            try
            {
                hrm = hc.GetAsync(requestUrl).Result;
                jobHistory.Result = hrm.IsSuccessStatusCode;
                jobHistory.ResultString = hrm.Content.ReadAsStringAsync().Result;
                jobHistory.ErrorMsg = hrm.IsSuccessStatusCode ? string.Empty : hrm.Content.ReadAsStringAsync().Result;
                jobHistory.HttpStatusCode = hrm.StatusCode;
            }
            catch (Exception ex)
            {
                jobHistory.Result = false;
                jobHistory.ErrorMsg = ex.Message;
            }

            _jobHistoryRepository.Insert(jobHistory);
        }
    }
}
