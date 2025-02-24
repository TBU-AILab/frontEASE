using FoP_IMT.Shared.Data.Enums.Shared.General;
using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Constants.UI.Generic;
using System.Reflection;

namespace FoP_IMT.Shared.Infrastructure.Utils.Extensions
{
    public static class AttributeExtensions
    {
        private static readonly IDictionary<PropertyDisplayResourceType, string> PropertyTypeDictionary = new Dictionary<PropertyDisplayResourceType, string>()
        {
            { PropertyDisplayResourceType.FIELD,  UIElementConstants.Field},
            { PropertyDisplayResourceType.PLACEHOLDER, UIElementConstants.Placeholder},
            { PropertyDisplayResourceType.ENUM, UIElementConstants.Enum },
            { PropertyDisplayResourceType.COLLECTION, UIElementConstants.Collection },
            { PropertyDisplayResourceType.DEFAULT, UIElementConstants.Default },
        };

        public static string GetResourceFieldValue<T>(string propertyName, PropertyDisplayResourceType type) where T : class
        {
            var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (property is null) return "N/A";

            var displayAttribute = property.GetCustomAttribute<ResourceAttribute>();
            var propertyType = PropertyTypeDictionary[type];
            return string.IsNullOrWhiteSpace(displayAttribute?.Key) ? "N/A" : $"{UIConstants.Data}.{displayAttribute.Key}.{propertyType}";
        }

        public static string GetEnumResourceValue(this Enum enumValue)
        {
            string typeName = enumValue.GetType().Name;
            string enumState = enumValue.ToString();

            var displayText = $"{UIConstants.Data}.{typeName}.{PropertyTypeDictionary[PropertyDisplayResourceType.ENUM]}.{enumState}";
            return displayText;
        }

        public static string GetCollectionResourceValue<T>() where T : class
        {
            var typeName = typeof(T).Name;

            var propertyType = PropertyTypeDictionary[PropertyDisplayResourceType.COLLECTION];
            return string.IsNullOrWhiteSpace(typeName) ? "N/A" : $"{UIConstants.Data}.{typeName}.{propertyType}";
        }
    }
}
