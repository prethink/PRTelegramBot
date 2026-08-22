using Microsoft.AspNetCore.Mvc;

namespace AspNetWebHook
{
    /// <summary>
    /// Static class containing extension methods for webhook routing.
    /// </summary>
    public static class WebHookExtensions
    {
        /// <summary>
        /// Maps a webhook route to the specified controller action.
        /// </summary>
        /// <typeparam name="TContoller">Controller type.</typeparam>
        /// <param name="endpoints">The object the route is added to.</param>
        /// <param name="route">Route template.</param>
        /// <returns>A builder for configuring the controller action endpoint.</returns>
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<TContoller>(this IEndpointRouteBuilder endpoints, string route)
            where TContoller : Controller
        {
            // The controller name without the Controller suffix.
            var controllerName = typeof(TContoller).Name.Replace("Controller", "", StringComparison.Ordinal);

            // The method that will handle the route.
            var actionName = typeof(TContoller).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}
