using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List.Converters
{
    public class TaskModuleParameterListOptionCoreDtoReverseConverter : ITypeConverter<TaskModuleParameterListOption?, TaskModuleParameterListOptionCoreDto?>
    {
        public TaskModuleParameterListOptionCoreDto? Convert(TaskModuleParameterListOption? source, TaskModuleParameterListOptionCoreDto? destination, ResolutionContext context)
        {
            if (source is not null)
            {
                destination ??= new TaskModuleParameterListOptionCoreDto();
                destination.ParameterValues = [.. source.ParameterValues
                    .Select(paramList =>
                        (IDictionary<string, TaskModuleParameterCoreDto>)paramList.ParameterItems
                            .ToDictionary(
                                param => param.Key,
                                param => context.Mapper.Map<TaskModuleParameterCoreDto>(param)
                            )
                    )];
            }
            return destination;
        }
    }
}
