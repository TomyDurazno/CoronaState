using System.Collections.Generic;
using System.Linq;

namespace CoronaManager.Models.DTO
{
    public class ContinentAndAmountsDTO
    {
        public string Name { get; set; }

        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }

        public ContinentAndAmountsDTO(string name, List<CoronaCountryState> countries)
        {
            Name = name;

            cases = countries.Sum(c => c.cases);
            deaths = countries.Sum(c => c.deaths);
            todayCases = countries.Sum(c => c.todayCases);
            todayDeaths = countries.Sum(c => c.todayDeaths);
            recovered = countries.Sum(c => c.recovered);
            active = countries.Sum(c => c.active);
            critical = countries.Sum(c => c.critical);
            casesPerOneMillion = countries.Sum(c => c.casesPerOneMillion);
        }
    }
}
