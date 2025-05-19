using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Application.AppServices.Tasks;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.User
{
    /// <summary>
    /// Controller for task management.
    /// </summary>
    [Authorize]
    public class TasksController : ApiControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(
            ITaskAppService taskAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _taskAppService = taskAppService;
        }

        /// <summary>
        /// Gets list of task overview models.
        /// </summary>
        /// <param name="filter"> The applied filter.</param>
        /// <returns>List of (shortened) task models.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<TaskInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTasks([FromQuery] TaskFilterActionRequestDto? filter)
        {
            IActionResult result;
            try
            {
                var response = await _taskAppService.LoadAll(filter);
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Gets list of task statuses.
        /// </summary>
        /// <returns>List of (minimalistic) task status models.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}/{TasksControllerConstants.StateParam}")]
        [ProducesResponseType(typeof(IList<TaskStatusDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTaskStatuses()
        {
            IActionResult result;
            try
            {
                var response = await _taskAppService.LoadAllStatuses();
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Loads detailed info about individual task.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <returns>Task DTO model.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadTask([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                var response = await _taskAppService.Load(id);
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Refreshes task options for selected task.
        /// </summary>
        /// <param name="task">Task model.</param>
        /// <returns>Task DTO model.</returns>
        [HttpPatch($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RefreshTaskOptions([Required, FromBody] TaskDto task)
        {
            IActionResult result;
            try
            {
                var response = await _taskAppService.Refresh(task);
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Creates new task item.
        /// </summary>
        /// <returns>Task DTO model.</returns>
        [HttpPost(TasksControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTask()
        {
            IActionResult result;
            try
            {
                var createdTask = await _taskAppService.Create();
                result = GetHttpResult(HttpStatusCode.Created, createdTask, nameof(CreateTask));
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Clones an existing task item.
        /// </summary>
        /// <param name="request">Task duplication request.</param>
        /// <param name="id">Task identifier.</param>
        /// <returns>Task DTO model.</returns>
        [HttpPost($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}/{TasksControllerConstants.Clone}")]
        [ProducesResponseType(typeof(IList<TaskDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CloneTask([Required, FromRoute] Guid id, [Required, FromBody] TaskDuplicateActionRequestDto request)
        {
            IActionResult result;
            try
            {
                var duplicatedTask = await _taskAppService.Duplicate(id, request);
                result = GetHttpResult(HttpStatusCode.Created, duplicatedTask, nameof(CloneTask));
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Updates an existing task item.
        /// </summary>
        /// <param name="task">DTO with new updated task info.</param>
        /// <returns>Task DTO model.</returns>
        [HttpPut($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTask([Required, FromBody] TaskDto task)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var updatedTask = await _taskAppService.Update(task);
                result = GetHttpResult(HttpStatusCode.OK, updatedTask);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }


        /// <summary>
        /// Deletes selected tasks.
        /// </summary>
        /// <param name="taskIDs">Deleted task identifiers.</param>
        [HttpDelete($"{TasksControllerConstants.BaseUrl}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTasks([Required, FromQuery] IList<Guid> taskIDs)
        {
            IActionResult result;
            try
            {
                await _taskAppService.Delete(taskIDs);
                result = GetHttpResult(HttpStatusCode.NoContent, null);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Changes states of existing tasks.
        /// </summary>
        /// <param name="taskIDs">Modified task identifiers.</param>
        /// <param name="state">Modified task new state.</param>
        [HttpPatch($"{TasksControllerConstants.BaseUrl}/{TasksControllerConstants.ChangeState}/{TasksControllerConstants.StateParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeTaskState([Required, FromQuery] IList<Guid> taskIDs, [Required, FromRoute] TaskState state)
        {
            IActionResult result;
            try
            {
                await _taskAppService.ChangeState(taskIDs, state);
                result = GetHttpResult(HttpStatusCode.NoContent, null);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }
    }
}
