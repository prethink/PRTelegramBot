# Создание команды только для администраторов

В данном примере разберем как можно использовать внутренний чекер команд для создания методов, которые могут использовать только администраторы.

Для начала нужно создать атрибут, в примере будет использоваться AdminOnlyExampleAttribute. Данным атрибутом будут помечаться те методы, которые могут вызвать только администраторы бота.

```csharp
namespace ConsoleExample.Attributes
{
    internal class AdminOnlyExampleAttribute : Attribute
    {
    }
}
```

Создадим чекер AdminExampleChecher, чекер будет проверять, может ли пользователь вызвать метод или нет.

```csharp
namespace ConsoleExample.Checkers
{
    // Обязательно реализовываем интерфейс IInternalCheck.
    internal class AdminExampleChecher : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // Из обработчика получаем ссылку на метод.
            var method = handler.Command.Method;
            // Из методы пытаемся получить наш атрибут.
            var adminAttribute = method.GetCustomAttribute<AdminOnlyExampleAttribute>();
            if(adminAttribute != null)
            {
                var userIsAdmin = await context.IsAdmin(context.Update.GetChatId());
                if(!userIsAdmin)
                    await MessageSender.Send(context, "Вы не админ!");
                // Пользователь админ возращаем результат Passed, что позволяет выполнить метод, иначе выполнение метода будет приостановлено.
                return userIsAdmin ? InternalCheckResult.Passed : InternalCheckResult.Custom;
            }
            return InternalCheckResult.Passed;
        }
    }
}
```

При создание бота добавляем наш чекер.

```csharp
var adminChecker = new InternalChecker(new List<CommandType>() { CommandType.Reply, CommandType.NextStep, CommandType.Inline, CommandType.DynamicReply, CommandType.Slash }, new AdminExampleChecher());

var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddCommandChecker(adminChecker)
                    .Build();
```

Создаем метод который должен быть доступен только для администраторов. Подставляем созданный нами атрибут.

```csharp
/// <summary>
/// Команда отработает для бота с botId 0.
/// Команда отработает при написание в чат "Только админы".
/// Пример работы кастомного чекера и кастомного атрибута.
/// </summary>
[AdminOnlyExample]
[ReplyMenuHandler("Только админы")]
public static async Task AdminOnlyExample(IBotContext context)
{
    bool isAdminUpdate = await context.IsAdmin();
    bool isAdminById = await context.IsAdmin(context.Update.GetChatId());
    await MessageSender.Send(context, $"Вы администратор бота: {isAdminById} {isAdminUpdate}");
}

```

<figure><img src="../.gitbook/assets/изображение (23).png" alt=""><figcaption></figcaption></figure>

Это один из примеров как можно использовать чекеры.
