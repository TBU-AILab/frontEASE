using FoP_IMT.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FoP_IMT.Shared.Infrastructure.Utils.Helpers.Tasks
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
