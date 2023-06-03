using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Helpers
{
    public class EnumHeaders
    {
        private static EnumHeaders _instance;
        private static readonly object _lock = new object();
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
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EnumHeaders();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Add(int key, Enum value)
        {
            lock(_lock)
            {
                if (_uniqueValues.Contains(value))
                {
                    throw new ArgumentException("Value already exists in the dictionary.", nameof(value));
                }
                _headers.Add(key, value);
                _uniqueValues.Add(value);
            }
        }

        public Enum Get(int key)
        {
            lock (_lock)
            {
                return _headers.First(x => x.Key == key).Value;
            }
        }

        public int Get(Enum key)
        {
            lock (_lock)
            {
                return _headers.First(x => x.Value.Equals(key)).Key;
            }
        }
    }
}
