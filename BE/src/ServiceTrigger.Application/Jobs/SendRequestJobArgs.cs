using System;

namespace ServiceTrigger.Jobs
{
    [Serializable]
    public class SendRequestJobArgs
    {
        public string Host { get; set; }
        public string ApiUrl { get; set; }
    }
}