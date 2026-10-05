namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests
{
    /// <summary>
    /// Explicit selection of task configuration fields changed by a bulk edit.
    /// Unselected fields are preserved independently for every affected task.
    /// </summary>
    public class TaskBulkEditFieldsDto
    {
        public bool OptimizationGoal { get; set; }
        public bool MaxContextSize { get; set; }
        public bool FeedbackFromSolution { get; set; }
        public bool SystemMessage { get; set; }
        public bool InitialMessage { get; set; }
        public bool RepeatedMessage { get; set; }
        public bool Connector { get; set; }
        public bool Evaluator { get; set; }
        public bool Solution { get; set; }
        public bool Tests { get; set; }
        public bool StoppingConditions { get; set; }
        public bool Analyses { get; set; }
        public bool Stats { get; set; }

        public bool HasChanges() =>
            OptimizationGoal || MaxContextSize || FeedbackFromSolution ||
            SystemMessage || InitialMessage || RepeatedMessage ||
            Connector || Evaluator || Solution || Tests ||
            StoppingConditions || Analyses || Stats;
    }
}
