using System.IO;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public class InMemoryIconCacheProvider : IIconCacheProvider
    {
        private const string CacheKeyPrefix = "geta.contenttypeicons.image.";

        private readonly IMemoryCache _cache;
        private readonly ContentTypeIconOptions _options;

        public InMemoryIconCacheProvider(IMemoryCache cache, IOptions<ContentTypeIconOptions> options)
        {
            _cache = cache;
            _options = options.Value;
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
            var entryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = _options.InMemoryCacheSlidingExpiration
            };
            _cache.Set(CacheKeyPrefix + key, ms.ToArray(), entryOptions);
        }
    }
}
