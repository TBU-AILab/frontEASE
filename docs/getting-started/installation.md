# Installation and first run

This page describes how to install and start FrontEASE locally using Docker.

The instructions below assume a standard local development setup on Windows, Linux, or macOS.

## Prerequisites

Before starting, install:

- **Git** — used to clone the repository,
- **Docker** — used to run the application services in containers.

!!! warning "Docker must be running"
    Docker has to be started before running the Docker Compose command.

## Clone the repository

Open a terminal in the directory where you want to place the project and run:

```bash
git clone --recurse-submodules https://github.com/TBU-AILab/frontEASE.git
cd frontEASE
```

The `--recurse-submodules` option is important because the project uses Git submodules.

## Create the `.env` file

In the root directory of the repository, create a file named `.env`.

Use the following content for a basic local setup:

```env
# FrontEASE database user
POSTGRES_USER="postgres"
POSTGRES_PASSWORD="root"

# Options: true | false
# If true, the database will be deleted and re-created with seed data.
# Use true for the first initialization.
SEED_DB="true"

# FrontEASE server and EASE backend URLs
API_BASE_URL="http://localhost:4000"
PYTHON_BASE_URL="http://localhost:8086"
```

!!! note "About SEED_DB"
For the first run, keep `SEED_DB="true"` so that the database is initialized with seed data. For later runs, you may want to set it to `false` if you do not want the database to be recreated.

!!! warning "Changing database credentials"
If you change `POSTGRES_USER` or `POSTGRES_PASSWORD`, make sure the corresponding connection strings in the application configuration are updated as well.

## Start the application

From the repository root, run:

```bash
docker compose up -d
```

This starts the application containers in the background.

To check running containers, use:

```bash
docker compose ps
```

To see logs, use:

```bash
docker compose logs
```

To stop the application, use:

```bash
docker compose down
```

## Open FrontEASE in the browser

After the containers start, open:

```text
http://localhost:5235
```

## Default login

For the seeded local setup, use the default demo credentials:

```text
Username: BigJoe
Password: root1234
```

!!! warning "Demo credentials"
These credentials are intended for the seeded local/demo setup. They should not be used as production credentials.

## What you should see after login

After logging in, you should see the FrontEASE web interface. The exact layout may change between versions, but the interface is generally used to manage tasks, configure experiments, monitor progress, and inspect generated outputs.

A successful first run means that:

* the Docker containers are running,
* the browser opens the FrontEASE interface,
* the login screen accepts the seeded demo credentials,
* the main application dashboard is displayed.

## Common first-run issues

### Docker is not running

If the Docker Compose command fails, check whether Docker Desktop or the Docker daemon is running.

### Port is already in use

If a container cannot start because a port is already allocated, another application or container may already be using the required port.

Typical ports used by the local setup include:

```text
5235   FrontEASE web interface
4000   FrontEASE server API
8086   EASE backend
5432   PostgreSQL database
```

Stop the conflicting service or adjust the configuration.

### Database was not initialized

If login does not work during the first run, check that:

```env
SEED_DB="true"
```

Then restart the containers.

Depending on your local state, you may also need to remove old containers or volumes before reinitializing the database.

## Next step

After the application starts successfully, continue with the [Interface overview](getting-started/interface-overview.md) and the first task tutorial.