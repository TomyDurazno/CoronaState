using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaManager.Models.DTO
{

    public class BarChartDTO
    {
        public BarChartDTO()
        {
            Datasets = new List<DataSetDTO>();
        }

        public string [] Labels { get; set; }
        public List<DataSetDTO> Datasets { get; set; }
    }

    public class DataSetDTO
    {
        public string Label { get; set; }
        public int [] Data { get; set; }
    }
}
