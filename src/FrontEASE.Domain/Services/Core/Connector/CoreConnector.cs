using AutoMapper;
using FrontEASE.DataContracts.Converters.Tasks.Parameters;
using FrontEASE.DataContracts.Models.Core.Errors;
using FrontEASE.DataContracts.Models.Core.Packages;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;

namespace FrontEASE.Domain.Services.Core.Connector
{
    public class CoreConnector : ICoreConnector
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _serializerOptions;

        private const string TASK_IDS_PARAM_NAME = "task_ids";

        public CoreConnector(
            AppSettings appSettings,
            HttpClient httpClient,
            IMapper mapper)
        {
            _mapper = mapper;
            _appSettings = appSettings;

            _httpClient = httpClient;
            _httpClient.MaxResponseContentBufferSize = int.MaxValue;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                MaxDepth = 64,
            };

            _serializerOptions.Converters.Add(new TaskModuleParameterCoreDtoConverter());
        }


        public async Task HandleModuleImport(Entities.Shared.Files.File moduleFile)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/system/import");

            using var content = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(moduleFile.Content);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, nameof(File).ToLower(), moduleFile.Name);

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(HandleModuleImport)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task HandleTaskCreate(Entities.Tasks.Task task)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task");
            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TaskInfoCoreDto>(_serializerOptions);
                _mapper.Map(responseData, task);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableContent)
                {
                    var validationError = await response.Content.ReadFromJsonAsync<CoreValidationError>(_serializerOptions);
                    throw new UnprocessableException(ParseValidationMessages(validationError!));
                }
                else
                {
                    var failResult = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"{nameof(HandleTaskCreate)} - Call FAILED - Exception: {failResult}");
                }
            }
        }

        public async Task HandleTaskDuplicate(IList<Entities.Tasks.Task> tasks, Guid origTaskID, string baseName, int copies)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/{origTaskID}/duplicate");
            var queryParams = new Dictionary<string, string>()
            {
                { "new_name", baseName },
                { "num", copies.ToString() }
            };

            url = BuildUrlWithParams(url, queryParams);
            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<IList<TaskInfoCoreDto>>(_serializerOptions);
                if (responseData is not null)
                {
                    for (var i = 0; i < responseData.Count; ++i)
                    {
                        var matchingTask = tasks.ElementAt(i);
                        _mapper.Map(responseData.ElementAt(i), matchingTask);
                    }
                }
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableContent)
                {
                    var validationError = await response.Content.ReadFromJsonAsync<CoreValidationError>(_serializerOptions);
                    throw new UnprocessableException(ParseValidationMessages(validationError!));
                }
                else
                {
                    var failResult = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"{nameof(HandleTaskCreate)} - Call FAILED - Exception: {failResult}");
                }
            }
        }

        public async Task HandleTaskInit(Entities.Tasks.Task task)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/{task.ID}");
            var mappedInputTask = _mapper.Map<TaskConfigFullCoreDto>(task);

            var response = await _httpClient.PutAsJsonAsync(url, mappedInputTask);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TaskInfoCoreDto>(_serializerOptions);
                _mapper.Map(responseData, task);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableContent)
                {
                    var validationError = await response.Content.ReadFromJsonAsync<CoreValidationError>(_serializerOptions);
                    throw new UnprocessableException(ParseValidationMessages(validationError!));
                }
                else
                {
                    var failResult = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"{nameof(HandleTaskInit)} FAILED - Exception: {failResult}");
                }
            }
        }

        public async Task HandleTaskDelete(IList<Entities.Tasks.Task> tasks)
        {
            var taskIDs = tasks.Select(x => x.ID).ToList();
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/batch/delete");

            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(taskIDs), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(HandleTaskDelete)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task RefreshTaskOptions(Entities.Tasks.Task task)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/{task.ID}/options");
            var mappedInputTask = _mapper.Map<TaskConfigFullCoreDto>(task);

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            { Content = new StringContent(JsonSerializer.Serialize(mappedInputTask), Encoding.UTF8, "application/json") };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<IList<TaskModuleCoreDto>>(_serializerOptions);
                _mapper.Map(responseData, task.Config.AvailableModules);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableContent)
                {
                    var validationError = await response.Content.ReadFromJsonAsync<CoreValidationError>(_serializerOptions);
                    throw new UnprocessableException(ParseValidationMessages(validationError!));
                }
                else
                {
                    var failResult = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"{nameof(RefreshTaskOptions)} - Call FAILED - Exception: {failResult}");
                }
            }
        }

        public async Task ChangeTaskState(IList<Entities.Tasks.Task> tasks, TaskState state)
        {
            var taskIDs = tasks.Select(x => x.ID).ToList();
            var url = $"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/batch";
            switch (state)
            {
                case TaskState.RUN:
                    url = $"{url}/run";
                    break;
                case TaskState.PAUSED:
                    url = $"{url}/pause";
                    break;
                case TaskState.STOP:
                    url = $"{url}/stop";
                    break;
            }
            var urlFull = new Uri(url);
            var response = await _httpClient.PatchAsJsonAsync(url, taskIDs);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<IList<TaskInfoCoreDto>>(_serializerOptions);
                if (responseData is not null)
                {
                    foreach (var responseTask in responseData)
                    {
                        var matchingTask = tasks.FirstOrDefault(x => x.ID == responseTask.ID);
                        _mapper.Map(responseTask, matchingTask);
                    }
                }
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableContent)
                {
                    var validationError = await response.Content.ReadFromJsonAsync<CoreValidationError>(_serializerOptions);
                    throw new UnprocessableException(ParseValidationMessages(validationError!));
                }
                else
                {
                    var failResult = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"{nameof(ChangeTaskState)} - Call FAILED - Exception: {failResult}");
                }
            }
        }

        public async Task<FileStreamResult> DownloadTaskFull(Guid taskID)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/{taskID}/download");
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStreamAsync();
                return new FileStreamResult(responseData, response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream");
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(DownloadTaskFull)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<FileStreamResult> DownloadTaskSolution(Guid taskID, Guid messageID)
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/{taskID}/solution/{messageID}/download");
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStreamAsync();
                return new FileStreamResult(responseData, response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream");
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(DownloadTaskSolution)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<IList<TaskInfoCoreDto>> GetTaskInfos()
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/all/info");
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var infos = await response.Content.ReadFromJsonAsync<IList<TaskInfoCoreDto>>(_serializerOptions);
                return infos ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetTaskInfos)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<IList<TaskFullCoreDto>> GetTasksFullData()
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/all/full");
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var infos = await response.Content.ReadFromJsonAsync<IList<TaskFullCoreDto>>(_serializerOptions);
                return infos ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetTasksFullData)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<IList<TaskInfoCoreDto>> GetTaskStates(IList<Guid>? taskIds)
        {
            var baseUrl = $"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/status";
            var url = baseUrl;

            if (taskIds != null && taskIds.Count > 0)
            {
                var query = string.Join("&", taskIds.Select(id => $"{TASK_IDS_PARAM_NAME}={Uri.EscapeDataString(id.ToString())}"));
                url = $"{baseUrl}?{query}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var states = await response.Content.ReadFromJsonAsync<IList<TaskInfoCoreDto>>(_serializerOptions);
                return states ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetTaskStates)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<IList<TaskDynamicInfoCoreDto>> GetTaskRunData(IList<Guid>? taskIDs, DateTime? dateFrom)
        {
            var baseUrl = $"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/data";
            var queryParams = new List<string>();

            if (dateFrom.HasValue)
            {
                queryParams.Add($"{nameof(dateFrom)}={Uri.EscapeDataString(dateFrom.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))}");
            }

            if (taskIDs?.Count > 0)
            {
                queryParams.AddRange(taskIDs.Select(id => $"{TASK_IDS_PARAM_NAME}={Uri.EscapeDataString(id.ToString())}"));
            }

            var url = queryParams.Count > 0 ? $"{baseUrl}?{string.Join("&", queryParams)}" : baseUrl;
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var runData = await response.Content.ReadFromJsonAsync<IList<TaskDynamicInfoCoreDto>>(_serializerOptions);
                return runData ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetTaskRunData)} - Call to {url} failed - Exception: {failResult}");
            }
        }

        #region Typelists

        public async Task<IList<TaskModuleCoreDto>> GetModuleTypes()
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/task/options");
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var modules = await response.Content.ReadFromJsonAsync<IList<TaskModuleCoreDto>>(_serializerOptions);
                return modules ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetModuleTypes)} - Call FAILED - Exception: {failResult}");
            }
        }

        #endregion

        #region Management

        public async Task<IList<CorePackageCoreDto>> GetPackages()
        {
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/system/pm/all");
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var packages = await response.Content.ReadFromJsonAsync<IList<CorePackageCoreDto>>(_serializerOptions);
                return packages ?? [];
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(GetPackages)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<bool> DeletePackages(IList<GlobalPreferenceCorePackage> packages)
        {
            var packagesCore = _mapper.Map<IList<CorePackageCoreDto>>(packages);
            var jsonString = JsonSerializer.Serialize(packagesCore);
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/system/pm/delete");

            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(DeletePackages)} - Call FAILED - Exception: {failResult}");
            }
        }

        public async Task<bool> AddPackages(IList<GlobalPreferenceCorePackage> packages)
        {
            var packagesCore = _mapper.Map<IList<CorePackageCoreDto>>(packages);
            var url = new Uri($"{_appSettings.IntegrationSettings!.PythonCore!.Server!.BaseUrl}/system/pm/add");

            var response = await _httpClient.PostAsJsonAsync(url, packagesCore);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var failResult = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{nameof(AddPackages)} - Call FAILED - Exception: {failResult}");
            }
        }


        #endregion

        #region Helpers

        private static Uri BuildUrlWithParams(Uri baseUrl, IDictionary<string, string> queryParams)
        {
            ArgumentNullException.ThrowIfNull(baseUrl);

            if (queryParams is null || queryParams.Count == 0)
            { return baseUrl; }

            var uriBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var param in queryParams)
            {
                query[param.Key] = param.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }

        private static IList<string> ParseValidationMessages(CoreValidationError validationError)
        {
            var messages = new List<string>();

            if (validationError?.Detail is not null)
            {
                foreach (var detail in validationError.Detail)
                {
                    string loc = detail.Loc is not null && detail.Loc.Count > 0 ? string.Join(" > ", detail.Loc) : string.Empty;
                    messages.Add(!string.IsNullOrEmpty(loc) ? detail.Msg : $"{loc}: {detail.Msg}");
                }
            }

            return messages;
        }

        #endregion
    }
}