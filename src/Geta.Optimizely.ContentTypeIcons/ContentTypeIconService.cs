using System;
using System.IO;
using System.Linq;
using System.Numerics;
using EPiServer.Framework.Internal;
using EPiServer.Shell;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Geta.Optimizely.ContentTypeIcons
{
    public class ContentTypeIconService : IContentTypeIconService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IPhysicalPathResolver _physicalPathResolver;
        private readonly IMemoryCache _cache;
        private readonly ContentTypeIconOptions _configuration;

        public ContentTypeIconService(
            IOptions<ContentTypeIconOptions> options,
            IFileProvider fileProvider,
            IPhysicalPathResolver physicalPathResolver,
            IMemoryCache cache)
        {
            _fileProvider = fileProvider;
            _physicalPathResolver = physicalPathResolver;
            _cache = cache;
            _configuration = options.Value;
        }

        /// <summary>
        /// Loads or creates a icon using the given settings
        /// </summary>
        /// <param name="settings">The ContentTypeIconSettings parameter</param>
        /// <returns></returns>
        public virtual Image LoadIconImage(ContentTypeIconSettings settings)
        {
            var fileName = settings.GetFileName(".png");
            var cachePath = GetFileFullPath(fileName);

            if (File.Exists(cachePath))
            {
                return Image.Load(cachePath);
            }

            using var stream = GenerateImage(settings);
            using var fileStream = File.Create(cachePath);
            using var img = Image.Load(stream);

            img.Save(fileStream, new PngEncoder());

            return img.Clone(_ => { });
        }

        protected virtual MemoryStream GenerateImage(ContentTypeIconSettings settings)
        {
            var family = settings.UseEmbeddedFont
                ? LoadFontFamilyFromClientResources(settings.EmbeddedFont)
                : LoadFontFamilyFromDisk(settings.CustomFontName);

            var font = family.CreateFont(settings.FontSize);

            using var image = new Image<Rgb24>(settings.Width, settings.Height);

            var center = new Vector2((float)image.Width / 2, (float)image.Height / 2);

            var textOptions = new RichTextOptions(font)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Origin = center
            };

            var character = char.ConvertFromUtf32(settings.Character);

            var background = Color.Parse(settings.BackgroundColor);
            var foreground = Color.Parse(settings.ForegroundColor);

            image.Mutate(i => i.Fill(background));
            image.Mutate(i => i.DrawText(textOptions, character, foreground));

            switch (settings.Rotate)
            {
                case Rotations.Rotate90:
                case Rotations.Rotate180:
                case Rotations.Rotate270:
                    image.Mutate(i => i.Rotate((float)settings.Rotate));
                    break;
                case Rotations.FlipHorizontal:
                    image.Mutate(i => i.Flip(FlipMode.Horizontal));
                    break;
                case Rotations.FlipVertical:
                    image.Mutate(i => i.Flip(FlipMode.Vertical));
                    break;
            }

            var stream = new MemoryStream();

            image.Save(stream, new PngEncoder());

            stream.Position = 0;

            return stream;
        }

        protected virtual FontFamily LoadFontFamilyFromClientResources(string fileName)
        {
            var cacheKey = $"geta.fontawesome.embedded.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out FontCollection fontCollection);

            if (fontCollection != null)
            {
                return fontCollection.Families.First();
            }

            try
            {
                var path = Paths.ToClientResource(Constants.ModuleName, $"ClientResources/{fileName}");

                fontCollection = new FontCollection();

                var file = _fileProvider.GetFileInfo(path);
                using var fontStream = file.CreateReadStream();
                fontCollection.Add(fontStream);

                _cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to load font {fileName} from EmbeddedResource", ex);
            }

            return fontCollection.Families.First();
        }

        protected virtual FontFamily LoadFontFamilyFromDisk(string fileName)
        {
            var cacheKey = $"geta.fontawesome.disk.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out FontCollection fontCollection);

            if (fontCollection != null)
            {
                return fontCollection.Families.First();
            }

            var customFontFolder = _configuration.CustomFontPath;
            var fontPath = $"{customFontFolder}{fileName}";

            var rebased = _physicalPathResolver.Rebase(fontPath);

            try
            {
                fontCollection = new FontCollection();
                fontCollection.Add(rebased);
                _cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to load custom font from path {fontPath}", ex);
            }

            return fontCollection.Families.First();
        }

        protected virtual string GetFileFullPath(string fileName)
        {
            var rootPath = _configuration.CachePath;
            return _physicalPathResolver.Rebase(rootPath + fileName);
        }
    }
}
