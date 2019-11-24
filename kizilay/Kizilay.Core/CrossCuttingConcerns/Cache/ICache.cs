using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.CrossCuttingConcerns.Cache
{
    public interface ICache
    {
        Task AddAsync(string key, object value, int duration = 60);
        void Add(string key, object value, int duration = 60);
        T Get<T>(string key);
        void RemoveByKey(string key);
        void RemoveByPattern(string keyPattern);
        void Clear();
        bool Contains(string key);
    }
}
