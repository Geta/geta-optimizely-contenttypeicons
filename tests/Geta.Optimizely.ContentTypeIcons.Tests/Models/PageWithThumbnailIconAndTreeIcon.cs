﻿using EPiServer.Core;
using Geta.Optimizely.ContentTypeIcons.Attributes;

namespace Geta.Optimizely.ContentTypeIcons.Tests.Models
{
    [ContentTypeIcon(FontAwesome5Solid.Anchor)]
    [TreeIcon]
    public class PageWithContentTypeIconAndTreeIcon : PageData
    {
    }
}
