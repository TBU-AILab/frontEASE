namespace FrontEASE.Shared.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ResourceAttribute(string Key) : Attribute
    {
        public string Key { get; set; } = Key;
    }
}
