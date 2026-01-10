namespace FrontEASE.DataGenerator.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OrderAttribute(int order) : Attribute
    {
        public int Order { get; } = order;
    }
}
