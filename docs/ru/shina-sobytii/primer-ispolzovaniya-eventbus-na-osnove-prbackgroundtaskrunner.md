# Пример использования EventBus на основе PRBackgroundTaskRunner

Данный пример демонстрирует, как класс может быть **подписчиком шины событий (EventBus)**, реагировать на события из разных частей приложения и корректно управлять своим жизненным циклом.

В качестве примера используется класс [**PRBackgroundTaskRunner**](../api/klassy/prbackgroundtaskrunner.md), отвечающий за запуск и остановку фоновых задач.

### Интерфейс IPRTaskRunnerSubscriber

Для удобства работы с конкретными сценариями поверх `IPRGlobalSubscriber` могут использоваться **специализированные интерфейсы подписчиков**.

Одним из таких интерфейсов является **`IPRTaskRunnerSubscriber`** — подписчик, предназначенный для обработки событий, связанных с управлением фоновыми задачами.

```csharp
public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
{
    void StopEvent(IEnumerable<long> botIds, Guid taskId);
    void StopEvent(Guid taskId);
}
```

[`IPRTaskRunnerSubscriber`](../api/interfeisy/iprtaskrunnersubscriber.md):

* наследуется от [`IPRGlobalSubscriber`](../api/interfeisy/iprglobalsubscriber.md);
* описывает набор методов, которые могут быть вызваны через [PREventBus](../api/klassy/preventbus.md);
* определяет контракт событий управления фоновыми задачами (в данном случае — остановка задачи).

Таким образом, **EventBus вызывает не абстрактные события**, а **конкретные методы интерфейсов-подписчиков**, что:

* обеспечивает строгую типизацию;
* упрощает навигацию по коду;
* исключает ошибки вызова неподдерживаемых событий.

### Реализация подписчика событий

```csharp
public sealed class PRBackgroundTaskRunner 
    : IPRBackgroundTaskRunner, IPRTaskRunnerSubscriber
```

Класс реализует интерфейс [`IPRTaskRunnerSubscriber`](../api/interfeisy/iprtaskrunnersubscriber.md), который является наследником [`IPRGlobalSubscriber`](../api/interfeisy/iprglobalsubscriber.md).\
Это означает, что данный класс:

* может быть подписан на события шины [PREventBus](../api/klassy/preventbus.md);
* обязан реализовать методы подписки и отписки;
* содержит набор методов, которые будут вызываться через [PREventBus](../api/klassy/preventbus.md).

### Подписка и отписка от событий

```csharp
public void Subscribe()
{
    PREventBus.Subscribe(this);
}

public void Unsubscribe()
{
    PREventBus.Unsubscribe(this);
}
```

Методы `Subscribe` и `Unsubscribe` отвечают за регистрацию и удаление экземпляра класса из шины событий.

* `Subscribe()` — регистрирует текущий экземпляр как подписчика всех событий, которые он поддерживает.
* `Unsubscribe()` — удаляет подписчика из EventBus, предотвращая дальнейшие вызовы событий.

Это позволяет централизованно управлять жизненным циклом подписчиков и избегать утечек памяти.

### Автоматическая отписка при уничтожении объекта

```csharp
public void Dispose()
{
    Unsubscribe();
}
```

Класс реализует интерфейс `IDisposable`, благодаря чему при уничтожении экземпляра выполняется автоматическая отписка от шины событий.

Это гарантирует, что:

* объект не будет получать события после завершения своей работы;
* в EventBus не останется «висячих» подписчиков.

### Подписка в конструкторе

```csharp
public PRBackgroundTaskRunner(PRBotBase bot)
{
    this.bot = bot;
    this.Subscribe();
}

```

Подписка на события происходит **сразу при создании экземпляра** `PRBackgroundTaskRunner`.

Таким образом:

* раннер начинает реагировать на события сразу после инициализации;
* разработчику не нужно вручную вызывать `Subscribe()`.

## Как вызывается событие через EventBus

Событие может быть инициировано из любой части приложения следующим образом:

```csharp
PREventBus.RaiseEvent<IPRTaskRunnerSubscriber>(
    subscriber => subscriber.StopEvent(taskId)
);
```

EventBus автоматически:

* найдёт всех подписчиков типа `IPRTaskRunnerSubscriber`;
* вызовет соответствующий метод у каждого из них;
* безопасно обработает ошибки отдельных подписчиков.
