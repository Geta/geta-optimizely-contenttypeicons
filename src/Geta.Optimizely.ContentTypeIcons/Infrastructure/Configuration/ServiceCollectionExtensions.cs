using System;
using System.Linq;
using EPiServer.Cms.Shell;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services)
        {
            return services.AddContentTypeIcons(_ => { });
        }

        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services,
            Action<ContentTypeIconOptions> setupAction)
        {
            AddModule(services);

            services.AddTransient<IContentTypeIconService, ContentTypeIconService>();
            services.AddTransient<TreeIconUiDescriptorConfiguration>();

            services.AddOptions<ContentTypeIconOptions>().Configure<IConfiguration>((options, configuration) =>
            {
                setupAction(options);
                configuration.GetSection("Geta:ContentTypeIcons").Bind(options);
            });

            return services;
        }

        private static void AddModule(IServiceCollection services)
        {
            services.AddCmsUI();
            services.Configure<ProtectedModuleOptions>(
                pm =>
                {
                    if (!pm.Items.Any(i => i.Name.Equals(Constants.ModuleName, StringComparison.OrdinalIgnoreCase)))
                    {
                        pm.Items.Add(new ModuleDetails { Name = Constants.ModuleName });
                    }
                });
        }
    }
}
