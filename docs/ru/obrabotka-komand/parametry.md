# Параметры

## BotId

Идентификатор бота с которым будет работать команда, если выставить значение -1, будет работать со всеми ботами в проекте.

## \<T> Commands

Команды на которые реагирует бот.

Для ReplyMenuHandlerAttribute, ReplyMenuDynamicHandlerAttribute, SlashHandlerAttribute \<T> это string.

Для InlineCallbackHandlerAttribute  \<T> это enum.

## [CommandComparison](../api/perechisleniya-enum/commandcomparison.md)

Параметры сравнения команд.&#x20;

Позволяет настраивать работу ReplyMenuHandlerAttribute, ReplyMenuDynamicHandlerAttribute и SlashHandlerAttribute. Указывает как будет проверять текст команды. В тексте сообщения текст только этой команды, или команда содержится в тексте сообщения.

В ReplyMenuHandlerAttribute и ReplyMenuDynamicHandlerAttribute по умолчанию выставлено значение Equals. В SlashHandlerAttribute Contains.

## [StringComparison](../api/perechisleniya-enum/stringcomparison.md)

Параметры сравнения строки. Используется только для ReplyMenuHandlerAttribute ReplyMenuDynamicHandlerAttribute и SlashHandlerAttribute. В InlineCallbackHandlerAttribute не задействован.

По умолчанию выставлено значение OrdinalIgnoreCase.&#x20;

Более подробно можно почитать на MSDN:\
[https://learn.microsoft.com/ru-ru/dotnet/api/system.stringcomparison?view=net-8.0](https://learn.microsoft.com/ru-ru/dotnet/api/system.stringcomparison?view=net-8.0)\
[https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/best-practices-strings](https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/best-practices-strings)

## [OptionMessage](../api/klassy/optionmessage.md)

Вспомогательный класс который хранит настройки для отправки сообщений в telegram.
