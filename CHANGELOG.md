# Changelog

All notable changes to this project will be documented in this file.

## [3.0.0]

### Changed
- Updated to .NET 8
- Updated SixLabors.ImageSharp from 3.0.1 to 3.1.12+ (fixes CVE-2024-32036, CVE-2024-27929, CVE-2024-32035)
- Updated SixLabors.Fonts from 1.0.0 to 2.1.3+
- Updated SixLabors.ImageSharp.Drawing from 1.0.0 to 2.1.7+
- Updated test dependencies to latest versions
- Made SixLabors package dependencies open-ended to allow future updates

## [2.0.1]
- Small refactorings

## [2.0.0]

### Changed
- Linux support - using ImageSharp to generate content icon images
- Added initialization by calling UseContentTypeIcons

### Notes
This version requires to explicitly install *EPiServer.CMS.AspNetCore.Templating* and *EPiServer.Framework* version 12.4.2+. Starting from this version, Optimizely (Episerver) is using latest *ImageSharp* which is required to generate images from fonts.

## [1.2.4]

### Changed
- Updated Font Awesome to release 5.13.0

## [1.2.3]

### Changed
- Added some more tests for using custom fonts
- Added XML docs, mainly for the attributes (ThumbnailAttribute and TreeIconAttribute)

## [1.2.0]

### Changed
- Updated FontAwesome to release 5.12.1
- Added custom authorize group "ThumbnailGroup"
- Security update
- New default background color
- Added more detailed documentation

## [1.1.5]

### Changed
- Added support for custom tree icons, thanks to https://github.com/johanbenschop

## [1.1.4]

### Changed
- Update Font Awesome to release 5.7.1.

## [1.1.3]

### Changed
- Update Font Awesome to release 5.6.3
- Update readme

## [1.1.2]

### Changed
- Added support for Font Awesome version 5.3.0.

## [1.0.9]

### Changed
- Episerver 11 update and changed web project to a class libary.

## [1.0.6]

### Changed
- Bugfix, issue with locked custom fonts
- Added support for loading custom fonts from disk

## [1.0.4]

### Changed
- Updated FontAwesome enum with support for all 4.7 fonts

## [1.0.3]

### Changed
- Bugfix: Changed to loading font from embedded resource instead of from file module, to prevent locking of font file.
- Bugfix: Changed to working with cloned image to prevent locking of generated thumbnails

## [1.0.2]

### Changed
- Initial release
