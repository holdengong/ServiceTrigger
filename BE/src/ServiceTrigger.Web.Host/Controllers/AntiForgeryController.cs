using Microsoft.AspNetCore.Antiforgery;
using ServiceTrigger.Controllers;

namespace ServiceTrigger.Web.Host.Controllers
{
    public class AntiForgeryController : ServiceTriggerControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
