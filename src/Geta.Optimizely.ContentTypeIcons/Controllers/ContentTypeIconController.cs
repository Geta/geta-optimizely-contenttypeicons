using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                throw new Exception("Unknown foreground or background color");
            }

            var image = _contentTypeIconService.LoadIconImage(settings);

            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }

        internal bool CheckValidFormatHtmlColor(string inputColor)
        {
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success) return true;

            var result = System.Drawing.Color.FromName(inputColor);
            return result.IsKnownColor;
        }
    }
}
