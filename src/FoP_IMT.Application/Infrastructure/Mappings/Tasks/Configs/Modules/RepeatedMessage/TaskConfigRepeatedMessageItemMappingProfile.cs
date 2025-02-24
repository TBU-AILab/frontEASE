using AutoMapper;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.RepeatedMessage
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
