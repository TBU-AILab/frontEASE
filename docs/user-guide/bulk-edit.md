# Edit multiple tasks

Bulk edit applies selected configuration changes to several tasks in one validated operation. It is useful when an experiment family needs the same connector, prompt correction, evaluator, test, stopping condition, analysis, or statistic.

## Requirements

The **Edit selected** action appears when:

- at least two tasks are selected,
- every selected task is in **Created** or **Initialized** state,
- the current user can access every selected task.

Tasks that are Running, Paused, Stopped, Finished, or in Break state cannot be bulk-edited. Clone them first if you need a new configuration derived from completed work.

## Open the bulk editor

1. Open **Tasks**.
2. Select two or more compatible rows using the selection column.
3. Select **Edit selected** above the grid.
4. Wait for the configuration template to load.

The first selected task supplies the starting values for general settings, prompts, and primary modules. Nothing is changed merely by opening the dialog.

## Opt in field by field

Every editable category has an enable checkbox. Only enabled fields are sent as changes. Disabled fields remain different on each task.

| Section | Available changes | Effect |
| --- | --- | --- |
| General settings | Optimization goal, maximum context size, feedback from solution | Replaces only the enabled setting. |
| Prompts | System message, initial message, repeated message | Replaces only the enabled prompt configuration. |
| Primary modules | Connector, evaluator, solution | Replaces the selected module and its parameters. |
| Repeatable modules | Tests, stopping conditions, analyses, statistics | Adds a new module or updates a matching module. |

The **Apply to _N_ tasks** button remains disabled until at least one field is enabled.

## Repeatable modules are additive

Tests, stopping conditions, analyses, and statistics deliberately start empty in the bulk editor. Add only the modules you want to distribute.

For each target task, FrontEASE matches a repeatable module by its category and short package name:

- if a match exists, its configuration is replaced with the values from the dialog;
- if no match exists, the configured module is added;
- unrelated modules already on the task are preserved.

This makes it safe to add one common test without erasing task-specific tests. Bulk edit currently does not provide a bulk-remove operation for repeatable modules; remove those from tasks individually.

## Fields intentionally left task-specific

Bulk edit does not change:

- task names,
- tags,
- authorship,
- user or organization access,
- task state,
- generated messages, solutions, logs, or results.

Use the normal edit or share actions for those values.

## Apply and validate

Before selecting **Apply**, review both the affected-task count and every enabled checkbox. FrontEASE builds a complete candidate configuration for each task and sends the batch to the server.

The server and EASE core validate all candidates before the application commits the database changes. If any candidate is invalid, the dialog displays validation errors and the batch is rejected; already-valid tasks are not intentionally updated on their own. Fix the shared configuration and apply again.

After a successful update:

- a success notification reports how many tasks changed,
- the task grid refreshes its summaries,
- each task keeps its prior state,
- each task is ready for individual review or a bulk run.

## Common workflows

### Change the model provider for an experiment family

1. Select the Created or Initialized tasks.
2. Enable **Connector**.
3. Select the connector package, saved token, model, and connector parameters.
4. Apply and confirm the updated connector column in the grid.

### Add the same regression test

1. Select the tasks.
2. Enable **Tests**.
3. Add and configure the test module.
4. Apply. Existing tests with other short names remain in place.

### Correct a prompt everywhere

1. Select the tasks.
2. Enable only the prompt that needs correction.
3. Edit its Markdown content.
4. Apply. Other prompts and all module choices remain unchanged.

### Standardize stopping behavior

1. Select the tasks.
2. Enable **Stopping conditions**.
3. Add the common stopping-condition package and parameters.
4. Apply. A condition with the same short name is updated; other conditions are retained.

## Safety checklist

Before a large bulk edit:

- filter the grid so the intended experiment family is easy to verify;
- confirm every selected task is meant to receive the same enabled values;
- remember that values are copied from the first selected task only as a starting point;
- configure saved tokens before selecting token-dependent modules;
- apply a small batch first when introducing a newly installed core module;
- open one updated task in the normal editor and review its complete configuration before bulk running the set.
