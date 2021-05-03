using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentTypeIcons(
            this IServiceCollection services,
            Action<ContentTypeIconOptions> setupAction)
        {
            services.AddTransient<IContentTypeIconService, ContentTypeIconService>();

            services.AddOptions<ContentTypeIconOptions>().Configure<IConfiguration>((options, configuration) =>
            {
                setupAction(options);
                configuration.GetSection("Geta:ContentTypeIcons").Bind(options);
            });

            return services;
        }
    }
}