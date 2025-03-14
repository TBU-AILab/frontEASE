using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageItemMappingProfile : Profile
    {
        public TaskConfigRepeatedMessageItemMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskConfigRepeatedMessageItem, TaskConfigRepeatedMessageItemDto>()
                .ReverseMap();

            CreateMap<TaskConfigRepeatedMessageItem, TaskConfigRepeatedMessageItem>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.RepeatedMessageID, opt => opt.Ignore())

                .ForMember(x => x.RepeatedMessage, opt => opt.Ignore());
        }
    }
}
