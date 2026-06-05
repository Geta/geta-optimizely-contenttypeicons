using System;
using System.Collections.Concurrent;
using System.IO;
using Geta.Optimizely.ContentTypeIcons.Infrastructure;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public class DiskIconCacheProvider : IIconCacheProvider
    {
        private readonly PhysicalPathResolver _pathResolver;
        private readonly ContentTypeIconOptions _options;
        private readonly ConcurrentDictionary<string, object> _locks = new();

        public DiskIconCacheProvider(
            IOptions<ContentTypeIconOptions> options,
            PhysicalPathResolver pathResolver)
        {
            _pathResolver = pathResolver;
            _options = options.Value;
        }

        public void Initialize()
        {
            var fullPath = _pathResolver.Rebase(_options.CachePath);
            if (!string.IsNullOrEmpty(fullPath) && !Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public bool TryGet(string key, out Image image)
        {
            var lockObj = _locks.GetOrAdd(key, _ => new object());
            lock (lockObj)
            {
                var fullPath = GetFullPath(key);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        image = Image.Load(fullPath);
                        return true;
                    }
                    catch
                    {
                        try { File.Delete(fullPath); } catch { }
                    }
                }

                image = null;
                return false;
            }
        }

        public void Set(string key, Image image)
        {
            var lockObj = _locks.GetOrAdd(key, _ => new object());
            lock (lockObj)
            {
                var fullPath = GetFullPath(key);
                using var fileStream = File.Create(fullPath);
                image.Save(fileStream, new PngEncoder());
            }
        }

        private string GetFullPath(string key)
        {
            return _pathResolver.Rebase(Path.Combine(_options.CachePath, key));
        }
    }
}
