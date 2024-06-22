using Microsoft.AspNetCore.Mvc;

namespace AspNetWebHook
{
    /// <summary>
    /// Статический класс, содержащий методы расширения для маршрутизации webhook'ов.
    /// </summary>
    public static class WebHookExtensions
    {
        /// <summary>
        /// Сопоставляет маршрут webhook с указанным действием контроллера.
        /// </summary>
        /// <typeparam name="TContoller">Тип контроллера.</typeparam>
        /// <param name="endpoints">Объект для добавления маршрута.</param>
        /// <param name="route">Шаблон маршрута.</param>
        /// <returns>Строитель для настройки конечной точки действия контроллера.</returns>
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<TContoller>(this IEndpointRouteBuilder endpoints, string route)
            where TContoller : Controller
        {
            // Название контроллера без Controller.
            var controllerName = typeof(TContoller).Name.Replace("Controller", "", StringComparison.Ordinal);

            // Метод, который будет обрабатывать маршрут.
            var actionName = typeof(TContoller).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}
