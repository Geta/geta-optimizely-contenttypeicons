﻿using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AlloyTemplates.Models.Pages
{
    /// <summary>
    /// Used for the pages mainly consisting of manually created content such as text, images, and blocks
    /// </summary>
    [SiteContentType(GUID = "9CCC8A41-5C8C-4BE0-8E73-520FF3DE8267")]
    [ContentTypeIcon(FontAwesome5Brands.Github, Rotations.None, "#ff0000", "#ffffff", 40)]
    public class StandardPage : SitePageData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}
