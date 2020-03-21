namespace CoronaManager.Models.DTO
{
    public class BarChartDTO
    {
        public string [] Labels { get; set; }
        public DataSetDTO [] Datasets { get; set; }

        public BarChartDTO(string[] labels, DataSetDTO[] datasets)
        {
            Labels = labels;
            Datasets = datasets;
        }
    }

    public class DataSetDTO
    {
        public string Label { get; set; }
        public int [] Data { get; set; }

        public DataSetDTO(string label, int [] data)
        {
            Label = label;
            Data = data;
        }
    }
}
