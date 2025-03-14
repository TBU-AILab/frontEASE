using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Shared.Infrastructure.Utils.Helpers.Tasks
{
    public static class DynamicParamUtils
    {
        public static ParameterType? GetParameterType(string parameterType)
        {
            foreach (ParameterType type in Enum.GetValues(typeof(ParameterType)))
            {
                if (string.Equals(type.ToString(), parameterType, StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }
            return null;
        }
    }
}
