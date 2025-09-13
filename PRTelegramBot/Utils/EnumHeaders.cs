using PRTelegramBot.Exceptions;

namespace PRTelegramBot.Utils
{
    public class EnumHeaders
    {
        private static readonly object _lock = new object();
        private static EnumHeaders instance;
        private HashSet<Enum> _uniqueValues;
        private Dictionary<int, Enum> _headers;

        private EnumHeaders()
        {
            _headers = new Dictionary<int, Enum>();
            _uniqueValues = new HashSet<Enum>();
        }

        public static EnumHeaders Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new EnumHeaders();
                        }
                    }
                }
                return instance;
            }
        }

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

        public bool ContainsKey(int key, Enum value)
        {
            lock (_lock)
                return _headers.ContainsKey(key) && _uniqueValues.Contains(_headers[key]);
        }

        public Enum Get(int key)
        {
            lock (_lock)
                return _headers.First(x => x.Key == key).Value;
        }

        public List<Enum> GetAll()
        {
            lock (_lock)
                return _headers.Select(x => x.Value).ToList();
        }

        public int Get(Enum key)
        {
            lock (_lock)
            {
                var @enum = _headers.FirstOrDefault(x => x.Value.Equals(key));
                if (@enum.Value == null)
                    throw new InlineCommandNotFoundException(key);

                return @enum.Key;
            }
        }
    }
}
