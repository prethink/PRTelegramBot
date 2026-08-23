# События команд

```csharp
/// <summary>
/// Событие до обработки reply команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreReplyCommandHandle;

/// <summary>
/// Событие после обработки reply команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostReplyCommandHandle;

/// <summary>
/// Событие до обработки dynamic reply команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreDynamicReplyCommandHandle;

/// <summary>
/// Событие после обработки dynamic reply команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostDynamicReplyCommandHandle;

/// <summary>
/// Событие до обработки slash команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreSlashCommandHandle;

/// <summary>
/// Событие после обработки slash команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostSlashCommandHandle;

/// <summary>
/// Событие до обработки inline команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreInlineCommandHandle;

/// <summary>
/// Событие после обработки inline команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostInlineCommandHandle;

/// <summary>
/// Событие до обработки next step команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPreNextStepCommandHandle;

/// <summary>
/// Событие после обработки next step команды.
/// </summary>
public event Func<BotEventArgs, Task>? OnPostNextStepCommandHandle;
```
