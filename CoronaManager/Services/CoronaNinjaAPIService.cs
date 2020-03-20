using CoronaManager.Models;
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
        string Url => "https://corona.lmao.ninja/countries";

        public AsyncLazy<List<CoronaState>> State;

        public CoronaNinjaAPIService()
        {
            State = new AsyncLazy<List<CoronaState>>(() => CoronaState());
        }

        private async Task<List<CoronaState>> CoronaState()
        {
            var client = new RestClient(new Uri(Url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaState>>();
        }

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertes()
        {
            var value = await State;

            return value.OrderByDescending(c => c.deaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.deaths }).ToList();
        }

        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertesHoy()
        {
            var value = await State;

            return value.OrderByDescending(c => c.todayDeaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.todayDeaths }).ToList();
        }

        public async Task<List<RadarDTO>> RadarChart()
        {
            var value = await State;

            var cuatroPaises = new[] { "Argentina", "Chile", "Uruguay", "Peru", "Paraguay" };

            Label[] GetLabels(CoronaState state)
            {
                return state.RadarData.Select(d => new Label() { Name = d.Key, Value = d.Value }).ToArray();
            }

            return value.Where(c => cuatroPaises.Contains(c.country)).Select(n => new RadarDTO() { Name = n.country, Labels = GetLabels(n) }).ToList();
        }

        public async Task<BarChartDTO> CasesByStatus(bool south = false)
        {
            var value = await State;

            List<CoronaState> countries;

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
    }
}
