namespace FrontEASE.Domain.DataQueries.Tasks
{
    public class TasksQuery
    {
        public bool LoadSolutions { get; set; }
        public bool LoadMessages { get; set; }
        public bool LoadConfig { get; set; }      
        public bool LoadConfigRepeatedMessage { get; set; }
        public bool LoadConfigModules { get; set; }
        public bool IncludeMembers { get; set; }
        public bool IncludeGroups { get; set; }
        public bool WithNoTracking { get; set; }
    }
}
