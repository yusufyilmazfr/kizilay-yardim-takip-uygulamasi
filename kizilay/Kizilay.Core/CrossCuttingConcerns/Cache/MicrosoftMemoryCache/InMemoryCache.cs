using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kizilay.Core.CrossCuttingConcerns.Cache.MicrosoftMemoryCache
{
    public class InMemoryCache : ICache
    {
        private ObjectCache _cache => MemoryCache.Default;

        public Task AddAsync(string key, object value, int duration = 60)
        {
            return Task.Run(() => { Add(key, value, duration); });
        }

        public void Add(string key, object value, int duration = 60)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(duration) };
            _cache.Add(key, value, policy);
        }

        public void Clear()
        {
            foreach (var item in _cache)
            {
                _cache.Remove(item.Key);
            }
        }

        public bool Contains(string key)
        {
            return _cache.GetCacheItem(key) != null;
        }

        public T Get<T>(string key)
        {
            var cacheItem = _cache.GetCacheItem(key);

            return (T)cacheItem.Value;
        }

        public void RemoveByKey(string key)
        {
            if (Contains(key))
                _cache.Remove(key);
        }

        public void RemoveByPattern(string keyPattern)
        {
            var regex = new Regex(keyPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            var matchedKeys = _cache.Where(i => regex.IsMatch(i.Key)).Select(i => i.Key).ToList();

            matchedKeys.ForEach(i => RemoveByKey(i));
        }
    }
}
