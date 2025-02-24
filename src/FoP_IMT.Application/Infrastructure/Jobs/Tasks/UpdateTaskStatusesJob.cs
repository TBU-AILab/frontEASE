﻿using AutoMapper;
using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Domain.Services.Tasks.Core;
using FoP_IMT.Shared.Data.Enums.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FoP_IMT.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskStatusesJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly ITaskCoreService _taskCoreService;
        private readonly ITaskRepository _taskRepository;

        private readonly string _jobName;

        public UpdateTaskStatusesJob(
            IMapper mapper,
            ITaskCoreService taskCoreService,
            ITaskRepository taskRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _taskCoreService = taskCoreService;
            _taskRepository = taskRepository;

            _jobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskStatusesJob!.CronName!;
        }

        public async Task Execute(PerformContext context)
        {
            var checkID = SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.InProgress);

            try
            {
                var query = new TasksQuery();
                var tasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && x.State == TaskState.RUN, query);
                var taskGuids = tasks.Select(x => x.ID).ToList();

                context.WriteLine($"Checking statuses for: {tasks.Count} items.");

                if (tasks.Any())
                {
                    var stateResults = await _taskCoreService.GetTaskStates();
                    foreach (var taskStateInfo in stateResults)
                    {
                        var matchingTask = tasks.SingleOrDefault(x => x.ID == taskStateInfo.Key);
                        if (matchingTask is not null)
                        {
                            context.WriteLine($"Mapping state for task: {matchingTask.ID}.");
                            _mapper.Map(taskStateInfo.Value, matchingTask);

                            if (matchingTask.State != taskStateInfo.Value.State)
                            {
                                context.WriteLine($"Task {matchingTask.ID} - Change state: {matchingTask.State} --> {taskStateInfo.Value}.");
                            }
                        }
                    }
                    await _taskRepository.SaveChangesAsync();
                }

                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} SUCCESS.");
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Ok);
            }
            catch (Exception ex)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Error);
            }
        }
    }
}
