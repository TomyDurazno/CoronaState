using CoronaManager.Models;
using CoronaManager.Models.DTO;
using CoronaManager.Properties;
using LazyCache;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Services
{
    public class CoronaNinjaAPIService : IChartService
    {
        #region State Getters

        async Task<T> GetJArray<T>(string url)
        {
            var client = new RestClient(new Uri(url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<T>();
        }

        IAppCache cache;

        async Task<List<CoronaCountryState>> GetCountryState()
        {
            var expires = DateTimeOffset.Now.AddMinutes(Convert.ToInt32(Resources.CacheExpiresIn));
            return await cache.GetOrAdd("NinjaState", () => GetJArray<List<CoronaCountryState>>(Resources.Ninja_Countries_Url), expires);
        }

        async Task<List<CoronaHopkinsCSSEState>> GetHopkinsCSSEState() => await GetJArray<List<CoronaHopkinsCSSEState>>(Resources.Ninja_Hopkins_Url);

        #endregion

        #region State Singletons

        public AsyncLazy<List<CoronaCountryState>> GlobalState;
        public AsyncLazy<List<CoronaHopkinsCSSEState>> HopkinsCSSEState;

        public AsyncLazy<List<Continent>> ContinentsState;
        public AsyncLazy<List<ContinentAndAmountsDTO>> ContinentAndAmountsState;

        public ConstantsLazyService LazyService;

        #endregion

        #region Compounds

        async Task<List<Continent>> GetContinents()
        {
            var files = new[]
            {
                new Continent(Continent.Africa.Name(), LazyService.Africa.Value),
                new Continent(Continent.Asia.Name(), LazyService.Asia.Value),
                new Continent(Continent.Oceania.Name(), LazyService.Oceania.Value),
                new Continent(Continent.Europe.Name(),  LazyService.Europe.Value),
                new Continent(Continent.North_America.Name(), LazyService.North_America.Value),
                new Continent(Continent.South_America.Name(), LazyService.South_America.Value)
            }.ToList();

            return await Task.FromResult(files);
        }

        async Task<List<ContinentAndAmountsDTO>> GetAmountByContinent()
        {
            var countries = await GlobalState;
            var continents = await ContinentsState;

            return countries.GroupBy(c => continents.FirstOrDefault(ct => ct.Countries.Contains(c.country))?.Name ?? "Other")
                            .Select(g => new ContinentAndAmountsDTO(g.Key, g.ToList()))
                            .ToList();
        }

        #endregion

        #region Constructor

        public CoronaNinjaAPIService(ConstantsLazyService lazyService, IAppCache cach)
        {
            GlobalState = new AsyncLazy<List<CoronaCountryState>>(GetCountryState);
            HopkinsCSSEState = new AsyncLazy<List<CoronaHopkinsCSSEState>>(GetHopkinsCSSEState);
            ContinentsState = new AsyncLazy<List<Continent>>(GetContinents);
            ContinentAndAmountsState = new AsyncLazy<List<ContinentAndAmountsDTO>>(GetAmountByContinent);

            LazyService = lazyService;
            cache = cach; 
        }

        #endregion

        async Task<List<CoronaCountryState>> State(Continents continent)
        {
            switch (continent)
            {
                case Continents.All:
                    return await GlobalState;
                case Continents.Asia:
                    return (await GlobalState)
                            .Where(c => LazyService.Asia.Value.Contains(c.country))
                            .ToList();
                case Continents.Africa:
                    return (await GlobalState)
                            .Where(c => LazyService.Africa.Value.Contains(c.country))
                            .ToList();
                case Continents.Europe:
                    return (await GlobalState)
                            .Where(c => LazyService.Europe.Value.Contains(c.country))
                            .ToList();
                case Continents.North_America:
                    return (await GlobalState)
                            .Where(c => LazyService.North_America.Value.Contains(c.country))
                            .ToList();
                case Continents.South_America:
                    return (await GlobalState)
                            .Where(c => LazyService.South_America.Value.Contains(c.country))
                            .ToList();
                case Continents.Oceania:
                    return (await GlobalState)
                            .Where(c => LazyService.Oceania.Value.Contains(c.country))
                            .ToList();
                default:
                    return await GlobalState;
            }
        }

        #region Public API Methods

        public async Task<ChartDTO> Top5CountriesBy(Continents continent, Func<CoronaCountryState, int> selector)
        {
            var state = await State(continent);

            var countries = state.OrderByDescending(selector)
                                 .Take(5);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func).ToArray();

            var datasets = new[]
            {
                new DataSetDTO("amount", Scalar(selector), LazyService.FiveColors.Value),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        public async Task<ChartDTO> StatusByContinent(Continents continent)
        {
            var state = await State(continent);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.Take(10).Select(func).ToArray();

            return new ChartDTO(Scalar(c => c.country), new[] { new DataSetDTO("Active", Scalar(c => c.active),  ChartColors.Cyan),
                                                                new DataSetDTO("Recovered", Scalar(c => c.recovered), ChartColors.SoftGray),
                                                                new DataSetDTO("Critical", Scalar(c => c.critical),  ChartColors.SoftRed) });
        }

        public async Task<ChartDTO> ByContinents(Func<ContinentAndAmountsDTO, int> selector)
        {
            var state = await ContinentAndAmountsState;

            T[] Scalar<T>(Func<ContinentAndAmountsDTO, T> func) => state.Select(func)
                                                                        .ToArray();

            var datasets = new[]
            {
                new DataSetDTO("amount", Scalar(selector), LazyService.AllColors.Value),
            };

            return new ChartDTO(Scalar(c => c.Name), datasets);
        }

        public async Task<ChartDTO> TodayLineChart(Continents continent)
        {
            var state = await State(continent);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.todayDeaths)
                                                                     .Select(func)
                                                                     .Take(25)
                                                                     .ToArray();

            return new ChartDTO(Scalar(c => c.country), 
                   new DataSetDTO("Cases", Scalar(c => c.todayCases), ChartColors.SoftGreen),
                   new DataSetDTO("Deaths", Scalar(c => c.todayDeaths), ChartColors.Red));
        }

        public async Task<ChartDTO> AllTimeDeathsLineChart(Continents continent)
        {
            var state = await State(continent);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.deaths)
                                                                     .Select(func)
                                                                     .Take(25)
                                                                     .ToArray();

            return new ChartDTO(Scalar(c => c.country), 
                   new DataSetDTO("Cases", Scalar(c => c.cases), ChartColors.SoftGreen),
                   new DataSetDTO("Deaths", Scalar(c => c.deaths), ChartColors.Red));
        }

        #endregion 
    }
}
