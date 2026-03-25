using System.IO;
using EPiServer.Framework.Internal;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public class DiskIconCacheProvider : IIconCacheProvider
    {
        private readonly IPhysicalPathResolver _physicalPathResolver;
        private readonly ContentTypeIconOptions _options;

        public DiskIconCacheProvider(
            IOptions<ContentTypeIconOptions> options,
            IPhysicalPathResolver physicalPathResolver)
        {
            _physicalPathResolver = physicalPathResolver;
            _options = options.Value;
        }

        public void Initialize()
        {
            var fullPath = _physicalPathResolver.Rebase(_options.CachePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public bool TryGet(string key, out Image image)
        {
            var fullPath = GetFullPath(key);
            if (File.Exists(fullPath))
            {
                image = Image.Load(fullPath);
                return true;
            }

            image = null;
            return false;
        }

        public void Set(string key, Image image)
        {
            var fullPath = GetFullPath(key);
            using var fileStream = File.Create(fullPath);
            image.Save(fileStream, new PngEncoder());
        }

        private string GetFullPath(string key)
        {
            return _physicalPathResolver.Rebase(_options.CachePath + key);
        }
    }
}
