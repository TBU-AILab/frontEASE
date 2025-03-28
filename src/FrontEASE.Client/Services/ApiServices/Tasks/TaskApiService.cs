﻿using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Tasks
{
    public class TaskApiService : ApiServiceBase, ITaskApiService
    {
        private readonly ITaskManipulationService _taskManipulationService;

        public TaskApiService(
            HttpClient client,
            IMapper mapper,
            IErrorHandlingService errorHandlingService,
            ITaskManipulationService taskManipulationService) : base(client, mapper, errorHandlingService)
        {
            _taskManipulationService = taskManipulationService;
        }

        public async Task<TaskDto?> LoadTask(Guid taskID)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}";
            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return (await response.Content.ReadFromJsonAsync<TaskDto>())!;
        }

        public async Task<IList<TaskInfoDto>> LoadTaskInfos()
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}";
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

        public async Task<TaskDto?> CloneTask(Guid taskID)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}/{TasksControllerConstants.Clone}";
            var response = await _client.PostAsJsonAsync(url, taskID);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<TaskDto?> RefreshTaskOptions(TaskDto refreshTaskDto)
        {
            _taskManipulationService.PrepareTaskRequest(refreshTaskDto, false, true);
            var url = $"{TasksControllerConstants.BaseUrl}";
            var response = await _client.PatchAsJsonAsync(url, refreshTaskDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<TaskDto?> UpdateTask(TaskDto updateTaskDto)
        {
            _taskManipulationService.PrepareTaskRequest(updateTaskDto, true, true);
            var url = $"{TasksControllerConstants.BaseUrl}";
            var response = await _client.PutAsJsonAsync(url, updateTaskDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<bool> ChangeTaskState(TaskInfoDto task, TaskState state)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{task.ID}/{(int)state}";
            var response = await _client.PatchAsync(url, null);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteTask(Guid taskID)
        {
            var url = $"{TasksControllerConstants.BaseUrl}/{taskID}";
            var response = await _client.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }
    }
}
