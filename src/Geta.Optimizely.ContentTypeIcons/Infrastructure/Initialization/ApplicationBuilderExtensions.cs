using System;
using System.IO;
using EPiServer.Framework.Internal;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseContentTypeIcons(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices;

            var descriptorConfiguration = services.GetRequiredService<TreeIconUiDescriptorConfiguration>();
            descriptorConfiguration.Initialize();

            EnsureCacheFolderCreated(services);

            return app;
        }

        private static void EnsureCacheFolderCreated(IServiceProvider services)
        {
            var options = services.GetRequiredService<IOptions<ContentTypeIconOptions>>();
            var settings = options.Value;
            var physicalPathResolver = services.GetRequiredService<IPhysicalPathResolver>();

            // verify cache directory exists
            var fullPath = physicalPathResolver.Rebase(settings.CachePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
    }
}
