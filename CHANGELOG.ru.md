# Changelog

[English](CHANGELOG.md) | **Русский**


## 26.08.2026 - v1.1.0

> Поддержка [Bot API 10.3](https://core.telegram.org/bots/api#recent-changes). Telegram.Bot обновлён с 22.10.2.1 до 22.10.3.

### 🔄 Breaking changes

- `MessageUtils.CreateReplyParametersFromOptions` теперь возвращает `ReplyParameters?` и отдаёт `null`, когда в настройках не указано сообщение для ответа. В проектах с включённым nullable вызывающий код получит предупреждение, пока это не учтёт. Причина — в исправлении ниже.

### 🐞 Bugs

- Slash-команды в группе теряли аргументы. Telegram адресует команду боту, дописывая его username — нажатие на `/get_3` отправляет `/get_3@my_bot`, — и этот суффикс доживал до разбиения на аргументы. У бота с именем `cs2_server_bot` единственный аргумент `3` превращался в `3@cs2`, `server` и `bot`. Теперь упоминание срезается до того, как текст прочитают.
- Webhook-бот никогда не узнавал собственный username: `PRBotWebHook` пропускал вызов `getMe`, который `PRBotPolling` и `PRBot` делают при старте, поэтому `BotName` оставался `null` на всё время жизни бота. `BotCollection.GetBotOrNull` читает это свойство и бросал `NullReferenceException`, если в коллекции был webhook-бот. Теперь вызов делается и на webhook-пути, а поиск больше не считает, что имя обязательно есть, — `getMe` всё ещё может упасть.
- Фреймворк добавлял объект `reply_parameters` в каждое отправляемое сообщение, даже когда отвечать было не на что. Bot API требует, чтобы эти параметры указывали либо сообщение, либо эфемерное сообщение, и на обычных отправках Telegram молча игнорировал пустой объект. Игнорирует он его не везде: эфемерное сообщение, заменяющее то, чья кнопка была нажата, отклонялось с `MESSAGE_ID_INVALID`. Теперь при отсутствии цели ответа поле в запрос не попадает вовсе.

### 🚀 New functionality

- Добавлен `InlineDisabled` — кнопка, которую Telegram показывает серой и по которой не присылает callback. До этого недоступный пункт приходилось либо убирать из меню, отчего вёрстка прыгает, либо оставлять живым и отказывать уже после нажатия.
- Добавлено `OptionMessage.EphemeralMessageParameters`, пробрасывается через `MessageSender` и `MediaSender` (фото, файлы и медиа по URL). Эфемерное сообщение рисуется поверх чата для одного пользователя и не попадает в историю — бот может ответить одному человеку в группе так, чтобы остальные не читали.
- Добавлен `MessageSender.SendEphemeral`, который заполняет эти параметры из текущего update: получателя и — если update пришёл от нажатия кнопки — callback query, на который отвечает. Перегрузка принимает явный id пользователя, чтобы ответить не отправителю, а `replaceCallbackQueryMessage` показывает ответ вместо исходного сообщения, а не поверх него.
- Добавлено `OptionMessage.ReplyToEphemeralMessageId`, которое ложится в `ReplyParameters.EphemeralMessageId`. `SendEphemeral` заполняет его из входящего update — именно это позволяет боту без прав администратора продолжить разговор внутри эфемерного оверлея. Telegram разрешает эфемерное сообщение только в трёх случаях: в течение 15 секунд после callback query, в течение 15 секунд после входящего эфемерного сообщения, либо в любой момент, если бот администратор чата, — иначе отвечает `BOT_NOT_ADMIN`.
- Добавлен `MessageSender.SendRichMessage`. Принимать rich-сообщения фреймворк умел через `OnRichMessageHandle`, а отправлять — нет, поэтому приходилось спускаться к `ITelegramBotClient` и терять по дороге все настройки `OptionMessage`. Четыре перегрузки принимают либо HTML, либо собранный вручную `InputRichMessage`, для текущего чата или заданного, и мапят настройки ровно так же, как обычное сообщение: меню, тему, защиту контента, бизнес-подключение, эффект, платную рассылку, топик личных сообщений, предлагаемый пост и эфемерные параметры. Сами типы блоков не оборачиваются: `InputRichMessage` принимает HTML напрямую, и упрощать обёртке нечего.
- Добавлено событие `MessageEvents.OnCommunityChatJoinedHandle` — срабатывает, когда пользователь заходит в чат из сообщества.
- Добавлено событие `UpdateEvents.OnStoppedMessageGenerationHandle` — срабатывает, когда пользователь нажимает кнопку остановки на сообщении, которое бот стримит. В update приходит идентификатор черновика, так что стоящую за ним работу можно отменить.

### ♻️ Refactoring

- `SendRichMessageRequest` теперь под присмотром `RequestParameterCoverageTests`. Раньше его там не было, поэтому добавленный в него параметр прошёл бы незамеченным — ровно тот случай, ради которого guard-тесты и существуют.
- Telegram.Bot заменил параметры отправки `receiverUserId` и `callbackQueryId` одним объектом `EphemeralMessageParameters`. Фреймворк эти два наружу никогда не отдавал, поэтому пользователям библиотеки менять ничего не нужно.

## 23.08.2026 - v1.0.0

### 🔄 Breaking changes

- Удалено незавершённое пространство имён `PRTelegramBot.Workflow`: `IWorkflowNode`, `IWorkflowState`, `IWorkflowCondition`, `IWorkflowManulTask`, `TelegramStateManager` и остальные типы из него. Это были пустые заглушки без единого члена, нигде не использовались.
- Удалён интерфейс `IInlineStorage`. Он никогда не реализовывался и не использовался.
- Исправлены опечатки в именах параметров. Затрагивает только тех, кто передаёт эти аргументы по имени:
  - `StepTelegram.RegisterNextStep` и конструкторы `StepTelegram`: `expiriedTime` -> `expiredTime`
  - `PRBotBuilder.SetInlineSerializer`: `serializator` -> `serializer`
  - `BackgroundTaskExtension.GetMetadata`: `metadates` -> `existingMetadata`
- `OptionMessage.thumbnail` переименовано в `OptionMessage.Thumbnail`. Это был единственный публичный член, не соответствующий PascalCase.
- Необязательные параметры, принимающие `null`, объявлены nullable (`OptionMessage? option = null` и подобные). Это только метаданные — существующий код продолжает компилироваться, а проекты с включёнными проверками nullable просто получат честную картину.
- `UpdateExtension.TryGetBot` объявляет свой `out`-параметр как `PRBotBase?`, потому что при ненайденном боте там `null`.
- `GetChatId`, `GetMessageId` и `GetUserId` теперь бросают `InvalidOperationException` с понятным сообщением вместо `NullReferenceException`, когда в update нет чата, сообщения или отправителя. `TryGetChatId` в этих случаях по-прежнему возвращает `false`.
- Расщеплённые надвое пространства имён объединены. Каждое из них жило в одной папке, но файлы объявляли два разных namespace — из-за чего для сборки меню могло требоваться два `using` без всякой причины:
  - `PRTelegramBot.InlineButtons` -> `PRTelegramBot.Models.InlineButtons`
  - `PRTelegramBot.Core.Factory` -> `PRTelegramBot.Core.Factories`
  - `PRTelegramBot.Models.TCommands` -> `PRTelegramBot.Models.CallbackCommands` (папка переименована следом)
- Удалён устаревший фасад `PRTelegramBot.Helpers.Message` вместе с 21 методом. Он был помечен устаревшим в v0.9.0 и лишь перенаправлял вызовы в `MessageSender`, `MessageEditor`, `MessageDeleter`, `MessageCopier`, `MediaSender` и `MediaEditor` — используйте их напрямую.
- Удалён `PRTelegramBot.Models.InlineButton`. Он нигде не использовался, а его `GetContent` бросал `NotImplementedException`.
- `PRLoggerEvents<T>` и `PRLoggerEventsFactory` стали `internal`. Они остаются fallback'ом, который сохраняет работу логирования через события, когда `ILoggerFactory` не задан, но создавать их вручную не предполагалось.
- `InlineCallbackWithConfirmation.DataCollection` больше не публичное. Ожидающие подтверждения ищет сам фреймворк, а публичное поле лишь позволяло испортить это состояние снаружи.
- Два атрибута, выражающие одну и ту же идею, назывались по-разному и ставили слова в порядке, обратном типам Telegram.Bot, по которым фильтруют. Теперь они читаются одинаково и совпадают с `ChatType` и `MessageType`:
  - `RequiredTypeChatAttribute` -> `RequireChatTypeAttribute`, свойство `TypesChat` -> `ChatTypes`
  - `RequireTypeMessageAttribute` -> `RequireMessageTypeAttribute`, свойство `TypeMessages` -> `MessageTypes`
- `AccessAttribute`, `RequireChatTypeAttribute`, `RequireMessageTypeAttribute` и `WhiteListAnonymousAttribute` объявляют `[AttributeUsage(AttributeTargets.Method)]`. Без него их можно было повесить на класс, поле или параметр — туда фреймворк не смотрит, и об ошибке никто не сообщал.
- `AccessAttribute` и `WhiteListAnonymousAttribute` стали `sealed`, как и все остальные атрибуты библиотеки.

### 🚀 New functionality

- В `OptionMessage` добавлены параметры отправки, которые Telegram.Bot уже поддерживал, а фреймворк не пробрасывал: `BusinessConnectionId`, `MessageEffectId`, `AllowPaidBroadcast`, `DirectMessagesTopicId` и `SuggestedPostParameters`. Их передают `MessageSender`, `MediaSender` (фото, группы фото, файлы и медиа по ссылке) и `MessageCopier` — каждый в том объёме, в каком их принимает соответствующий метод Bot API.
- Добавлены события сообщений, пропущенные при обновлениях Telegram.Bot: `OnGiftUpgradeSentHandle`, `OnChatOwnerChangedHandle`, `OnChatOwnerLeftHandle`, `OnManagedBotCreatedHandle`, `OnPollOptionAddedHandle`, `OnPollOptionDeletedHandle`, `OnLivePhotoHandle`, `OnRichMessageHandle`, `OnCommunityChatAddedHandle` и `OnCommunityChatRemovedHandle`. Диспетчер сообщений теперь покрывает все `MessageType`, кроме `Text` — он всегда шёл через конвейер команд.
- Добавлены события update, пропущенные по той же причине: `OnManagedBotHandle`, `OnGuestMessageHandle` и `OnSubscriptionHandle`.
- Добавлено `OptionMessage.ShowCaptionAboveMedia`, пробрасывается в `MediaSender.SendPhoto`, `MessageCopier` и `MediaEditor.EditCaption`.
- Добавлена `InlineCopyText` — кнопка, копирующая заданный текст в буфер обмена. Telegram.Bot поддерживал её давно, а обёртки во фреймворке не было.
- Добавлен `ReplyKeyboardBuilder.AddRequestManagedBot` — кнопка, предлагающая пользователю создать и передать бота под управление текущего.

### 🧩 Common

- Telegram.Bot обновлён до 22.10.2.1
- Комментарии в коде переведены на английский.
- Добавлены английские версии README и CHANGELOG; русские лежат рядом как `README.ru.md` и `CHANGELOG.ru.md`.
- README перестроен под читателя, который приходит, ничего не зная о проекте: он начинается с того, что фреймворк добавляет поверх Telegram.Bot, и содержит раздел быстрого старта с требованиями, установкой и hello world, который компилируется как есть. Список функционала сгруппирован по темам вместо плоского перечня из сорока пунктов, добавлены разделы про версионирование, участие в разработке и лицензию, а также badge с поддерживаемой версией Bot API.
- Описание пакета теперь `A .NET framework for building Telegram bots on top of Telegram.Bot: attribute-based command routing, menus, middleware, DI and background tasks`. Прежнее нигде не называло платформу, а это первое, что нужно знать человеку, увидевшему пакет в результатах поиска. Именно этот текст показывает NuGet в выдаче; той же формулировкой открывается раздел «О проекте» в обоих README.
- Задокументированы все публичные члены: пробелов в XML-документации больше нет, повреждённые док-комментарии починены. IntelliSense полный.
- `PageExtension.GetPaged` больше не помечен `async` — он ничего не ожидал. Для вызывающего кода сигнатура не изменилась.
- Сеттеры `RunningBackgroundTask` и `SlashHandlerAttribute.SplitChar` изменены с `protected` на `private`. Оба класса `sealed`, поэтому снаружи эти сеттеры и так были недоступны.
- `InlineUtils.GetInlineButton` больше не разбирает конкретные типы кнопок через switch, а вызывает `GetInlineButton()` у самой кнопки. Встроенные кнопки конвертируются ровно как раньше, но тип, которого в switch не было — добавленный позже или объявленный вне библиотеки, — теперь конвертируется, а не падает с `NotImplementedException`; переопределённая в наследнике конвертация тоже учитывается. Кроме того, метод бросает `ArgumentNullException`, если ему передали null. Это касается всего, что строит inline-клавиатуры: `InlineKeyboardBuilder`, `MenuGenerator` и календаря.

### 🐞 Bugs

- Поправил проблему с рекурсией при проверке администратора через context.
- `AutoEditMessageСycle` переименован в `AutoEditMessageCycle`: в старом имени была кириллическая «С».
- `UpdateExtension.GetUserId` возвращал неверный идентификатор для callbackQuery: читался `CallbackQuery.Message.From` — это бот, отправивший сообщение, — вместо `CallbackQuery.From`, то есть нажавшего кнопку пользователя. Всё, что завязано на пользователя — кэш, шаги, проверки доступа, — получало идентификатор бота для каждого пользователя.
- `UpdateExtension.GetUserId` бросал `NullReferenceException` для постов в канале, у которых `From` всегда пуст.
- Ожидающие подтверждения, создаваемые `InlineCallbackWithConfirmation`, хранились вечно: каждая построенная кнопка добавлялась в статический словарь, из которого ничего никогда не удалялось, — долгоживущий бот утекал ими вместе с вложенными callback'ами. Теперь запись удаляется, как только подтверждение отвечено, а неотвеченные отбрасываются через час после создания.
- Тот же словарь был обычным `Dictionary`, в который писали из параллельно обрабатываемых update, что может его испортить. Заменён на `ConcurrentDictionary`.
- `MessageUtils.SplitIntoChunks` зацикливался навсегда при размере блока ноль или меньше: смещение не росло, вызов подвисал, а список результатов рос до исчерпания памяти. Теперь метод бросает `ArgumentOutOfRangeException` при неположительном размере блока и `ArgumentNullException` при пустом тексте. Сам фреймворк всегда передаёт `PRConstants.MAX_MESSAGE_LENGTH`, поэтому под ударом были только прямые вызовы этого публичного метода.
- Событие `OnPaidMessagePriceChangedHandle` было объявлено, но не подключено к диспетчеру сообщений и потому никогда не срабатывало. Теперь подключено.
- Исключения больше не проглатываются молча. Теперь они пишутся в лог в `PREventBus` (сбойный подписчик больше не исчезает бесследно), в `MessageAwaiter` при неудачном удалении сообщения-заглушки и в `TryGetConfigValue` при ошибке чтения конфигурации.
- Обработчики событий по-прежнему вызываются без `await`, чтобы медленный подписчик не задерживал остальные update, — но сбой внутри такого обработчика теперь логируется, а не теряется вместе с необслуженной задачей.
- `FileInlineConverter(string path)` игнорировал переданное имя папки и всегда использовал папку с буквальным именем `path`. Теперь используется переданное имя, а пустое отвергается. Кто пользовался этим конструктором, писал не в ту папку; после обновления данные inline-кнопок переедут в запрошенную папку, поэтому подтверждения, ожидавшие ответа на момент обновления, найдены не будут.
- `InlineConfirmation.ActionWithConfirmation` обращался к разобранной команде без проверки. Если callback_data разобрать не удавалось — например, когда обработчик читает не тот `EntityTCommand<T>`, который несёт кнопка, — конвертер возвращал null, а обработчик писал в лог `NullReferenceException` вместо того, чтобы что-то сказать пользователю. Теперь это приводит к тому же сообщению «что-то пошло не так», что и неизвестное подтверждение.
- Из 32 файлов исходников убран продублированный BOM. Он появился из-за инструмента, которым переводились комментарии; файлы компилировались, но каждый начинался с лишнего символа нулевой ширины.

## 20.06.2026 - v0.9.10

### 🧩 Common

- Обновлены Microsoft.Extensions пакеты до версии 9.0.17:
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.Hosting.Abstractions
- Microsoft.Extensions.Logging.Abstractions
- Telegram.Bot обновлён до 22.10.1

## 26.04.2026 - v0.9.9

### 🧩 Common
- Telegram.Bot: обновлен до 22.9.6.1

### 📝 Background Tasks
- Поправил проблему с запуском фоновых задач.

## 02.03.2026 - v0.9.8

### 🧩 Common
- Telegram.Bot: обновлен до 22.9.5

## 10.02.2026 - v0.9.7

### 🧩 Common
- Telegram.Bot: обновлен до 22.9.0

## 02.02.2026 - v0.9.6

### 🧩 Common
- Поправил баги в FileInlineConverter. Теперь корректно формируется билдер inline кнопок.

## 03.01.2026 - v0.9.5

### 🧩 Common
- В PRBotBase добавлен метод SetServiceProvider.
- Если в DI добавлен ILogger, а не ILoggerFactory, бот попытается его использовать для логирования.

#### Приоритет источников логера:   
1. Фабрика логеров, заданная в билдере   
2. ILogger из DI   
3. ILoggerFactory из DI   
4. Встроенная фабрика логеров (fallback)   

## 03.01.2026 - v0.9.4

### 🧩 Common
- Telegram.Bot: обновлен до 22.8.1

## 02.01.2026 - v0.9.3

### 🧩 Common
- Telegram.Bot: обновлен до 22.8.0

## 28.12.2025 - v0.9.2

### 🧩 Common
- Библиотека `Microsoft.Extensions.Configuration` обновлена до версии 9.0.11
- Библиотека `Microsoft.Extensions.Configuration.Json` обновлена до версии 9.0.11
- Библиотека `Microsoft.Extensions.Configuration.Json` обновлена до версии 9.0.11
- Библиотека `Microsoft.Extensions.Hosting.Abstractions` обновлена до версии 9.0.11
- Добавлена библиотека `Microsoft.Extensions.Logging.Abstractions` версии 9.0.11

### 🧾 Logger
- Добавлена поддержка `ILogger` и `ILoggerFactory`.
- Через `PRBotBuilder` можно указать собственную фабрику логеров `ILoggerFactory`, которая будет использоваться для создания `ILogger`.
- Также поддерживается получение `ILoggerFactory` из DI-контейнера.
- Если ни один из вариантов не задан, используется встроенная (дефолтная) фабрика логеров, которая обеспечивает обратную совместимость со старым механизмом.   
#### Приоритет источников логера:   
1. Фабрика логеров, заданная в билдере   
2. ILoggerFactory из DI   
3. Встроенная фабрика логеров (fallback)   


## 23.12.2025 - v0.9.1

### 🔄 Breaking changes
- В `MiddlewareBase` стал абстрактным классом.
- В `MiddlewareBase` добавлено новое свойство `ExecutionOrder`. Определяет порядок выполнения.

### Common
- В PRConstants добавлена константа ALL_BOTS_ID = -1. Данный идентификатор используется, когда следует применить команду для всех ботов. Может относиться не только к командам.
- Мелкий рефакторинг
- Добавлен модуль фоновых задач. Фоновые задачи поддерживают DI
- В `MiddlewareBase` добавлена поддержка DI.
- Добавлена поддержка шины событий.


## 13.12.2025 - v0.9.0

### 🔄 Breaking changes
- `PRBotBuilder` перенесён из `PRTelegramBot.Core` в `PRTelegramBot.Builders`
- Метод `Message.NotifyFromCallBack` перенесён в `IBotContext`

### 🧱 Builders
- Добавлен builder reply-кнопок — `ReplyKeyboardBuilder`
- Добавлен builder inline-кнопок — `InlineKeyboardBuilder`

### ♻️ Refactoring
- Проведён рефакторинг класса `Message`  
  Класс разделён на отдельные компоненты:
  - `MessageSender`
  - `MessageEditor`
  - `MessageDeleter`
  - `MessageNotification`
  - `MessageCopier`
  - `MediaEditor`
  - `MediaSender`

### 📋 Inline меню/конвертация
- Добавлен интерфейс `IInlineMenuConverter` для конвертации данных для inline меню.    
- В Builder бота теперь можно указать свою реализацию конвертации меню `.SetInlineMenuConverter(IInlineMenuConverter inlineMenuConverter)`     
- Добавлен класс `FileInlineConverter`, реализующий `IInlineMenuConverter` для конвертации данных в inline меню с использованием файловой системы для обхода ограничения размера `callback_data`.   

### 🧱 Builders

### 🧭 Контекст выполнения 
- Добавлен BotContextScope, обеспечивающий доступ к текущему экземпляру бота и контексту в рамках обработки обновления.   
Теперь можно легко получить их в любом месте кода, если этот код был вызван обновлением telegram:   
`var currentContext = CurrentScope.Context;    
var currentBot = CurrentScope.Bot;    
var services = CurrentScope.Services (IServiceProvider);`    

### 📡 События
- Добавлены события для `updateType`:  
  - `PurchasedPaidMedia`
  - `BusinessMessage`
- Добавлены события для `messageType`: 
  - `PaidMedia`
  - `RefundedPayment`
  - `Gift`
  - `UniqueGift`
  - `PaidMessagePriceChanged`
  - `Checklist`
  - `ChecklistTasksDone`
  - `ChecklistTasksAdded`
  - `DirectMessagePriceChanged`
  - `SuggestedPostApproved`
  - `SuggestedPostApprovalFailed`
  - `SuggestedPostDeclined`
  - `SuggestedPostPaid`
  - `SuggestedPostRefunded`

### 🏗 Инициализация бота
- В билдер добавлена возможность указать Action инициализации бота. `SetInitializeAction(Action action)`. Данный Action будет вызван при старте бота после инициализации всех менеджеров.

### 👮 Менеджеры и интерфейсы
- `AdminManager` теперь реализовывает интерфейс `IAdminManager`.
- В интерфейсы IUserManager, IWhiteListManager, IAdminManager добавлен метод Initialize().

### 💉 Интеграция с DI
- Интерфейсы IInlineMenuConverter, IPRSerializer, IAdminManager, IWhiteListManager должны подружиться с DI. 
Если вы используете DI контейнер, то зарегистрируйте их там и боты сами подтянут о них информацию в AdminManager, WhiteListManager.
Приоритетность использования ботом данных интерфейсов работает в следующем порядке.
1. Через установку билдера SetAdminManager, SetWhiteListManager, SetInlineMenuConverter, SetPRSerializer
2. Через DI
3. Локальные/дефолтные классы.

## 08.12.2025 - V0.8.6
- Telegram.Bot: обновлен до 22.7.6

## 04.12.2025 - V0.8.5
- В атрибут SlashHandlerAttribute добавлена возможность указать символ разделителя для аргументов. Пример [SlashHandler('_', "/get")]
- Добавлена возможность при выполнение slash команд получить список аргументов из контекста. 
var args = context.GetSlashArgs();
var args = context.GetSlashArgs<int>();
var args = context.GetSlashArgs<bool>();
- /start с deeplink теперь можно использовать в своих slash методах, а не как раньше, только через события.

## 29.11.2025 - V0.8.4
- В билдере теперь есть возможность указать каким сериализатором пользоваться (SetInlineSerializer) для Inline кнопок. JsonSerializerWrapper или ToonSerializerWrapper. ToonSerializerWrapper использует меньше байт в callback_data.
- При создание экземпляра сериализатора можно устанавливать параметры сериализации.
- Добавлен класс глобальных настроек проекта PRSettingsProvider.
- Добавлена библиотека ToonNet.
- Добавлена Microsoft.Extensions.Hosting.Abstractions для возможности использования бота как IHostedService сервиса.

## 09.11.2025 - V0.8.3
- Telegram.Bot: обновлен до 22.7.5

## 31.10.2025 - V0.8.2
- Telegram.Bot: обновлен до 22.7.4

## 27.10.2025 - V0.8.1
- Telegram.Bot: обновлен до 22.7.3
- Рефакторинг метода GetFullNameFromChat

## 15.09.2025 - V0.8
- Рефакторинг кода. Так же спасибо за помощь @Harlok13.
- Добавлен IBotContext который хранит в себе: Всех экземпляры ботов системы. Текущий экземпляр бота. Update. BotClient, CurrentUpdateType, CancelationToken.
- Сигнатура методов, команд ...ITelegramBotClient botClient, Update update... заменена на IBotContext context
- Добавлены методы расширений для IBotContext по аналогии с update. Cache, Steps и другие.
- CacheExtension. 
-- Добавлен метод GetOrCreate.
-- Поправлен метод CreateCacheData. Теперь при его вызове будет всегда создаваться новый кэш.
- Поправлены примеры ботов.
- Добавлен новый метод расширения для получения идентификатора пользователя GetUserId()
- Документация будет обновлена позже, после слияния с мастером.


### Миграции:
#### MiddlewareBase:
- InvokeOnPreUpdateAsync(ITelegramBotClient context.BotClient, context.Update update, Func<Task> next) -> InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
- InvokeOnPostUpdateAsync(ITelegramBotClient context.BotClient, context.Update update, Func<Task> next) -> InvokeOnPostUpdateAsync(IBotContext context)

#### IExecuteStep и его реализации:
ExecuteStep(ITelegramBotClient context.BotClient, context.Update update) -> ExecuteStep(IBotContext context)

#### PRBotBuilder
- SetIpAddresWebHook(string ipAddress) -> SetIpAddressWebHook(string ipAddress)
- AddRecevingOptions(ReceiverOptions recevierOptions) -> AddReceivingOptions(ReceiverOptions receiverOptions) 

#### PRBotWebHook
- GetWebHookInfo(CancellationToken cancellationToken = default) -> GetWebHookInfoAsync(CancellationToken cancellationToken = default)

#### PRBotBase
- Start -> StartAsync
- Stop -> StopAsync

Методы в вашем коде нужно привести к сигнатуре от (ITelegramBotClient context.BotClient, context.Update update) к (IBotContext context) и поправить другие места в коде куда передавались или брались старые аргументы аргументы.
Примеры:
update -> context.Update
botClient -> context.BotClient


## 04.09.2025 - V0.7.12
- Исправлена проверка размера callback_data. Автор @Harlok13
- Telegram.Bot: обновлен до 22.7.2

## 31.08.2025 - V0.7.11
- Еще доработки по Di Scope для nextStep.

## 29.08.2025 - V0.7.10
- Исправление Di Scope.
- Исправлена проблема при выполнение шагов, когда шаг ограничен по времени.
- В IExecuteStep добавлен метод CanExecute.

## 27.08.2025 - V0.7.9.6
- В CacheExtension добавлен метод удаления ключа кеша через update. RemoveCacheData.

## 20.08.2025 - V0.7.9.5
- Telegram.Bot: обновлен до 22.6.2

## 13.07.2025 - V0.7.9.4
- Telegram.Bot: обновлен до 22.6.0

## 05.05.2025 - V0.7.9.3
- В генератор меню Reply добавлен параметр OneTimeKeyboard
- Фиксы проверки флагов привилегий

## 18.02.2025 - V0.7.9
- Telegram.Bot: обновлен до 22.4.3
- Рефакторинг методов в Messages, чтобы соотвествовали telegram.bot

## 13.02.2025 - V0.7.8
- Telegram.Bot: обновлен до 22.4.0

## 04.01.2025 - V0.7.7
- Telegram.Bot: обновлен до 22.3.0

## 25.12.2024 - V0.7.6
- update: Добавлен inline обработчик для экземпляров классов. Позволяет назначить тип команды для определенного типа класса, который реализует интерфейс ICallbackQueryCommandHandler. Добавлен пример для консольного приложения и для asp.net di.
- refactoring: RegisterCommand из Options перенесен в CommandOptions.
- refactoring: Метод SplitIntoChunks перенесен в MessageUtils из класса Message.

## 05.12.2024 - V0.7.5
- Telegram.Bot: обновлен до 22.2.0

## 19.11.2024 - V0.7.4
- Telegram.Bot: обновлен до 22.1.0

## 10.11.2024 - V0.7.3
- Telegram.Bot: обновлен до 22.0.2

## 01.08.2024 - V0.7.2
- Telegram.Bot: обновлен до 21.8.0

## 27.07.2024 - V0.7.1
- fix: Добавлена настройка для ограничения спама логов ошибок в случае если пропала сеть. TelegramOptions.AntiSpamErrorMinute значение по умолчанию 1 минута.

## 21.07.2024 - V0.7
- update: Проект теперь позиционирует себя как framework.
- Telegram.Bot: обновлен до 21.7.1
- feature: Добавлена возможность встраиваться в обработку update типа message и callbackQuery. Позволяет реализовать и добавить собственные обработчики для текстовых и inline команд.
- feature: Теперь можно указывать в атрибутах команд несколько идентификаторов ботов. Раньше можно было только конкретного или все.
- refactoring: WebhookTelegramOptions переименован в WebHookOptions и теперь является часть класса TelegramOptions, а не наследником.
- refactoring: Добавлен новый тип событий CommandEvents. Туда перенесены все события связанные с командами.
- refactoring: Упростил работу с календарем.

## 18.07.2024 - V0.6.6
- Telegram.Bot: обновлен до 21.7

## 14.07.2024 - V0.6.5
- Telegram.Bot: обновлен до 21.6.2

## 07.07.2024 - V0.6.4
- feature: InlineCallback теперь реализует интерфейс IDisposable. Если в данных будет указано ActionWithLastMessage delete сообщение автоматически удалиться.
- feature: Добавлен класс расширения для типа Message и методы AutoDeleteMessage, AutoEdit, AutoEditCycle.
- fix: В nuget пакете не отображались xml комментарии

## 06.07.2024 - V0.6.3
- feature: Добавлены новые inline кнопки InlinePay InlineCallbackGame InlineSwitchInlineQuery InlineSwitchInlineQueryChosenChat InlineSwitchInlineQueryCurrentChat InlineLoginUrl.
- feature: Добавлены обертка InlineCallbackWithConfirmation для кнопок InlineCallBack. Позволяет вызвать сообщение подтверждения перед выполнением.
- feature: В TCommandBase и в наследников добавлено свойство ActionWithLastMessage, позволяет указать что делать с последним сообщением. Ничего, удалить, отредактировать.
- feature: Добавлено новое событие OnErrorCommand, если при выполнение команды произошла ошибка
- feature: В UpdateExtension добавлен метод GetChatIdClass который возвращает ChatId в формате класса
- fix: Если при обработке произошла ошибка, вызывалось событие missingCommand.

## 01.07.2024 - V0.6.2
- update: Ядро telegram.bot обновлено с 21.2.0 до 21.4.0 версии.
- feature: В интерфейс IInternalCheck добавлен аргумент CommandHandler
- feature: Добавлены новые события в update типа сообщения. OnPreReplyCommandHandle, OnPostReplyCommandHandle, OnPreDynamicReplyCommandHandle, OnPostDynamicReplyCommandHandle,
    OnPreSlashCommandHandle, OnPostSlashCommandHandle, OnPreInlineCommandHandle, OnPostInlineCommandHandle, OnPreNextStepCommandHandle, OnPostNextStepCommandHandle
- feature: UpdateExtension добавлены методы IsUserChatId, TryGetChatId
- feature: Добавлен класс MessageAwaiter, позволяет создавать сообщение заглушку перед обработкой данных и автоматически удалять его после
- feature: Убраны await для команд reply, slash, inline, dynamicreply чтобы не задерживали обработку других update
- feature: Добавлен polling режим. Теперь есть classic (функционал telegram.bot), polling, webhook.

## 30.06.2024 - V0.6.1
- update: Ядро telegram.bot обновлено с 19 до 21.2.0 версии.
- update: Из-за обновления убран newtonsoft json
- update: Добавлены новые события для сообщений Giveaway, GiveawayWinners, GiveawayCompleted, BoostAdded, ChatBackgroundSet
- feature: Добавлен интерфейс IUserManager и классы AdminManager, WhiteListManager. Из TelegramOptions удалены свойства Admins и WhiteListUsers.
- feature: Добавлена middleware система перед обработкой и после update
- feature: Добавлен атрибут WhiteListAnonymous, если данный атрибут присуствует на методе обработки, он будет выполнен для всех пользователей, даже если они на находятся в белом списке
- feature: Для WhiteListManager добавлены настройки как должен работать белый список
- feature: Возможность добавить свои проверки перед выполнением конкретных команд reply, dynamicreply, nextstep, inline, slash.
- refactoring: Для правильного создания ботов теперь используется только PRBotBuilder.
- refactoring: TEvents события которые относятся к сообщениям перенесены в класс MessageEvents
- refactoring: TEvents события которые относятся к обновлениям перенесены в класс UpdateEvents
- refactoring: В билдере AddAdmin и AddWhiteListUser заменен параметр long на params long[]

## 22.06.2024 - V0.6
- update: Обновлена библиотека Microsoft.Extensions.Configuration.Binder до 8 версии
- update: Обновлена библиотека Microsoft.Extensions.Configuration.Json до 8 версии
- test: Unit тесты
- feature: Добавлена возможность подставлять при создание бота свой обработчик обновлений
- feature: Добавлена возможность подставлять при создание бота свой регистратор команд
- feature: Добавлена возможность работать с webhook
- feature: Добавлен атрибут BotHandler для определения класса работы с dependency injection
- feature: AccessUtil для работы с флагами доступа и масками
- feature: PRBotBuilder добавлен класс, который позволяет создавать бота через Fluent Builder
- feature: Добавлен класс BotCollection, который хранит в себе все экземпляры ботов
- feature: Добавлена возможность указать в атрибутах команд идентификатор бота -1, эти методы будут доступны из всех ботов
- feature: OptionMessage добавлены свойства из Telegram.Bot.Net
- feature: Добавлена возможность в командах указывать параметры сравнения
- feature: Добавлены события всех других типов update
- feature: При пошаговом выполнение команд добавлена возможность игнорировать обычные (приоритетные) команды
- feature: В интерфейс IExecuteStep добавленные свойства для игнорирования основных команд и указания последнего шага
- feature: При создание бота можно выставить свой клиент. Позволяет использовать собственные локальные сервера, а не сервера telegram
- refactoring: StepService переименован в StepExtension
- refactoring: Descriptions переименован в DescriptionExtension
- refactoring: Cache переименован в CacheExtension
- refactoring: PageHelper переименован в PageExtension
- refactoring: THeader переименован в PRTelegramBotCommand
- refactoring: В TelegramOptions добавлен параметр configPath
- refactoring: TelegramConfig заменен TelegramOptions
- refactoring: Рефакторинг ServiceProviderExtension
- refactoring: Удален класс TextConfig
- refactoring: Удален enum BaseEventTelegram
- refactoring: В логирование заменен Enum на string
- refactoring: Переработаны события, добавлены отдельные классы для аргументов.
- refactoring: Все события перенесены в свойство Events. bot.Events
- refactoring: Рефакторинг Router, разделен на несколько классов
- refactoring: Рефакторинг
- fix: Исправлены проблемы с командой /start
- fix: Кэш и шаги теперь связаны с конкретным ботом и пользователем
- fix: Для сообщений добавлены все события

## 02.01.2024 - V0.5.5
- feature: Добавлена возможность подставлять свой enum в common logs
- feature: Добавлен InlineCommandNotFoundException
- feature: Добавлена GroupUtils в котором есть метод IsGroupMember, IsGroupAdmin, IsGroupCreator
- feature: Заместо StepCommand теперь используется абстракция в виде интерфейса IExecuteStep
- refactoring: Рефакторинг Router
- refactoring: Типы для DI теперь создаются с жизненным циклом Transient а не Singleton
- refactoring: Класс Step переименован в StepService
- refactoring: Step.RegisterNextStep переименован в RegisterStepHandler
- fix: IsSlashCommand теперь проверяет первый символ /

## 24.12.2023 - V0.5.4
- refactoring: ReflectionUtils перенесен в пространство имен PRTelegramBot.Utils
- refactoring: ReflectionHelper переименован в ReflectionUtils
- refactoring: Calendar перенесен в пространство имен PRTelegramBot.Utils
- refactoring: MenuGenerator перенесен в пространство имен PRTelegramBot.Utils
- refactoring: Generator перенесен в пространство имен PRTelegramBot.Utils
- feature: botClient позволяет вызывать методы простых и ошибочных логов.
- feature: Возможность добавлять/удалять reply и slash команды через экземпляр класса PRBot
- feature: botClient.GetBotAdminIds() возвращает администраторов бота
- feature: Добавлена динамическая регистрация команд inline
- fix: Метод SendPhoto не отправлял сообщения если optionmessage был не пустой
- fix: Enum записывает правильные значения из int

## 18.12.2023 - V0.5.3
- delete: Удален атрибут TelegramBotHandler
- fix: поправлен поиск и создание классов для обработчиков telegram бота
  
## 17.12.2023 - V0.5.2
- fix: AddBotHandlers возвращает IServiceProvaider

## 17.12.2023 - V0.5.1
- fix: Изменен url проекта на https://prtelegrambot.gitbook.io/prtelegrambot/obrabotka-komand/obrabotka-inline-komand

## 17.12.2023 - V0.5
- feature: Добавлена динамическая регистрация команд reply и slash
- feature: Добавлена работа с dependency injection и пример на asp.net
