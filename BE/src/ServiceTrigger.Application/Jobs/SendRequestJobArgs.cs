using System;

namespace ServiceTrigger.Jobs
{
    [Serializable]
    public class SendRequestJobArgs
    {
        public int JobId { get; set; }
        public string Host { get; set; }
        public string ApiUrl { get; set; }
    }
}