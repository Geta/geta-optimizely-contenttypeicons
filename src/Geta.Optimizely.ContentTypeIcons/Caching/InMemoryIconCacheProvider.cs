using System.IO;
using Microsoft.Extensions.Caching.Memory;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public class InMemoryIconCacheProvider : IIconCacheProvider
    {
        private const string CacheKeyPrefix = "geta.contenttypeicons.image.";

        private readonly IMemoryCache _cache;

        public InMemoryIconCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Initialize() { }

        public bool TryGet(string key, out Image image)
        {
            if (_cache.TryGetValue(CacheKeyPrefix + key, out byte[] bytes))
            {
                image = Image.Load(bytes);
                return true;
            }

            image = null;
            return false;
        }

        public void Set(string key, Image image)
        {
            using var ms = new MemoryStream();
            image.Save(ms, new PngEncoder());
            _cache.Set(CacheKeyPrefix + key, ms.ToArray());
        }
    }
}
