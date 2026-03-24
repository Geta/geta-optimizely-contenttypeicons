using SixLabors.ImageSharp;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public interface IIconCacheProvider
    {
        /// <summary>
        /// Called once at application startup to allow the provider to perform any required initialization.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Tries to retrieve a cached icon image by key.
        /// </summary>
        bool TryGet(string key, out Image image);

        /// <summary>
        /// Stores an icon image in the cache under the given key.
        /// </summary>
        void Set(string key, Image image);
    }
}
