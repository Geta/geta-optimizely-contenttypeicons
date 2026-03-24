using System;
using System.Linq;
using EPiServer.Cms.Shell;
using EPiServer.Shell.Modules;
using Geta.Optimizely.ContentTypeIcons.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        private static readonly Action<AuthorizationPolicyBuilder> DefaultPolicy = p =>
        {
            p.RequireAuthenticatedUser();
            p.RequireRole("Administrators", "CmsAdmins", "CmsEditors", "WebAdmins", "WebEditors", "ThumbnailGroup");
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
            ArgumentNullException.ThrowIfNull(configurePolicy);

            AddModule(services);
            services.AddAuthorizationBuilder()
                .AddPolicy(Constants.AuthorizationPolicy, configurePolicy);

            services.AddTransient<IContentTypeIconService, ContentTypeIconService>();
            services.AddTransient<TreeIconUiDescriptorConfiguration>();
            services.AddTransient<IIconCacheProvider, DiskIconCacheProvider>();

            services.AddOptions<ContentTypeIconOptions>().Configure<IConfiguration>((options, configuration) =>
            {
                setupAction(options);
                configuration.GetSection("Geta:ContentTypeIcons").Bind(options);
            });

            return services;
        }

        /// <summary>
        /// Replaces the default <see cref="DiskIconCacheProvider"/> with a custom <see cref="IIconCacheProvider"/> implementation.
        /// </summary>
        /// <example>
        /// services.AddContentTypeIcons(...).SetCacheProvider&lt;InMemoryIconCacheProvider&gt;();
        /// </example>
        public static IServiceCollection SetCacheProvider<T>(this IServiceCollection services)
            where T : class, IIconCacheProvider
        {
            services.Replace(ServiceDescriptor.Transient<IIconCacheProvider, T>());
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
