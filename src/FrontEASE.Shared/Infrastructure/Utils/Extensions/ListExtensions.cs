namespace FrontEASE.Shared.Infrastructure.Utils.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(items);

            switch (collection)
            {
                case List<T> list:
                    list.AddRange(items);
                    break;
                default:
                    foreach (var item in items)
                    {
                        collection.Add(item);
                    }
                    break;
            }
        }
    }
}
