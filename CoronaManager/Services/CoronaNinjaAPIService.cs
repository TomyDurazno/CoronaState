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
using System.Threading.Tasks;

namespace CoronaManager.Services
{
    public class CoronaNinjaAPIService
    {
        string[] SudAmerica = new [] { "Argentina", "Brazil", "Bolivia", "Chile", "Uruguay", "Peru", "Paraguay", "Colombia" };

        #region State Singletons

        public AsyncLazy<List<CoronaCountryState>> CountryState;
        public AsyncLazy<List<CoronaHopkinsCSSEState>> HopkinsCSSEState;

        public AsyncLazy<List<Continent>> ContinentsState;
        public AsyncLazy<List<ContinentAndAmountsDTO>> ContinentAndAmountsState;

        string Url => "https://corona.lmao.ninja/countries";
        async Task<List<CoronaCountryState>> GetCountryState()
        {
            var client = new RestClient(new Uri(Url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaCountryState>>();
        }

        string Hopkins => "https://corona.lmao.ninja/jhucsse";
        async Task<List<CoronaHopkinsCSSEState>> GetHopkinsCSSEState()
        {
            var client = new RestClient(new Uri(Hopkins));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaHopkinsCSSEState>>();
        }

        //Compounds
        async Task<List<Continent>> GetContinents()
        {
            var files = new [] 
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

        #region Public

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertes()
        {
            var value = await CountryState;

            return value.OrderByDescending(c => c.deaths)
                        .Take(5)
                        .Select(n => new CountryAndNumberDTO(n.country, n.deaths))
                        .ToList();
        }

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertesHoy()
        {
            var value = await CountryState;

            return value.OrderByDescending(c => c.todayDeaths)
                        .Take(5)
                        .Select(n => new CountryAndNumberDTO(n.country, n.todayDeaths))
                        .ToList();
        }

        public async Task<BarChartDTO> CasesByStatus(bool south = false)
        {
            var value = await CountryState;

            IEnumerable<CoronaCountryState> countries;

            countries = south ? countries = value.OrderByDescending(d => d.deaths).Where(c => SudAmerica.Contains(c.country))
                              : countries = value.OrderByDescending(d => d.deaths).Take(8);

            T [] Scalar<T>(Func<CoronaCountryState, T> func) => countries.Select(func).ToArray();

            return new BarChartDTO(Scalar(c => c.country), new [] { new DataSetDTO("Active", Scalar(c => c.active)),
                                                                    new DataSetDTO("Recovered", Scalar(c => c.recovered)),
                                                                    new DataSetDTO("Critical", Scalar(c => c.critical)) });
        }

        public async Task<List<CoronaHopkinsCSSEState>> GetHopkinsState() => await HopkinsCSSEState;

        public async Task<List<Continent>> GetContinentsState() => await ContinentsState;

        public async Task<List<ContinentAndAmountsDTO>> AmountByContinent() => await ContinentAndAmountsState;

        #endregion
    }
}
