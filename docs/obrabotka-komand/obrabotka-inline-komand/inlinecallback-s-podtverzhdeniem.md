# InlineCallback с подтверждением

## InlineInlineCallback с подтверждением и базовой обработкой результата нет

Для InlineCallback кнопок можно сделать обертку с помощью которой пользователь должен будет сначала подтвердить действие и только после этого оно будет выполнено.

Создаете InlineCallback кнопку, после чего оборачиваете ее с помощью [InlineCallbackWithConfirmation](../../api/klassy/inlinecallbackwithconfirmation.md).

Пример кода:

```csharp
/// <summary>
/// Команда отработает для бота с botId 0.
/// Команда отработает если пользователь напишет InlineConfirm.
/// </summary>
[ReplyMenuHandler("InlineConfirm")]
public static async Task InlineConfirm(IBotContext context)
{
    //Кнопка для которой нужно создать подтверждение.
    var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Кнопка с подтвержением", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
    //Обертка кнопки.
    var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Delete);

    //Создание нового меню.
    List<IInlineContent> menu = new() { exampleWithConfirmation } ;
    var testMenu = MenuGenerator.InlineKeyboard(1, menu);
    var option = new OptionMessage();

    //Передача меню в настройки
    option.MenuInlineKeyboardMarkup = testMenu;
    string msg = "InlineCallback с подтверждением";
    //Отправка сообщение с меню
    await MessageSender.Send(context, msg, option);
}
```

<figure><img src="../../.gitbook/assets/изображение (18).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (19).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (20).png" alt=""><figcaption></figcaption></figure>

Если на кнопку "нет " не делать обработку, она по умолчанию удалит текущее сообщение.

## InlineInlineCallback с подтверждением и кнопкой назад или кастомной обработкой кнопки нет

```csharp
/// <summary>
/// Команда отработает для бота с botId 0.
/// Команда отработает если пользователь напишет InlineConfirmWithBack.
/// </summary>
[ReplyMenuHandler("InlineConfirmWithBack")]
[InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleBack)]
public static async Task InlineConfirmWithBack(IBotContext context)
{
    //Кнопка для которой нужно создать подтверждение.
    var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Кнопка с подтвержением", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
    //Кнопка обработчик назад или кастомная.
    var exampleBack = new InlineCallback("Назад", CustomTHeaderTwo.ExampleBack);

    //Обертка кнопки.
    var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Edit, exampleBack);

    //Создание нового меню.
    List<IInlineContent> menu = new() { exampleWithConfirmation };
    var testMenu = MenuGenerator.InlineKeyboard(1, menu);
    var option = new OptionMessage();

    //Передача меню в настройки
    option.MenuInlineKeyboardMarkup = testMenu;
    string msg = "InlineCallback с подтверждением и обработкой кнопки назад или кастомной";
    //Отправка сообщение с меню
    if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        await MessageEditor.Edit(context, msg, option);
    else
        await MessageSender.Send(context, msg, option);
}
```

<figure><img src="../../.gitbook/assets/изображение (14).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (16).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (17).png" alt=""><figcaption></figcaption></figure>
