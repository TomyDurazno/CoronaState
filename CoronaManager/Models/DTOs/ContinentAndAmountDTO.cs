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

            cases = countries.Sum(c => c.Cases);
            deaths = countries.Sum(c => c.Deaths);
            todayCases = countries.Sum(c => c.TodayCases);
            todayDeaths = countries.Sum(c => c.TodayDeaths);
            recovered = countries.Sum(c => c.Recovered);
            active = countries.Sum(c => c.Active);
            critical = countries.Sum(c => c.Critical);
            casesPerOneMillion = countries.Sum(c => c.CasesPerOneMillion);
        }
    }
}
