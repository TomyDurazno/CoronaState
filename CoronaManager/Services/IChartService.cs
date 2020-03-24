using CoronaManager.Models.DTO;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Services
{
    public interface IChartService
    {
        public Task<ChartDTO> TakeBy(int n, Continents continent, Func<CoronaCountryState, string> namer, Expression<Func<CoronaCountryState, int>> expression);

        public Task<ChartDTO> TakeByMultiple(int n, Continents continent, Func<CoronaCountryState, string> namer, params Expression<Func<CoronaCountryState, int>>[] expressions);

        public Task<ChartDTO> ByContinents(Func<ContinentAndAmountsDTO, int> selector);
    }
}
