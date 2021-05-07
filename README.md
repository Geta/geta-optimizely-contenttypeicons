# Geta.Optimizely.ContentTypeIcons

* Master<br>
![](http://tc.geta.no/app/rest/builds/buildType:(id:GetaPackages_EPiFontThumbnail_00ci),branch:master/statusIcon)

## Description
This package consists of an override to the built in "ImageUrlAttribute" that is used to specify preview images for the different content types in your Optimizely project. The only difference is that with this attribute, the images are generated using a configured background color, foreground color and a reference to a FontAwesome icon. The same configured icon can also replace the tree icon in the page tree (feature needs to be explicitly turned on using configuration).

![Screenshot of package](/images/fontthumbnail_overview.jpg)

## Features
* Generates preview images for the different content types in your Optimizely project
* Replace tree icons with custom icons for content types
* Support for using Font Awesome Free 5 and 4 icons
* Supports customized foreground and background color on generated images
* Loading custom fonts

## How to get started?
* Install NuGet package (use [Optimizely Nuget](http://nuget.episerver.com))
* ``Install-Package Geta.Optimizely.ContentTypeIcons``

_Please notice that this attribute cannot be used in conjunction with any other attributes inheriting from ImageUrlAttribute (for example SiteImageUrl or basic ImageUrl-attributes) on the same contenttype. All existing ImageUrl-attributes on the contenttype where you want to use this, needs to be removed._

## Details
Using the built in ImageUrlAttribute, you specify the images to be presented like this:
```cs
[ImageUrlAttribute("~/images/contenttypes/articlepage.png")]
```

Using this package you can specify it like this instead:
```cs
[ContentTypeIcon(FontAwesome5Brands.Github)]
```
There are a couple different enum types available: `FontAwesome5Brands`, `FontAwesome5Regular` and `FontAwesome5Solid` for the different Font Awesome 5 styles. There is also the `FontAwesome` enum for the Font Awesome version 4 icons.

Or with overriddes for specifying different colors and size:
```cs
[ContentTypeIcon(FontAwesome5Brands.Github,"#000000","#ffffff",40)]
```
The defaults if nothing else is specified is of course the Geta colors as seen in the screenshot.

The images that gets generated are cached in [appDataPath]\thumb_cache\

## Configuration

For the ConentTypeIcons to work, you have to call `AddContentTypeIcons` extension method in `Startup.ConfigureServices` method. This method provides a configuration of default values and allows to enable tree icon feature. Below is a code with all possible configuration options:

```cs
services.AddContentTypeIcons(x =>
{
    x.EnableTreeIcons = true;
    x.ForegroundColor = "#ffffff";
    x.BackgroundColor = "#02423F";
    x.FontSize = 40;
    x.CachePath = "[appDataPath]\\thumb_cache\\";
    x.CustomFontPath = "[appDataPath]\\fonts\\";
});
```

It is also possible to configure the application in `appsettings.json` file. A configuration from the `appsettings.json` will override configuration configured in `Startup`. Below is an `appsettings.json` configuration example. In the example, the tree icon feature is enabled even if it would be false in the Startup. Although, it will not override other configuration properties as those are not configured in the `appsettings.json`.

```json
"Geta": {
    "ContentTypeIcons": {
        "EnableTreeIcons":  true
    }
}
```

### Tree icons

![Screenshot of package](/images/treeicon_overview.jpg)

You can enable or disable the tree icons by setting `EnableTreeIcons` value.

By default the same icons will be used in the content tree if you have defined an icon using the ContentTypeIcon-attribute.
```cs
[ContentTypeIcon(FontAwesome5Brands.Github)]
```

You can however disable this for specific content types using the ignore property on the TreeIcon-attribute on the content type, like this:

```cs
[ContentType(DisplayName = "Blog List Page")]
[ContentTypeIcon(FontAwesome5Solid.Blog)]
[TreeIcon(Ignore = true)]
public class BlogListPage : FoundationPageData
{
    ...
}
```

There is also support for overriding the icon defined in the ContentTypeIcon-attribute like this:
```cs
[ContentType(DisplayName = "Blog List Page")]
[ContentTypeIcon(FontAwesome5Solid.Blog)]
[TreeIcon(FontAwesome5Solid.CheckDouble)]
public class BlogListPage : FoundationPageData
{
    ...
}
```

or with the optional rotation (FlipVertical, FlipHorizontal, Rotate90, Rotate180, Rotate270):
```cs
[TreeIcon(FontAwesome5Solid.CheckDouble, Rotations.Rotate90)]
```

### Fonts

You can configure default foreground and background colors and font size by setting `ForegroundColor`, `BackgroundColor` and `FontSize` values.

### Loading custom fonts

To load custom icon fonts, you can place the font you want to use in the default folder `[appDataPath]\fonts\`. This can also be customized using configuration property - `CustomFontPath`.

_You also have to make sure to set the properties of the custom font to "Copy to output directory"_

Then specify the font and the character reference from the specific font to use in the `ContentTypeIcon` attribute constructor like this.

```cs
[ContentTypeIcon("fontello.ttf",0xe801)]
```

**If you are unsure about what value to enter as a character reference**

Usually when you download an icon font, you also get an accompanying css file for with character references, which can look like this:
```css
.icofont-brand-adidas:before
{
  content: "\e897";
}
```

Take the content reference from the css (\e897) and replace the \ with 0x so the end result is 0xe897 instead of "\e897" and use that value for referencing the correct character in the attribute like this:

```cs
[ContentTypeIcon("customfont.ttf",0xe897)]
```

### Caching

All generated icon images are stored in the cache folder and then served instead of regenerating those. A default cache folder can be changed by configuring the `CachePath` property.

## Changelog

[Changelog](CHANGELOG.md)


## More info
https://getadigital.com/blog/contenttype-preview-images-w.-icons/

https://getadigital.com/blog/new-version-of-fontthumbnail/

## Package maintainer
https://github.com/degborta

## Contributors
https://github.com/johanbenschop
