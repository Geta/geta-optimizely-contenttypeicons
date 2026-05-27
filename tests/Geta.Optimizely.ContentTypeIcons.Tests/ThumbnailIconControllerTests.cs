using System;
using System.Collections.Generic;
using System.Linq;
using Geta.Optimizely.ContentTypeIcons.Controllers;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
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
            Assert.NotNull(result);
            using var image = Image.Load<Rgba32>(result.FileStream);
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
            Assert.NotNull(result);
            using var image = Image.Load<Rgba32>(result.FileStream);
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
            // Act
            var isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

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
            // Act
            var isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.True(isValid, $"Expected '{color}' to be a valid named color");
        }

        [Theory]
        [InlineData("#FF")]
        [InlineData("#THISISATEST")]
        [InlineData("#-132332")]
        public void CheckValidFormatHtmlColor_Invalid(string color)
        {
            // Act
            var isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("notacolor")]
        [InlineData("invalidcolorname")]
        [InlineData("xyz123")]
        public void CheckValidFormatHtmlColor_InvalidNamedColors(string color)
        {
            // Act
            var isValid = ContentTypeIconController.CheckValidFormatHtmlColor(color);

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

        private static IEnumerable<Color> GetUniqueImageColors(Image<Rgba32> image)
        {
            var colors = new HashSet<Color>();
            image.ProcessPixelRows(accessor =>
            {
                for (var y = 0; y < accessor.Height; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    foreach (ref var pixel in row)
                    {
                        colors.Add(new Color(pixel));
                    }
                }
            });
            return colors;
        }
    }
}
