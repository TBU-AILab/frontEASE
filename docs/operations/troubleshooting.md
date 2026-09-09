# Troubleshooting

Start with the smallest check that distinguishes a browser problem from a server, database, or EASE core problem.

## Check container health

From the repository root:

```bash
docker compose ps
```

The normal application services are `postgres`, `dotnet-server`, `dotnet-client`, and `backend-core`. On the docs branch, the `docs` service may also be present.

Read recent logs for one service:

```bash
docker compose logs --tail=200 dotnet-server
docker compose logs --tail=200 backend-core
docker compose logs --tail=200 postgres
```

Follow logs during a reproduction:

```bash
docker compose logs -f dotnet-server backend-core
```

## Local addresses do not respond

Verify these defaults:

| Address | Expected service |
| --- | --- |
| `http://localhost:5235` | FrontEASE interface |
| `http://localhost:4000` | FrontEASE server |
| `http://localhost:8086` | EASE core |
| `http://localhost:8000` | Documentation preview on the docs branch |

If a container reports a port-allocation error, stop the conflicting process or change the host-side port mapping. Keep the client/server base URLs consistent with any changed ports.

## Login fails after first installation

For the seeded demo environment:

1. Confirm `SEED_DB="true"` was set for initial database creation.
2. Confirm PostgreSQL reports healthy.
3. Inspect `dotnet-server` logs for migration or seed errors.
4. Use the seeded credentials only in a local demo environment.

After the database is initialized, set `SEED_DB="false"` before later restarts when you want to preserve data.

!!! danger
    Recreating the database or removing its Docker volume destroys persisted users, tasks, and preferences. Export needed results and back up data first.

## Task remains Created

A task normally becomes Initialized after a valid configuration is saved. If it remains Created:

- review validation messages at the bottom of the editor;
- confirm a compatible Solution, Connector, and Evaluator are selected;
- check required module parameters and token selectors;
- verify that EASE core is reachable from the server;
- inspect core logs for module import or initialization errors.

## Run action is missing

Run is shown only for Initialized and Paused tasks. If the task is Created, complete initialization. If it is already Running, use Pause or Stop. Stopped, Finished, and Break tasks are terminal in the current grid workflow; clone one to run a modified variant.

## Edit selected is missing

Bulk edit requires at least two selected tasks and every selected task must be Created or Initialized. Clear tasks in other states from the selection. If the button is still absent, confirm that the account has access to every task.

## Bulk edit is rejected

The batch is validated as a set. One incompatible connector, evaluator, solution type, or module parameter can reject the request.

1. Read every validation message in the dialog.
2. Open one affected task in the normal editor and confirm the intended module is available.
3. Check required saved tokens and model names.
4. Try the change on a small representative selection.
5. Inspect .NET server and core logs if the message is not sufficient.

## Task ends in Break

Open the log indicator in the task's State cell and review the overview. Common causes include unavailable provider models, invalid credentials, generated output that a solution module cannot parse, evaluator/test failures, and missing core packages.

Avoid editing the historical task after execution. Clone it, correct the configuration, and run the clone.

## Core module options are empty or stale

- Confirm the core container is running.
- Open **Management → Core → Packages** and verify expected packages.
- Open **Management → Core → Modules** and verify module discovery.
- Review **Extended** available-model configuration when connector model lists are affected.
- Reopen the task editor after package or model configuration changes.

Package installation and deletion affect every user of the instance. Coordinate those operations on shared deployments.

## Downloads fail

Check browser download permissions, free disk space, and core availability. Individual solution and full-task archives are produced from core task artifacts, so a missing artifact or inaccessible core service can prevent the server from completing the download.

## Documentation preview

The docs service mounts the repository and serves MkDocs on port 8000. After editing Markdown, refresh the browser. To diagnose a rendering error:

```bash
docker compose logs --tail=200 docs
```

For a clean validation run inside the existing image:

```bash
docker compose exec docs mkdocs build --strict
```

## Collect useful diagnostic information

When reporting a problem, include:

- the FrontEASE and EASE core commit IDs;
- the task state and action attempted;
- exact validation or log text with secrets removed;
- relevant server/core log lines and timestamps;
- whether the problem affects one task or every task;
- the selected module short names and versions.

Never include provider tokens, passwords, connection strings, or generated content that is confidential.
