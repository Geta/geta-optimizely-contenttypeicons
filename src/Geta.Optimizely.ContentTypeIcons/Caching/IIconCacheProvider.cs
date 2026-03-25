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
        /// <param name="key">The cache key for the icon image.</param>
        /// <param name="image">
        /// When this method returns <c>true</c>, contains a newly created <see cref="Image"/> instance
        /// representing the cached icon. Callers own the returned image and are responsible for disposing it.
        /// Implementations must not return a shared or reused <see cref="Image"/> instance that may be
        /// disposed or mutated elsewhere.
        /// </param>
        /// <returns>
        /// <c>true</c> if an icon image was found for the specified key; otherwise, <c>false</c>. When this
        /// method returns <c>false</c>, <paramref name="image"/> must be set to <c>null</c>.
        /// </returns>
        bool TryGet(string key, out Image image);

        /// <summary>
        /// Stores an icon image in the cache under the given key.
        /// </summary>
        /// <param name="key">The cache key under which to store the icon image.</param>
        /// <param name="image">
        /// The icon image to cache. The caller will dispose this <see cref="Image"/> instance immediately
        /// after <see cref="Set"/> returns, so implementations must not store or otherwise retain a reference
        /// to this instance. Providers that need to keep the image must clone, serialize, or otherwise create
        /// their own representation for long-term storage.
        /// </param>
        void Set(string key, Image image);
    }
}
