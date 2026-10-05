# Roles, states, and actions

This page summarizes the authorization roles and task-state rules implemented by the current application.

## Application roles

| Role | Typical access |
| --- | --- |
| User | Creates and manages their own tasks and tasks shared with them. Can manage personal tokens, tags, and display preferences. |
| Admin | Can view and manipulate tasks across users, open user management, and administer EASE core packages and modules. |
| Owner | Has the broadest access, including organization management and management of User and Admin accounts. |

The interface hides sections that are not available to the signed-in role. The server repeats authorization and ownership checks; hiding a control is not the security boundary.

## Task states

| State | Meaning |
| --- | --- |
| Created | Draft task record. Its configuration has not completed core initialization. |
| Initialized | Configuration is valid in EASE core and the task is ready to run. |
| Running | The task is currently executing. |
| Paused | Execution is paused and can be resumed. |
| Stopped | Execution was deliberately halted. |
| Finished | Execution completed successfully. |
| Break | Execution ended with an error. Inspect its logs and configuration. |

## Actions by state

The task grid follows these rules:

| Current state | Run | Pause | Stop | Edit | Bulk edit | Overview | Share | Clone | Delete |
| --- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| Created | — | — | — | ✓ | ✓ | — | — | ✓ | ✓ |
| Initialized | ✓ | — | — | ✓ | ✓ | — | — | ✓ | ✓ |
| Running | — | ✓ | ✓ | — | — | ✓ | ✓ | ✓ | ✓ |
| Paused | ✓ | — | — | — | — | ✓ | ✓ | ✓ | ✓ |
| Stopped | — | — | — | — | — | ✓ | ✓ | ✓ | ✓ |
| Finished | — | — | — | — | — | ✓ | ✓ | ✓ | ✓ |
| Break | — | — | — | — | — | ✓ | ✓ | ✓ | ✓ |

Bulk edit requires at least two selected tasks, all in Created or Initialized state. Other bulk state actions appear only when every selected task permits the same transition.

Deletion also applies ownership rules. For a bulk delete, every selected task must be deletable and either owned by the current user or deletable by an Owner.

## Access assignments

Task access can be granted to individual users and organizations. The task author or an Owner can edit access while configuring an unstarted task. After execution begins, use the Share action.

Access to a task can include sensitive experiment material: prompts, model outputs, fitness and feedback, metadata, and generated files. Grant the narrowest access that supports the collaboration.

## State-transition guidance

- Use **Pause** when you expect to resume the same run.
- Use **Stop** when the run should end.
- Use **Clone** when you need a changed configuration after execution has begun.
- Use **Break** logs to diagnose a failed run; users do not manually set Break.
- Download required outputs before **Delete**.
