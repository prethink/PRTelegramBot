namespace PRTelegramBot.Core.Middlewares
{
    internal class EmptyMiddleware : MiddlewareBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override int ExecutionOrder => 0;

        #endregion
    }
}
