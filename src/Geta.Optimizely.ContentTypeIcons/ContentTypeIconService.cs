using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using EPiServer.Shell;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Geta.Optimizely.ContentTypeIcons
{
    public class ContentTypeIconService : IContentTypeIconService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMemoryCache _cache;
        private readonly ContentTypeIconOptions _configuration;

        public ContentTypeIconService(
            IOptions<ContentTypeIconOptions> options,
            IFileProvider fileProvider,
            IMemoryCache cache)
        {
            _fileProvider = fileProvider;
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

            /*if (File.Exists(cachePath))
            {
                return Image.Load(cachePath, new PngDecoder());
            }*/

            using var stream = GenerateImage(settings);
            using var fileStream = File.Create(cachePath);
            using var img = Image.Load(stream, new PngDecoder());

            img.Save(fileStream, new PngEncoder());

            return img.Clone(_ => { });
        }

        internal virtual MemoryStream GenerateImage(ContentTypeIconSettings settings)
        {
            var stream = new MemoryStream();

            var family = settings.UseEmbeddedFont
                ? LoadFontFamilyFromClientResources(settings.EmbeddedFont)
                : LoadFontFamilyFromDisk(settings.CustomFontName);

            var font = family.CreateFont(settings.FontSize);

            using var img = new Image<Rgb24>(settings.Width, settings.Height);
            
            Vector2 center = new Vector2(img.Width / 2, img.Height / 2);

            TextOptions textOptions = new(font)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Origin = center
            };

            var chr = char.ConvertFromUtf32(settings.Character);
            
            var bg = Color.Parse(settings.BackgroundColor);
            var fg = Color.Parse(settings.ForegroundColor);
            img.Mutate(i => i.Fill(bg));
            
            img.Mutate(i => i.DrawText(textOptions, chr, fg));
            
            img.Save(stream, new PngEncoder());

            stream.Position = 0;

            return stream;
            

            /*
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            switch (settings.Rotate)
            {
                case Rotations.Rotate90:
                case Rotations.Rotate180:
                case Rotations.Rotate270:
                    g.TranslateTransform(settings.Width / 2, settings.Height / 2);
                    g.RotateTransform((int)settings.Rotate);
                    g.TranslateTransform(-(settings.Width / 2), -(settings.Height / 2));
                    break;
            }

            switch (settings.Rotate)
            {
                case Rotations.FlipHorizontal:
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case Rotations.FlipVertical:
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
            }

            bitmap.Save(stream, ImageFormat.Png);

            return stream;*/
        }

        protected virtual SixLabors.Fonts.FontFamily LoadFontFamilyFromClientResources(string fileName)
        {
            var cacheKey = $"geta.fontawesome.embedded.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out FontCollection fontCollection);

            if (fontCollection == null)
            {
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
                    throw new Exception($"Unable to load font {fileName} from EmbeddedResource", ex);
                }
            }

            return fontCollection.Families.First();
        }

        protected virtual FontFamily LoadFontFamilyFromDisk(string fileName)
        {
            var cacheKey = $"geta.fontawesome.disk.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out FontCollection fontCollection);

            if (fontCollection == null)
            {
                var customFontFolder = _configuration.CustomFontPath;
                var fontPath = $"{customFontFolder}{fileName}";

                var rebased = VirtualPathUtilityEx.RebasePhysicalPath(fontPath);

                try
                {
                    fontCollection = new FontCollection();
                    fontCollection.Add(rebased);
                    // RemoveFontResourceEx(rebased, 16, IntPtr.Zero);
                    _cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to load custom font from path {fontPath}", ex);
                }
            }

            return fontCollection.Families.First();
        }

        protected virtual string GetFileFullPath(string fileName)
        {
            var rootPath = _configuration.CachePath;
            return VirtualPathUtilityEx.RebasePhysicalPath(rootPath + fileName);
        }

        // https://stackoverflow.com/questions/26671026/how-to-delete-the-file-of-a-privatefontcollection-addfontfile
        // Force unregister of font in GDI32 because of bug
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int RemoveFontResourceEx(string lpszFilename, int fl, IntPtr pdv);
    }
}