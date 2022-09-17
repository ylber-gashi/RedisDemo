using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace RedisDemo.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;

        public CacheService()
        {
            ConfigureRedis();
        }

        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrWhiteSpace(value))
                return JsonConvert.DeserializeObject<T>(value);

            return default;
        }

        public object RemoveData(string key)
        {
            bool keyExists = _db.KeyExists(key);
            if (keyExists)
                return _db.KeyDelete(key);

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expireTime);

            return isSet;
        }
    }
}
