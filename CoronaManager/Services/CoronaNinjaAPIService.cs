using CoronaManager.Models;
using CoronaManager.Models.DTO;
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
        string Url => "https://corona.lmao.ninja/countries";

        string Hopkins => "https://corona.lmao.ninja/jhucsse";

        #region State Singletons

        public AsyncLazy<List<CoronaCountryState>> CountryState;
        public AsyncLazy<List<CoronaHopkinsCSSEState>> HopkinsCSSEState;
        public AsyncLazy<List<Continent>> ContinentsState;
        public AsyncLazy<List<ContinentAndAmountsDTO>> ContinentAndAmountsState;

        async Task<List<CoronaCountryState>> GetCountryState()
        {
            var client = new RestClient(new Uri(Url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaCountryState>>();
        }
        async Task<List<CoronaHopkinsCSSEState>> GetHopkinsCSSEState()
        {
            var client = new RestClient(new Uri(Hopkins));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaHopkinsCSSEState>>();
        }

        async Task<List<Continent>> GetContinents(string localpath)
        {
            var files = new [] { "Africa.txt", "Asia.txt", "Oceania.txt", "Europe.txt", "North America.txt", "South America.txt" };

            var paths = files.Select(f => (localpath, file: f));

            var result = await Task.WhenAll(paths.Select(s => Read(s.localpath, s.file)));

            return result.Select(r => new Continent() { Name = r.name, Countries = r.countries.ToList() }).ToList();
        }

        async Task<(string name, IEnumerable<string> countries)> Read(string localpath, string pathToFile)
        {
            var name = pathToFile.Split(".").First();

            string source = string.Empty;

            using (StreamReader SourceReader = File.OpenText(localpath + pathToFile))
            {
                source = await SourceReader.ReadToEndAsync();
            }

            var countries = source.Split(Environment.NewLine);

            return (name, countries);
        }

        async Task<List<ContinentAndAmountsDTO>> GetAmountByContinent()
        {
            var countries = await CountryState;
            var continents = await ContinentsState;

            var grouped = countries.GroupBy(c => continents.FirstOrDefault(ct => ct.Countries.Contains(c.country))?.Name ?? "Other");

            return grouped.Select(g => new ContinentAndAmountsDTO(g.Key, g.ToList())).ToList();
        }

        #endregion

        #region Constructor

        public CoronaNinjaAPIService(string localpath)
        {
            CountryState = new AsyncLazy<List<CoronaCountryState>>(() => GetCountryState());
            HopkinsCSSEState = new AsyncLazy<List<CoronaHopkinsCSSEState>>(() => GetHopkinsCSSEState());
            ContinentsState = new AsyncLazy<List<Continent>>(() => GetContinents(localpath));
            ContinentAndAmountsState = new AsyncLazy<List<ContinentAndAmountsDTO>>(() => GetAmountByContinent());
        }

        #endregion

        #region Public

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertes()
        {
            var value = await CountryState;

            return value.OrderByDescending(c => c.deaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.deaths }).ToList();
        }

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertesHoy()
        {
            var value = await CountryState;

            return value.OrderByDescending(c => c.todayDeaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.todayDeaths }).ToList();
        }

        public async Task<List<RadarDTO>> RadarChart()
        {
            var value = await CountryState;

            var cuatroPaises = new [] { "Argentina", "Chile", "Uruguay", "Peru", "Paraguay" };

            Label [] GetLabels(CoronaCountryState state) => state.RadarData.Select(d => new Label() { Name = d.Key, Value = d.Value }).ToArray();

            return value.Where(c => cuatroPaises.Contains(c.country)).Select(n => new RadarDTO() { Name = n.country, Labels = GetLabels(n) }).ToList();
        }

        public async Task<BarChartDTO> CasesByStatus(bool south = false)
        {
            var value = await CountryState;

            List<CoronaCountryState> countries;

            var sudAmerica = new[] { "Argentina", "Brazil", "Bolivia", "Chile", "Uruguay", "Peru", "Paraguay", "Colombia" };

            if (!south)
            {
                countries = value.OrderByDescending(d => d.deaths).Take(8).ToList();
            }
            else
            {
                countries = value.Where(c => sudAmerica.Contains(c.country)).OrderByDescending(d => d.deaths).ToList();
            }

            var barChartDTO = new BarChartDTO();

            barChartDTO.Labels = countries.Select(c => c.country).ToArray();

            barChartDTO.Datasets = new[]
            {
                new DataSetDTO()
                {
                    Label = "Active",
                    Data = countries.Select(c => c.active).ToArray()
                },
                new DataSetDTO()
                {
                    Label = "Recovered",
                    Data = countries.Select(c => c.recovered).ToArray()
                },
                new DataSetDTO()
                {
                    Label = "Critical",
                    Data = countries.Select(c => c.critical).ToArray()
                }
            }
            .ToList();

            return barChartDTO;
        }

        public async Task<List<CoronaHopkinsCSSEState>> GetHopkinsState() => await HopkinsCSSEState;

        public async Task<List<Continent>> GetContinentsState() => await ContinentsState;

        public async Task<List<ContinentAndAmountsDTO>> AmountByContinent() => await ContinentAndAmountsState;

        #endregion
    }
}
