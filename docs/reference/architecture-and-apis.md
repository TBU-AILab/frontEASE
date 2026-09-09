# Architecture and APIs

FrontEASE combines a browser client, a .NET server, PostgreSQL, and the Python EASE core. The default Docker Compose setup runs each responsibility in its own container.

## Runtime services

| Service | Default local address | Responsibility |
| --- | --- | --- |
| FrontEASE client | `http://localhost:5235` | Blazor web interface, authentication state, forms, grids, charts, and downloads. |
| FrontEASE server | `http://localhost:4000` | Authorization, application workflows, persistence, validation coordination, and the client-facing API. |
| EASE core | `http://localhost:8086` | Dynamic packages, task initialization and execution, solution generation, evaluation, and task artifacts. |
| PostgreSQL | `localhost:5432` | Users, organizations, task records, access assignments, preferences, and synchronized task data. |
| Documentation | `http://localhost:8000` | MkDocs development preview when the docs service is enabled. |

All application containers share the `fop-imt-network` Docker network. Browser-facing URLs are configured separately from service-to-service connectivity.

## Request flow

```text
Browser
  │
  │ HTTP / JSON
  ▼
FrontEASE .NET server ───── PostgreSQL
  │
  │ core DTOs / HTTP
  ▼
EASE FastAPI core ───────── task artifacts and dynamic modules
```

The client does not call PostgreSQL or manipulate core task files directly. Application services on the .NET server perform access checks and coordinate database and core operations.

## Main server API groups

The current server exposes these functional groups below `/api`:

| Group | Selected routes |
| --- | --- |
| Tasks | `/tasks`, `/tasks/{id}`, `/tasks/{id}/clone`, `/tasks/bulk-edit`, `/tasks/change-state/{state}`, `/tasks/share/{id}` |
| Users | `/users`, `/users/{id}`, `/users/all` |
| Organizations | `/companies`, `/companies/{id}`, `/companies/all` |
| Management | `/management`, `/management/global`, `/management/tags` |
| Core administration | `/core/module`, `/core/available-models`, `/core/models` |
| Type lists | `/typelists/module-options` |
| Files | `/files/directory/{type}/{id}` |

These endpoints are application internals and may evolve during the open beta. Use the generated OpenAPI description of the running server when integrating against a specific revision.

## Core API responsibilities

The FastAPI service provides task initialization, options, state transitions, downloads, package management, and available-model configuration. Notable route families include:

- `/task` and `/task/{task_id}` for individual task configuration and information;
- `/batch/run`, `/batch/pause`, `/batch/stop`, and `/batch/delete` for group actions;
- `/batch/task` for validated multi-task configuration updates;
- `/system/import` and `/system/pm/*` for packages and modules;
- `/available-models` and `/update-models` for connector model catalogs;
- task and solution download routes for generated artifacts.

## How bulk edit is coordinated

Bulk edit crosses both repositories:

1. The client submits task IDs, a template configuration, and explicit field-selection flags to `PUT /api/tasks/bulk-edit`.
2. The .NET application service checks selection size, access, task existence, editable states, and enabled fields.
3. It merges only the enabled fields into a complete configuration for every task.
4. The server sends all candidates to `PUT /batch/task` in EASE core.
5. Core deep-copies and initializes every candidate without immediately replacing the live tasks.
6. Only after every candidate validates does core persist and swap the staged task objects.
7. The .NET service updates its task records inside a database transaction and returns refreshed task summaries.

This boundary prevents a normal validation failure on one candidate from intentionally producing a half-applied batch.

## Repository layout

The most important projects are:

| Path | Purpose |
| --- | --- |
| `src/FrontEASE.Client` | Blazor UI and API clients. |
| `src/FrontEASE.Server` | HTTP controllers and application host. |
| `src/FrontEASE.Application` | Application workflows and authorization-aware orchestration. |
| `src/FrontEASE.Domain` | Domain services and the EASE core connector. |
| `src/FrontEASE.Infrastructure` | Persistence and infrastructure implementations. |
| `src/FrontEASE.Shared` | Client/server DTOs, enums, validation, and shared constants. |
| `src/FrontEASE.DataContracts` | Transport models used at external boundaries. |
| `src/FoP_IMT.Core` | Git submodule containing the Python EASE core. |
| `docs` | MkDocs source and screenshots. |

## Extending the system

When adding a feature that changes task configuration:

1. define shared request and result contracts;
2. add the authorized server endpoint and application workflow;
3. extend the core transport DTO and validation path;
4. preserve task-state and access invariants;
5. update the client with clear opt-in behavior and validation feedback;
6. test .NET compilation and the affected Python workflow;
7. update this manual and the OpenAPI-visible behavior.

Dynamic task modules should continue to expose their parameters through core module metadata so the client can render their configuration without package-specific UI code.
