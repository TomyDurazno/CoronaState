using CoronaManager.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Services
{
    public interface IChartService
    {
        public Task<ChartDTO> Top5CountriesBy(Continents continent, Func<CoronaCountryState, int> selector);

        public Task<ChartDTO> Top10CountriesBy(Continents continent, Func<CoronaCountryState, int> selector);

        public Task<ChartDTO> StatusByContinent(Continents continent);

        public Task<ChartDTO> ByContinents(Func<ContinentAndAmountsDTO, int> selector);

        public Task<ChartDTO> TodayLineChart(Continents continent);

        public Task<ChartDTO> AllTimeDeathsLineChart(Continents continent);
    }
}
