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
