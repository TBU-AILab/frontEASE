using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List.Converters
{
    public class TaskModuleParameterListOptionCoreDtoConverter : ITypeConverter<TaskModuleParameterListOptionCoreDto, TaskModuleParameterListOption>
    {
        public TaskModuleParameterListOption Convert(TaskModuleParameterListOptionCoreDto source, TaskModuleParameterListOption destination, ResolutionContext context)
        {
            destination ??= new TaskModuleParameterListOption();
            destination.ParameterValues = source?.ParameterValues?
                .Select(dict =>
                {
                    var paramList = new TaskModuleParameterListOptionParams
                    {
                        ParameterItems = dict
                            .Select(kvp =>
                            {
                                var param = context.Mapper.Map<TaskModuleParameter>(kvp.Value);
                                param.Key = kvp.Key;
                                return param;
                            })
                            .ToList()
                    };
                    return paramList;
                })
                .ToList() ?? [];
            return destination;
        }
    }
}
