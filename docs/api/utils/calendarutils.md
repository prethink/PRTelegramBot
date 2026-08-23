# CalendarUtils

<pre class="language-csharp"><code class="lang-csharp"><strong>/// &#x3C;summary>
</strong>/// Создать новый календарь.
/// &#x3C;/summary>
/// &#x3C;param name="context">Контекст бота.&#x3C;/param>
/// &#x3C;param name="culture">Язык календаря.&#x3C;/param>
/// &#x3C;param name="headerCallbackCommand">Заголовок callback команды.&#x3C;/param>
/// &#x3C;param name="option">Параметры сообщения.&#x3C;/param>
/// &#x3C;param name="message">Текст сообщение.&#x3C;/param>
public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, OptionMessage option, string message)

/// &#x3C;summary>
/// Создать новый календарь.
/// &#x3C;/summary>
/// &#x3C;param name="context">Контекст бота.&#x3C;/param>
/// &#x3C;param name="culture">Язык календаря.&#x3C;/param>
/// &#x3C;param name="headerCallbackCommand">Заголовок callback команды.&#x3C;/param>
/// &#x3C;param name="message">Текст сообщение.&#x3C;/param>
public static async Task Create(IBotContext context, CultureInfo culture, Enum headerCallbackCommand, string message)

/// &#x3C;summary>
/// Создать новый календарь.
/// &#x3C;/summary>
/// &#x3C;param name="context">Контекст бота.&#x3C;/param>
/// &#x3C;param name="headerCallbackCommand">Заголовок callback команды.&#x3C;/param>
/// &#x3C;param name="message">Текст сообщение.&#x3C;/param>
public static async Task Create(IBotContext context, Enum headerCallbackCommand, string message)
</code></pre>
