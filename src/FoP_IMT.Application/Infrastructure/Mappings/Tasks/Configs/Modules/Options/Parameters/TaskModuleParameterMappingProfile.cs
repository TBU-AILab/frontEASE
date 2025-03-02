using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters
{
    public class TaskModuleParameterMappingProfile : Profile
    {
        public TaskModuleParameterMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameter, TaskModuleParameterDto>()
                .ForMember(x => x.Value, cd => cd.MapFrom(map => map.Value))
                .ReverseMap();

            CreateMap<TaskModuleParameter, TaskModuleParameterNoValidationDto>()
                .ForMember(x => x.EnumDescriptions, cd => cd.MapFrom(map => map.EnumDescriptions))
                .ForMember(x => x.EnumLongNames, cd => cd.MapFrom(map => map.EnumLongNames))
                .ForMember(x => x.EnumOptions, cd => cd.MapFrom(map => map.EnumOptions))
                .ForMember(x => x.Default, cd => cd.MapFrom(map => map.Default))
                .ReverseMap();

            CreateMap<TaskModuleParameterEntity, TaskModuleParameterDto>()
                .ForMember(x => x.Value, cd => cd.MapFrom(map => map.Value))
                .ReverseMap();

            CreateMap<TaskModuleParameterEntity, TaskModuleParameterNoValidationDto>()
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameter, TaskModuleParameter>()
                .ForMember(x => x.EnumDescriptions, cd => cd.MapFrom(map => map.EnumDescriptions))
                .ForMember(x => x.EnumLongNames, cd => cd.MapFrom(map => map.EnumLongNames))
                .ForMember(x => x.EnumOptions, cd => cd.MapFrom(map => map.EnumOptions))
                .ForMember(x => x.Value, cd => cd.MapFrom(map => map.Value))
                .ForMember(x => x.Default, cd => cd.MapFrom(map => map.Default));

            CreateMap<TaskModuleParameterEntity, TaskModuleParameterEntity>()
                .ForMember(x => x.ID, cd => cd.Ignore())
                .ForMember(x => x.ValueID, cd => cd.Ignore())
                .ForMember(x => x.Module, cd => cd.Ignore())
                .ForMember(x => x.ModuleID, cd => cd.Ignore())

                .ForMember(x => x.Value, cd => cd.MapFrom(map => map.Value));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameter, TaskModuleParameterCoreDto>()
                .ForMember(x => x.EnumDescriptions, cd => cd.MapFrom(map => map.EnumDescriptions))
                .ForMember(x => x.EnumLongNames, cd => cd.MapFrom(map => map.EnumLongNames))
                .ForMember(x => x.EnumOptions, cd => cd.MapFrom(map => map.EnumOptions))
                .ForMember(x => x.Default, cd => cd.MapFrom(map => map.Default))
                .ReverseMap();
        }
    }
}
