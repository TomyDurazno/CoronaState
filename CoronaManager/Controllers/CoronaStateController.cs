using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaManager.Models;
using CoronaManager.Services;
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

        public CoronaStateController(ILogger<CoronaStateController> logger)
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
        [Route("/radarchart")]
        public async Task<List<RadarDTO>> RadarChart() => await serviceNinja.RadarChart();

        [HttpGet]
        [Route("/casesbystatus")]
        public async Task<BarChartDTO> CasesByStatus(bool south = false) => await serviceNinja.CasesByStatus(south);

        #endregion
    }
}
