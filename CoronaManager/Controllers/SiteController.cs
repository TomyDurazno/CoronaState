using CoronaManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoronaManager.Controllers
{
    
    public class SiteController : Controller
    {
        ConstantsLazyService LazyService;

        public SiteController(ConstantsLazyService lazyService)
        {
            LazyService = lazyService;
        }

        [Route("/Index2")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("")]
        public async Task<IActionResult> Index2()
        {
            var view = await LazyService.GetViewState();

            return View(view);
        }
    }
}