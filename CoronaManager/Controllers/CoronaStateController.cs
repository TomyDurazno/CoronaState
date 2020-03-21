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

        public CoronaStateController(ILogger<CoronaStateController> logger)
        {
            _logger = logger;         
            serviceNinja = new CoronaNinjaAPIService();
        }

        #endregion

        #region Endpoints

        [HttpGet]
        [Route("/top5bydeaths")]
        public async Task<ChartDTO> Top5CountriesByDeaths() => await serviceNinja.Top5CountriesByDeaths();

        [HttpGet]
        [Route("/top5bydeathstoday")]
        public async Task<ChartDTO> Top5CountriesByDeathsToday() => await serviceNinja.Top5CountriesByDeathsToday();

        [HttpGet]
        [Route("/casesbystatus")]
        public async Task<ChartDTO> CasesByStatus(bool south = false) => await serviceNinja.CasesByStatus(south);

        [HttpGet]
        [Route("/bycontinent")]
        public async Task<ChartDTO> AmountByContinent() => await serviceNinja.AmountByContinent();

        [HttpGet]
        [Route("/linechart")]
        public async Task<ChartDTO> TodayLineChart() => await serviceNinja.GetTodayLineChart();

        #endregion
    }
}
