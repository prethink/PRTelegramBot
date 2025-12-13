using ConsoleExample.Extension;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleEvents
    {
        public static async Task OnWrongTypeChat(BotEventArgs e)
        {
            string msg = "Неверный тип чата";
            await MessageSender.Send(e.Context, msg);
        }

        public static async Task OnMissingCommand(BotEventArgs args)
        {
            string msg = "Не найдена команда";
            await MessageSender.Send(args.Context, msg);
        }

        public static async Task OnErrorCommand(BotEventArgs args)
        {
            string msg = "Произошла ошибка при обработке команды";
            await MessageSender.Send(args.Context, msg);
        }

        /// <summary>
        /// Событие проверки привилегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="mask">Маска доступа</param>
        /// Подписка на событие проверки привелегий <see cref="Program"/>
        public static async Task OnCheckPrivilege(PrivilegeEventArgs e)
        {
            if (!e.Mask.HasValue)
            {
                // Нет маски доступа, выполняем метод.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // Получаем значение маски требуемого доступа.
            var requiredAccess = e.Mask.Value;

            // Получаем флаги доступа пользователя.
            // Здесь вы на свое усмотрение реализываете логику получение флагов, например можно из базы данных получить.
            var userFlags = e.Context.Update.LoadExampleFlagPrivilege();

            if (requiredAccess.HasFlag(userFlags))
            {
                // Доступ есть, выполняем метод.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // Доступа нет.
            string errorMsg = "У вас нет доступа к данной функции";
            await MessageSender.Send(e.Context, errorMsg);
            return;

        }

        public static async Task OnUserStartWithArgs(StartEventArgs args)
        {
            string msg = "Пользователь отправил старт с аргументом";
            await MessageSender.Send(args.Context, msg);
        }
        public static async Task OnWrongTypeMessage(BotEventArgs e)
        {
            string msg = "Неверный тип сообщения";
            await MessageSender.Send(e.Context, msg);
        }
    }
}
