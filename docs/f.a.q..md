# F.A.Q.

## При запуске бота, ошибка 404 not found

<figure><img src=".gitbook/assets/изображение (22).png" alt=""><figcaption></figcaption></figure>

Это ошибка может возникать если токен является не валидным. Проверьте правильно ли написан токен.

## Бот не реагирует на inline команды или вовсе не реагирует

У некоторых пользователей бывали ситуации, когда бот не реагировал на inline команды. Решение было обновить токен для бота в botfather.

## Не удалось найти пакет Telegram.bot c версией xxx

<figure><img src=".gitbook/assets/изображение (38).png" alt=""><figcaption></figcaption></figure>

Решение от **kilya31@Витя**

\
Если возникает ошибка "Не удалось найти пакет". Добавьте в nuget еще 1 источник пакетов "https://nuget.voids.site/v3/index.json". \
Средства -> Параметры -> Диспетчер пакетов nuget -> источники пакетов.&#x20;

<figure><img src=".gitbook/assets/изображение (39).png" alt=""><figcaption></figcaption></figure>

P.S. начиная с версии 22 telegram.bot это решение становится не актуальным. В 22 версии его вернули в nuget.
