using CoronaManager.Models;
using CoronaManager.Properties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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