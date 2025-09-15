namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Билдер для построения цепочки middleware.
    /// </summary>
    public class MiddlewareBuilder
    {
        #region Методы

        /// <summary>
        /// Собрать цепочку middleware.
        /// </summary>
        /// <param name="middlewares">Обработчики.</param>
        /// <returns>Цепочка обработчиков.</returns>
        public virtual MiddlewareBase Build(List<MiddlewareBase> middlewares)
        {
            if (middlewares is null)
                return new MiddlewareBase();

            MiddlewareBase current = new MiddlewareBase();

            if (middlewares.Count == 1)
            {
                current = middlewares[0];
            }
            else if(middlewares.Count > 1)
            {
                current = middlewares[0];
                current.SetNext(middlewares[1]);
                for (int i = 1; i < middlewares.Count; i++) 
                {
                    if(i + 1 < middlewares.Count)
                    {
                        middlewares[i].SetNext(middlewares[i + 1], middlewares[i - 1]);
                    }
                    else
                    {
                        middlewares[i].SetPrevious(middlewares[i - 1]);
                    }
                }
            }

            return current;
        }

        #endregion
    }
}
