using System.Collections;
using System.Reflection;
using System.Text;

namespace FrontEASE.Shared.Infrastructure.Utils.Extensions
{
    public static class UrlExtensions
    {
        public static string ToQueryString<T>(this T? obj) where T : class
        {
            if (obj is null) return string.Empty;

            var queryStringBuilder = new StringBuilder();
            BuildQueryStringRecursive(obj, "", queryStringBuilder);

            if (queryStringBuilder.Length > 0)
            {
                queryStringBuilder[0] = '?';
            }
            return queryStringBuilder.ToString();
        }

        public static string ToQueryString<T>(this IEnumerable<T> collection, string key)
        {
            if (collection is null || !collection.Any() || string.IsNullOrEmpty(key))
                return string.Empty;

            var query = string.Join("&", collection.Select(item => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(item?.ToString() ?? string.Empty)}"));
            return "?" + query;
        }

        private static void BuildQueryStringRecursive(object? obj, string prefix, StringBuilder queryStringBuilder)
        {
            if (obj is null) return;

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.CanRead);

            foreach (var p in properties)
            {
                var value = p.GetValue(obj, null);
                if (value is null) continue;

                var currentKey = string.IsNullOrEmpty(prefix) ? p.Name : $"{prefix}.{p.Name}";

                if (value is IEnumerable enumerable && p.PropertyType != typeof(string))
                {
                    var itemAdded = false;
                    foreach (var item in enumerable)
                    {
                        if (item is not null)
                        {
                            queryStringBuilder.Append($"&{Uri.EscapeDataString(currentKey)}={Uri.EscapeDataString(item.ToString() ?? string.Empty)}");
                            itemAdded = true;
                        }
                    }
                }
                else if (p.PropertyType == typeof(string) || Nullable.GetUnderlyingType(p.PropertyType) == typeof(string))
                {
                    var stringValue = (string?)value;
                    if (!string.IsNullOrEmpty(stringValue))
                        queryStringBuilder.Append($"&{Uri.EscapeDataString(currentKey)}={Uri.EscapeDataString(stringValue)}");
                }
                else if (p.PropertyType == typeof(DateTime) || Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime))
                {
                    if (value is DateTime dtValue && dtValue != default)
                        queryStringBuilder.Append($"&{Uri.EscapeDataString(currentKey)}={Uri.EscapeDataString(dtValue.ToString("MM/dd/yyyy"))}");
                }
                else if (p.PropertyType.IsValueType || Nullable.GetUnderlyingType(p.PropertyType)?.IsValueType == true)
                {
                    var defaultValue = p.PropertyType.IsValueType && Nullable.GetUnderlyingType(p.PropertyType) is null
                                      ? Activator.CreateInstance(p.PropertyType) : null;

                    if (!Equals(value, defaultValue))
                    {
                        queryStringBuilder.Append($"&{Uri.EscapeDataString(currentKey)}={Uri.EscapeDataString(value.ToString() ?? string.Empty)}");
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    BuildQueryStringRecursive(value, currentKey, queryStringBuilder);
                }
            }
        }
    }
}