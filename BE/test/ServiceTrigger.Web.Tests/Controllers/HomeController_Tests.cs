using System.Threading.Tasks;
using ServiceTrigger.Web.Controllers;
using Shouldly;
using Xunit;

namespace ServiceTrigger.Web.Tests.Controllers
{
    public class HomeController_Tests: ServiceTriggerWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
