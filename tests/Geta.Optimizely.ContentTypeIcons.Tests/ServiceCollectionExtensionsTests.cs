using Geta.Optimizely.ContentTypeIcons.Caching;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Geta.Optimizely.ContentTypeIcons.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void SetCacheProvider_ReplacesDefaultProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IIconCacheProvider, InMemoryIconCacheProvider>();
            services.SetCacheProvider<DiskIconCacheProvider>();

            var descriptor = Assert.Single(services, d => d.ServiceType == typeof(IIconCacheProvider));
            Assert.Equal(typeof(DiskIconCacheProvider), descriptor.ImplementationType);
        }

        [Fact]
        public void SetCacheProvider_RegistersAsSingleton()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IIconCacheProvider, InMemoryIconCacheProvider>();
            services.SetCacheProvider<DiskIconCacheProvider>();

            var descriptor = Assert.Single(services, d => d.ServiceType == typeof(IIconCacheProvider));
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        }

        [Fact]
        public void SetCacheProvider_RegistersWithSpecifiedLifetime()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IIconCacheProvider, InMemoryIconCacheProvider>();
            services.SetCacheProvider<DiskIconCacheProvider>(ServiceLifetime.Transient);

            var descriptor = Assert.Single(services, d => d.ServiceType == typeof(IIconCacheProvider));
            Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
        }
    }
}
