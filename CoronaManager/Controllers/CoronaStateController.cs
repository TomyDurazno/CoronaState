using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoronaManager.Controllers
{
    [ApiController]
    [Route("/countries")]
    public class CoronaStateController : ControllerBase
    {
        string Url => "https://corona.lmao.ninja/countries";

        private readonly ILogger<CoronaStateController> _logger;

        public CoronaStateController(ILogger<CoronaStateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CoronaState>> Get()
        {
            var client = new RestClient(new Uri(Url));

            var request = new RestRequest(Method.GET);

            var result = await client.ExecuteTaskAsync(request);

            return JArray.Parse(result.Content).ToObject<List<CoronaState>>();
        }
    }
}
