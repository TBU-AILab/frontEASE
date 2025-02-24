namespace FoP_IMT.Shared.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ResourceAttribute : Attribute
    {
        public string Key { get; set; }

        public ResourceAttribute(string Key)
        {
            this.Key = Key;
        }
    }
}
