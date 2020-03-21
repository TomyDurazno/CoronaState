using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoronaManager.Models.DTO
{
    public class ChartDTO
    {
        public string [] Labels { get; set; }
        public DataSetDTO [] Datasets { get; set; }

        public ChartDTO(string [] labels, DataSetDTO[] datasets)
        {
            Labels = labels;
            Datasets = datasets;
        }
    }

    public class DataSetDTO
    {
        public string Label { get; set; }
        public int[] Data { get; set; }
        public dynamic BackgroundColor { get; set; }

        [JsonIgnore]
        public string BorderColor { get; set; }
        [JsonIgnore]
        public bool Hidden { get; set; }

        public DataSetDTO(string label, int [] data)
        {
            Label = label;
            Data = data;
        }

        public DataSetDTO(string label, int[] data, params string [] backgroundcolor) : this (label, data)
        {
            if (backgroundcolor.Count() == 1)
                BackgroundColor = backgroundcolor.First();
            else
                BackgroundColor = backgroundcolor;
        }
    }
}
