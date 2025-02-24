using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.RepeatedMessage;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageMappingProfile : Profile
    {
        public TaskConfigRepeatedMessageMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskConfigRepeatedMessage, TaskConfigRepeatedMessageDto>()
                .ForMember(x => x.RepeatedMessageItems, cd => cd.MapFrom(map => map.RepeatedMessageItems))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskConfigRepeatedMessage, TaskConfigRepeatedMessage>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.TaskConfig, opt => opt.Ignore())

                .ForMember(x => x.RepeatedMessageItems, cd => cd.MapFrom(map => map.RepeatedMessageItems));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskConfigRepeatedMessage, TaskConfigRepeatedMessageCoreDto>()
                .ForMember(dto => dto.Type, opt => opt.MapFrom(entity => entity.RepeatedMessageType))
                .ForMember(dto => dto.Msgs, opt => opt.MapFrom(entity => entity.RepeatedMessageItems.Select(item => item.Content).ToList()))
                .ForMember(dto => dto.Weights, opt => opt.MapFrom(entity => entity.RepeatedMessageItems.Select(item => item.Weight).ToList()))
                .ReverseMap()
                .ForMember(entity => entity.RepeatedMessageType, opt => opt.MapFrom(dto => dto.Type))
                .ForMember(entity => entity.RepeatedMessageItems, opt => opt.MapFrom(dto => dto.Msgs.Zip(dto.Weights, (msg, weight) => new TaskConfigRepeatedMessageItem
                {
                    Content = msg,
                    Weight = weight
                }).ToList()));
        }
    }
}
