using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.Enum;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.Enum
{
    public class TaskModuleParameterEnumOptionMappingProfile : Profile
    {
        public TaskModuleParameterEnumOptionMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameterEnumOption, TaskModuleParameterEnumOptionDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumOption, TaskModuleParameterEnumOptionNoValidationDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumValueEntity, TaskModuleParameterEnumOptionDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumValueEntity, TaskModuleParameterEnumOptionNoValidationDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumValueEntity, TaskModuleParameterEnumOption>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterEnumOption, TaskModuleParameterEnumOption>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue));

            CreateMap<TaskModuleParameterEnumValueEntity, TaskModuleParameterEnumValueEntity>()
                .ForMember(x => x.ID, cd => cd.Ignore())
                .ForMember(x => x.ModuleValueID, cd => cd.Ignore())
                .ForMember(x => x.ParameterValue, cd => cd.Ignore())

                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterEnumOption, TaskModuleParameterEnumOptionCoreDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumValueEntity, TaskModuleParameterEnumOptionCoreDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumOption, TaskModuleCoreDto>()
                .ReverseMap()
                .ForMember(
                    entity => entity.ModuleValue,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        context.Mapper.Map<TaskModule>(src))
                );
        }
    }
}
