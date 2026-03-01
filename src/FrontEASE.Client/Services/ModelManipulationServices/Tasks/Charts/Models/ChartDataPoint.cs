namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models
{
    public class ChartDataPoint
    {
        public ChartDataPoint()
        {
            this.Label = string.Empty;
        }

        public int Order { get; set; }
        public string Label { get; set; }
        public decimal Value { get; set; }
    }
}
