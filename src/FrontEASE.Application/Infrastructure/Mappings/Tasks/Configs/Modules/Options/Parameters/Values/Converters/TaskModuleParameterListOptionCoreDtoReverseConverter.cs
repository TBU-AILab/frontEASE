using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.Converters
{
    public class TaskModuleParameterListOptionCoreDtoReverseConverter : ITypeConverter<TaskModuleParameterListOption, TaskModuleParameterListOptionCoreDto>
    {
        public TaskModuleParameterListOptionCoreDto Convert(TaskModuleParameterListOption source, TaskModuleParameterListOptionCoreDto destination, ResolutionContext context)
        {
            destination ??= new TaskModuleParameterListOptionCoreDto();
            destination.ParameterValues = [.. source.ParameterValues.Select(list =>
                (IDictionary<string, TaskModuleParameterCoreDto>)list.ToDictionary(param => param.Key, param => context.Mapper.Map<TaskModuleParameterCoreDto>(param)))];
            return destination;
        }
    }
}
