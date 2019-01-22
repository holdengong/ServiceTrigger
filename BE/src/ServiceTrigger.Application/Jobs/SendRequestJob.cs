using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    public class SendRequestJob : BackgroundJob<SendRequestJobArgs>, ITransientDependency
    {

        public SendRequestJob()
        {
        }

        public override void Execute(SendRequestJobArgs args)
        {
            var url = args.Host.Trim('/') + "/" + args.ApiUrl;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var result = request.GetResponse();
        }
    }
}
