using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Geta.Optimizely.ContentTypeIcons.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Geta.Optimizely.ContentTypeIcons.Tests
{
    public class ContentTypeIconControllerTests : IClassFixture<ContentTypeIconControllerFixture>
    {
        private readonly ContentTypeIconControllerFixture _fixture;

        public ContentTypeIconControllerTests(ContentTypeIconControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome), "fontawesome.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Brands), "fa-brands-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Regular), "fa-regular-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Solid), "fa-solid-900.ttf")]
        public void Index_FromDisk(int icon, string customFont)
        {
            // Arrange
            _fixture.Settings.Character = icon;
            _fixture.Settings.CustomFontName = customFont;

            // Act
            var result = _fixture.Controller.Index(_fixture.Settings) as FileStreamResult;

            // Assert
            var image = Image.FromStream(result?.FileStream);
            Assert.NotNull(image);
            Assert.True(GetUniqueImageColors(image).Count() > 1, "Image is blank.");
        }
        
        [Theory]
        [InlineData(0xe897, "icofont.ttf")]
        [InlineData(0xe898, "icofont.ttf")]
        [InlineData(0xe89a, "icofont.ttf")]
        [InlineData(0xe89b, "icofont.ttf")]
        [InlineData(0xf2b9, "fontawesome.ttf")]
        public void Index_FromDisk_IcoFont(int icon, string customFont)
        {
            // Arrange
            _fixture.Settings.Character = icon;
            _fixture.Settings.CustomFontName = customFont;

            // Act
            var result = _fixture.Controller.Index(_fixture.Settings) as FileStreamResult;

            // Assert
            var image = Image.FromStream(result?.FileStream);
            Assert.NotNull(image);
            Assert.True(GetUniqueImageColors(image).Count() > 1, "Image is blank.");
        }

        [Theory]
        [InlineData("#FFF")]
        [InlineData("#fff")]
        [InlineData("#fff000")]
        [InlineData("#000")]
        public void CheckValidFormatHtmlColor_Valid(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("red")]
        [InlineData("blue")]
        [InlineData("green")]
        [InlineData("white")]
        [InlineData("black")]
        [InlineData("Red")]
        [InlineData("Blue")]
        public void CheckValidFormatHtmlColor_ValidNamedColors(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.True(isValid, $"Expected '{color}' to be a valid named color");
        }

        [Theory]
        [InlineData("#FF")]
        [InlineData("#THISISATEST")]
        [InlineData("#-132332")]
        [InlineData("#00034333")]
        [InlineData("000")]
        public void CheckValidFormatHtmlColor_Invalid(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("notacolor")]
        [InlineData("invalidcolorname")]
        [InlineData("xyz123")]
        public void CheckValidFormatHtmlColor_InvalidNamedColors(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.False(isValid, $"Expected '{color}' to be an invalid named color");
        }

        public static IEnumerable<object[]> GetEnumValues(Type type)
        {
            return GetEnumValues(type, null);
        }

        public static IEnumerable<object[]> GetEnumValues(Type type, string fileName)
        {
            foreach (var icon in Enum.GetValues(type))
            {
                fileName = fileName ?? ImageUrlHelper.GetEmbeddedFontLocation((Enum)icon);
                yield return new[] { icon, fileName };
            }
        }

        private static IEnumerable<Color> GetUniqueImageColors(Image image)
        {
            using (var bitmap = new Bitmap(image))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                var bitmapBytes = new byte[bitmapData.Width * bitmapData.Height * 3];
                var colors = new Color[bitmapData.Width * bitmapData.Height];
                Marshal.Copy(bitmapData.Scan0, bitmapBytes, 0, bitmapBytes.Length);
                bitmap.UnlockBits(bitmapData);

                for (var i = 0; i < colors.Length; i++)
                {
                    var start = i * 3;
                    colors[i] = Color.FromArgb(bitmapBytes[start], bitmapBytes[start + 1], bitmapBytes[start + 2]);
                }

                return colors.Distinct();
            }
        }
    }
}
