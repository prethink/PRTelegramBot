# Консольный пример

[English](README.md) | **Русский**

Консольный бот, который проходит почти по всему функционалу фреймворка: команды всех видов, меню, события, middleware, фоновые задачи и вспомогательные утилиты.

Целевая платформа: **net7.0**

## Запуск

1. Получите токен бота у [@BotFather](https://t.me/BotFather).
2. Подставьте токен в `Program.cs`:
   ```csharp
   var telegram = new PRBotBuilder("token")
   ```
3. При желании добавьте свой Telegram id в `.AddAdmin(...)`, чтобы у вас работали примеры для администраторов.
4. Запустите проект и отправьте боту `/start`.

После старта консоль остаётся открытой — чтобы завершить работу, введите `exit`.

## Конфигурационные файлы

В `Configs/` лежат json-файлы, которые бот читает при запуске. Они подключаются через `.AddConfigPaths(Initializer.GetConfigPaths())`.

| Файл | Что хранит |
| --- | --- |
| `telegram.json` | Токен бота, администраторы, белый список, параметры запуска |
| `commands.json` | Тексты динамических reply-команд |
| `buttons.json` | Подписи кнопок |
| `messages.json` | Тексты сообщений |

Именно `commands.json` даёт работу динамическим командам: значение по ключу становится текстом-триггером, поэтому команду можно переименовать без перекомпиляции.

## Что демонстрируется

| Область | Где смотреть |
| --- | --- |
| Reply-команды, режимы сравнения, команды с параметрами | `Examples/Commands/ExampleReplyCommands.cs` |
| Slash-команды, аргументы, `/start` с deeplink | `Examples/Commands/ExampleSlashCommands.cs` |
| Inline-команды и генерация меню | `Examples/Commands/ExampleInlineCommands.cs` |
| Inline-кнопки с подтверждением | `Examples/Commands/ExampleInlineConfirmation.cs` |
| Пошаговое выполнение команд | `Examples/Commands/ExampleStepCommand.cs` |
| Билдеры клавиатур | `Examples/Builders/InlineKeyboard.cs` |
| Экземплярный inline-обработчик | `Examples/InlineClassHandlers/InlineDefaultClassHandler.cs` |
| События: update, сообщения, логи, привилегии | `Examples/Events/` |
| Middleware до и после update | `Middlewares/` |
| Фоновые задачи, с атрибутом и без | `BackgroundTasks/` |
| Собственные проверки перед выполнением команды | `Checkers/` |
| Свой атрибут для ограничения доступа | `Attributes/AdminOnlyExampleAttribute.cs` |
| Календарь | `Examples/ExampleCalendar.cs` |
| Постраничный вывод | `Examples/ExamplePage.cs` |
| Кэш пользователя | `Examples/ExampleUserCache.cs` |
| Белый список | `Examples/ExampleWhiteList.cs` |
| Проверка администратора | `Examples/ExampleAdminCheck.cs` |
| Автоудаление / авторедактирование, сообщения-заглушки | `Examples/ExampleUtils.cs` |
| Кнопка WebApp | `Examples/WebApp.html` |

Всё это регистрируется в `Services/Initializer.cs` — с него удобнее всего начинать чтение.

## На что обратить внимание

В `Program.cs` стоит `.SetInlineMenuConverter(new FileInlineConverter())`. Он хранит данные inline-кнопок в файлах вместо `callback_data` и тем самым обходит ограничение Telegram в 64 байта. Без него кнопка «Example with a long text» с большим текстом просто не поместилась бы.

---

Смотрите также: [основной README](../../README.ru.md) · [документация](https://prethink.gitbook.io/prtelegrambot/ru/)
