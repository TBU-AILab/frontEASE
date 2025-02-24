namespace FoP_IMT.DataContracts.Models.Core.Shared
{
    public class TaskKeyValueItemCoreDto
    {
        public TaskKeyValueItemCoreDto()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
