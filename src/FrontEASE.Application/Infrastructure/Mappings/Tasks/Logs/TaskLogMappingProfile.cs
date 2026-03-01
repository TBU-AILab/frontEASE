using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Logs;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Logs;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Logs
{
    public class TaskLogMappingProfile : Profile
    {
        public TaskLogMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskLog, TaskLogDto>()
                .ReverseMap();

            CreateMap<TaskLog, TaskLog>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.TaskID, opt => opt.Ignore())

                .ForMember(x => x.Task, opt => opt.Ignore());
        }
    }
}
