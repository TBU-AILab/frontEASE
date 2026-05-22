# FrontEASE User Manual

FrontEASE is the graphical user interface for the EASE framework.

EASE, short for **Effortless Algorithmic Solution Evolution**, is a modular framework for iterative solution generation, evaluation, and refinement. It is designed for workflows where generated solutions are repeatedly evaluated and improved based on feedback. FrontEASE provides a web-based interface for working with this process without requiring users to interact with the backend only through code or API calls.

!!! info "Open beta"
    FrontEASE is currently provided as an open beta version of the framework. Some features, names, workflows, or screens may change as the system evolves.

## What FrontEASE is used for

FrontEASE allows users to:

- create and configure tasks,
- define modular solution-generation pipelines,
- run experiments,
- inspect generated messages and solutions,
- view fitness values, feedback, and metadata,
- follow the progress of iterative runs,
- access results and analysis outputs.

In a typical workflow, the user defines a task, selects or configures the required modules, starts the run, and then monitors how solutions evolve across iterations.

## Basic idea of EASE

EASE is based on a modular workflow. Instead of solving a problem with one fixed algorithm, the system can combine different modules responsible for tasks such as prompting, solution generation, evaluation, feedback creation, stopping conditions, and analysis.

A simplified EASE workflow looks like this:

```text
Task definition
      ↓
Initial prompt / input
      ↓
Solution generation
      ↓
Evaluation
      ↓
Feedback
      ↓
Improved solution
      ↓
Repeated until stopping condition is reached
```

This makes EASE useful for experiments where solutions are generated and refined iteratively, for example in algorithm design, optimization, code generation, simulation-based evaluation, or other structured generation tasks.

## FrontEASE and the backend

FrontEASE is not the whole EASE framework. It is the frontend interface that communicates with backend services.

In a local Docker-based setup, the system typically consists of:

* the FrontEASE client interface,
* the FrontEASE server,
* the PostgreSQL database,
* the EASE backend service.

The frontend is used in the browser, while the backend performs the actual task execution and evaluation.

## Who should use this manual

This manual is intended for users who want to:

* install and run FrontEASE locally,
* understand the basic interface,
* create and run tasks,
* inspect generated outputs and results,
* use FrontEASE for experiments without needing to modify the source code.

Developer-level topics, such as implementing new modules or changing the internal architecture, should be covered in the separate technical documentation.

## Next step

Continue with [Installation and first run](getting-started/installation.md).