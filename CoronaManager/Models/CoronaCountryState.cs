using System;
using System.Collections.Generic;

namespace CoronaManager
{
    public class CoronaCountryState
    {
        public string country { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }

        public Dictionary<string, int> RadarData => new Dictionary<string, int>() 
        {
            { "recovered" , recovered },
            { "active" , active },
            { "critical" , critical }
        };


    }
}
