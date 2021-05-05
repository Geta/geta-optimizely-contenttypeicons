using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using EPiServer.Shell;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

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

            if (File.Exists(cachePath))
            {
                using var bmpTemp = new Bitmap(cachePath);
                return new Bitmap(bmpTemp);
            }
            else
            {
                using var stream = GenerateImage(settings);
                using var fileStream = File.Create(cachePath);
                using var bmpTemp = new Bitmap(stream);

                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                stream.Dispose();
                stream.Close();

                return new Bitmap(bmpTemp);
            }
        }

        internal virtual MemoryStream GenerateImage(ContentTypeIconSettings settings)
        {
            var family = settings.UseEmbeddedFont
                ? LoadFontFamilyFromClientResources(settings.EmbeddedFont)
                : LoadFontFamilyFromDisk(settings.CustomFontName);

            var cc = new ColorConverter();
            var bg = (Color) cc.ConvertFrom(settings.BackgroundColor);
            var fg = (Color) cc.ConvertFrom(settings.ForegroundColor);

            var stream = new MemoryStream();

            using (var font = new Font(family, settings.FontSize))
            {
                using var bitmap = new Bitmap(settings.Width, settings.Height, PixelFormat.Format24bppRgb);
                using var g = Graphics.FromImage(bitmap);

                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(bg);

                switch (settings.Rotate)
                {
                    case Rotations.Rotate90:
                    case Rotations.Rotate180:
                    case Rotations.Rotate270:
                        g.TranslateTransform(settings.Width / 2, settings.Height / 2);
                        g.RotateTransform((int) settings.Rotate);
                        g.TranslateTransform(-(settings.Width / 2), -(settings.Height / 2));
                        break;
                }

                var format1 = new StringFormat(StringFormatFlags.NoClip);
                using (var format = new StringFormat(StringFormatFlags.NoClip))
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    var displayRectangle = new Rectangle(new Point(0, 0), new Size(settings.Width, settings.Height));
                    var chr = char.ConvertFromUtf32(settings.Character);

                    using var brush = new SolidBrush(fg);

                    g.DrawString(chr, font, brush, displayRectangle, format);
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
            }

            family.Dispose();
            return stream;
        }

        protected virtual FontFamily LoadFontFamilyFromClientResources(string fileName)
        {
            var cacheKey = $"geta.fontawesome.embedded.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out PrivateFontCollection fontCollection);

            if (fontCollection == null)
            {
                try
                {
                    fontCollection = new PrivateFontCollection();

                    // var resource = $"{Constants.EmbeddedFontPath}.{fileName}";

                    var path = Paths.ToResource(Constants.ModuleName, fileName);
                    var file = _fileProvider.GetFileInfo(path);
                    var fontStream = file.CreateReadStream();
                    // create an unsafe memory block for the font data
                    var data = Marshal.AllocCoTaskMem((int) fontStream.Length);
                    // create a buffer to read in to
                    var fontdata = new byte[fontStream.Length];
                    // read the font data from the resource
                    fontStream.Read(fontdata, 0, (int) fontStream.Length);
                    // copy the bytes to the unsafe memory block
                    Marshal.Copy(fontdata, 0, data, (int) fontStream.Length);
                    // pass the font to the font collection
                    fontCollection.AddMemoryFont(data, (int) fontStream.Length);
                    // close the resource stream
                    fontStream.Close();
                    fontStream.Dispose();
                    // free the unsafe memory
                    Marshal.FreeCoTaskMem(data);

                    _cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to load font {fileName} from EmbeddedResource", ex);
                }
            }

            return fontCollection.Families[0];
        }

        protected virtual FontFamily LoadFontFamilyFromDisk(string fileName)
        {
            var cacheKey = $"geta.fontawesome.disk.fontcollection.{fileName}";
            _cache.TryGetValue(cacheKey, out PrivateFontCollection fontCollection);

            if (fontCollection == null)
            {
                var customFontFolder = _configuration.CustomFontPath;
                var fontPath = $"{customFontFolder}{fileName}";

                var rebased = VirtualPathUtilityEx.RebasePhysicalPath(fontPath);

                try
                {
                    fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile(rebased);
                    RemoveFontResourceEx(rebased, 16, IntPtr.Zero);
                    _cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to load custom font from path {fontPath}", ex);
                }
            }

            return fontCollection.Families[0];
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