namespace FrontEASE.Domain.DataQueries.Management
{
    public class UserPreferencesQuery : DataQueryBase
    {
        public bool LoadTokenOptions { get; set; }
        public bool LoadConnectorTypes { get; set; }
        public bool LoadTagOptions { get; set; }
        public bool LoadGeneralOptions { get; set; }
        public bool LoadTaskGridColumns { get; set; }
    }
}