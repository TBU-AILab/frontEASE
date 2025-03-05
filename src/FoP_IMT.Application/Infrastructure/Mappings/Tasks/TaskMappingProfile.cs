using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Domain.Entities.Tasks.Task, TaskDto>()
                .ForMember(x => x.Messages, cd => cd.MapFrom(map => map.Messages))
                .ForMember(x => x.Solutions, cd => cd.MapFrom(map => map.Solutions))
                .ForMember(x => x.Members, cd => cd.MapFrom(map => map.Members))
                .ForMember(x => x.MemberGroups, cd => cd.MapFrom(map => map.MemberGroups))
                .ForMember(x => x.Config, cd => cd.MapFrom(map => map.Config))
                
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Members.Single(x => x.Id == map.AuthorID.ToString())));


            CreateMap<TaskDto, Domain.Entities.Tasks.Task>()
                .ForMember(x => x.Messages, cd => cd.MapFrom(map => map.Messages))
                .ForMember(x => x.Solutions, cd => cd.MapFrom(map => map.Solutions))
                .ForMember(x => x.Members, cd => cd.MapFrom(map => map.Members))
                .ForMember(x => x.MemberGroups, cd => cd.MapFrom(map => map.MemberGroups))
                .ForMember(x => x.Config, cd => cd.MapFrom(map => map.Config))

                .ForMember(x => x.AuthorID, cd => cd.MapFrom(map => map.Author.Id));


            CreateMap<Domain.Entities.Tasks.Task, Domain.Entities.Tasks.Task>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.ConfigID, opt => opt.Ignore())
                .ForMember(x => x.AuthorID, opt => opt.Ignore())

                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.DateUpdated, opt => opt.Ignore())

                .ForMember(x => x.Messages, cd => cd.MapFrom(map => map.Messages))
                .ForMember(x => x.Solutions, cd => cd.MapFrom(map => map.Solutions))
                .ForMember(x => x.MemberGroups, cd => cd.MapFrom(map => map.MemberGroups))
                .ForMember(x => x.Members, cd => cd.MapFrom(map => map.Members))
                .ForMember(x => x.Config, cd => cd.MapFrom(map => map.Config));


            CreateMap<Domain.Entities.Tasks.Task, TaskConfigCoreDto>()
                .ForMember(x => x.Name, cd => cd.MapFrom(map => map.Config.Name))
                .ForMember(x => x.FeedbackFromSolution, cd => cd.MapFrom(map => map.Config.FeedbackFromSolution))
                .ForMember(x => x.MaxContextSize, cd => cd.MapFrom(map => map.Config.MaxContextSize))
                .ForMember(x => x.SystemMessage, cd => cd.MapFrom(map => map.Config.SystemMessage))
                .ForMember(x => x.InitialMessage, cd => cd.MapFrom(map => map.Config.InitMessage))

                .ForMember(x => x.RepeatedMessage, cd => cd.MapFrom(map => map.Config.RepeatedMessage))
                .ForMember(x => x.Modules, cd => cd.MapFrom(map => map.Config.Modules))
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Members.Single(x => x.Id == map.AuthorID.ToString()).Email));

            CreateMap<Domain.Entities.Tasks.Task, TaskConfigFullCoreDto>()
                .ForMember(x => x.Name, cd => cd.MapFrom(map => map.Config.Name))
                .ForMember(x => x.FeedbackFromSolution, cd => cd.MapFrom(map => map.Config.FeedbackFromSolution))
                .ForMember(x => x.MaxContextSize, cd => cd.MapFrom(map => map.Config.MaxContextSize))
                .ForMember(x => x.SystemMessage, cd => cd.MapFrom(map => map.Config.SystemMessage))
                .ForMember(x => x.InitialMessage, cd => cd.MapFrom(map => map.Config.InitMessage))

                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Members.Single(x => x.Id == map.AuthorID.ToString()).Email))
                .ForMember(x => x.RepeatedMessage, cd => cd.MapFrom(map => map.Config.RepeatedMessage))
                .ForMember(x => x.Modules, cd => cd.MapFrom(map => map.Config.Modules));
        }
    }
}
