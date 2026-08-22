namespace PRTelegramBot.Core.Middlewares
{
    internal class EmptyMiddleware : MiddlewareBase
    {
        #region Base class

        /// <inheritdoc />
        public override int ExecutionOrder => 0;

        #endregion
    }
}
