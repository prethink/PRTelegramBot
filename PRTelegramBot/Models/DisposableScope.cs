using Microsoft.Extensions.DependencyInjection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Обертка над <see cref="IServiceScope"/>, обеспечивающая безопасное освобождение ресурсов.
    /// </summary>
    /// <remarks>
    /// Используется для управления временем жизни scope при работе с Dependency Injection.
    /// Гарантирует однократный вызов <see cref="Dispose"/> даже при повторном освобождении.
    /// </remarks>
    public sealed class DisposableScope : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Провайдер сервисов, связанный с текущим scope.
        /// </summary>
        /// <remarks>
        /// Используется для разрешения зависимостей с жизненным циклом <c>Scoped</c>.
        /// Может быть <c>null</c>, если <see cref="IServiceScope"/> не был передан.
        /// </remarks>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Внутренний scope Dependency Injection.
        /// </summary>
        private readonly IServiceScope scope;

        /// <summary>
        /// Флаг, указывающий, что объект уже был освобожден.
        /// </summary>
        private bool disposed;

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;
            scope?.Dispose();
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="scope">Scope, созданный через <see cref="IServiceScopeFactory"/>.</param>
        public DisposableScope(IServiceScope scope)
        {
            this.scope = scope;
            ServiceProvider = scope?.ServiceProvider;
        }

        #endregion
    }
}
