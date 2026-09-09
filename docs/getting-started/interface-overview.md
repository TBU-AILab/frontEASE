# Interface overview

This page gives a short overview of the main parts of the FrontEASE interface.

The exact layout may change between versions, but the basic workflow is usually the same: log in, create or select a task, configure its modules, start execution, and inspect the generated outputs and results.

!!! note "Screenshots"
    The screenshots in this manual are intended to illustrate the typical local/demo setup. Your interface may look slightly different depending on the current version of FrontEASE, browser size, selected configuration, and available modules.

---

## Login screen

After opening FrontEASE in the browser, the first screen is the login page.

For the seeded local setup, you can use the default demo credentials described in the installation guide.

![FrontEASE login screen](../assets/screenshots/login-screen.png)

/// caption
Login screen of the local FrontEASE instance.
///

After successful login, the application opens the main interface.

---

## Main workspace

After login, FrontEASE opens the Tasks workspace. It provides access to existing tasks, task creation, task details, and state-dependent actions.

![FrontEASE main dashboard](../assets/screenshots/main-dashboard.png)

/// caption
Main dashboard after logging into FrontEASE.
///

From this workspace, users can:

- view existing tasks,
- create a new task,
- open task details,
- inspect the current state of a task,
- navigate to outputs, messages, solutions, and analyses.

The dashboard is mainly intended as an entry point. Most detailed work is done inside a selected task.

---

## Navigation menu

The fixed navigation bar provides the application's primary routes. On smaller screens, use the menu toggle to expand it.

![FrontEASE navigation menu](../assets/screenshots/navigation-menu.png)

/// caption
Navigation menu with access to the main sections of FrontEASE.
///

The available entries are:

- **Tasks** — the experiment grid and all task workflows;
- **Users** — user management for Admins and Owners, plus organization management for Owners;
- **Management** — personal tokens, tags, display preferences, and role-gated core administration;
- **Hangfire** — background-job dashboard for Owners;
- **Swagger** — generated server API documentation for Owners;
- the account menu — signed-in user details and logout.

!!! tip
    If you are new to FrontEASE, start from the task list or dashboard and open an existing seeded example before creating your own task.

---

## Task list

The task list shows available tasks in the system. A task represents one configured experiment or execution workflow.

![FrontEASE task list](../assets/screenshots/task-list.png)

/// caption
Task list with available task entries.
///

A task entry can contain:

- task name or identifier,
- current task state,
- creation or update time,
- selected configuration,
- available actions.

The states are Created, Initialized, Running, Paused, Stopped, Finished, and Break. Available row and bulk actions change with state.

The task list is useful for checking which experiments already exist and whether they are still running or completed.

---

## Task overview

The task overview page shows information about a selected task. This is usually the main place for inspecting one concrete experiment.

![FrontEASE task overview](../assets/screenshots/task-overview.png)

/// caption
Task overview page showing information about a selected task.
///

The overview includes:

- basic task information,
- current state,
- selected modules,
- generated messages,
- generated solutions,
- fitness or evaluation values,
- feedback,
- metadata,
- available result files or analysis outputs.

The left pane shows configuration, the center pane shows the chronological message history, and the right pane shows general information, charts, solutions, feedback, metadata, and downloads.

---

## Module configuration

EASE tasks are configured through modules. Modules define what the task does, how solutions are generated, how they are evaluated, and when the run should stop.

In normal use, select an available module and fill in the parameters defined by its package. See [Tasks, modules, and iterations](../concepts/tasks-and-modules.md) for the module roles and [Task management](../user-guide/task-management.md) for the complete editor workflow.

## Messages and solutions

During an EASE run, the overview displays system, user, and AI messages alongside evaluated solutions. AI cards identify the model where available; solution cards show fitness, feedback, metadata, and a download action.

This section is useful for understanding the iterative process.

A typical sequence may contain:

1. an initial message or prompt,
2. a generated solution,
3. evaluation of the solution,
4. feedback based on the evaluation,
5. a new improved solution,
6. repeated iterations until the stopping condition is reached.

For algorithm-generation experiments, the solution is often executable code and the evaluation result is represented by a fitness value or another task-specific metric.

---

## Results and analysis

When solutions exist, FrontEASE displays value-evolution and convergence charts. You can download a single solution archive or the complete set of task solution files.

The available outputs depend on the task configuration and enabled analysis modules.

Typical outputs may include:

- final best solution,
- fitness values,
- iteration history,
- generated files,
- plots or reports,
- task metadata,
- downloadable result packages.

See [Results and downloads](../user-guide/results-and-downloads.md) for a guided tour.

## Typical user workflow

A common FrontEASE workflow looks like this:

```text
Log in
  ↓
Open the dashboard or task list
  ↓
Configure modules and parameters
  ↓
Create a new task or open an existing one
  ↓
Start the task
  ↓
Monitor task state
  ↓
Inspect generated messages, solutions, feedback, and metadata
  ↓
Open results and analysis outputs
```
The following pages explain these steps in more detail, including single-task and bulk workflows.

## Next step

Continue with [Users and management](../user-guide/users-and-management.md).
