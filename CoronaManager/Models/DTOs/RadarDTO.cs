using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaManager.Models.DTO
{
    public class RadarDTO
    {
        public string Name { get; set; }
        public Label [] Labels { get; set; }
    }

    public class Label
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
