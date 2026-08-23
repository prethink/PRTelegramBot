# Конфигурационные файлы для ботов

Каждый экземпляр бота может хранить в себе пути до конфигурационных файлов. Для этого воспользуйтесь методом AddConfigPath("ключ для конфига", "путь до конфига").

```csharp
var telegram = new PRBotBuilder("Token")
                    .AddConfigPath("Buttons", ".\\Configs\\buttons.json")
                    .AddConfigPath("Messages", ".\\Configs\\messages.json")
                    .Build();
```

Получение пути до файла

<pre class="language-csharp"><code class="lang-csharp"><strong>// Получить путь до конфигурационного файла по ключу.
</strong><strong>var configPath = context.GetBotDataOrNull().Options.ConfigPaths["Messages"];
</strong></code></pre>

Получение значения из конфигурационного файла

<pre class="language-csharp"><code class="lang-csharp">// Получить значение из конфигурационного файла по ключу.
// BotConfigJsonProvider - провайдер работы с конфигурационными файлами.
// string - возращаемый тип значения
// "Messages" - ключ конфигурационного файла
// "MSG_EXAMPLE_TEXT" - ключ для получения значения из файла
<strong>string msg = context.GetConfigValue&#x3C;BotConfigJsonProvider, string>("Messages", "MSG_EXAMPLE_TEXT");
</strong></code></pre>
