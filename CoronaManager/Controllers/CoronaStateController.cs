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
        public async Task<ChartDTO> Top5CountriesByDeaths(Continents continent) => await NinjaService.TakeBy(5, continent, c => c.Country, c => c.Deaths);

        [HttpGet]
        [Route("/top5bydeathstoday")]
        public async Task<ChartDTO> Top5CountriesByDeathsToday(Continents continent) => await NinjaService.TakeBy(5, continent, c => c.Country, c => c.TodayDeaths);

        [HttpGet]
        [Route("/casesbystatus")]
        public async Task<ChartDTO> CasesByStatus(Continents continent) => await NinjaService.TakeByMultiple(10, continent, c => c.Country, c => c.Active, c => c.Recovered, c => c.Critical);

        [HttpGet]
        [Route("/casesbycontinent")]
        public async Task<ChartDTO> CasesByContinent() => await NinjaService.ByContinents(c => c.cases);

        [HttpGet]
        [Route("/top10casesbyCountries")]
        public async Task<ChartDTO> Top10CasesByCountries(Continents continent) => await NinjaService.TakeBy(10, continent, c => c.Country, c => c.Cases);

        [HttpGet]
        [Route("/top10deathsbyCountries")]
        public async Task<ChartDTO> Top10DeathsByCountries(Continents continent) => await NinjaService.TakeBy(10, continent, c => c.Country, c => c.Deaths);

        [HttpGet]
        [Route("/deathsbycontinent")]
        public async Task<ChartDTO> DeathsByContinent() => await NinjaService.ByContinents(c => c.deaths);

        [HttpGet]
        [Route("/linechart")]
        public async Task<ChartDTO> TodayLineChart(Continents continent) => await NinjaService.TakeByMultiple(25, continent, c => c.Country, c => c.TodayCases, c => c.TodayDeaths);

        [HttpGet]
        [Route("/linechartalltime")]
        public async Task<ChartDTO> LineChartAllTime(Continents continent) => await NinjaService.TakeByMultiple(25, continent, c => c.Country, c => c.Cases, c => c.Deaths);

        [HttpGet]
        [Route("/casesperonemillion")]
        //TakeByMultiple con una sola expression para no perder el color de la categoría
        public async Task<ChartDTO> CasesPerOneMillion(Continents continent) => await NinjaService.TakeByMultiple(10, continent, c => c.Country, c => c.CasesPerOneMillion);

        #endregion
    }
}
