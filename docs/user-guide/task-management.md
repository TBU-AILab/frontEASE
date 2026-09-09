# Task management

The **Tasks** page is the main workspace for creating, finding, configuring, running, sharing, cloning, and deleting experiments.

![Task list](../assets/screenshots/task-list.png)

/// caption
The task grid, including filters, state, and row actions.
///

## Understand the task grid

The grid can display the following columns:

- name and optimization direction,
- tags,
- creation and last-update timestamps,
- author,
- state and any recorded error indicator,
- LLM connector,
- solution type,
- available actions.

Your visible columns are configurable in **Management → General → Tasks**. The information icon opens a small summary card without leaving the grid.

### Find tasks

Use the filter row to narrow the grid by the values shown in each column. Text filters use partial matching. The tags control accepts one or more existing tags, and the state filter selects a lifecycle state. You can also sort supported columns and change the page size with the pager below the grid.

The list refreshes task states periodically. A task that is running may therefore change to **Finished** or **Break** without a full page reload.

## Create a task

1. Open **Tasks**.
2. Select the **plus** icon at the upper right of the task list.
3. Enter a name and choose **Maximization** or **Minimization**.
4. Optionally assign existing tags.
5. Enter the system, initial, and repeated messages.
6. Set the maximum context size and feedback behavior.
7. Configure the Solution, LLM connector, and Evaluator modules.
8. Add any Tests, Stopping conditions, Analyses, and Statistics.
9. If you own the task, configure access for users or organizations.
10. Save the task.

The task is first created as a draft. Saving sends its complete configuration to EASE core for validation and initialization. A valid task becomes **Initialized** and can be run. If module validation fails, the editor displays the processing errors; fix those fields and save again.

For a fully worked example, follow [Create your first task](first-text-task.md).

## Configure prompts and iteration context

The editor supports Markdown for the system and initial messages.

- Put durable role, format, and safety instructions in the **system message**.
- Put the concrete problem and acceptance criteria in the **initial message**.
- Use the **repeated message** to tell the model how to improve the preceding solution.
- Keep **maximum context size** small when token use matters; use the infinity setting only when the complete history is useful and supported by the connector.
- Enable **feedback from solution** when evaluator feedback should guide the next generation.

## Configure modules

Select a module by its package name, then complete the parameter fields supplied by that package. Primary modules are single-choice. Tests, stopping conditions, analyses, and statistics are repeatable; use their add control to create multiple configured entries.

Saved provider credentials are selected through token fields rather than pasted directly into a task. Create tokens under **Management → Tokens** before configuring connectors or evaluators that require them.

!!! warning "Stopping conditions"
    A task without a suitable stopping condition may run longer than intended. Verify iteration, time, or target-fitness limits before starting an experiment.

## Row actions

The icons at the right of each task depend on its state:

| Action | What it does |
| --- | --- |
| Run | Starts an Initialized task or resumes a Paused task. |
| Pause | Pauses a Running task so it can be resumed later. |
| Stop | Halts a Running task. A stopped task cannot be resumed from the grid. |
| Overview | Opens configuration, messages, charts, solutions, feedback, and metadata. |
| Edit | Changes a Created or Initialized task. |
| Share | Changes user and organization access for a task that has started or ended. |
| Clone | Creates one or more new tasks from an existing configuration. |
| Delete | Removes one or more tasks after confirmation. |

See the complete availability table in [Roles, states, and actions](../reference/roles-states-actions.md).

## Edit a task

Editing is available only while a task is **Created** or **Initialized**. Open the pen action, update the configuration, and save. The core validates the new configuration before it replaces the stored version.

Once a task has started, its configuration is treated as part of the experiment record. Clone the task when you want to run a variation of a completed or running experiment.

## Clone tasks

The clone action is available in every normal task state.

1. Select the copy icon.
2. Enter the base name for the clone. The paste icon restores the source name.
3. Choose between 1 and 15 copies.
4. Confirm **Clone**.

The cloned task belongs to the user who performs the clone. Clones begin as new task records and can be edited before execution.

## Share task access

Task access can be assigned to individual users and organizations. The author or an Owner can configure access while editing an unstarted task. For tasks that have started or ended, use the share action in the grid.

Review the selected users, organizations, and assigned roles before saving. Sharing a task can expose its prompts, generated content, evaluation feedback, metadata, and downloadable solution files.

## Run, pause, resume, and stop

- **Run** changes an Initialized task to Running.
- **Pause** requests that a Running task pause.
- **Run** on a Paused task resumes it.
- **Stop** requests a terminal stopped state.

State changes require confirmation. The same actions appear above the grid when every selected task supports the requested transition.

## Work with several tasks

Select rows using the first grid column. When two or more compatible tasks are selected, FrontEASE displays bulk actions above the grid:

- **Edit selected** for Created and Initialized tasks,
- **Run** for Initialized or Paused tasks,
- **Pause** and **Stop** for Running tasks,
- **Delete** when every task can be deleted and ownership permits it.

Selections with mixed incompatible states do not show an invalid action. For configuration changes, continue with [Edit multiple tasks](bulk-edit.md).

## Delete tasks

Deleting is intentionally separate from stopping. A running task is first interrupted by the server before its records are removed. Bulk delete is available only when every selected task passes the state and ownership checks.

!!! danger
    Deletion removes task records and can make generated outputs unavailable. Download anything you need before confirming it.
