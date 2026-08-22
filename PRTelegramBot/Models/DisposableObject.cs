namespace PRTelegramBot.Models
{
    /// <summary>
    /// A wrapper object that runs custom logic when resources are released.
    /// </summary>
    /// <remarks>
    /// Used to register an arbitrary action that will be executed
    /// when <see cref="Dispose"/> is called. Handy for temporary subscriptions, hooks
    /// and other cases where the finalizing logic has to run for certain.
    /// </remarks>
    public sealed class DisposableObject : IDisposable
    {
        #region Fields and properties

        /// <summary>
        /// The action executed when the object is disposed.
        /// </summary>
        /// <remarks>
        /// After <see cref="Dispose"/> is called it is set to <c>null</c>,
        /// which prevents it from running twice.
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

        #region Constructors

        /// <summary>
        /// Constructors.
        /// </summary>
        /// <param name="onDispose">The action executed when <see cref="Dispose"/> is called.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="onDispose"/> is <c>null</c>.</exception>
        public DisposableObject(Action onDispose)
        {
            this.onDispose = onDispose ?? throw new ArgumentNullException(nameof(onDispose));
        }

        #endregion
    }
}
