# GeneratorUtils

В процессе написания...

## Создание реферальной ссылки для telegram бота

```csharp
        /// <summary>
        /// Генерация реферальной ссылки.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="refLink">Текст реферальной ссылки.</param>
        /// <returns>Сгенерированная реферальная ссылка https://t.me/{bot.Username}?start={refLink}.</returns>
        /// <exception cref="ArgumentNullException">Вызывается в случае пустого текста.</exception>
        public async static Task<string> GetGeneratedRefLink(this IBotContext context, string refLink)
```



## Создание купонов, промо кодов

```csharp
/// <summary>
/// Генерирует купон
/// Можно использовать для разных акций или промо кодов
/// </summary>
/// <param name="segmentLength">Длина сегмента кода</param>
/// <param name="countSplit">Количество разделителей</param>
/// <param name="symbolSplit">Символ разделителя, по умолчанию - </param>
/// <returns>Сгенерированный купон</returns>
public static string Coupon(int segmentLength = 6, int countSplit = 1, char symbolSplit = '-')
```
