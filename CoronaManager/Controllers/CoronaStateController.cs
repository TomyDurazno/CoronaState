using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoronaManager.Models;
using CoronaManager.Models.DTO;
using CoronaManager.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using RestSharp;

namespace CoronaManager.Controllers
{
    [ApiController]
    [Route("state")]
    public class CoronaStateController : ControllerBase
    {
        #region Properties

        ILogger<CoronaStateController> _logger;
        
        CoronaNinjaAPIService serviceNinja;

        #endregion        

        #region Constructor

        public CoronaStateController(ILogger<CoronaStateController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
         
            serviceNinja = new CoronaNinjaAPIService();
        }

        #endregion

        #region Endpoints

        [HttpGet]
        [Route("/top5muertes")]
        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertes() => await serviceNinja.Top5PaisesPorMuertes();

        [HttpGet]
        [Route("/top5muertesdehoy")]
        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertesHoy() => await serviceNinja.Top5PaisesPorMuertesHoy();

        [HttpGet]
        [Route("/casesbystatus")]
        public async Task<BarChartDTO> CasesByStatus(bool south = false) => await serviceNinja.CasesByStatus(south);

        [HttpGet]
        [Route("/hopkins")]
        public async Task<List<CoronaHopkinsCSSEState>> HopkinsState() => await serviceNinja.GetHopkinsState();

        [HttpGet]
        [Route("/bycontinent")]
        public async Task<List<ContinentAndAmountsDTO>> AmountByContinent() => await serviceNinja.AmountByContinent();

        #endregion
    }
}
