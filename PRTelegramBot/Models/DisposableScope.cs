using Microsoft.Extensions.DependencyInjection;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// A wrapper around <see cref="IServiceScope"/> that makes releasing resources safe.
    /// </summary>
    /// <remarks>
    /// Used to manage the scope lifetime when working with Dependency Injection.
    /// Guarantees that <see cref="Dispose"/> is called only once, even on repeated disposal.
    /// </remarks>
    public sealed class DisposableScope : IDisposable
    {
        #region Fields and properties

        /// <summary>
        /// The service provider bound to the current scope.
        /// </summary>
        /// <remarks>
        /// Used to resolve dependencies with a <c>Scoped</c> lifetime.
        /// May be <c>null</c> if no <see cref="IServiceScope"/> was supplied.
        /// </remarks>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// The internal Dependency Injection scope.
        /// </summary>
        private readonly IServiceScope scope;

        /// <summary>
        /// Flag indicating that the object has already been disposed.
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

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scope">The scope created through <see cref="IServiceScopeFactory"/>.</param>
        public DisposableScope(IServiceScope scope)
        {
            this.scope = scope;
            ServiceProvider = scope?.ServiceProvider;
        }

        #endregion
    }
}
