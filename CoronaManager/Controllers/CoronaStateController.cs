using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using RestSharp;

namespace CoronaManager.Controllers
{
    [ApiController]
    [Route("state")]
    public class CoronaStateController : ControllerBase
    {
        string Url => "https://corona.lmao.ninja/countries";

        private readonly ILogger<CoronaStateController> _logger;

        public AsyncLazy<List<CoronaState>> State;

        public CoronaStateController(ILogger<CoronaStateController> logger)
        {
            _logger = logger;
            State = new AsyncLazy<List<CoronaState>>(() => CoronaState());
        }

        public async Task<List<CoronaState>> CoronaState()
        {
            var client = new RestClient(new Uri(Url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteTaskAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaState>>();
        }

        [HttpGet]
        [Route("/top5muertes")]
        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertes()
        {
            var value = await State;

            return value.OrderByDescending(c => c.deaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.deaths }).ToList();
        }

        [HttpGet]
        [Route("/top5muertesdehoy")]
        public async Task<List<CountryAndNumberDTO>> Top5PaisesPorMuertesHoy()
        {
            var value = await State;

            return value.OrderByDescending(c => c.todayDeaths).Take(5).Select(n => new CountryAndNumberDTO() { Country = n.country, Amount = n.todayDeaths }).ToList();
        }

        [HttpGet]
        [Route("/radarchart")]
        public async Task<List<RadarDTO>> RadarChart()
        {
            var value = await State;

            var cuatroPaises = new[] { "Argentina", "Chile", "Uruguay", "Peru", "Paraguay" };

            Label[] GetLabels(CoronaState state)
            {
                return state.RadarData.Select(d => new Label() { Name = d.Key, Value = d.Value }).ToArray();
            }

            return value.Where(c => cuatroPaises.Contains(c.country)).Select(n => new RadarDTO() { Name = n.country, Labels = GetLabels (n)}).ToList();
        }

        [HttpGet]
        [Route("/stackedbars")]
        public async Task<BarChartDTO> StackedBars(bool south = false)
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
