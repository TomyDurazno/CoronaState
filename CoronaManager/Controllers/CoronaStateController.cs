using System.Threading.Tasks;
using CoronaManager.Models.DTO;
using CoronaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [Route("/casesbycontinent")]
        public async Task<ChartDTO> CasesByContinent() => await serviceNinja.CasesByContinent();

        [HttpGet]
        [Route("/deathsbycontinent")]
        public async Task<ChartDTO> DeathsByContinent() => await serviceNinja.DeathsByContinent();

        [HttpGet]
        [Route("/linechart")]
        public async Task<ChartDTO> TodayLineChart() => await serviceNinja.GetTodayLineChart();

        [HttpGet]
        [Route("/linechartsouth")]
        public async Task<ChartDTO> TodayLineChartSouth() => await serviceNinja.GetTodayLineChartSouth();

        [HttpGet]
        [Route("/linechartalltime")]
        public async Task<ChartDTO> LineChartAllTime() => await serviceNinja.GetAllTimeLineChart();

        [HttpGet]
        [Route("/linechartalltimesouth")]
        public async Task<ChartDTO> LineChartAllTimeSouth() => await serviceNinja.GetAllTimeLineChartSouth();

        #endregion
    }
}
