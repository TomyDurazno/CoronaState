using System.Threading.Tasks;
using CoronaManager.Models.DTO;
using CoronaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Controllers
{
    [ApiController]
    [Route("state")]
    public class CoronaStateController : ControllerBase
    {
        #region Properties

        ILogger<CoronaStateController> _logger;
        IChartService NinjaService;

        #endregion        

        #region Constructor

        public CoronaStateController(ILogger<CoronaStateController> logger, IChartService ninjaService)
        {
            _logger = logger;
            NinjaService = ninjaService;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        [Route("/top5bydeaths")]
        public async Task<ChartDTO> Top5CountriesByDeaths(Continents continent) => await NinjaService.Top5CountriesBy(continent, c => c.deaths);

        [HttpGet]
        [Route("/top5bydeathstoday")]
        public async Task<ChartDTO> Top5CountriesByDeathsToday(Continents continent) => await NinjaService.Top5CountriesBy(continent, c => c.todayDeaths);

        [HttpGet]
        [Route("/casesbystatus")]
        public async Task<ChartDTO> CasesByStatus(Continents continent) => await NinjaService.StatusByContinent(continent);

        [HttpGet]
        [Route("/casesbycontinent")]
        public async Task<ChartDTO> CasesByContinent() => await NinjaService.ByContinents(c => c.cases);

        [HttpGet]
        [Route("/top10casesbyCountries")]
        public async Task<ChartDTO> Top10CasesByCountries(Continents continent) => await NinjaService.Top10CountriesBy(continent, c => c.cases);

        [HttpGet]
        [Route("/deathsbycontinent")]
        public async Task<ChartDTO> DeathsByContinent() => await NinjaService.ByContinents(c => c.deaths);

        [HttpGet]
        [Route("/linechart")]
        public async Task<ChartDTO> TodayLineChart(Continents continent) => await NinjaService.TodayLineChart(continent);

        [HttpGet]
        [Route("/linechartalltime")]
        public async Task<ChartDTO> LineChartAllTime(Continents continent) => await NinjaService.AllTimeDeathsLineChart(continent);

        #endregion
    }
}
