namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public class ContentTypeIconOptions
    {
        public const int DefaultWidth = 120;
        public const int DefaultHeight = 90;

        public const string DefaultCustomFontPath = "[appDataPath]/fonts/";
        public string CustomFontPath { get; set; } = DefaultCustomFontPath;

        public const string DefaultCachePath = "[appDataPath]/thumb_cache/";
        public string CachePath { get; set; } = DefaultCachePath;

        public const string DefaultBackgroundColor = "#02423F";
        public string BackgroundColor { get; set; } = DefaultBackgroundColor;

        public const string DefaultForegroundColor = "#ffffff";
        public string ForegroundColor { get; set; } = DefaultForegroundColor;

        public const int DefaultFontSize = 40;
        public int FontSize { get; set; } = DefaultFontSize;

        public bool EnableTreeIcons { get; set; }
    }
}
