using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace ConsoleExample.Examples
{
    internal class ExamplePay
    {
        ///// <summary>
        ///// Напишите в чате "stepstart"
        ///// Метод регистрирует следующий шаг пользователя
        ///// </summary>
        //[ReplyMenuHandler("Pay")]
        //public static async Task Pay(ITelegramBotClient botClient, Update update)
        //{
        //    var chatId = new ChatId(update.GetChatId());
        //    List<LabeledPrice> prices = new();
        //    prices.Add(new LabeledPrice("Товар 1", 5));
        //    prices.Add(new LabeledPrice("Товар 2", 15));
        //    prices.Add(new LabeledPrice("Товар 3", 25));
        //    prices.Add(new LabeledPrice("Товар 4", 55));
        //    var request = new SendInvoiceRequest(chatId, "Test", "Описание", "Russian Ruble", null, "tsss", prices);
        //    await botClient.SendInvoiceAsync(request);
        //}
    }
}
