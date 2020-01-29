using System;

namespace BeyondNet.App.Ums.Helpers.Interface
{
    public interface ICacheStorageHelper
    {
        void Remove(string key);
        void Store(string key, object data);
        void Store(string key, object data, TimeSpan slidingExpiration);
        void Store(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        T Retrieve<T>(string key);
    }
}
