namespace Ext.Shared.Caching.Contracts
{
    using System;

    public interface IRedisCache
    {
        void Set(string key, string value);
        void Set(string key, string value, int db);
        void SetWithTimeout(string key, string value, TimeSpan timeout);
        void SetWithTimeout(string key, string value, TimeSpan timeout, int db);
        bool Exists(string key);
        bool Exists(string key, int db);
        string Get(string key);
        string Get(string key, int db);
        void Delete(string key);
        void Delete(string key, int db);
        void Set<T>(string key, T value) where T : class;
        void Set<T>(string key, T value, int db) where T : class;
        void SetWithTimeout<T>(string key, T value, TimeSpan timeout) where T : class;
        void SetWithTimeout<T>(string key, T value, TimeSpan timeout, int db) where T : class;
        T Get<T>(string key) where T : class;
        T Get<T>(string key, int db) where T : class;

        void HashSet(string key, string hashField, string value);
        void HashSet(string key, string hashField, string value, int db);
        string HashGet(string key, string hashField);
        string HashGet(string key, string hashField, int db);
        void HashDelete(string key, string hashField);
        void HashDelete(string key, string hashField, int db);
        void HashSet<T>(string key, string hashField, T value) where T : class;
        void HashSet<T>(string key, string hashField, T value, int db) where T : class;
        T HashGet<T>(string key, string hashField) where T : class;
        T HashGet<T>(string key, string hashField, int db) where T : class;
    }
}
