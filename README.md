# 🤖 Текущая версия: 0.6.1
<p align="center">
  <strong>Если данный проект был для вас полезен и хотите его поддержать, поставьте ⭐</strong>
</p>

[https://prtelegrambot.gitbook.io/prtelegrambot/](https://prtelegrambot.gitbook.io/prtelegrambot/)  - актуальная документация.        
[https://www.nuget.org/packages/PRTelegramBot/](https://www.nuget.org/packages/PRTelegramBot/) - nuget.     
[https://t.me/predevchat](https://t.me/predevchat) - чат для вопросов.        
# ⚛️ Ядро библиотеки
TelegramBot v21.2.0 https://github.com/TelegramBots/Telegram.Bot

# 📰 Описание
Библиотека с простой маршрутизацией команд для создания telegram ботов.      
Пример использования в консольном приложении. https://github.com/prethink/PRTelegramBot/tree/master/ConsoleExample

# 💎 Возможности

- Работа с reply командами. Простые текстовые команды.
- Работа с dynamicReply командами. Текстовые команды которые подхватываются из конфигурационного файла без компиляции.
- Для reply и dynamicreply возможность работать с командами у которых в конце есть скобки, например "Тест (1)"
- Работа с slash командами. /get_1 /users и другие текстовые.
- Гибкая и простая работа с inline командами. Генератор и парсер inline команд.
- Гибкое и простое создание меню reply и inline.
- Работа с конфигурационными файлами для каждого бота. Возможность реализовать свой провайдер работы с конфигурационными файлами. По умолчанию работает с json.
- Админ менеджер для управления администраторами в боте. Возможность реализовать свой админ менеджер.
- Гибкий менеджер белого списка пользователей. Возможность добавить методы которые будут игнорироваться белым списком. Возможность реализовать свой менеджер белого списка.
- Гибкая обработка update. Возможность реализовать свой обработчик update.
- Гибкая система событий.
- Возможность создания несколько ботов в 1 проекте.
- Система middleware, возможность добавить свои обработчики до и после update. Система схожа с middleware из asp.net.
- Возможность добавления внутренних проверок перед выполнением команд reply, dynamicreply, slash, inline.
- Динамическое добавление и удаление команд. Возможность реализовать свой регистратор команд.
- Возможность сбросить все старые обновления перед запуском бота.
- Возможность выполнить пошаговый набор reply команд.
- Подключения к собственным серверам для работы с ботами.
- Создание polling и webhook ботов.
- Встроенный функционал календаря для работы с датами.
- Постраничная работа в сообщениях.
- Хранения кэша пользователей.
- Возможность ограничения доступа к методам.
- Работа с dependency injection.
- Парсинг сообщений, команд, кнопок из конфигурационных файлов.


# 🔑 Зависимости

 - TelegramBot v21.2.0 https://github.com/TelegramBots/Telegram.Bot
 - Microsoft.Extensions.Configuration.Binder v8.0.0 https://github.com/dotnet/runtime
 - Microsoft.Extensions.Configuration.Json v8.0.0 https://github.com/dotnet/runtime
 - Microsoft.Extensions.DependencyInjection.Abstractions v8.0.0 https://github.com/dotnet/runtime

# 🧱 Интегрированные пакеты
 - CalendarPicker | karb0f0s   https://github.com/karb0f0s/CalendarPicker     
