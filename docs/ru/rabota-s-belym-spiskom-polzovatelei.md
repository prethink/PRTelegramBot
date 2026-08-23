# Работа с белым списком пользователей

Библиотека позволяет работать с функционалом белого списка. Если есть пользователи в белом списке, значит только эти пользователи смогут пользоваться ботом.  При создание бота можно добавлять пользователей в белый список помощью идентификаторов.

```csharp
var telegram = new PRBotBuilder("")
                    .AddUserWhiteList(1111)
                    .AddUserWhiteList(2222, 3333, 4444, 555)
                    .AddUsersWhiteList(new List<long>() { 5555, 6666, 77777})
                    .Build();
```

Для работы с белым списком используется свойство WhiteListManager в [TelegramOptions](api/klassy/telegramoptions/). [WhiteListManager](api/klassy/whitelistmanager.md)  реализует интерфейс [IWhiteListManager](api/interfeisy/iwhitelistmanager.md)**,** это значит, что в случае необходимости можно подставить свой класс, который например будет работать с базой данных. Для этого зарегистрируйте его через **DI** или воспользуйтесь методом SetWhiteListManager при создание бота.

<figure><img src=".gitbook/assets/изображение (34).png" alt=""><figcaption></figcaption></figure>



## Сделать отдельные команды доступные для всех пользователей

Работу белого списка можно сделать более гибкой, позволить некоторые команды выполнять всем пользователям, даже которые не находятся в списке. Для этого нужно при создание бота выставить настройку WhiteListSettings.OnlyCommands для белого списка и добавить к методам в котором будет проигнорирована проверка специальный атрибут WhiteListAnonymous.

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .AddUserWhiteList(552135213512)
                    .SetWhiteListSettings(WhiteListSettings.OnlyCommands)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();
```

SetWhiteListSettings(WhiteListSettings.OnlyCommands) - устанавливает настройку, которая активирует работу белого списка только в команда reply, slash, inline.

WhiteListAnonymous - атрибут позволяет проигнорировать проверку пользователя на нахождение в белом списке.

Пример:

```csharp
using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Models.Enums;

namespace ConsoleExample.Examples
{
    internal class ExampleWhiteList
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если включен белый список и в нем есть пользователи, отработает только для них.
        /// </summary>
        [ReplyMenuHandler("OnlyWhiteList")]
        public static async Task OnlyWhiteList(IBotContext context)
        {
            string msg = nameof(OnlyWhiteList);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Если выставлена настройка <see cref="WhiteListSettings.OnlyCommands"></see> и есть люди в белом списке, этот метод будет работать для всех.
        /// </summary>
        [WhiteListAnonymous]
        [ReplyMenuHandler("AllUsers")]
        public static async Task AllUsers(IBotContext context)
        {
            string msg = nameof(AllUsers);
            await MessageSender.Send(context, msg);
        }
    }
}

```
