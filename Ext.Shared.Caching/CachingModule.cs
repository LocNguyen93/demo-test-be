namespace Ext.Shared.Caching
{
    using Autofac;
    using Contracts;
    using Providers;
    using System;

    public class CachingModule : Module
    {
        public string RedisCacheConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(RedisCacheConnectionString))
                throw new ArgumentNullException(nameof(RedisCacheConnectionString));

            builder.RegisterType<RedisCache>()
                .WithParameter("redisCacheConnectionString", RedisCacheConnectionString)
                .As<IRedisCache>()
                .SingleInstance();
        }
    }
}
