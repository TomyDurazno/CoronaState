namespace CoronaManager.Models
{
    public class Stats
    {
        public string confirmed { get; set; }
        public string deaths { get; set; }
        public string recovered { get; set; }
    }

    public class Coordinates
    {
        public string lattitude { get; set; }
        public string longitude { get; set; }
    }

    public class CoronaHopkinsCSSEState
    {
        public string country { get; set; }
        public string province { get; set; }
        public object updatedAt { get; set; }
        public Stats stats { get; set; }
        public Coordinates coordinates { get; set; }
    }
}
