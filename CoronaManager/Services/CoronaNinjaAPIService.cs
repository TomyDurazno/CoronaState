using CoronaManager.Models;
using CoronaManager.Models.DTO;
using CoronaManager.Properties;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaManager.Services
{
    public class CoronaNinjaAPIService
    {        
        #region State Getters

        async Task<T> GetJArray<T>(string url)
        {
            var client = new RestClient(new Uri(url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<T>();
        }

        async Task<List<CoronaCountryState>> GetCountryState() => await GetJArray<List<CoronaCountryState>>(Resources.Ninja_Countries_Url);

        async Task<List<CoronaHopkinsCSSEState>> GetHopkinsCSSEState() => await GetJArray<List<CoronaHopkinsCSSEState>>(Resources.Ninja_Hopkins_Url);

        #endregion

        #region State Singletons

        public AsyncLazy<List<CoronaCountryState>> CountryState;
        public AsyncLazy<List<CoronaCountryState>> SouthAmericaState;

        public AsyncLazy<List<CoronaHopkinsCSSEState>> HopkinsCSSEState;

        public AsyncLazy<List<Continent>> ContinentsState;
        public AsyncLazy<List<ContinentAndAmountsDTO>> ContinentAndAmountsState;

        public Lazy<string[]> AllColors;
        public Lazy<string[]> FiveColors;

        #endregion

        #region Compounds

        async Task<List<Continent>> GetContinents()
        {
            var files = new[]
            {
                new { name = Continent.Africa.Name(), Countries = Continent.Africa.AllCountries()},
                new { name = Continent.Asia.Name(), Countries = Continent.Asia.AllCountries()},
                new { name = Continent.Oceania.Name(), Countries = Continent.Oceania.AllCountries()},
                new { name = Continent.Europe.Name(), Countries = Continent.Europe.AllCountries() },
                new { name = Continent.North_America.Name(), Countries = Continent.North_America.AllCountries() },
                new { name = Continent.South_America.Name(), Countries = Continent.South_America.AllCountries() }
            };

            var result = files.Select(r => new Continent(r.name, r.Countries)).ToList();

            return await Task.FromResult(result);
        }

        async Task<List<ContinentAndAmountsDTO>> GetAmountByContinent()
        {
            var countries = await CountryState;
            var continents = await ContinentsState;

            return countries.GroupBy(c => continents.FirstOrDefault(ct => ct.Countries.Contains(c.country))?.Name ?? "Other")
                            .Select(g => new ContinentAndAmountsDTO(g.Key, g.ToList()))
                            .ToList();
        }

        async Task<List<CoronaCountryState>> GetSouthAmerica()
        {
            var state = await CountryState;

            var southAmerica = Continent.South_America.AllCountries();

            return state.Where(c => southAmerica.Contains(c.country)).ToList();
        }

        #endregion

        #region Constructor

        public CoronaNinjaAPIService()
        {
            CountryState = new AsyncLazy<List<CoronaCountryState>>(GetCountryState);
            HopkinsCSSEState = new AsyncLazy<List<CoronaHopkinsCSSEState>>(GetHopkinsCSSEState);
            ContinentsState = new AsyncLazy<List<Continent>>(GetContinents);
            ContinentAndAmountsState = new AsyncLazy<List<ContinentAndAmountsDTO>>(GetAmountByContinent);
            SouthAmericaState = new AsyncLazy<List<CoronaCountryState>>(GetSouthAmerica);

            AllColors = new Lazy<string[]>(ChartColors.GetAllColors, true);
            FiveColors = new Lazy<string[]>(() => AllColors.Value.Take(5).ToArray());
        }

        #endregion

        #region Public API Methods

        public async Task<ChartDTO> Top5CountriesByDeaths()
        {
            var state = await CountryState;

            var countries = state.OrderByDescending(c => c.deaths)
                                 .Take(5);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func)
                                                                         .ToArray();

            var datasets = new []
            { 
                new DataSetDTO("amount", Scalar(c => c.deaths), FiveColors.Value),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        public async Task<ChartDTO> Top5CountriesByDeathsToday()
        {
            var state = await CountryState;

            var countries = state.OrderByDescending(c => c.todayDeaths)
                                 .Take(5);

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func)
                                                                        .ToArray();

            var datasets = new[]
            {
                new DataSetDTO("amount", Scalar(c => c.todayDeaths), FiveColors.Value),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        public async Task<ChartDTO> CasesByStatus(bool south = false)
        {
            var state = south ? await SouthAmericaState : await CountryState;

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => state.Take(10).Select(func).ToArray();

            return new ChartDTO(Scalar(c => c.country), new[] { new DataSetDTO("Active", Scalar(c => c.active),  ChartColors.Blue),
                                                                new DataSetDTO("Recovered", Scalar(c => c.recovered), ChartColors.Green),
                                                                new DataSetDTO("Critical", Scalar(c => c.critical),  ChartColors.Red) });
        }

        public async Task<ChartDTO> CasesByContinent()
        {
            var state = await ContinentAndAmountsState;

            T[] Scalar<T>(Func<ContinentAndAmountsDTO, T> func) => state.Select(func)
                                                                        .ToArray();

            var datasets = new []
            {
                new DataSetDTO("amount", Scalar(c => c.cases), AllColors.Value),
            };

            return new ChartDTO(Scalar(c => c.Name), datasets);
        }

        public async Task<ChartDTO> DeathsByContinent()
        {
            var state = await ContinentAndAmountsState;

            T[] Scalar<T>(Func<ContinentAndAmountsDTO, T> func) => state.Select(func)
                                                                        .ToArray();

            var datasets = new[]
            {
                new DataSetDTO("amount", Scalar(c => c.deaths), AllColors.Value),
            };

            return new ChartDTO(Scalar(c => c.Name), datasets);
        }

        public async Task<ChartDTO> GetTodayLineChart()
        {
            var state = await CountryState;

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.todayDeaths)
                                                                     .Select(func)
                                                                     .Take(25)
                                                                     .ToArray();

            return new ChartDTO(Scalar(c => c.country), 
                   new DataSetDTO("Cases", Scalar(c => c.todayCases), ChartColors.Red),
                   new DataSetDTO("Deaths", Scalar(c => c.todayDeaths), ChartColors.Green));
        }

        public async Task<ChartDTO> GetTodayLineChartSouth()
        {
            var state = await SouthAmericaState;

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.todayDeaths)
                                                                     .Select(func)
                                                                     .Take(25)
                                                                     .ToArray();

            return new ChartDTO(Scalar(c => c.country),
                   new DataSetDTO("Cases", Scalar(c => c.todayCases), ChartColors.Red),
                   new DataSetDTO("Deaths", Scalar(c => c.todayDeaths), ChartColors.Green));
        }

        public async Task<ChartDTO> GetAllTimeLineChart()
        {
            var state = await CountryState;

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.deaths)
                                                                     .Select(func)
                                                                     .Take(25)
                                                                     .ToArray();

            return new ChartDTO(Scalar(c => c.country), 
                   new DataSetDTO("Cases", Scalar(c => c.cases), ChartColors.Red),
                   new DataSetDTO("Deaths", Scalar(c => c.deaths), ChartColors.Green));
        }

        public async Task<ChartDTO> GetAllTimeLineChartSouth()
        {
            var state = await SouthAmericaState;

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.Select(func).ToArray();

            return new ChartDTO(Scalar(c => c.country),
                   new DataSetDTO("Cases", Scalar(c => c.cases), ChartColors.Red),
                   new DataSetDTO("Deaths", Scalar(c => c.deaths), ChartColors.Green));
        }

        #endregion 
    }
}
