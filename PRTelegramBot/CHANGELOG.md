
-- 2023.12.24 - V0.5.4
- refatctoring: ReflectionUtils перенесен в пространство имен PRTelegramBot.Utils
- refatctoring: ReflectionHelper переименован в ReflectionUtils
- refatctoring: Calendar перенесен в пространство имен PRTelegramBot.Utils
- refatctoring: MenuGenerator перенесен в пространство имен PRTelegramBot.Utils
- refatctoring: Generator перенесен в пространство имен PRTelegramBot.Utils
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