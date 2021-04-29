using System.Drawing;
using Geta.Optimizely.ContentTypeIcons.Settings;

namespace Geta.Optimizely.ContentTypeIcons
{
    public interface IFontThumbnailService
    {
        /// <summary>
        /// Loads or creates a thumbnail using the given settings
        /// </summary>
        /// <param name="settings">The ThumbnailSettings parameter</param>
        /// <returns></returns>
        Image LoadThumbnailImage(ThumbnailSettings settings);
    }
}