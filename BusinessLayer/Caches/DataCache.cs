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

        private T GetOfCache(string key)
        {
            return _cache[key];
        }

        private void SetToCache(string key, T o)
        {
            _cache.Add(key, o);
        }

        public T GetObject(string key)
        {
            if (GetOfCache(key) == null)
            {
                T data = _dataFactory.GetData(key);

                if (data != null)
                {
                    SetToCache(key, data);
                }

                return data;
            }
            else
            {
                return GetOfCache(key);
            }
        }
    }
}
