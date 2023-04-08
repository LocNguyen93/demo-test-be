namespace Ext.Shared.Caching.Providers
{
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;
    using System.Net.Sockets;
    using System.Threading;

    public class RedisCache : IRedisCache
    {
        private readonly string redisCacheConnectionString;
        private readonly ILogger<RedisCache> logger;
        private Lazy<ConnectionMultiplexer> lazyConnection;

        private const int defaultDbIndex = 0;

        public RedisCache(string redisCacheConnectionString, ILoggerFactory loggerFactory)
        {
            this.redisCacheConnectionString = redisCacheConnectionString;
            lazyConnection = CreateConnection();
            logger = loggerFactory.CreateLogger<RedisCache>();
        }

        private Lazy<ConnectionMultiplexer> CreateConnection()
        {
            return new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisCacheConnectionString));
        }

        private ConnectionMultiplexer Connection
        {
            get { return lazyConnection.Value; }
        }

        public void Set(string key, string value)
        {
            Set(key, value, defaultDbIndex);
        }

        public void Set(string key, string value, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.StringSet(key, value);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public void SetWithTimeout(string key, string value, TimeSpan timeout)
        {
            SetWithTimeout(key, value, timeout, defaultDbIndex);
        }

        public void SetWithTimeout(string key, string value, TimeSpan timeout, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.StringSet(key, value, timeout);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public bool Exists(string key)
        {
            return Exists(key, defaultDbIndex);
        }

        public bool Exists(string key, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                return cache.KeyExists(key);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
                return false;
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
                return false;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error reading from redis");
                return false;
            }
        }

        public string Get(string key)
        {
            return Get(key, defaultDbIndex);
        }

        public string Get(string key, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                return cache.StringGet(key);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
                return null;
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
                return null;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error reading from redis");
                return string.Empty;
            }
        }

        public void Delete(string key)
        {
            Delete(key, defaultDbIndex);
        }

        public void Delete(string key, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.KeyDelete(key);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting from redis");
            }
        }

        public void Set<T>(string key, T value) where T : class
        {
            Set(key, value, defaultDbIndex);
        }

        public void Set<T>(string key, T value, int db) where T : class
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.StringSet(key, JsonConvert.SerializeObject(value));
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public void SetWithTimeout<T>(string key, T value, TimeSpan timeout) where T : class
        {
            SetWithTimeout(key, value, timeout, defaultDbIndex);
        }

        public void SetWithTimeout<T>(string key, T value, TimeSpan timeout, int db) where T : class
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.StringSet(key, JsonConvert.SerializeObject(value), timeout);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public T Get<T>(string key) where T : class
        {
            return Get<T>(key, defaultDbIndex);
        }

        public T Get<T>(string key, int db) where T : class
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                var cachedValue = cache.StringGet(key);
                return JsonConvert.DeserializeObject<T>(cachedValue);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
                return null;
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
                return null;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error reading from redis");
                return null;
            }
        }

        public void HashSet(string key, string hashField, string value)
        {
            HashSet(key, hashField, value, defaultDbIndex);
        }

        public void HashSet(string key, string hashField, string value, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.HashSet(key, hashField, value);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public string HashGet(string key, string hashField)
        {
            return HashGet(key, hashField, defaultDbIndex);
        }

        public string HashGet(string key, string hashField, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                return cache.HashGet(key, hashField);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
                return null;
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
                return null;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error reading from redis");
                return string.Empty;
            }
        }

        public void HashDelete(string key, string hashField)
        {
            HashDelete(key, hashField, defaultDbIndex);
        }

        public void HashDelete(string key, string hashField, int db)
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.HashDelete(key, hashField);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting from redis");
            }
        }

        public void HashSet<T>(string key, string hashField, T value) where T : class
        {
            HashSet(key, hashField, value, defaultDbIndex);
        }

        public void HashSet<T>(string key, string hashField, T value, int db) where T : class
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                cache.HashSet(key, hashField, JsonConvert.SerializeObject(value));
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error writing to redis");
            }
        }

        public T HashGet<T>(string key, string hashField) where T : class
        {
            return HashGet<T>(key, hashField, defaultDbIndex);
        }

        public T HashGet<T>(string key, string hashField, int db) where T : class
        {
            try
            {
                var cache = Connection.GetDatabase(db);
                var cachedValue = cache.HashGet(key, hashField);
                return JsonConvert.DeserializeObject<T>(cachedValue);
            }
            catch (RedisConnectionException e)
            {
                logger.LogError(e, "Redis connection error");
                ForceReconnect();
                return null;
            }
            catch (SocketException e)
            {
                logger.LogError(e, "Redis socket error");
                ForceReconnect();
                return null;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error reading from redis");
                return null;
            }
        }

        #region Reconnect strategy

        private static long lastReconnectTicks = DateTimeOffset.MinValue.UtcTicks;
        private static DateTimeOffset firstError = DateTimeOffset.MinValue;
        private static DateTimeOffset previousError = DateTimeOffset.MinValue;

        private static object reconnectLock = new object();

        // In general, let StackExchange.Redis handle most reconnects, 
        // so limit the frequency of how often this will actually reconnect.
        public static TimeSpan ReconnectMinFrequency = TimeSpan.FromSeconds(60);

        // if errors continue for longer than the below threshold, then the 
        // multiplexer seems to not be reconnecting, so re-create the multiplexer
        public static TimeSpan ReconnectErrorThreshold = TimeSpan.FromSeconds(30);

        public void ForceReconnect()
        {
            var utcNow = DateTimeOffset.UtcNow;
            var previousTicks = Interlocked.Read(ref lastReconnectTicks);
            var previousReconnect = new DateTimeOffset(previousTicks, TimeSpan.Zero);
            var elapsedSinceLastReconnect = utcNow - previousReconnect;

            // If mulitple threads call ForceReconnect at the same time, we only want to honor one of them.
            if (elapsedSinceLastReconnect > ReconnectMinFrequency)
            {
                lock (reconnectLock)
                {
                    utcNow = DateTimeOffset.UtcNow;
                    elapsedSinceLastReconnect = utcNow - previousReconnect;

                    if (firstError == DateTimeOffset.MinValue)
                    {
                        // We haven't seen an error since last reconnect, so set initial values.
                        firstError = utcNow;
                        previousError = utcNow;
                        return;
                    }

                    if (elapsedSinceLastReconnect < ReconnectMinFrequency)
                        return; // Some other thread made it through the check and the lock, so nothing to do.

                    var elapsedSinceFirstError = utcNow - firstError;
                    var elapsedSinceMostRecentError = utcNow - previousError;

                    var shouldReconnect =
                        elapsedSinceFirstError >= ReconnectErrorThreshold   // make sure we gave the multiplexer enough time to reconnect on its own if it can
                        && elapsedSinceMostRecentError <= ReconnectErrorThreshold; //make sure we aren't working on stale data (e.g. if there was a gap in errors, don't reconnect yet).

                    // Update the previousError timestamp to be now (e.g. this reconnect request)
                    previousError = utcNow;

                    if (shouldReconnect)
                    {
                        firstError = DateTimeOffset.MinValue;
                        previousError = DateTimeOffset.MinValue;

                        var oldMultiplexer = lazyConnection;
                        CloseConnection(oldMultiplexer);
                        lazyConnection = CreateConnection();
                        lastReconnectTicks = utcNow.UtcTicks;
                    }
                }
            }
        }

        private static void CloseConnection(Lazy<ConnectionMultiplexer> oldMultiplexer)
        {
            if (oldMultiplexer != null)
            {
                try
                {
                    oldMultiplexer.Value.Close();
                }
                catch (Exception)
                {
                    // Example error condition: if accessing old.Value causes a connection attempt and that fails.
                }
            }
        }

        #endregion
    }
}