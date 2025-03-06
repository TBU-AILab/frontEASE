using FrontEASE.Shared.Data.Enums.Shared.General;

namespace FrontEASE.Client.Infrastructure.Utils
{
    public static class VisualisationHelper
    {
        public static string GetElementIdByOperation<T>(string elementBase, DataOperation operation, string? ID) where T : class
        {
            var elementIDString = $"{typeof(T).Name}-{operation.ToString().ToLowerInvariant()}-{elementBase}";
            if (!string.IsNullOrWhiteSpace(ID))
            {
                elementIDString = $"{elementIDString}-{ID}";
            }
            return elementIDString;
        }
    }
}
