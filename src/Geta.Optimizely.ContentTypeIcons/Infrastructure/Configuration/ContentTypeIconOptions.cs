namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    public class ContentTypeIconOptions
    {
        public static int DefaultWidth = 120;
        public static int DefaultHeight = 90;

        public static string DefaultCustomFontPath = "[appDataPath]\\fonts\\";
        public string CustomFontPath { get; set; } = DefaultCustomFontPath;

        public static string DefaultCachePath = "[appDataPath]\\thumb_cache\\";
        public string CachePath { get; set; } = DefaultCachePath;

        public static string DefaultBackgroundColor = "#02423F";
        public string BackgroundColor { get; set; } = DefaultBackgroundColor;

        public static string DefaultForegroundColor = "#ffffff";
        public string ForegroundColor { get; set; } = DefaultForegroundColor;

        public static int DefaultFontSize = 40;
        public int FontSize { get; set; } = DefaultFontSize;

        public bool EnableTreeIcons { get; set; }
    }
}