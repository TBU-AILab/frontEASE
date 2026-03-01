namespace FrontEASE.Domain.DataQueries
{
    public abstract class DataQueryBase
    {
        public bool WithNoTracking { get; set; }
        public bool AsSplitQuery { get; set; }
    }
}
