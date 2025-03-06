using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage
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
