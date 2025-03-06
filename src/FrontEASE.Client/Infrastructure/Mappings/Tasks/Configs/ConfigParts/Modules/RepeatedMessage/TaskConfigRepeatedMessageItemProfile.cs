using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageItemProfile : Profile
    {
        public TaskConfigRepeatedMessageItemProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskConfigRepeatedMessageItemDto, TaskConfigRepeatedMessageItemDto>();
        }
    }
}
