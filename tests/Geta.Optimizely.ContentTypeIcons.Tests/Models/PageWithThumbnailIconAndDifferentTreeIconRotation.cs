using EPiServer.Core;
using Geta.Optimizely.ContentTypeIcons.Attributes;

namespace Geta.Optimizely.ContentTypeIcons.Tests.Models
{
    [ContentTypeIcon(FontAwesome5Solid.Wind, Rotations.Rotate180)]
    [TreeIcon(FontAwesome5Regular.Flag, Rotations.Rotate90)]
    public class PageWithContentTypeIconAndDifferentTreeIconRotation : PageData
    {
    }
}
