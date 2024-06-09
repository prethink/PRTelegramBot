namespace AspNetWebHook
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebHookExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoints"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<T>(
             this IEndpointRouteBuilder endpoints,
             string route)
        {
            var controllerName = typeof(T).Name.Replace("Controller", "", StringComparison.Ordinal);
            var actionName = typeof(T).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}
