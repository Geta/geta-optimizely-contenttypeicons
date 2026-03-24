using Geta.Optimizely.ContentTypeIcons.Caching;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseContentTypeIcons(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices;

            var descriptorConfiguration = services.GetRequiredService<TreeIconUiDescriptorConfiguration>();
            descriptorConfiguration.Initialize();

            var cacheProvider = services.GetRequiredService<IIconCacheProvider>();
            cacheProvider.Initialize();

            return app;
        }
    }
}
