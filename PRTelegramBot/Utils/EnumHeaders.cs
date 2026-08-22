using PRTelegramBot.Exceptions;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Registry that maps an integer header to the enum value of an inline command.
    /// </summary>
    /// <remarks>
    /// Inline commands travel inside <c>callback_data</c> as integers, so every command enum
    /// gets a numeric header here. The registry is a process-wide singleton and is guarded by a lock.
    /// </remarks>
    public class EnumHeaders
    {
        #region Fields and properties

        private static readonly object _lock = new object();
        private static EnumHeaders instance;
        private HashSet<Enum> _uniqueValues;
        private Dictionary<int, Enum> _headers;

        /// <summary>
        /// The single instance of the registry.
        /// </summary>
        public static EnumHeaders Instance
        {
            get
            {
                if (instance is null)
                {
                    lock (_lock)
                    {
                        if (instance is null)
                        {
                            instance = new EnumHeaders();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers an enum value under the given header.
        /// </summary>
        /// <remarks>If the value is already registered, the call is ignored.</remarks>
        /// <param name="key">Numeric header of the command.</param>
        /// <param name="value">The command enum value.</param>
        public void Add(int key, Enum value)
        {
            lock (_lock)
            {
                if (_uniqueValues.Contains(value))
                {
                    //throw new ArgumentException("Value already exists in the dictionary.", nameof(value));
                    return;
                }
                _headers.Add(key, value);
                _uniqueValues.Add(value);
            }
        }

        /// <summary>
        /// Checks whether the header is registered and still points at a known value.
        /// </summary>
        /// <param name="key">Numeric header of the command.</param>
        /// <param name="value">The command enum value.</param>
        /// <returns>True if the header is registered; False otherwise.</returns>
        public bool ContainsKey(int key, Enum value)
        {
            lock (_lock)
                return _headers.ContainsKey(key) && _uniqueValues.Contains(_headers[key]);
        }

        /// <summary>
        /// Gets the enum value registered under the given header.
        /// </summary>
        /// <param name="key">Numeric header of the command.</param>
        /// <returns>The command enum value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the header is not registered.</exception>
        public Enum Get(int key)
        {
            lock (_lock)
                return _headers.First(x => x.Key == key).Value;
        }

        /// <summary>
        /// Gets every registered command enum value.
        /// </summary>
        /// <returns>The list of registered values.</returns>
        public List<Enum> GetAll()
        {
            lock (_lock)
                return _headers.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets the header the enum value is registered under.
        /// </summary>
        /// <param name="key">The command enum value.</param>
        /// <returns>The numeric header of the command.</returns>
        /// <exception cref="InlineCommandNotFoundException">Thrown when the value is not registered.</exception>
        public int Get(Enum key)
        {
            lock (_lock)
            {
                var @enum = _headers.FirstOrDefault(x => x.Value.Equals(key));
                if (@enum.Value is null)
                    throw new InlineCommandNotFoundException(key);

                return @enum.Key;
            }
        }

        #endregion

        #region Constructors

        private EnumHeaders()
        {
            _headers = new Dictionary<int, Enum>();
            _uniqueValues = new HashSet<Enum>();
        }

        #endregion
    }
}
