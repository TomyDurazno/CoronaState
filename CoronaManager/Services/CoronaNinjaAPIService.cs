using CoronaManager.Models;
using CoronaManager.Models.DTO;
using CoronaManager.Properties;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaManager.Services
{
    public class CoronaNinjaAPIService
    {
        string [] SudAmerica = new [] { "Argentina", "Brazil", "Bolivia", "Chile", "Uruguay", "Peru", "Paraguay", "Colombia" };

        #region State Getters

        async Task<T> GetJArray<T>(string url)
        {
            var client = new RestClient(new Uri(url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<T>();
        }

        string Url = "https://corona.lmao.ninja/countries";
        async Task<List<CoronaCountryState>> GetCountryState() => await GetJArray<List<CoronaCountryState>>(Url);

        string Hopkins = "https://corona.lmao.ninja/jhucsse";
        async Task<List<CoronaHopkinsCSSEState>> GetHopkinsCSSEState() => await GetJArray<List<CoronaHopkinsCSSEState>>(Hopkins);

        #endregion

        #region State Singletons

        public AsyncLazy<List<CoronaCountryState>> CountryState;
        public AsyncLazy<List<CoronaHopkinsCSSEState>> HopkinsCSSEState;

        public AsyncLazy<List<Continent>> ContinentsState;
        public AsyncLazy<List<ContinentAndAmountsDTO>> ContinentAndAmountsState;

        #endregion

        #region Compounds

        async Task<List<Continent>> GetContinents()
        {
            var files = new[]
            {
                new { name = "Africa", Countries = Resources.Africa.Split(Environment.NewLine).ToList() },
                new { name = "Asia", Countries = Resources.Asia.Split(Environment.NewLine).ToList()  },
                new { name = "Oceania", Countries = Resources.Oceania.Split(Environment.NewLine).ToList()  },
                new { name = "Europe", Countries = Resources.Europe.Split(Environment.NewLine).ToList()  },
                new { name = "North America", Countries =  Resources.North_America.Split(Environment.NewLine).ToList()  },
                new { name = "South America", Countries = Resources.South_America.Split(Environment.NewLine).ToList()  }
            };

            var result = files.Select(r => new Continent() { Name = r.name, Countries = r.Countries }).ToList();

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

        #endregion

        #region Constructor

        public CoronaNinjaAPIService()
        {
            CountryState = new AsyncLazy<List<CoronaCountryState>>(GetCountryState);
            HopkinsCSSEState = new AsyncLazy<List<CoronaHopkinsCSSEState>>(GetHopkinsCSSEState);
            ContinentsState = new AsyncLazy<List<Continent>>(GetContinents);
            ContinentAndAmountsState = new AsyncLazy<List<ContinentAndAmountsDTO>>(GetAmountByContinent);
        }

        #endregion

        #region Public API Methods

        public async Task<ChartDTO> Top5CountriesByDeaths()
        {
            var value = await CountryState;

            var countries = value.OrderByDescending(c => c.deaths)
                                 .Take(5);

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func).ToArray();

            var colors = new [] { ChartColors.Blue, ChartColors.Red, ChartColors.Green, ChartColors.Purple, ChartColors.Yellow };

            var datasets = new []
            { 
                new DataSetDTO("amount", Scalar(c => c.deaths), colors),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        public async Task<ChartDTO> Top5CountriesByDeathsToday()
        {
            var value = await CountryState;

            var countries = value.OrderByDescending(c => c.todayDeaths)
                                 .Take(5);

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func).ToArray();

            var colors = new [] { ChartColors.Blue, ChartColors.Red, ChartColors.Green, ChartColors.Purple, ChartColors.Yellow };

            var datasets = new[]
            {
                new DataSetDTO("amount", Scalar(c => c.todayDeaths), colors),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        public async Task<ChartDTO> CasesByStatus(bool south = false)
        {
            var value = await CountryState;

            IEnumerable<CoronaCountryState> countries;

            countries = south ? countries = value.OrderByDescending(d => d.deaths).Where(c => SudAmerica.Contains(c.country))
                              : countries = value.OrderByDescending(d => d.deaths).Take(8);

            T[] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func).ToArray();

            return new ChartDTO(Scalar(c => c.country), new[] { new DataSetDTO("Active", Scalar(c => c.active),  ChartColors.Blue),
                                                                new DataSetDTO("Recovered", Scalar(c => c.recovered), ChartColors.Green),
                                                                new DataSetDTO("Critical", Scalar(c => c.critical),  ChartColors.Red) });
        }

        public async Task<ChartDTO> AmountByContinent()
        {
            var state = await ContinentAndAmountsState;

            T[] Scalar<T>(Func<ContinentAndAmountsDTO, T> func) => state.Select(func).ToArray();

            var colors = typeof(ChartColors).GetFields().Select(p => p.GetValue(null).ToString()).ToArray();

            var datasets = new []
            {
                new DataSetDTO("amount", Scalar(c => c.cases), colors),
            };

            return new ChartDTO(Scalar(c => c.Name), datasets);
        }

        public async Task<ChartDTO> GetTodayLineChart()
        {
            var state = await CountryState;

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => state.OrderByDescending(s => s.todayDeaths).Select(func).Take(25).ToArray();

            var rnd = new Random();

            var datasets = new [] 
            { 
                new DataSetDTO("Cases", Scalar(c => c.todayCases), ChartColors.Red),
                new DataSetDTO("Deaths", Scalar(c => c.todayDeaths), ChartColors.Green),
            };

            return new ChartDTO(Scalar(c => c.country), datasets);
        }

        #endregion

        
    }
}
