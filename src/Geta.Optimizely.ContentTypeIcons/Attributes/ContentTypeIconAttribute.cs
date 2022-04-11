using System;
using EPiServer.DataAnnotations;

namespace Geta.Optimizely.ContentTypeIcons.Attributes
{
    /// <summary>Assigns an icon to be used when rendering the preview image.</summary>
    /// <remarks>
    /// Used by <see cref="T:EPiServer.DataAbstraction.ContentType" /> to set the image rendered for the preview when creating the content.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContentTypeIconAttribute : ImageUrlAttribute
    {
        public string CustomFont { get; }
        public int Character { get; }
        internal Enum Icon { get; }
        internal string BackgroundColor { get; }
        internal string ForegroundColor { get; }
        internal int FontSize { get; }
        internal Rotations Rotate { get; } = Rotations.None;
        internal bool IsCustomFont { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ContentTypeIconAttribute(
            FontAwesome icon,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1,
            Rotations rotate = Rotations.None)
            : base(string.Empty)
        {
            Icon = icon;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ContentTypeIconAttribute(
            FontAwesome5Brands icon,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1,
            Rotations rotate = Rotations.None)
            : base(string.Empty)
        {
            Icon = icon;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ContentTypeIconAttribute(
            FontAwesome5Regular icon,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1,
            Rotations rotate = Rotations.None)
            : base(string.Empty)
        {
            Icon = icon;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ContentTypeIconAttribute(
            FontAwesome5Solid icon,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1,
            Rotations rotate = Rotations.None)
            : base(string.Empty)
        {
            Icon = icon;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ContentTypeIconAttribute(
            FontAwesome icon,
            Rotations rotate = Rotations.None,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1)
            : base(string.Empty)
        {
            Icon = icon;
            Rotate = rotate;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ContentTypeIconAttribute(
            FontAwesome5Brands icon,
            Rotations rotate = Rotations.None,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1)
            : base(string.Empty)
        {
            Icon = icon;
            Rotate = rotate;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ContentTypeIconAttribute(
            FontAwesome5Regular icon,
            Rotations rotate = Rotations.None,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1)
            : base(string.Empty)
        {
            Icon = icon;
            Rotate = rotate;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ContentTypeIconAttribute(
            FontAwesome5Solid icon,
            Rotations rotate = Rotations.None,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1)
            : base(string.Empty)
        {
            Icon = icon;
            Rotate = rotate;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Optimizely.ContentTypeIcons.Attributes.ContentTypeIconAttribute" /> class.
        /// </summary>
        /// <param name="customFont"></param>
        /// <param name="character"></param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ContentTypeIconAttribute(
            string customFont,
            int character,
            string backgroundColor = "",
            string foregroundColor = "",
            int fontSize = -1)
            : base(string.Empty)
        {
            IsCustomFont = true;
            CustomFont = customFont;
            Character = character;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            FontSize = fontSize;
        }

        public override string Path =>
            IsCustomFont
                ? ImageUrlHelper.GetUrl(CustomFont, Character, BackgroundColor, ForegroundColor, FontSize)
                : ImageUrlHelper.GetUrl(Icon, BackgroundColor, ForegroundColor, FontSize, Rotate);
    }
}
