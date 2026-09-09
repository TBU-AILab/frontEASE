# Results and downloads

The task overview connects an experiment's configuration with its message history and evaluated solutions. It is available after a task has started, including Running, Paused, Stopped, Finished, and Break states.

![Task overview](../assets/screenshots/task-overview.png)

/// caption
The task overview combines configuration, messages, and result information.
///

## Open the overview

From **Tasks**, select the magnifying-glass action on a task. The dialog has three working areas:

| Area | Contents |
| --- | --- |
| Left | Saved prompts, iteration settings, modules and parameters, and access assignments. |
| Center | Chronological System, User, and AI messages. |
| Right | General task information, result charts, evaluated solutions, feedback, metadata, and downloads. |

While a task is Running, FrontEASE refreshes the open overview periodically. A small loading indicator can appear during a refresh.

## Read the message history

Messages are ordered by creation time. Each message card identifies its role and timestamp. AI messages also show the model name when the connector reports it, and message headers can show token counts according to your preferences.

Use the history to answer three questions:

1. What instructions were active for this iteration?
2. What candidate did the model generate?
3. What feedback was used to request the next candidate?

The display format for System and User messages is configurable under **Management → General → Tasks**.

## Interpret solutions

Each solution card can contain:

- its fitness value,
- evaluator feedback,
- task- or module-specific metadata,
- a download action.

Fitness must be interpreted with the task's optimization goal. For **Maximization**, larger values are better; for **Minimization**, smaller values are better.

Select a solution card to highlight the AI message that produced it. Select it again to clear the association.

## Use the charts

When solutions exist, the overview shows:

- **value evolution**, which plots evaluated values over the run;
- **convergence evolution**, which helps show how the best-so-far result changes.

Charts summarize the run but do not replace inspection of evaluator feedback and metadata. A flat curve can mean convergence, a strict stopping rule, repeated invalid candidates, or a metric with little sensitivity.

## Download outputs

There are two download levels:

- the download icon on a solution saves the files for that individual solution;
- the download icon in the Solutions heading saves the complete task solution archive.

Archive names include the task name and identifiers so results from similar experiments remain distinguishable. Browser download settings determine the final save location.

!!! tip
    Download the full archive before deleting a task or reinitializing a local database.

## Inspect failures

A task in **Break** state ended with an error. In the task grid, the state cell can expose recorded log entries. Review those entries first, then check:

- connector credentials and model availability,
- evaluator and test parameters,
- generated candidate format,
- core container logs,
- connectivity between the FrontEASE server and EASE core.

The configuration shown in the overview is read-only. Clone the task to create an editable copy for a corrected run.

## Compare experiment variants

For repeatable comparisons:

1. Clone a known task into several variants.
2. Edit only the intended independent variable, or use [bulk edit](bulk-edit.md) for common changes.
3. Run the variants.
4. Compare optimization direction, fitness curves, final feedback, metadata, and downloaded artifacts.
5. Keep task names and tags systematic so the variants remain easy to filter.
