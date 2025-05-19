using AutoMapper;
using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.List.Helpers;
using FrontEASE.Client.Pages.Tasks.Overview.Components.Sections.Config.Components.Modules.Params.Helpers;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Helpers
{
    public class TaskMessageMappingProfile : Profile
    {
        public TaskMessageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParamFlags, TaskModuleParamFlags>();
            CreateMap<ListParamFlags, ListParamFlags>();
        }
    }
}
