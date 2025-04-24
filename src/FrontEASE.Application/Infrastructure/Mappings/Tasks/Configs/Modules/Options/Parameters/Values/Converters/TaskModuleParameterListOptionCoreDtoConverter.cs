using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.Converters
{
    public class TaskModuleParameterListOptionCoreDtoConverter : ITypeConverter<TaskModuleParameterListOptionCoreDto, TaskModuleParameterListOption>
    {
        public TaskModuleParameterListOption Convert(TaskModuleParameterListOptionCoreDto source, TaskModuleParameterListOption destination, ResolutionContext context)
        {
            destination ??= new TaskModuleParameterListOption();
            destination.ParameterValues = source?.ParameterValues?
                .Select(dict => (IList<TaskModuleParameter>)[.. dict
                    .Select(kvp =>
                    {
                        var param = context.Mapper.Map<TaskModuleParameter>(kvp.Value);
                        param.Key = kvp.Key;
                        return param;
                    })]).ToList() ?? [];
            return destination;
        }
    }
}
