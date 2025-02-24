using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Messages;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Messages;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Messages
{
    public class TaskMessageMappingProfile : Profile
    {
        public TaskMessageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskMessage, TaskMessageDto>()
                .ReverseMap();

            CreateMap<TaskMessage, TaskMessage>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.TaskID, opt => opt.Ignore())

                .ForMember(x => x.DateCreated, opt => opt.Ignore())

                .ForMember(x => x.Task, opt => opt.Ignore())
                .ForMember(x => x.TaskSolution, opt => opt.Ignore());

            CreateMap<TaskMessageCoreDto, TaskMessage>()
                .ForMember(x => x.TaskSolution, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
