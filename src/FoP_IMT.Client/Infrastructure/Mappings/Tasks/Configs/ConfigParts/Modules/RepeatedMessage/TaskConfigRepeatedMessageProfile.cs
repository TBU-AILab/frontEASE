using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageProfile : Profile
    {
        public TaskConfigRepeatedMessageProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskConfigRepeatedMessageDto, TaskConfigRepeatedMessageDto>()
                .ForMember(x => x.RepeatedMessageItems, cd => cd.MapFrom(map => map.RepeatedMessageItems));
        }
    }
}
