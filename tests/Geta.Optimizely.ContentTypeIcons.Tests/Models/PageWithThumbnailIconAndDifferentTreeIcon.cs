using EPiServer.Core;
using Geta.Optimizely.ContentTypeIcons.Attributes;

namespace Geta.Optimizely.ContentTypeIcons.Tests.Models
{
    [ContentTypeIcon(FontAwesome5Solid.Clock)]
    [TreeIcon(FontAwesome5Regular.Clock)]
    public class PageWithContentTypeIconAndDifferentTreeIcon : PageData
    {
    }
}
