using Microsoft.AspNetCore.Mvc;

namespace ServiceTrigger.Web.Controllers
{
    public class HomeController : ServiceTriggerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}