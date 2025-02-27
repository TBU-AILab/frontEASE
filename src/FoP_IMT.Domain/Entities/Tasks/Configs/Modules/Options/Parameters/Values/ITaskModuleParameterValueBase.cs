namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values
{
    public interface ITaskModuleParameterValueBase
    {
        public float? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}
