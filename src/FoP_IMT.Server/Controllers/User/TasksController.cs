using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Application.AppServices.Tasks;
using FoP_IMT.Domain.Infrastructure.Exceptions;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;
using FoP_IMT.Shared.Data.Enums.Tasks;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FoP_IMT.Server.Controllers.User
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
        /// <returns>List of (shortened) task models.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<TaskInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTasks()
        {
            IActionResult result;
            try
            {
                var response = await _taskAppService.LoadAll();
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
        [HttpPatch($"{TasksControllerConstants.BaseUrl}")]
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
        /// <param name="id">Task identifier.</param>
        /// <returns>Task DTO model.</returns>
        [HttpPost($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}/{TasksControllerConstants.Clone}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CloneTask([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                var duplicatedTask = await _taskAppService.Duplicate(id);
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
        [HttpPut(TasksControllerConstants.BaseUrl)]
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
        /// Deletes a task.
        /// </summary>
        /// <param name="id">Deleted task identifier.</param>
        [HttpDelete($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTask([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                await _taskAppService.Delete(id);
                result = GetHttpResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Changes state of existing task.
        /// </summary>
        /// <param name="id">Modified task identifier.</param>
        /// <param name="state">Modified task new state.</param>
        [HttpPatch($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}/{TasksControllerConstants.StateParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeTaskState([Required, FromRoute] Guid id, [Required, FromRoute] TaskState state)
        {
            IActionResult result;
            try
            {
                await _taskAppService.ChangeState(id, state);
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
