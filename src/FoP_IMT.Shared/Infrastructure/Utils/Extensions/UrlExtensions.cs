using System.Reflection;
using System.Text;

namespace FoP_IMT.Shared.Infrastructure.Utils.Extensions
{
    public static class UrlExtensions
    {
        private static readonly StringBuilder _query = new();

        public static string ToQueryString<T>(this T obj) where T : class
        {
            _query.Clear();
            BuildQueryString(obj, "");

            if (_query.Length > 0) _query[0] = '?';
            return _query.ToString();
        }

        private static void BuildQueryString<T>(T? obj, string prefix = "") where T : class
        {
            if (obj is null) return;

            foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.GetValue(obj, []) is not null)
                {
                    var value = p.GetValue(obj, []);


                    if (p.PropertyType.IsArray && value?.GetType() == typeof(DateTime[]))
                        foreach (var item in (DateTime[])value)
                            _query.Append($"&{prefix}{p.Name}={item.ToString("MM/dd/yyyy")}");

                    else if (p.PropertyType.IsArray)
                        foreach (var item in (Array)value!)
                            _query.Append($"&{prefix}{p.Name}={item}");

                    else if (p.PropertyType == typeof(string) || Nullable.GetUnderlyingType(p.PropertyType) == typeof(string))
                    {
                        if (!string.IsNullOrEmpty((string?)value))
                            _query.Append($"&{prefix}{p.Name}={value}");
                    }

                    else if (p.PropertyType == typeof(DateTime) || Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime))
                    {
                        if (value is DateTime dtValue && dtValue != default)
                            _query.Append($"&{prefix}{p.Name}={dtValue:MM/dd/yyyy}");
                    }

                    else if (p.PropertyType.IsValueType && !value!.Equals(Activator.CreateInstance(p.PropertyType)))
                        _query.Append($"&{prefix}{p.Name}={value}");

                    else if (p.PropertyType.IsClass)
                        BuildQueryString(value, $"{prefix}{p.Name}.");
                }
            }
        }
    }
}
