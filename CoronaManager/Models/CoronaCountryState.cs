namespace CoronaManager
{
    public class CoronaCountryState
    {
        public CountryInfo countryInfo { get; set; }
        public string Country { get; set; }
        public int Cases { get; set; }
        public int TodayCases { get; set; }
        public int Deaths { get; set; }
        public int TodayDeaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public int Critical { get; set; }
        public int CasesPerOneMillion { get; set; }
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
