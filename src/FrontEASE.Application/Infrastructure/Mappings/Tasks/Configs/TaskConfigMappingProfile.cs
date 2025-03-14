using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs
{
    public class TaskConfigMappingProfile : Profile
    {
        public TaskConfigMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskConfig, TaskConfigDto>()
                .ForMember(dto => dto.AvailableModules, opt => opt.MapFrom(src => src.AvailableModules))
                .ForMember(dto => dto.Connector, opt => opt.MapFrom(src => src.Modules.FirstOrDefault(m => m.PackageType == ModuleType.LLM_CONNECTOR) ?? new TaskModuleEntity { PackageType = ModuleType.LLM_CONNECTOR }))
                .ForMember(dto => dto.Evaluator, opt => opt.MapFrom(src => src.Modules.FirstOrDefault(m => m.PackageType == ModuleType.EVALUATOR) ?? new TaskModuleEntity { PackageType = ModuleType.EVALUATOR }))
                .ForMember(dto => dto.Solution, opt => opt.MapFrom(src => src.Modules.FirstOrDefault(m => m.PackageType == ModuleType.SOLUTION) ?? new TaskModuleEntity { PackageType = ModuleType.SOLUTION }))
                .ForMember(dto => dto.Tests, opt => opt.MapFrom(src => src.Modules.Where(m => m.PackageType == ModuleType.TEST).ToList()))
                .ForMember(dto => dto.StoppingConditions, opt => opt.MapFrom(src => src.Modules.Where(m => m.PackageType == ModuleType.STOPPING_CONDITION).ToList()))
                .ForMember(dto => dto.Analyses, opt => opt.MapFrom(src => src.Modules.Where(m => m.PackageType == ModuleType.ANALYSIS).ToList()))
                .ForMember(dto => dto.Stats, opt => opt.MapFrom(src => src.Modules.Where(m => m.PackageType == ModuleType.STATISTIC).ToList()))
                .ReverseMap()
                .ForMember(src => src.Modules, opt => opt.MapFrom(dto =>
                    dto.Tests.Concat(dto.StoppingConditions)
                        .Concat(dto.Analyses)
                        .Concat(dto.Stats)
                        .Concat(new[] { dto.Connector, dto.Evaluator, dto.Solution })))
                .ForMember(src => src.ID, opt => opt.Ignore())
                .ForMember(src => src.RepeatedMessageID, opt => opt.Ignore())
                .ForMember(src => src.AvailableModules, opt => opt.Ignore())
                .ForMember(src => src.Task, opt => opt.Ignore());
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskConfig, TaskConfig>()
                .ForMember(dto => dto.RepeatedMessage, opt => opt.MapFrom(src => src.RepeatedMessage))
                .ForMember(dto => dto.Modules, opt => opt.MapFrom(src => src.Modules))
                .ForMember(dto => dto.AvailableModules, opt => opt.MapFrom(src => src.AvailableModules))

                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.Task, opt => opt.Ignore())
                .ForMember(x => x.RepeatedMessageID, opt => opt.Ignore());
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskConfig, TaskConfigCoreDto>()
                .ForMember(dto => dto.RepeatedMessage, opt => opt.MapFrom(src => src.RepeatedMessage))
                .ForMember(dto => dto.Modules, opt => opt.MapFrom(src => src.Modules))
                .ReverseMap();

            CreateMap<TaskConfig, TaskConfigFullCoreDto>()
               .ForMember(dto => dto.Modules, opt => opt.MapFrom(src => src.Modules))
               .ReverseMap()
               .ForMember(entity => entity.Modules, opt => opt.MapFrom(src => src.Modules));
        }
    }
}
