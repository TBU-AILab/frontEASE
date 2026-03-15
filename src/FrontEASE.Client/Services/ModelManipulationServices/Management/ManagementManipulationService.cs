using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
using FrontEASE.Shared.Data.DTOs.Management.General;
using FrontEASE.Shared.Data.DTOs.Management.General.Columns;
using FrontEASE.Shared.Data.DTOs.Management.Tags;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;

namespace FrontEASE.Client.Services.ModelManipulationServices.Management
{
    public class ManagementManipulationService(IMapper mapper) : IManagementManipulationService
    {
        public void SetItemPriorities(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions.Select((value, i) => new { i, value }))
            { item.value.Priority = item.i; }
        }

        public void SetItemConnectorTypes(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions)
            {
                if (item.SelectedTokenConnectorTypes?.Count > 0)
                {
                    item.ConnectorTypes.Clear();
                    item.ConnectorTypes.AddRange(item.SelectedTokenConnectorTypes.Select(x => new UserPreferenceTokenOptionConnectorTypeDto { ConnectorType = x }));
                }
            }
        }

        public void ReinitializeTagModel(UserPreferenceTagOptionDto tag)
        {
            var cleanModel = new UserPreferenceTagOptionDto();
            mapper.Map(cleanModel, tag);
        }

        public void ReinitializeTokenModel(UserPreferenceTokenOptionDto token)
        {
            var cleanModel = new UserPreferenceTokenOptionDto();
            mapper.Map(cleanModel, token);
        }

        public void ReinitializePackageModel(GlobalPreferenceCorePackageDto package)
        {
            var cleanModel = new GlobalPreferenceCorePackageDto();
            mapper.Map(cleanModel, package);
        }

        public void SyncTaskGridColumn(UserPreferenceGeneralOptionsDto preferences, TaskGridColumnsVisibility column, bool isChecked)
        {
            EnsureTaskGridColumnsModel(preferences);

            var existingColumn = preferences.TaskGridColumns.FirstOrDefault(x => x.Column == column);

            if (isChecked && existingColumn is null)
            {
                preferences.TaskGridColumns.Add(new UserPreferenceGeneralOptionTaskGridColumnDto()
                {
                    Column = column,
                });
                return;
            }

            if (!isChecked && existingColumn is not null)
            {
                preferences.TaskGridColumns = [.. preferences.TaskGridColumns.Where(x => x.Column != column)];
            }
        }

        public void EnsureTaskGridColumnsModel(UserPreferenceGeneralOptionsDto preferences)
        {
            if (preferences.TaskGridColumns is null)
            {
                preferences.TaskGridColumns = [];
                return;
            }

            preferences.TaskGridColumns = [.. preferences.TaskGridColumns
                .GroupBy(x => x.Column)
                .Select(x => x.First())];
        }
    }
}