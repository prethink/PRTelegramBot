-- 2025.08.31 - V0.7.11
- Еще доработки по Di Scope для nextStep.

-- 2025.08.29 - V0.7.10
- Исправление Di Scope.
- Исправлена проблема при выполнение шагов, когда шаг ограничен по времени.
- В IExecuteStep добавлен метод CanExecute.

-- 2025.08.27 - V0.7.9.6
- В CacheExtension добавлен метод удаления ключа кеша через update. RemoveCacheData.

-- 2025.08.20 - V0.7.9.5
- Telegram.Bot: обновлен до 22.6.2

-- 2025.07.13 - V0.7.9.4
- Telegram.Bot: обновлен до 22.6.0

-- 2025.05.05 - V0.7.9.3
- В генератор меню Reply добавлен параметр OneTimeKeyboard
- Фиксы проверки флагов привилегий

-- 2025.02.18 - V0.7.9
- Telegram.Bot: обновлен до 22.4.3
- Рефакторинг методов в Messages, чтобы соотвествовали telegram.bot

-- 2025.02.13 - V0.7.8
- Telegram.Bot: обновлен до 22.4.0

-- 2025.01.04 - V0.7.7
- Telegram.Bot: обновлен до 22.3.0

-- 2024.12.25 - V0.7.6
- update: Добавлен inline обработчик для экземпляров классов. Позволяет назначить тип команды для определенного типа класса, который реализует интерфейс ICallbackQueryCommandHandler. Добавлен пример для консольного приложения и для asp.net di.
- refactoring: RegisterCommand из Options перенесен в CommandOptions.
- refactoring: Метод SplitIntoChunks перенесен в MessageUtils из класса Message.

-- 2024.12.05 - V0.7.5
- Telegram.Bot: обновлен до 22.2.0

-- 2024.11.19 - V0.7.4
- Telegram.Bot: обновлен до 22.1.0

-- 2024.11.10 - V0.7.3
- Telegram.Bot: обновлен до 22.0.2

-- 2024.08.01 - V0.7.2
- Telegram.Bot: обновлен до 21.8.0

-- 2024.07.27 - V0.7.1
- fix: Добавлена настройка для ограничения спама логов ошибок в случае если пропала сеть. TelegramOptions.AntiSpamErrorMinute значение по умолчанию 1 минута.

-- 2024.07.21 - V0.7
- update: Проект теперь позиционирует себя как framework.
- Telegram.Bot: обновлен до 21.7.1
- feature: Добавлена возможность встраиваться в обработку update типа message и callbackQuery. Позволяет реализовать и добавить собственные обработчики для текстовых и inline команд.
- feature: Теперь можно указывать в атрибутах команд несколько идентификаторов ботов. Раньше можно было только конкретного или все.
- refactoring: WebhookTelegramOptions переименован в WebHookOptions и теперь является часть класса TelegramOptions, а не наследником.
- refactoring: Добавлен новый тип событий CommandEvents. Туда перенесены все события связанные с командами.
- refactoring: Упростил работу с календарем.

-- 2024.07.18 - V0.6.6
- Telegram.Bot: обновлен до 21.7

-- 2024.07.14 - V0.6.5
- Telegram.Bot: обновлен до 21.6.2

-- 2024.07.07 - V0.6.4
- feature: InlineCallback теперь реализует интерфейс IDisposable. Если в данных будет указано ActionWithLastMessage delete сообщение автоматически удалиться.
- feature: Добавлен класс расширения для типа Message и методы AutoDeleteMessage, AutoEdit, AutoEditCycle.
- fix: В nuget пакете не отображались xml комментарии

-- 2024.07.06 - V0.6.3
- feature: Добавлены новые inline кнопки InlinePay InlineCallbackGame InlineSwitchInlineQuery InlineSwitchInlineQueryChosenChat InlineSwitchInlineQueryCurrentChat InlineLoginUrl.
- feature: Добавлены обертка InlineCallbackWithConfirmation для кнопок InlineCallBack. Позволяет вызвать сообщение подтверждения перед выполнением.
- feature: В TCommandBase и в наследников добавлено свойство ActionWithLastMessage, позволяет указать что делать с последним сообщением. Ничего, удалить, отредактировать.
- feature: Добавлено новое событие OnErrorCommand, если при выполнение команды произошла ошибка
- feature: В UpdateExtension добавлен метод GetChatIdClass который возвращает ChatId в формате класса
- fix: Если при обработке произошла ошибка, вызывалось событие missingCommand.

-- 2024.07.01 - V0.6.2
- update: Ядро telegram.bot обновлено с 21.2.0 до 21.4.0 версии.
- feature: В интерфейс IInternalCheck добавлен аргумент CommandHandler
- feature: Добавлены новые события в update типа сообщения. OnPreReplyCommandHandle, OnPostReplyCommandHandle, OnPreDynamicReplyCommandHandle, OnPostDynamicReplyCommandHandle,
    OnPreSlashCommandHandle, OnPostSlashCommandHandle, OnPreInlineCommandHandle, OnPostInlineCommandHandle, OnPreNextStepCommandHandle, OnPostNextStepCommandHandle
- feature: UpdateExtension добавлены методы IsUserChatId, TryGetChatId
- feature: Добавлен класс MessageAwaiter, позволяет создавать сообщение заглушку перед обработкой данных и автоматически удалять его после
- feature: Убраны await для команд reply, slash, inline, dynamicreply чтобы не задерживали обработку других update
- feature: Добавлен polling режим. Теперь есть classic (функционал telegram.bot), polling, webhook.

-- 2024.06.30 - V0.6.1
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

-- 2024.06.22 - V0.6
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

-- 2024.01.02 - V0.5.5
- feature: Добавлена возможность подставлять свой enum в common logs
- feature: Добавлен InlineCommandNotFoundException
- feature: Добавлена GroupUtils в котором есть метод IsGroupMember, IsGroupAdmin, IsGroupCreator
- feature: Заместо StepCommand теперь используется абстракция в виде интерфейса IExecuteStep
- refactoring: Рефакторинг Router
- refactoring: Типы для DI теперь создаются с жизненным циклом Transient а не Singleton
- refactoring: Класс Step переименован в StepService
- refactoring: Step.RegisterNextStep переименован в RegisterStepHandler
- fix: IsSlashCommand теперь проверяет первый символ /

-- 2023.12.24 - V0.5.4
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

-- 2023.12.18 - V0.5.3
- delete: Удален атрибут TelegramBotHandler
- fix: поправлен поиск и создание классов для обработчиков telegram бота
  
-- 2023.12.17 - V0.5.2
- fix: AddBotHandlers возвращает IServiceProvaider

-- 2023.12.17 - V0.5.1
- fix: Изменен url проекта на https://prtelegrambot.gitbook.io/prtelegrambot/obrabotka-komand/obrabotka-inline-komand

-- 2023.12.17 - V0.5.0
- feature: Добавлена динамическая регистрация команд reply и slash
- feature: Добавлена работа с dependency injection и пример на asp.net
