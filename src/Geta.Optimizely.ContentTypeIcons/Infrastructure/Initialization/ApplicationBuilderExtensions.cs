using System;
using System.IO;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            var env = services.GetRequiredService<IWebHostEnvironment>();

            var appDataPath = Path.Combine(env.ContentRootPath, "App_Data");
            var fullPath = settings.CachePath.Replace("[appDataPath]", appDataPath, StringComparison.OrdinalIgnoreCase);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
    }
}
