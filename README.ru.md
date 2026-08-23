<img alt="PRTelegramBot" src="LogoBot.png" width="96"/>

# PRTelegramBot

![Static Badge](https://img.shields.io/badge/version-v1.0.0-brightgreen) [![Static Badge](https://img.shields.io/badge/Telegram_Bot_API-10.2-blue)](https://core.telegram.org/bots/api) ![Static Badge](https://img.shields.io/badge/telegram.bot-22.10.2.1-blue) ![NuGet Downloads](https://img.shields.io/nuget/dt/prtelegrambot) ![NuGet Version](https://img.shields.io/nuget/v/prtelegrambot) [![License: MIT](https://img.shields.io/badge/license-MIT-green)](LICENSE)

[English](README.md) | **Русский**


> Если проект был вам полезен, вы можете поддержать его развитие на Boosty:
> https://boosty.to/prethink
> Звезда ⭐ репозиторию тоже будет отличной поддержкой.

[https://prethink.gitbook.io/prtelegrambot/](https://prethink.gitbook.io/prtelegrambot/) - актуальная документация.
[https://www.nuget.org/packages/PRTelegramBot/](https://www.nuget.org/packages/PRTelegramBot/) - nuget.
[https://t.me/prethinkdev](https://t.me/prethinkdev) - чат для вопросов.
[CHANGELOG.ru.md](CHANGELOG.ru.md) - история версий.

# 📰 О проекте

Фреймворк для создания Telegram-ботов на .NET поверх Telegram.Bot: роутинг команд через атрибуты, меню, middleware, DI и фоновые задачи.

Разрабатывается с 2023 года, сейчас — на **Bot API 10.2** через [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot). Обёртка не спрятана: все её методы и типы остаются вам доступны. Сверху фреймворк добавляет тот слой, который иначе пишется руками в каждом проекте заново — роутинг команд, меню, состояние между сообщениями, контроль доступа, конфигурация и фоновые задачи.

Обработчики — это обычные методы, помеченные атрибутом. Никакой таблицы регистрации, которую надо держать в актуальном состоянии: фреймворк находит их рефлексией при старте, поэтому добавить команду значит добавить метод.

```csharp
[SlashHandler("/start")]
public static async Task Start(IBotContext context)
{
    await MessageSender.Send(context, "Hello, World!");
}
```

Примеры с видео: [https://github.com/prethink/PRTelegramYoutube](https://github.com/prethink/PRTelegramYoutubeOld)

# 🚀 Быстрый старт

### Требования

Библиотека собрана под **.NET 6.0** и работает на любой более новой версии, так что достаточно установленного [.NET SDK](https://dotnet.microsoft.com/en-us/download). Ещё нужен токен бота от [BotFather](https://t.me/botfather) — как его получить, описано в [официальной инструкции](https://core.telegram.org/bots/tutorial#obtain-your-bot-token).

### Установка

Создайте консольное приложение и добавьте пакет:

```sh
dotnet new console -o MyBot
cd MyBot
dotnet add package PRTelegramBot
```

### Hello world

Содержимое `Program.cs`:

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

var bot = new PRBotBuilder("YOUR_BOT_TOKEN").Build();
await bot.StartAsync();

// Не даёт консольному приложению закрыться.
await Task.Delay(Timeout.Infinite);

public static class Commands
{
    // Срабатывает, когда пользователь отправляет /start.
    [SlashHandler("/start")]
    public static async Task Start(IBotContext context)
    {
        await MessageSender.Send(context, "Hello, World!");
    }

    // Срабатывает, когда текст сообщения точно равен "Ping", без учёта регистра.
    [ReplyMenuHandler("Ping")]
    public static async Task Ping(IBotContext context)
    {
        await MessageSender.Send(context, "Pong");
    }
}
```

Запустите через `dotnet run` и отправьте боту `/start`. Обратите внимание: на `Commands` нигде нет ссылки из `Program.cs` — фреймворк сам находит оба обработчика при старте.

По умолчанию бот получает обновления через [polling](https://core.telegram.org/bots/faq#how-do-i-get-updates): для него не нужен публичный адрес, и начать с него быстрее всего. Webhook настраивается тем же билдером.

> [!WARNING]
> Не храните токен бота в системе контроля версий. Используйте [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), переменные окружения или конфигурационный файл, исключённый из репозитория.

# 🧩 Примеры

| Пример | Что показывает |
| --- | --- |
| [Консольный](Examples/ConsoleExample/README.ru.md) | Почти весь фреймворк в одном месте: команды всех видов, меню, события, middleware, фоновые задачи. Начинать удобнее с него. |
| [ASP.NET](Examples/AspNetExample/README.ru.md) | Бот внутри ASP.NET Core, всё разрешается через dependency injection. Polling. |
| [ASP.NET webhook](Examples/AspNetWebHookExample/README.ru.md) | Два бота на одном webhook-endpoint, различаются по секретному токену. |

Есть также [шаблон для быстрого старта](Templates/FastBotTemplate) нового консольного бота.

# 💎 Функционал

### Команды и роутинг

 - **Работа с reply командами.** Поддержка простых текстовых команд.
 - **Работа с динамическими командами ответа.** Текстовые команды, загружаемые из конфигурационного файла без необходимости компиляции.
 - **Обработка команд с параметрами.** Возможность работы с командами, содержащими параметры в скобках, например, "Тест (1)".
 - **Работа с slash командами.** Обработка команд типа /get_1, /users и других текстовых команд, с настраиваемым символом-разделителем аргументов, типизированным доступом к ним через `context.GetSlashArgs<T>()` и поддержкой /start с deeplink.
 - **Гибкая работа с inline-командами.** Генератор и парсер inline-команд.
 - **Пошаговое выполнение команд.** Возможность выполнения пошаговых наборов reply-команд.
 - **Динамическое управление командами.** Возможность добавления и удаления команд в реальном времени с реализацией собственного регистратора команд.
 - **Проверки перед выполнением команд.** Внутренние проверки для команд reply, dynamicreply, nextstep, slash и inline.
 - **Создание собственных обработчиков для update типа message и callbackQuery.** Реализация своих обработчиков как reply, slash, inlineCallback.

### Меню, клавиатуры и сообщения

 - **Создание меню.** Простое и гибкое создание reply и inline меню.
 - **Билдеры клавиатур.** `ReplyKeyboardBuilder` и `InlineKeyboardBuilder` для удобного построения клавиатур: строки, столбцы, пустые кнопки-заполнители и кнопки запросов (контакт, локация, опрос, чат, пользователи, WebApp).
 - **Билдер сообщений.** `MessageBuilder` собирает текст по шаблону с позиционными аргументами и именованными токенами вида `{QA}`, в том числе с ленивым вычислением значений.
 - **Inline-подтверждения.** `InlineCallbackWithConfirmation` оборачивает кнопку так, что перед выполнением у пользователя запрашивается подтверждение.
 - **Постраничная работа с сообщениями.** Управление сообщениями с постраничной навигацией.
 - **Сообщения-заглушки.** `MessageAwaiter` отправляет сообщение на время обработки данных и удаляет его после.
 - **Встроенный функционал календаря.** Работа с датами и календарями.
 - **Работа с медиа.** `MediaSender` и `MediaEditor` для фото, групп фото, файлов и медиа по ссылке; `MessageCopier` для копирования сообщений.

### Запуск и инфраструктура

 - **Создание polling и webhook ботов.** Поддержка различных методов работы с ботами.
 - **Работа как hosted service.** Бот является `IHostedService` и встраивается напрямую в ASP.NET Core и Generic Host.
 - **Многоботная система.** Возможность создания нескольких ботов в одном проекте.
 - **Подключение к собственным серверам.** Работа ботов через собственные сервера.
 - **Сброс старых update.** Возможность сброса всех старых update перед запуском бота.
 - **Фоновые задачи.** Периодические задачи с метаданными, лимитами повторов и ошибок, с поддержкой DI.
 - **Работа с dependency injection.** Поддержка внедрения зависимостей.
 - **Scope выполнения.** `CurrentScope` даёт доступ к текущему боту, его контексту и сервисам в любом месте кода, вызванного обновлением Telegram.
 - **Логирование.** Работает с `ILogger` / `ILoggerFactory` — из билдера или из DI, со встроенным fallback.

### Пользователи и доступ

 - **Админ-менеджер.** Управление администраторами бота с возможностью реализации собственного админ-менеджера.
 - **Менеджер белого списка пользователей.** Гибкое управление белым списком с возможностью добавления методов, игнорируемых белым списком, и реализации собственного менеджера белого списка.
 - **Ограничение доступа к методам.** Возможность ограничения доступа к определенным методам.
 - **Хранение кэша пользователей.** Работа с пользовательским кэшем.
 - **Утилиты для групп.** `GroupUtils` проверяет, является ли пользователь участником, администратором или создателем группы.

### Расширяемость

 - **Система middleware.** Добавление собственных обработчиков до и после update, аналогично middleware в ASP.NET.
 - **Система событий.** Гибкая система обработки событий.
 - **Шина событий.** `PREventBus` и глобальные подписчики для рассылки событий по всему приложению.
 - **Обработка update.** Возможность реализации собственного обработчика update.
 - **Конвертеры inline-данных.** `IInlineMenuConverter` позволяет выбрать, как формируется `callback_data`; встроенный `FileInlineConverter` хранит данные в файлах и обходит ограничение Telegram в 64 байта.
 - **Сменные сериализаторы.** `JsonSerializerWrapper` или `ToonSerializerWrapper` для данных inline-кнопок — ToonNet даёт более компактный `callback_data`.
 - **Работа с конфигурационными файлами.** Поддержка конфигурационных файлов для каждого бота с возможностью реализации собственного провайдера конфигураций. По умолчанию используется JSON.
 - **Парсинг из конфигурационных файлов.** Парсинг сообщений, команд и кнопок из конфигурационных файлов.
 - **Функционал предоставляемый telegram.bot.**

# 🧱 Интегрированные пакеты
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker
 - ToonNet   https://www.nuget.org/packages/ToonNet

# 🛡️ Версионирование

Версия 1.0.0 — первый стабильный релиз. С этого момента публичный API следует [семантическому версионированию](https://semver.org/): ломающие изменения выходят только в мажорных версиях, новая функциональность — в минорных, исправления — в патчах. То, что планируется удалить, сначала помечается `[Obsolete]`, поэтому при обновлении вы получите предупреждение компилятора раньше, чем что-то сломается.

Каждый релиз описан в [changelog](CHANGELOG.ru.md), ломающие изменения идут первыми.

# 🤝 Участие в разработке и обратная связь

Pull request'ы с исправлениями, новой функциональностью и документацией приветствуются — для крупных изменений сначала заведите issue, чтобы обсудить решение до того, как работа будет сделана.

Если у вас вопрос по использованию фреймворка — спрашивайте в [Telegram-чате](https://t.me/prethinkdev). Про баги и пожелания заводите [issue на GitHub](https://github.com/prethink/PRTelegramBot/issues).

# 📄 Лицензия

Распространяется под [лицензией MIT](LICENSE).
