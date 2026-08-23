---
description: Interface for step-by-step command execution.
---

# IExecuteStep

Interface for step-by-step command execution.

## Properties

| Property | Description |
| --- | --- |
| `bool IgnoreBasicCommands { get; set; }` | Ignore the basic commands while steps are running. |
| `bool LastStepExecuted { get; set; }` | Whether this was the last step and it has completed. |

## Methods

| Method | Description |
| --- | --- |
| `Func<IBotContext, Task> GetExecuteMethod()` | Gets the reference to the method that has to be executed. |
| `Task<ExecuteStepResult> ExecuteStep(IBotContext context)` | Executes the command. |
| `bool CanExecute()` | Whether the step can be executed |

