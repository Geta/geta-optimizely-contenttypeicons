using EPiServer.Core;
using Geta.Optimizely.ContentTypeIcons.Attributes;

namespace Geta.Optimizely.ContentTypeIcons.Tests.Models
{
    [ContentTypeIcon(FontAwesome5Solid.Cookie, Rotations.Rotate180)]
    public class PageWithContentTypeIconAndRotation : PageData
    {
    }
}
