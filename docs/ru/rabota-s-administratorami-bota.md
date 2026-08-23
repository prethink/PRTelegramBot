# Работа с администраторами бота

Библиотека позволяет настраивать список администраторов. При создание бота можно добавлять администраторов с помощью идентификаторов пользователей.

```csharp
var telegram = new PRBotBuilder("")
                    .AddAdmin(1111111)
                    .AddAdmin(33333, 5555 ,6666 , 777)
                    .AddAdmins(new List<long>() { 222222, 33333 , 44444, 55555 })
                    .Build();
```

Проверить, что пользователь является администратором, можно через метод расширения **IsAdmin** для интерфейса ITelegramBotClient который используется при вывозе всех команд

```csharp
[ReplyMenuHandler("Admin menu")]
public static async Task AdminMenu(IBotContext context)
{
    if (await context.IsAdmin(context.GetChatId()))
    {
       //Пользователь админ что-то делаем
    }
}
```

Для работы с администраторами используется свойство AdminManager в [TelegramOptions](api/klassy/telegramoptions/). [AdminManager ](api/klassy/adminlistmanager.md)реализует интерфейс [IAdminManager](api/interfeisy/iadminmanager.md)**,** это значит, что в случае необходимости можно подставить свой класс, который например будет работать с базой данных. Для этого зарегистрируйте его через **DI** или воспользуйтесь методом SetAdminManager при создание бота.<br>

<figure><img src=".gitbook/assets/изображение (33).png" alt=""><figcaption></figcaption></figure>

