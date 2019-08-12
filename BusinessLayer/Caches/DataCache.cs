using BusinessLayer.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Caches
{
    public class DataCache<T> : ICache<T>
    {
        private IAbstractFactory<T> _dataFactory;
        private Dictionary<string, T> _cache;

        public DataCache(IAbstractFactory<T> abstractFactory)
        {
            _dataFactory = abstractFactory;
            _cache = new Dictionary<string, T>();
        }

        public void SetObject(string key, T o)
        {
            _cache.Add(key, o);
        }

        public T GetObject(string key)
        {
            if (_cache[key] == null)
            {
                T data = _dataFactory.GetData(key);

                if (data != null)
                {
                    SetObject(key, data);
                }

                return data;
            }
            else
            {
                return _cache[key];
            }
        }
    }
}
