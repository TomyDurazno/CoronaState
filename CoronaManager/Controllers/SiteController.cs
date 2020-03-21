using Microsoft.AspNetCore.Mvc;

namespace CoronaManager.Controllers
{
    [Route("")]
    public class SiteController : Controller
    {
        public IActionResult Index() => View();
    }
}