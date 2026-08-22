namespace ConsoleExample.Examples
{
    internal class ExamplePay
    {
        ///// <summary>
        ///// Send "stepstart" in the chat
        ///// Registers the user's next step
        ///// </summary>
        //[ReplyMenuHandler("Pay")]
        //public static async Task Pay(ITelegramBotClient context.BotClient, context.Update update)
        //{
        //    var chatId = new ChatId(update.GetChatId());
        //    List<LabeledPrice> prices = new();
        //    prices.Add(new LabeledPrice("Item 1", 5));
        //    prices.Add(new LabeledPrice("Item 2", 15));
        //    prices.Add(new LabeledPrice("Item 3", 25));
        //    prices.Add(new LabeledPrice("Item 4", 55));
        //    var request = new SendInvoiceRequest(chatId, "Test", "Description", "Russian Ruble", null, "tsss", prices);
        //    await botClient.SendInvoiceAsync(request);
        //}
    }
}
