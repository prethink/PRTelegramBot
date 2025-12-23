namespace PRTelegramBot.Models
{
    /// <summary>
    /// Объект-обертка для выполнения пользовательской логики при освобождении ресурсов.
    /// </summary>
    /// <remarks>
    /// Используется для регистрации произвольного действия, которое будет выполнено
    /// при вызове <see cref="Dispose"/>. Удобен для временных подписок, хуков
    /// и других сценариев, где необходимо гарантированно выполнить завершающую логику.
    /// </remarks>
    public sealed class DisposableObject : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Действие, выполняемое при освобождении объекта.
        /// </summary>
        /// <remarks>
        /// После вызова <see cref="Dispose"/> устанавливается в <c>null</c>,
        /// что предотвращает повторное выполнение.
        /// </remarks>
        private Action? onDispose;

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            onDispose?.Invoke();
            onDispose = null;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструкторы.
        /// </summary>
        /// <param name="onDispose">Действие, которое будет выполнено при вызове <see cref="Dispose"/>.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="onDispose"/> равен <c>null</c>.</exception>
        public DisposableObject(Action onDispose)
        {
            this.onDispose = onDispose ?? throw new ArgumentNullException(nameof(onDispose));
        }

        #endregion
    }
}
