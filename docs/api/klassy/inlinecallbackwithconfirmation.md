---
description: Позволяет выполнить действие inlineCallback с подтверждением.
---

# InlineCallbackWithConfirmation

Конструкторы

```csharp
/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, InlineCallback noCallBack)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, InlineCallback noCallBack)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, InlineCallback noCallBack)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="noButton">Название кнопки действия нет.</param>
/// <param name="messageText">Текст сообщения.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string noButton, string messageText)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="noButton">Название кнопки действия нет.</param>
/// <param name="messageText">Текст сообщения.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string noButton, string messageText)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="noButton">Название кнопки действия нет.</param>
/// <param name="messageText">Текст сообщения.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string noButton, string messageText)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="messageText">Текст сообщения.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string messageText, InlineCallback noCallBack)

/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="messageText">Текст сообщения.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string messageText, InlineCallback noCallBack)


/// <summary>
/// Конструктор.
/// </summary>
/// <param name="inlineCallBack">InlineCallback кнопка.</param>
/// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
/// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
/// <param name="yesButton">Названия кнопки действия да.</param>
/// <param name="messageText">Текст сообщения.</param>
/// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string messageText, InlineCallback noCallBack)
```
