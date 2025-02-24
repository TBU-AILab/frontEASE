using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage
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
