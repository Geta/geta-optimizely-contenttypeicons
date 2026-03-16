using System;
using System.Linq;
using EPiServer.Cms.Shell;
using EPiServer.Shell.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        private static readonly Action<AuthorizationPolicyBuilder> DefaultPolicy = p =>
        {
            p.RequireAuthenticatedUser();
            p.RequireRole([
                "Administrators",
                "CmsAdmins",
                "CmsEditors",
                "WebAdmins",
                "WebEditors",
                "ThumbnailGroup"
            ]);
        };

        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services)
        {
            return services.AddContentTypeIcons(_ => { }, DefaultPolicy);
        }

        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services,
            Action<ContentTypeIconOptions> setupAction)
        {
            return services.AddContentTypeIcons(setupAction, DefaultPolicy);
        }

        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services,
            Action<ContentTypeIconOptions> setupAction,
            Action<AuthorizationPolicyBuilder> configurePolicy)
        {
            if (configurePolicy is null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            AddModule(services);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AuthorizationPolicy, configurePolicy);
            });

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
