using Geta.Optimizely.ContentTypeIcons.Settings;
using SixLabors.ImageSharp;

namespace Geta.Optimizely.ContentTypeIcons
{
    public interface IContentTypeIconService
    {
        /// <summary>
        /// Loads or creates a icon using the given settings
        /// </summary>
        /// <param name="settings">The ContentTypeIconSettings parameter</param>
        /// <returns></returns>
        Image LoadIconImage(ContentTypeIconSettings settings);
    }
}