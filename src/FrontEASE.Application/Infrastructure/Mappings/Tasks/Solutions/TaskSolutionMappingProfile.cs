using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Solutions;
using FrontEASE.Domain.Entities.Tasks.Shared;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Solutions
{
    public class TaskSolutionMappingProfile : Profile
    {
        public TaskSolutionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskSolution, TaskSolutionDto>()
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata))
                .ReverseMap();

            CreateMap<TaskSolution, TaskSolution>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.TaskID, opt => opt.Ignore())
                .ForMember(x => x.TaskMessageID, opt => opt.Ignore())

                .ForMember(x => x.Task, opt => opt.Ignore())
                .ForMember(x => x.TaskMessage, opt => opt.Ignore())

                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata));


            CreateMap<TaskSolution, TaskSolutionCoreDto>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(map => map.Metadata.ToDictionary(kv => kv.Key, kv => kv.Value)))
                .ReverseMap()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(map => map.Metadata != null
                    ? map.Metadata.Select(kv => new TaskKeyValueItem { Key = kv.Key, Value = kv.Value }).ToList()
                    : new List<TaskKeyValueItem>()));
        }
    }
}
