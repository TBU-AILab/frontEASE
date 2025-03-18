using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.General;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;
using FrontEASE.Shared.Data.DTOs.Shared.Addresses;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Shared.Users.Auth;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Shared;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Data.Enums.Shared.Addresses;
using FrontEASE.Shared.Data.Enums.Shared.General;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.Enums.Tasks.Messages;
using FrontEASE.Shared.Data.Enums.Tasks.Visualisation;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Data.Enums.Users.Visualisation;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;
using System.Net;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Resources.Defaults
{
    public class DefaultResourcesConfiguration
    {
        public static IList<Resource> GetDefaults()
        {
            var resources = new List<Resource>();

            resources.AddRange(InitFieldResources());
            resources.AddRange(InitFieldPlaceholderResources());
            resources.AddRange(InitEnumResources());
            resources.AddRange(InitValueResources());
            resources.AddRange(InitTextationResources());
            resources.AddRange(InitCollectionResources());
            resources.AddRange(InitValidationResources());
            resources.AddRange(InitExceptionResources());
            resources.AddRange(InitGeneralResources());
            return resources;
        }

        private static IList<Resource> InitValueResources()
        {
            return
            [
                /* Values */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Yes}", Value="Yes" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.No}", Value="No" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Passed}", Value="Passed" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Failed}", Value="Failed" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}", Value="N/A" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Empty}", Value="---" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Readonly}", Value="Read-Only" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NoValue}", Value="No value" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Actions}", Value="Actions" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.Info}", Value="Info" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIConstants.Key}", Value="Key" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIConstants.Value}", Value="Value" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Default}.{UIConstants.Value}", Value="Default value" },

                /* Routes */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.BaseRoute}", Value="/" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.TasksRoute}", Value="Tasks" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.UsersRoute}", Value="Users" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.ManagementRoute}", Value="Management" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.HangfireRoute}", Value="Hangfire" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.SwaggerRoute}", Value="Swagger" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.AccountRoute}.{UIRouteConstants.LoginRoute}", Value="Account/Login" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.AccountRoute}.{UIRouteConstants.LogoutRoute}", Value="Account/Logout" },

                /* Pages */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Page}.{UIPageConstants.Tasks}", Value="Tasks" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Page}.{UIPageConstants.UsersAndCompanies}", Value="Users and Companies" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Page}.{UIPageConstants.Management}", Value="Personal settings" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Page}.{UIPageConstants.Login}", Value="Login" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Page}.{UIPageConstants.Error}", Value="Error" },

            ];
        }

        private static IList<Resource> InitValidationResources()
        {
            return
            [
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.CollectionNotEmpty}", Value="Collection must contain at least one item." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Required}", Value="Field \"{0}\" is required." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.TextLengthBetween}", Value="Text length must be in range {0} - {1}." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.InvalidValue}", Value="Invalid value: \"{0}\" is not a permitted enumeration value." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Email}", Value="Invalid value: \"{0}\" is not a valid E-mail." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Base64String}", Value="Value is not a valid base64 string." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.NumericRange}", Value="Value must be in range {0} - {1}." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.TextLengthExact}", Value="Text must be {0} characters long." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.DateInRange}", Value="Date must be in range {0} - {1}." },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterOneOfRequired}", Value="Parameter \"{0}\" must be filled." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterNumericRange}", Value="Value must be in range {0} - {1}." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterOneOfEnumValues}", Value="Selected value must be one of the following values: {0}." },
            ];
        }

        private static IList<Resource> InitTextationResources()
        {
            return
            [
                /* TaskConfigRepeatedMessageDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{nameof(TaskConfigRepeatedMessageDto.RepeatedMessageItems)}.{UIStateConstants.Explanation}", Value = "When repeated message does not have any explicitly configured item, default base prompt (\"Improve\") will be used" }
            ];
        }

        private static IList<Resource> InitCollectionResources()
        {
            return
            [
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetCollectionResourceValue<ApplicationUserDto>(), Value = "Users" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetCollectionResourceValue<CompanyDto>(), Value = "Companies" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetCollectionResourceValue<TaskMessageDto>(), Value = "Messages" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetCollectionResourceValue<TaskDto>(), Value = "Tasks" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetCollectionResourceValue<UserPreferenceTokenOptionDto>(), Value = "Connection Tokens" },
            ];
        }

        private static IList<Resource> InitExceptionResources()
        {
            return
            [
                /* General */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.Unauthorized}", Value="Unauthorized access" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.NotFound}", Value="Not found" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.BadRequest}", Value="Bad request" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.InternalServerError}", Value="Internal server error" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.Forbidden}", Value="Forbidden" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.UnprocessableContent}", Value="Unprocessable content" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIConstants.Error}.{UIActionConstants.Login}", Value="Error - Invalid login request" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.Unauthorized}.{UIStateConstants.Text}", Value="The current user has insufficient privileges to perform this operation." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{UIStateConstants.Text}", Value="The resource you are trying to access was not found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{UIStateConstants.Text}", Value="Invalid request parameters." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.InternalServerError}.{UIStateConstants.Text}", Value="Unclassified error - server failure." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.Forbidden}.{UIStateConstants.Text}", Value="Access has been forbidden." },

                /* ApplicationUserDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUserDto)}.{UIExceptionConstants.UserExists}", Value="User with this user name / email already exists." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(ApplicationUserDto)}.{UIElementConstants.Collection}", Value="No users have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.Delete}.{nameof(ApplicationUserDto)}.{UIConstants.Question}", Value="Do you really wish to permanently delete this user account?" },

                /* CompanyDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(CompanyDto)}.{UIElementConstants.Collection}", Value="No organizations have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.Delete}.{nameof(CompanyDto)}.{UIConstants.Question}", Value="Do you really wish to permanently delete this organisation?" },

                /* UserPreferenceTokenOptionDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(UserPreferenceTokenOptionDto)}.{UIElementConstants.Collection}", Value="No tokens have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.Delete}.{nameof(UserPreferenceTokenOptionDto)}.{UIConstants.Question}", Value="Do you really wish to permanently delete this token?" },

                /* TaskDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskDto)}.{UIElementConstants.Collection}", Value="No tasks have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.Delete}.{nameof(TaskDto)}.{UIConstants.Question}", Value="Do you really wish to permanently delete this task?" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.Clone}.{nameof(TaskDto)}.{UIConstants.Question}", Value="Do you really wish to duplicate this task?" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.StateChange}.{nameof(TaskDto)}.{TaskState.RUN}.{UIConstants.Question}", Value="Do you really wish to RUN this task?" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.StateChange}.{nameof(TaskDto)}.{TaskState.STOP}.{UIConstants.Question}", Value="Do you really wish to STOP this task?" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Specific}.{UIActionConstants.StateChange}.{nameof(TaskDto)}.{TaskState.PAUSED}.{UIConstants.Question}", Value="Do you really wish to PAUSE this task?" },

                /* TaskConfigDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskConfigDto)}.{nameof(TaskConfigDto.Analyses)}.{UIElementConstants.Collection}", Value="No analyses have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskConfigDto)}.{nameof(TaskConfigDto.Tests)}.{UIElementConstants.Collection}", Value="No tests have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskConfigDto)}.{nameof(TaskConfigDto.Stats)}.{UIElementConstants.Collection}", Value="No statistics have been found." },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskConfigDto)}.{nameof(TaskConfigDto.StoppingConditions)}.{UIElementConstants.Collection}", Value="No stopping conditions have been found." },

                /* TaskMessageDto */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.NotFound}.{nameof(TaskMessageDto)}.{UIElementConstants.Collection}", Value="No messages have been found." },

            ];
        }


        private static IList<Resource> InitGeneralResources()
        {
            return
            [
                /* App */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIStateConstants.AppName}", Value="Effortless Algorithmic Solution Evolution" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIStateConstants.AppNameShort}", Value="EASE" },

                /* Statuses */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Result}.{UIStateConstants.ActionSuccess}", Value="Action successful" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Result}.{UIStateConstants.ActionFail}", Value="Action failed" },

                /* Actions */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Login}", Value="Login" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Logout}", Value="Logout" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Search}", Value="Search" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Close}", Value="Close" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Load}", Value="Load" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Select}", Value="Select" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Save}", Value="Save" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Delete}", Value="Delete" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Update}", Value="Update" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Discard}", Value="Discard" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Overview}", Value="Overview" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.StateChange}", Value="Change state" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Run}", Value="Run" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Stop}", Value="Stop" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Pause}", Value="Pause" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Download}", Value="Download" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.Clone}", Value="Clone" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIActionConstants.BackToHomepage}", Value="Return to Home page" },

                /* Data Manipulations */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{ UIConstants.Base}.{ UIConstants.Generic}.{ UIActionConstants.Use}.{ UIStateConstants.Default}", Value="Use default value" },
 
                /* Errors and Exceptions */
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIConstants.Error}", Value="Error" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIConstants.Error}.{UIStateConstants.Text}", Value="Oops - Error" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Base}.{UIConstants.Generic}.{UIConstants.Error}.{UIStateConstants.Explanation}", Value="Sorry for the inconvenience, the application has encountered the following error:" },
            ];
        }

        private static IList<Resource> InitEnumResources()
        {
            return
            [
                /* UserCompanyManagementType */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserCompanyManagementType.USERS.GetEnumResourceValue(), Value = "Users" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserCompanyManagementType.COMPANIES.GetEnumResourceValue(), Value = "Organizations" },

                /* UserPreferencesManagementType */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserPreferencesManagementType.TOKENS.GetEnumResourceValue(), Value = "Tokens" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserPreferencesManagementType.GENERAL.GetEnumResourceValue(), Value = "General" },

                /* UserRole */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserRole.USER.GetEnumResourceValue(), Value = "User" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserRole.ADMIN.GetEnumResourceValue(), Value = "Administrator" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = UserRole.OWNER.GetEnumResourceValue(), Value = "Owner" },

                /* Country */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = Country.CZECHIA.GetEnumResourceValue(), Value = "Czech Republic" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = Country.SLOVAKIA.GetEnumResourceValue(), Value = "Slovak Republic" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = Country.OTHER.GetEnumResourceValue(), Value = "Other" },

                /* TaskState */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.CREATED.GetEnumResourceValue(), Value = "Created" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.INIT.GetEnumResourceValue(), Value = "Initialized" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.RUN.GetEnumResourceValue(), Value = "Running" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.PAUSED.GetEnumResourceValue(), Value = "Paused" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.STOP.GetEnumResourceValue(), Value = "Stopped" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.FINISH.GetEnumResourceValue(), Value = "Finished" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = TaskState.BREAK.GetEnumResourceValue(), Value = "Error" },

                /* MessageRole */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = MessageRole.USER.GetEnumResourceValue(), Value = "User" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = MessageRole.AI.GetEnumResourceValue(), Value = "AI" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = MessageRole.SYSTEM.GetEnumResourceValue(), Value = "System" },

                /* ColorScheme */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = ColorScheme.LIGHT.GetEnumResourceValue(), Value = "Light" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = ColorScheme.DARK.GetEnumResourceValue(), Value = "Dark" },

                /* RepeatedMessageType */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = RepeatedMessageType.SINGLE.GetEnumResourceValue(), Value = "Single" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = RepeatedMessageType.RANDOM.GetEnumResourceValue(), Value = "Random" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = RepeatedMessageType.RANDOM_WEIGHTED.GetEnumResourceValue(), Value = "Random weighed" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = RepeatedMessageType.CIRCULAR.GetEnumResourceValue(), Value = "Circular" },

                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{nameof(RepeatedMessageType)}.{RepeatedMessageType.SINGLE}.{UIStateConstants.Explanation}", Value="Single message (first if more are defined)" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{nameof(RepeatedMessageType)}.{RepeatedMessageType.RANDOM}.{UIStateConstants.Explanation}", Value="Message randomly selected" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{nameof(RepeatedMessageType)}.{RepeatedMessageType.RANDOM_WEIGHTED}.{UIStateConstants.Explanation}", Value="Message selected with weighted randomness" },
                new Resource(){ CountryCodeID = LanguageCode.EN, ResourceCode = $"{UIConstants.Data}.{nameof(RepeatedMessageType)}.{RepeatedMessageType.CIRCULAR}.{UIStateConstants.Explanation}", Value="Message sent in a circular pattern" },

            ];
        }

        private static IList<Resource> InitFieldResources()
        {
            return
            [
                /* LoginDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<LoginDto>(nameof(LoginDto.Username), PropertyDisplayResourceType.FIELD), Value = "Login" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<LoginDto>(nameof(LoginDto.Password), PropertyDisplayResourceType.FIELD), Value = "Password" },
                
                /* ApplicationUserDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.UserName), PropertyDisplayResourceType.FIELD), Value = "Login" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Role), PropertyDisplayResourceType.FIELD), Value = "Role" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Email), PropertyDisplayResourceType.FIELD), Value = "Email" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Password), PropertyDisplayResourceType.FIELD), Value = "Password" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Image), PropertyDisplayResourceType.FIELD), Value = "Profile picture" },
                
                /* CompanyDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Image), PropertyDisplayResourceType.FIELD), Value = "Logo" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Address), PropertyDisplayResourceType.FIELD), Value = "Address" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Name), PropertyDisplayResourceType.FIELD), Value = "Name" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.NameAbbreviation), PropertyDisplayResourceType.FIELD), Value = "Abbreviation" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Users), PropertyDisplayResourceType.FIELD), Value = "Users" },
                
                /* AddressDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.Country), PropertyDisplayResourceType.FIELD), Value = "Country" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.ZIPCode), PropertyDisplayResourceType.FIELD), Value = "Postal code" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.City), PropertyDisplayResourceType.FIELD), Value = "City" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.DescriptiveNumber), PropertyDisplayResourceType.FIELD), Value = "Descriptive n°" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.OrientationNumber), PropertyDisplayResourceType.FIELD), Value = "Orientation n°" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.Street), PropertyDisplayResourceType.FIELD), Value = "Street" },

                /* TaskInfoDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.Author), PropertyDisplayResourceType.FIELD), Value = "Author" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.DateCreated), PropertyDisplayResourceType.FIELD), Value = "Created" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.DateUpdated), PropertyDisplayResourceType.FIELD), Value = "Updated" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.Name), PropertyDisplayResourceType.FIELD), Value = "Name" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.State), PropertyDisplayResourceType.FIELD), Value = "State" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.ConnectorType), PropertyDisplayResourceType.FIELD), Value = "Connector type" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskInfoDto>(nameof(TaskInfoDto.SolutionType), PropertyDisplayResourceType.FIELD), Value = "Solution type" },

                /* TaskDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.Author), PropertyDisplayResourceType.FIELD), Value = "Author" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.Config), PropertyDisplayResourceType.FIELD), Value = "Configuration" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.Solutions), PropertyDisplayResourceType.FIELD), Value = "Solution history" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.DateCreated), PropertyDisplayResourceType.FIELD), Value = "Created" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.DateUpdated), PropertyDisplayResourceType.FIELD), Value = "Updated" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.State), PropertyDisplayResourceType.FIELD), Value = "State" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.MemberGroups), PropertyDisplayResourceType.FIELD), Value = "Visible to (groups)" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.Members), PropertyDisplayResourceType.FIELD), Value = "Visible to (users)" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.CurrentIteration), PropertyDisplayResourceType.FIELD), Value = "Current iteration" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.IterationsValid), PropertyDisplayResourceType.FIELD), Value = "Valid iterations" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskDto>(nameof(TaskDto.IterationsInvalidConsecutive), PropertyDisplayResourceType.FIELD), Value = "Invalid consecutive iterations" },

                /* TaskConfigDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Connector), PropertyDisplayResourceType.FIELD), Value = "Connector" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Evaluator), PropertyDisplayResourceType.FIELD), Value = "Evaluator" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Solution), PropertyDisplayResourceType.FIELD), Value = "Solution" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Stats), PropertyDisplayResourceType.FIELD), Value = "Stats" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Analyses), PropertyDisplayResourceType.FIELD), Value = "Analyses" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Tests), PropertyDisplayResourceType.FIELD), Value = "Tests" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.StoppingConditions), PropertyDisplayResourceType.FIELD), Value = "Stopping conditions" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.RepeatedMessage), PropertyDisplayResourceType.FIELD), Value = "Repeated message" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.InitMessage), PropertyDisplayResourceType.FIELD), Value = "Initial message" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.SystemMessage), PropertyDisplayResourceType.FIELD), Value = "System message" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.FeedbackFromSolution), PropertyDisplayResourceType.FIELD), Value = "Get feedback from solution" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.MaxContextSize), PropertyDisplayResourceType.FIELD), Value = "Max. context size (messages)" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Name), PropertyDisplayResourceType.FIELD), Value = "Name" },
                
                /* TaskModuleDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleDto>(nameof(TaskModuleDto.Parameters), PropertyDisplayResourceType.FIELD), Value = "Parameters" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleDto>(nameof(TaskModuleDto.ShortName), PropertyDisplayResourceType.FIELD), Value = "Type" },

                /* UserPreferencesDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferencesDto>(nameof(UserPreferencesDto.TokenOptions), PropertyDisplayResourceType.FIELD), Value = "Tokens" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferencesDto>(nameof(UserPreferencesDto.GeneralOptions), PropertyDisplayResourceType.FIELD), Value = "General options" },

                /* UserPreferenceGeneralOptionsDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceGeneralOptionsDto>(nameof(UserPreferenceGeneralOptionsDto.ColorScheme), PropertyDisplayResourceType.FIELD), Value = "Color scheme" },

                /* UserPreferenceTokenOptionDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Token), PropertyDisplayResourceType.FIELD), Value = "Token" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Description), PropertyDisplayResourceType.FIELD), Value = "Description" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Name), PropertyDisplayResourceType.FIELD), Value = "Token name" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.ConnectorTypes), PropertyDisplayResourceType.FIELD), Value = "Connector types" },

                /* UserPreferenceTokenOptionConnectorTypeDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionConnectorTypeDto>(nameof(UserPreferenceTokenOptionConnectorTypeDto.ConnectorType), PropertyDisplayResourceType.FIELD), Value = "Connector type" },

                /* TaskSolutionDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskSolutionDto>(nameof(TaskSolutionDto.Feedback), PropertyDisplayResourceType.FIELD), Value = "Feedback" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskSolutionDto>(nameof(TaskSolutionDto.Fitness), PropertyDisplayResourceType.FIELD), Value = "Fitness" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskSolutionDto>(nameof(TaskSolutionDto.Metadata), PropertyDisplayResourceType.FIELD), Value = "Metadata" },

                /* TaskConfigRepeatedMessageDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigRepeatedMessageDto>(nameof(TaskConfigRepeatedMessageDto.RepeatedMessageType), PropertyDisplayResourceType.FIELD), Value = "Repeated message type" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigRepeatedMessageDto>(nameof(TaskConfigRepeatedMessageDto.RepeatedMessageItems), PropertyDisplayResourceType.FIELD), Value = "Message options" },

                /* TaskConfigRepeatedMessageItemDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigRepeatedMessageItemDto>(nameof(TaskConfigRepeatedMessageItemDto.Content), PropertyDisplayResourceType.FIELD), Value = "Content" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigRepeatedMessageItemDto>(nameof(TaskConfigRepeatedMessageItemDto.Weight), PropertyDisplayResourceType.FIELD), Value = "Weight" },
            
                /* TaskKeyValueItemDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskKeyValueItemDto>(nameof(TaskKeyValueItemDto.Key), PropertyDisplayResourceType.FIELD), Value = "Key" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskKeyValueItemDto>(nameof(TaskKeyValueItemDto.Value), PropertyDisplayResourceType.FIELD), Value = "Value" },

                /* TaskModuleParameterValueDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterValueDto>(nameof(TaskModuleParameterValueDto.StringValue), PropertyDisplayResourceType.FIELD), Value = "String value" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterValueDto>(nameof(TaskModuleParameterValueDto.IntValue), PropertyDisplayResourceType.FIELD), Value = "Integer value" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterValueDto>(nameof(TaskModuleParameterValueDto.BoolValue), PropertyDisplayResourceType.FIELD), Value = "Boolean value" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterValueDto>(nameof(TaskModuleParameterValueDto.FloatValue), PropertyDisplayResourceType.FIELD), Value = "Decimal value" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterValueDto>(nameof(TaskModuleParameterValueDto.EnumValue), PropertyDisplayResourceType.FIELD), Value = "Enum value" },

                /* TaskModuleParameterEnumOptionBaseDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterEnumOptionDto>(nameof(TaskModuleParameterEnumOptionDto.StringValue), PropertyDisplayResourceType.FIELD), Value = "Enum value - string" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskModuleParameterEnumOptionDto>(nameof(TaskModuleParameterEnumOptionDto.ModuleValue), PropertyDisplayResourceType.FIELD), Value = "Enum value - module" },
            
                /* TaskMessageDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskMessageDto>(nameof(TaskMessageDto.Tokens), PropertyDisplayResourceType.FIELD), Value = "Tokens" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskMessageDto>(nameof(TaskMessageDto.Role), PropertyDisplayResourceType.FIELD), Value = "Role" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskMessageDto>(nameof(TaskMessageDto.Model), PropertyDisplayResourceType.FIELD), Value = "Model" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskMessageDto>(nameof(TaskMessageDto.Content), PropertyDisplayResourceType.FIELD), Value = "Content" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskMessageDto>(nameof(TaskMessageDto.DateCreated), PropertyDisplayResourceType.FIELD), Value = "Created" },

                /* ImageDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ImageDto>(nameof(ImageDto.ImageUrl), PropertyDisplayResourceType.FIELD), Value = "Image URL" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ImageDto>(nameof(ImageDto.ImageData), PropertyDisplayResourceType.FIELD), Value = "Base64 data" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ImageDto>(nameof(ImageDto.MimeType), PropertyDisplayResourceType.FIELD), Value = "MIME type" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ImageDto>(nameof(ImageDto.Name), PropertyDisplayResourceType.FIELD), Value = "Name" },

                /* ResourceDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ResourceDto>(nameof(ResourceDto.Value), PropertyDisplayResourceType.FIELD), Value = "Value" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ResourceDto>(nameof(ResourceDto.ResourceCode), PropertyDisplayResourceType.FIELD), Value = "Code" },

            ];
        }

        private static IList<Resource> InitFieldPlaceholderResources()
        {
            return
            [
                /* LoginDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<LoginDto>(nameof(LoginDto.Username), PropertyDisplayResourceType.PLACEHOLDER), Value = "BigJoe" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<LoginDto>(nameof(LoginDto.Password), PropertyDisplayResourceType.PLACEHOLDER), Value = "******" },

                /* ApplicationUserDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.UserName), PropertyDisplayResourceType.PLACEHOLDER), Value = "The new guy" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Email), PropertyDisplayResourceType.PLACEHOLDER), Value = "thenewguy@org.com" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Password), PropertyDisplayResourceType.PLACEHOLDER), Value = "******" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<ApplicationUserDto>(nameof(ApplicationUserDto.Image), PropertyDisplayResourceType.PLACEHOLDER), Value = "Select an image ..." },        
            
                /* CompanyDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Image), PropertyDisplayResourceType.PLACEHOLDER), Value = "Select an image ..." },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.Name), PropertyDisplayResourceType.PLACEHOLDER), Value = "FAI (TBU) - Artificial Intelligence Laboratory" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<CompanyDto>(nameof(CompanyDto.NameAbbreviation), PropertyDisplayResourceType.PLACEHOLDER), Value = "A.I.Lab" },

                /* AddressDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.ZIPCode), PropertyDisplayResourceType.PLACEHOLDER), Value = "760 05" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.City), PropertyDisplayResourceType.PLACEHOLDER), Value = "Zlín 5" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.DescriptiveNumber), PropertyDisplayResourceType.PLACEHOLDER), Value = "4511" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.OrientationNumber), PropertyDisplayResourceType.PLACEHOLDER), Value = "7114/1A" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<AddressDto>(nameof(AddressDto.Street), PropertyDisplayResourceType.PLACEHOLDER), Value = "Nad Stráněmi" },

                /* UserPreferenceTokenOptionDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Token), PropertyDisplayResourceType.PLACEHOLDER), Value = "7899a795-41b5-4214-84cc-0a945edc10bc" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Description), PropertyDisplayResourceType.PLACEHOLDER), Value = "This is a company-issued token for the work-related OpenAI API usage." },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<UserPreferenceTokenOptionDto>(nameof(UserPreferenceTokenOptionDto.Name), PropertyDisplayResourceType.PLACEHOLDER), Value = "Lab - OpenAI token." },
                
                /* TaskKeyValueItemDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskKeyValueItemDto>(nameof(TaskKeyValueItemDto.Key), PropertyDisplayResourceType.PLACEHOLDER), Value = "Key" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskKeyValueItemDto>(nameof(TaskKeyValueItemDto.Value), PropertyDisplayResourceType.PLACEHOLDER), Value = "Value" },

                /* TaskConfigDto */
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.Name), PropertyDisplayResourceType.PLACEHOLDER), Value = "Name" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.SystemMessage), PropertyDisplayResourceType.PLACEHOLDER), Value = "This is system message" },
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.InitMessage), PropertyDisplayResourceType.PLACEHOLDER), Value = "This is initial message" }
                new Resource() { CountryCodeID = LanguageCode.EN, ResourceCode = AttributeExtensions.GetResourceFieldValue<TaskConfigDto>(nameof(TaskConfigDto.TaskConfigRepeatedMessageItemDto), PropertyDisplayResourceType.PLACEHOLDER), Value = "This is repeating message" }

            ];
        }
    }
}
