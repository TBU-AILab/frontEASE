namespace FoP_IMT.Domain.Entities.Tasks.Shared
{
    public class TaskKeyValueItem
    {
        public TaskKeyValueItem()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
