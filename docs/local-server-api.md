# Local Server API

Пример как подключить бота к своему серверу, а не к серверам телеграм:

<pre class="language-csharp"><code class="lang-csharp"><strong>//В параметрах нужно указать токен и адрес куда должнен подключаться бота.
</strong><strong>var telegramOptions = new TelegramBotClientOptions("Token", "http://baseurl");
</strong><strong>//Данные параметры нужно передать при создание нового бота.
</strong>var telegram = new PRBotBuilder(new TelegramBotClient(telegramOptions)).Build();
</code></pre>
