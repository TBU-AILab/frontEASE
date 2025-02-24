using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValueMappingProfile : Profile
    {
        public TaskModuleParameterValueMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueNoValidationDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueNoValidationDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValue>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue));

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueEntity>()
                .ForMember(x => x.ID, cd => cd.Ignore())
                .ForMember(x => x.EnumValueID, cd => cd.Ignore())
                .ForMember(x => x.Parameter, cd => cd.Ignore())

                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueCoreDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueCoreDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ReverseMap();
        }
    }
}
