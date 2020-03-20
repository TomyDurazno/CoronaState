using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaManager.Models
{
    public class Continent
    {
        public string Name { get; set; }
        public List<string> Countries { get; set; }

        public Continent()
        {
            Countries = new List<string>();
        }
    }
}
