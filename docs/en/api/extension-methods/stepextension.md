---
description: Lets the user execute commands step by step
---

# StepExtension

Lets the user execute commands step by step

## Methods

| Method | Description |
| --- | --- |
| `static void RegisterStepHandler(this Update update, IExecuteStep command)` | Registers the next step. |
| `static TExecuteStep? GetStepHandler<TExecuteStep>(this Update update) where TExecuteStep : IExecuteStep` | Gets the user's handler, or null. |
| `static IExecuteStep? GetStepHandler(this Update update)` | Gets the current step handler. |
| `static void ClearStepUserHandler(this Update update)` | Clears the user's steps. |
| `static bool HasStepHandler(this Update update)` | Checks whether the user has a step registered. |

