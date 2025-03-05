using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs
{
    public class TaskConfigMappingProfile : Profile
    {
        public TaskConfigMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskConfigDto, TaskConfigDto>()
                .ForMember(x => x.StoppingConditions, cd => cd.MapFrom(map => map.StoppingConditions))
                .ForMember(x => x.Tests, cd => cd.MapFrom(map => map.Tests))
                .ForMember(x => x.Analyses, cd => cd.MapFrom(map => map.Analyses))
                .ForMember(x => x.Stats, cd => cd.MapFrom(map => map.Stats))
                .ForMember(x => x.Connector, cd => cd.MapFrom(map => map.Connector))
                .ForMember(x => x.Evaluator, cd => cd.MapFrom(map => map.Evaluator))
                .ForMember(x => x.AvailableModules, cd => cd.MapFrom(map => map.AvailableModules))
                .ForMember(x => x.RepeatedMessage, cd => cd.MapFrom(map => map.RepeatedMessage));
        }
    }
}
