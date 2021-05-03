using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization
{
    public static class MapContentTypeIconsExtensions
    {
        public static IEndpointRouteBuilder MapContentTypeIcons(this IEndpointRouteBuilder builder)
        {
            builder.MapControllerRoute(
                "ThumbnailIcon",
                Constants.UrlPattern,
                new {controller = "ThumbnailIcon", action = "GenerateThumbnail"});

            return builder;
        }
    }
}