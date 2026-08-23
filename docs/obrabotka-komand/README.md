# Обработка команд

В библиотеки есть 3 основных метода обработки команд:

* Reply - обработка сообщения пользователя
* Slash - обработка сообщения пользователя с использованием slash команды. В тексте сообщения /slash команды являются кликабельными. Могут выглядеть так /get или /get\_1
* Inline - обработка с фоновым действием.

## Сигнатура методов обработки

Методы обработки в экземпляре класса с использованием dependency injection

<pre class="language-csharp"><code class="lang-csharp">[BotHandler]
public class BotHandler
{ 
    private readonly ILogger&#x3C;BotHandler> _logger;

    public BotHandler(ILogger&#x3C;BotHandler> logger)
    {
        _logger = logger;
    }


    [Атрибуты обработки] 
    public async Task НазваниеМетода(IBotContext context)
    {
     //Код
    }
<strong>}
</strong>
</code></pre>

Статический метод обработки

```csharp
[Атрибуты обработки] 
public static async Task НазваниеМетода(IBotContext context) 
{
 //Тело функции
}

```
