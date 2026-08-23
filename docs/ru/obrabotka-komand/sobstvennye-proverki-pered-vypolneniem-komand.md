# Собственные проверки перед выполнением команд

Для каждого типа команд можно добавить собственные промежуточные проверки. В случае если проверка будет не пройдена, можно прервать выполнение команды.

Для примера рассмотрим создание промежуточного чекера для команд типа [Reply](../api/perechisleniya-enum/commandtype.md).

<pre class="language-csharp"><code class="lang-csharp"><strong>// Создаем собственные чекер, который будет использован только для Reply команд
</strong><strong>var checkerReplyCommand = new InternalChecker(CommandType.Reply, new ReplyExampleChecker());
</strong>// При создание бота добавляем чекер.
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddCommandChecker(checkerReplyCommand)
                    .Build();
</code></pre>

Пример самого чекера. Чекер обязательно должен реализовывать интерфейс [IInternalCheck](../api/interfeisy/iinternalcheck.md) и возвращать результат проверки.&#x20;

```csharp
namespace ConsoleExample.Checkers
{
    internal class ReplyExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // Что-то проверяем перед выполнением reply команд.
            // InternalCheckResult.Passed - продолжить выполнение команды, любые другие результаты остановят выполнение команды.
            return InternalCheckResult.Passed;
        }
    }
}
```

Пример:

* [Создание команды только для администраторов.](../fishki-poleznye-praktiki/sozdanie-komandy-tolko-dlya-administratorov.md)

