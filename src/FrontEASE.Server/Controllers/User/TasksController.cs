using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Application.AppServices.Tasks;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
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
    public class TasksController(
        ITaskAppService taskAppService,
        IResourceHandler resourceHandler,
        IResourceAppService resourceAppService,
        AppSettings appSettings) : ApiControllerBase(resourceHandler, resourceAppService, appSettings)
    {

        /// <summary>
        /// Gets list of task overview models.
        /// </summary>
        /// <param name="filter"> The applied filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of (shortened) task models.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<TaskInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTasks([FromQuery] TaskFilterActionRequestDto? filter, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await taskAppService.LoadAll(filter, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of (minimalistic) task status models.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.All}/{TasksControllerConstants.StateParam}")]
        [ProducesResponseType(typeof(IList<TaskStatusDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTaskStatuses(CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await taskAppService.LoadAllStatuses(cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task DTO model.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadTask([Required, FromRoute] Guid id, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await taskAppService.Load(id, cancellationToken);
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
        /// Loads simple info about individual task.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task DTO model.</returns>
        [HttpGet($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}/{TasksControllerConstants.SimpleMode}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadTaskSimple([Required, FromRoute] Guid id, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await taskAppService.LoadSimple(id, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task DTO model.</returns>
        [HttpPatch($"{TasksControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RefreshTaskOptions([Required, FromBody] TaskDto task, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await taskAppService.Refresh(task, cancellationToken);
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
                var createdTask = await taskAppService.Create(CancellationToken.None);
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
                var duplicatedTask = await taskAppService.Duplicate(id, request, CancellationToken.None);
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
                var updatedTask = await taskAppService.Update(task, CancellationToken.None);
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
                await taskAppService.Delete(taskIDs, CancellationToken.None);
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
                await taskAppService.ChangeState(taskIDs, state, CancellationToken.None);
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
        /// Changes visibility of an existing task
        /// </summary>
        /// <param name="task">Task with modified access rules.</param>
        [HttpPatch($"{TasksControllerConstants.BaseUrl}/{TasksControllerConstants.Share}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ShareTask([Required, FromBody] TaskDto task)
        {
            IActionResult result;
            try
            {
                var updatedTask = await taskAppService.Share(task, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, updatedTask);
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
