using ServiceTrigger.EntityFrameworkCore;

namespace ServiceTrigger.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly ServiceTriggerDbContext _context;

        public TestDataBuilder(ServiceTriggerDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}