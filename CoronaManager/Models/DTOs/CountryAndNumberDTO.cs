namespace CoronaManager.Models.DTO
{
    public class CountryAndNumberDTO
    {
        public string Country { get; set; }

        public int Amount { get; set; }

        public CountryAndNumberDTO(string country, int amount)
        {
            Country = country;
            Amount = amount;
        }
    }
}
