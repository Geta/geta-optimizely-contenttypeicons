using System;
using System.IO;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Png;

namespace Geta.Optimizely.ContentTypeIcons.Controllers
{
    [Route(Constants.RouteTemplate)]
    public class ContentTypeIconController : Controller
    {
        private readonly IContentTypeIconService _contentTypeIconService;

        public ContentTypeIconController(IContentTypeIconService contentTypeIconService)
        {
            _contentTypeIconService = contentTypeIconService ?? throw new ArgumentNullException(nameof(contentTypeIconService));
        }

        [Authorize(Roles = "Administrators, CmsAdmins, CmsEditors, WebAdmins, WebEditors, ThumbnailGroup")]
        public ActionResult Index(ContentTypeIconSettings settings)
        {
            if (!CheckValidFormatHtmlColor(settings.BackgroundColor) || !CheckValidFormatHtmlColor(settings.ForegroundColor))
            {
                throw new InvalidOperationException("Unknown foreground or background color");
            }

            using var image = _contentTypeIconService.LoadIconImage(settings);

            var stream = new MemoryStream();
            image.Save(stream, new PngEncoder());
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }

        internal static bool CheckValidFormatHtmlColor(string inputColor)
        {
            // Use SixLabors.ImageSharp.Color.TryParse which supports both hex colors and named colors
            // This is cross-platform compatible and doesn't require runtime configuration switches
            return SixLabors.ImageSharp.Color.TryParse(inputColor, out _);
        }
    }
}
