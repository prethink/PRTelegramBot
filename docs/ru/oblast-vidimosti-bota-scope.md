# Область видимости бота (Scope)

С версии 0.9 выполнение `update` происходит внутри специального `Scope`, который хранит все необходимые данные для работы.\
Теперь в любом коде, вызванном этим `update`, можно безопасно получить:

* Текущий контекст: `var currentContext = CurrentScope.Context`
* Текущий бот: `var currentBot = CurrentScope.Bot`
* Текущие сервисы: `var services = CurrentScope.Services` (IServiceProvider)

<figure><img src=".gitbook/assets/изображение (4).png" alt=""><figcaption></figcaption></figure>

Пример:

<figure><img src=".gitbook/assets/изображение (5).png" alt=""><figcaption></figcaption></figure>
