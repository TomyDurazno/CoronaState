namespace CoronaManager
{
    public class CoronaCountryState
    {
        public CountryInfo countryInfo { get; set; }
        public string country { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }
    }

    public class CountryInfo
    {
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public object _id { get; set; }
        public double lat { get; set; }
        public double @long { get; set; }
        public string flag { get; set; }
    }
}
