using System.Globalization;
using System.Linq;
using System.Text;

namespace Geta.Optimizely.ContentTypeIcons.EnumGenerator
{
    internal static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string FormatSemver(this string value)
        {
            var requiredCount = 2;
            var separatorCount = value.Count(x => x == '.');
            var sb = new StringBuilder(value);

            for (var i = 0; i < requiredCount - separatorCount; i++)
            {
                sb.Append(".0");
            }
            
            return sb.ToString();
        }
    }
}
