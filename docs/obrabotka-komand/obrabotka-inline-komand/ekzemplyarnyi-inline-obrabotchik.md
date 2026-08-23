# Экземплярный inline обработчик

С версии 0.7.6 есть возможность создать отдельный экземпляр класса для inline обработки. \
Экземплярный inline обработчик представляет собой класс, который должен реализовать интерфейс [ICallbackQueryCommandHandler](../../api/interfeisy/icallbackquerycommandhandler.md), а именно метод Handle, который возвращает результат обработки.

<figure><img src="../../.gitbook/assets/изображение (7).png" alt=""><figcaption></figcaption></figure>

Экземплярный класс можно добавить через билдер при создание бота. Для одного класса используется одна команда. Класс может работать с di.

<figure><img src="../../.gitbook/assets/изображение (8).png" alt=""><figcaption></figcaption></figure>



Примеры работы с экземплярными классами можно увидеть в [AspNetExample ](../../../Examples/AspNetExample)(работа с DI) и [ConsoleExample](../../../Examples/ConsoleExample).
