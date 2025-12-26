using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Tasks
{
    public class TaskApiService(
        HttpClient client,
        IMapper mapper,
        IErrorHandlingService errorHandlingService,
        ITaskManipulationService taskManipulationService) : ApiServiceBase(client, mapper, errorHandlingService), ITaskApiService
    {

        #region Load

        public async Task<TaskDto?> LoadTask(Guid taskID, bool simple)
        {
            var url = simple ?
                $"{TasksControllerConstants.BaseUrl}/{taskID}/{TasksControllerConstants.SimpleMode}" :
                $"{TasksControllerConstants.BaseUrl}/{taskID}";

            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return (await response.Content.ReadFromJsonAsync<TaskDto>())!;
        }

        public async Task<IList<TaskInfoDto>> LoadTaskInfos(TaskFilterActionRequestDto? filter)
        {
            if (filter is not null)
            {
                taskManipulationService.PrepareTaskFilter(filter!);
            }

            var filterQuery = filter is null ? string.Empty : filter.ToQueryString();
            var url = $"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}{filterQuery}";
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }

            return response.StatusCode == HttpStatusCode.NotFound ? [] : (await response.Content.ReadFromJsonAsync<IList<TaskInfoDto>>())!;
        }

        public async Task<IList<TaskStatusDto>> LoadTaskStatuses()
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}/{TasksControllerConstants.StateParam}";
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return response.StatusCode == HttpStatusCode.NotFound ? [] : (await response.Content.ReadFromJsonAsync<IList<TaskStatusDto>>())!;
        }

        #endregion

        #region Insert

        public async Task<TaskDto?> InsertTask()
        {
            var response = await _client.PostAsJsonAsync<TaskDto?>(TasksControllerConstants.BaseUrl, null);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<IList<TaskDto>> CloneTask(Guid taskID, TaskDuplicateActionRequestDto request)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}/{TasksControllerConstants.Clone}";
            var response = await _client.PostAsJsonAsync(url, request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return await response.Content.ReadFromJsonAsync<IList<TaskDto>>() ?? [];
        }

        #endregion

        #region Manipulate

        public async Task<TaskDto?> RefreshTaskOptions(Guid taskID, TaskDto refreshTaskDto)
        {
            taskManipulationService.PrepareTaskRequest(refreshTaskDto, false, true);
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}";
            var response = await _client.PatchAsJsonAsync(url, refreshTaskDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        #endregion

        #region Update

        public async Task<TaskDto?> UpdateTask(Guid taskID, TaskDto updateTaskDto)
        {
            var (PreservedMembers, PreservedGroups) = taskManipulationService.PrepareTaskRequest(updateTaskDto, true, true);
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}";
            var response = await _client.PutAsJsonAsync(url, updateTaskDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }

            var task = await response.Content.ReadFromJsonAsync<TaskDto>();
            taskManipulationService.AssignTaskImages(task!, PreservedGroups, PreservedMembers);
            return task;
        }

        public async Task<(IList<ApplicationUserDto> Members, IList<CompanyDto> MemberGroups)?> ShareTask(Guid taskID, TaskDto updateTaskDto)
        {
            var (PreservedMembers, PreservedGroups) = taskManipulationService.PrepareTaskRequest(updateTaskDto, true, true);
            var url = $"{TasksControllerConstants.BaseUrl}/{TasksControllerConstants.Share}/{taskID}";
            var response = await _client.PatchAsJsonAsync(url, updateTaskDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            else
            {
                var task = await response.Content.ReadFromJsonAsync<TaskDto>();
                taskManipulationService.AssignTaskImages(task!, PreservedGroups, PreservedMembers);
                return new(task?.Members ?? [], task?.MemberGroups ?? []);
            }
        }

        public async Task<bool> ChangeTaskStates(IList<Guid> taskIDs, TaskState state)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{TasksControllerConstants.ChangeState}/{(int)state}{taskIDs.ToQueryString(nameof(taskIDs))}";
            var response = await _client.PatchAsync(url, null);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteTasks(IList<Guid> taskIDs)
        {
            var url = $"{TasksControllerConstants.BaseUrl}{taskIDs.ToQueryString(nameof(taskIDs))}";
            var response = await _client.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }

        #endregion
    }
}