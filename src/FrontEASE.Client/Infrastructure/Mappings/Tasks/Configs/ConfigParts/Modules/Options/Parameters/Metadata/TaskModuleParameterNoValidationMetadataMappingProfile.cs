using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;
using FrontEASE.Shared.Infrastructure.Utils.Helpers.Tasks;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Metadata
{
    public class TaskModuleParameterNoValidationMetadataMappingProfile : Profile
    {
        public TaskModuleParameterNoValidationMetadataMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterDto, TaskModuleParameterNoValidationMetadataDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DynamicParamUtils.GetParameterType(src.Type)));

            CreateMap<TaskModuleParameterNoValidationDto, TaskModuleParameterNoValidationMetadataDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LongName ?? src.ShortName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DynamicParamUtils.GetParameterType(src.Type)))
                .ForMember(dest => dest.EnumOptions, opt => opt.MapFrom(src => src.EnumOptions));

            CreateMap<TaskModuleParameterNoValidationMetadataDto, TaskModuleParameterNoValidationMetadataDto>()
                .ForMember(dest => dest.EnumOptions, opt => opt.MapFrom(src => src.EnumOptions));
        }
    }
}
