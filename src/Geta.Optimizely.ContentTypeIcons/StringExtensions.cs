using System.Globalization;
using System.Linq;
using System.Text;

namespace Geta.Optimizely.ContentTypeIcons
{
    internal static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }
        
        public static string ToDashCase(this string input)
        {
            bool ShouldAddDash(int i, char c)
            {
                return i > 0
                       && char.IsUpper(c)
                       && (!char.IsDigit(input[i - 1]) || !char.IsDigit(input[i - 2 > 0 ? i - 2 : 0]));
            }

            return string.Concat(input.Select((c, i) =>
                                                  ShouldAddDash(i, c)
                                                      ? "-" + c
                                                      : c.ToString())).ToLower();
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
