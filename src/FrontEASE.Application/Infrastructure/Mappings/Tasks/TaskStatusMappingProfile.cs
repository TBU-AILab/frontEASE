using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks
{
    public class TaskStatusMappingProfile : Profile
    {
        public TaskStatusMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Domain.Entities.Tasks.Task, TaskStatusDto>();

            CreateMap<TaskInfoCoreDto, Domain.Entities.Tasks.Task>()
                .ForMember(x => x.Config, opt => opt.Ignore())
                .ForMember(x => x.Messages, opt => opt.Ignore())
                .ForMember(x => x.Solutions, opt => opt.Ignore())
                .ForMember(x => x.Members, opt => opt.Ignore())
                .ForMember(x => x.MemberGroups, opt => opt.Ignore())
                .ForMember(x => x.AuthorID, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (dest.Config is not null)
                    {
                        dest.Config.Name = src.Name ?? string.Empty;
                    }
                })
                .ReverseMap();
        }
    }
}
