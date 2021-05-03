using System;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons
{
    internal static class ImageUrlHelper
    {
#pragma warning disable 649
        private static Injected<IOptions<ContentTypeIconOptions>> _injectedOptions; // TODO: see if can use proper DI.
#pragma warning restore 649

        public static string GetUrl(Enum icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
        {
            return BuildSettings(icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        public static string GetUrl(string customFont, int character, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
        {
            var settings = GetSettings(backgroundColor, foregroundColor, fontSize);

            settings.CustomFontName = customFont;
            settings.Character = character;

            return CompileUrl(settings);
        }

        internal static string BuildSettings(Enum icon, string backgroundColor, string foregroundColor, int fontSize, Rotations rotate)
        {
            var embeddedFont = GetEmbeddedFontLocation(icon);
            return BuildSettings(embeddedFont, (int)(object)icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        internal static string GetEmbeddedFontLocation(Enum icon)
        {
            switch (icon)
            {
                case FontAwesome _:
                    return "fa4.fonts.fontawesome-webfont.ttf";
                case FontAwesome5Brands _:
                    return "fa5.webfonts.fa-brands-400.ttf";
                case FontAwesome5Regular _:
                    return "fa5.webfonts.fa-regular-400.ttf";
                case FontAwesome5Solid _:
                    return "fa5.webfonts.fa-solid-900.ttf";
                default:
                    return string.Empty;
            }
        }

        internal static string BuildSettings(string embeddedFont, int icon, string backgroundColor, string foregroundColor, int fontSize, Rotations rotate)
        {
            var settings = GetSettings(backgroundColor, foregroundColor, fontSize);
            settings.EmbeddedFont = embeddedFont;
            settings.Character = icon;
            settings.Rotate = rotate;
            return CompileUrl(settings);
        }

        // Helper methods
        private static string CompileUrl(ContentTypeIconSettings settings)
        {
            var nvc = settings.GetUrlParameters();
            var parameters = string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])));

            return $"/{Constants.UrlPattern}?{parameters}";
        }

        public static ContentTypeIconSettings GetSettings(string backgroundColor, string foregroundColor, int fontSize)
        {
            var configuration = _injectedOptions.Service.Value;

            var settings = new ContentTypeIconSettings
            {
                BackgroundColor = string.IsNullOrEmpty(backgroundColor)
                    ? configuration.BackgroundColor
                    : backgroundColor,
                ForegroundColor = string.IsNullOrEmpty(foregroundColor)
                    ? configuration.ForegroundColor
                    : foregroundColor,
                FontSize = fontSize > 0 ? fontSize : configuration.FontSize,
                Width = ContentTypeIconOptions.DefaultWidth,
                Height = ContentTypeIconOptions.DefaultHeight
            };

            return settings;
        }
    }
}
