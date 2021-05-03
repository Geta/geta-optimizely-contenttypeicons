using System;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Mvc;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Geta.Optimizely.ContentTypeIcons.Controllers
{
    public class ContentTypeIconController : Controller
    {
        private readonly IContentTypeIconService _contentTypeIconService;

        public ContentTypeIconController(IContentTypeIconService contentTypeIconService)
        {
            _contentTypeIconService = contentTypeIconService ?? throw new ArgumentNullException(nameof(contentTypeIconService));
        }

        [Authorize(Roles = "Administrators, CmsAdmins, CmsEditors, WebAdmins, WebEditors, ThumbnailGroup")]
        public ActionResult GenerateIcon(ContentTypeIconSettings settings)
        {
            if (!CheckValidFormatHtmlColor(settings.BackgroundColor) || !CheckValidFormatHtmlColor(settings.ForegroundColor))
            {
                throw new Exception("Unknown foreground or background color");
            }

            var image = _contentTypeIconService.LoadIconImage(settings);

            return new ImageResult { Image = image, ImageFormat = ImageFormat.Png };
        }

        internal bool CheckValidFormatHtmlColor(string inputColor)
        {
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                return true;

            var result = System.Drawing.Color.FromName(inputColor);
            return result.IsKnownColor;
        }
    }
}
