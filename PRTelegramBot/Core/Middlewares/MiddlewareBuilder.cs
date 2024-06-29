namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Билдер для построения цепочки middleware.
    /// </summary>
    public class MiddlewareBuilder
    {
        #region Методы

        /// <summary>
        /// Собрать цепочку middleware
        /// </summary>
        /// <param name="middlewares">Обработчики.</param>
        /// <returns>Цепочка обработчиков.</returns>
        public MiddlewareBase Build(IEnumerable<MiddlewareBase> middlewares)
        {
            if (middlewares == null)
                return new MiddlewareBase();

            MiddlewareBase current = null;
            foreach (var middleware in middlewares)
            {
                if (current == null)
                    current = middleware;
                else
                    current.SetNext(middleware);
            }
            return current ?? new MiddlewareBase();
        }

        #endregion
    }
}
