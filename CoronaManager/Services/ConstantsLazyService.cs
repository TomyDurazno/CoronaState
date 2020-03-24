using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Models
{
    public class ConstantsLazyService
    {
        public Lazy<string[]> AllColors;
        public Lazy<string[]> FiveColors;
        public Lazy<string[]> TenColors;

        public Lazy<string[]> Africa;
        public Lazy<string[]> Asia;
        public Lazy<string[]> Oceania;
        public Lazy<string[]> Europe;
        public Lazy<string[]> North_America;
        public Lazy<string[]> South_America;

        public AsyncLazy<List<Chart>> GlobalCharts;
        public AsyncLazy<List<Chart>> ExcludedCharts;

        public ConstantsLazyService()
        {
            AllColors = new Lazy<string[]>(ChartColors.GetAllColors, true);
            FiveColors = new Lazy<string[]>(() => AllColors.Value.Take(5).ToArray());
            TenColors = new Lazy<string[]>(() => AllColors.Value.Take(10).ToArray());

            Africa = new Lazy<string[]>(Continent.Africa.AllCountries);
            Asia = new Lazy<string[]>(Continent.Asia.AllCountries);
            Oceania = new Lazy<string[]>(Continent.Oceania.AllCountries);
            Europe = new Lazy<string[]>(Continent.Europe.AllCountries);
            North_America = new Lazy<string[]>(Continent.North_America.AllCountries);
            South_America = new Lazy<string[]>(Continent.South_America.AllCountries);

            GlobalCharts = new AsyncLazy<List<Chart>>(() => LoadRows("Global"));
            ExcludedCharts = new AsyncLazy<List<Chart>>(FilterCharts);
        }

        async Task<List<Chart>> LoadRows(string fileName)
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly.GetManifestResourceStream($"CoronaManager.Tabs.{ fileName }.json");

            string content;

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                content = await reader.ReadToEndAsync();
            }

            return JArray.Parse(content).ToObject<List<Chart>>();
        }

        string[] excluded = new [] { "casesbyContinent", "deathsbyContinent" };
        async Task<List<Chart>> FilterCharts() => (await GlobalCharts).Where(c => !excluded.Contains(c.Name)).ToList();

        public async Task<ViewState> GetViewState()
        {
            var view = new ViewState();

            view.Tabs = new List<Tab>()
            {
                GetTab("Global", await GlobalCharts, Continents.All, true),
                GetTab("Asia", await ExcludedCharts, Continents.Asia),
                GetTab("Africa", await ExcludedCharts, Continents.Africa),
                GetTab("Europe", await ExcludedCharts, Continents.Europe),
                GetTab("North America", await ExcludedCharts, Continents.North_America),
                GetTab("South America", await ExcludedCharts, Continents.South_America),
                GetTab("Oceania", await ExcludedCharts, Continents.Oceania)
            };

            return view;
        }

        Tab GetTab(string name, List<Chart> charts, Continents continent, bool active = false)
        {
            var groups = charts.Select((x, i) => new { Index = i, Value = x })
                               .GroupBy(x => x.Index / 2);

            var tab = new Tab(name, continent);

            tab.IsActive = active;

            foreach (var group in groups)
            {
                var elements = group.ToList();

                if (elements.Count == 1)
                {
                    tab.Rows.Add(new RowChart(elements.ElementAt(0).Value, null));
                }
                else
                {
                    tab.Rows.Add(new RowChart(elements.ElementAt(0).Value, elements.ElementAt(1).Value));
                }
            }

            return tab;
        }
    }
}
