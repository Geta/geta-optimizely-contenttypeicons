using System;
using System.IO;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons.Controllers;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons.Tests
{
    public class ContentTypeIconControllerFixture : IDisposable
    {
        internal readonly ContentTypeIconController Controller;
        internal readonly ContentTypeIconSettings Settings;
        private readonly string _temporaryDirectory;

        public ContentTypeIconControllerFixture()
        {
            var currentDirectory = SetCurrentDirectory();

            var partialDirectory = $"[appDataPath]\\thumb_cache\\{Guid.NewGuid()}\\";
            _temporaryDirectory = VirtualPathUtilityEx.RebasePhysicalPath(partialDirectory);

            Directory.CreateDirectory(_temporaryDirectory);

            var options = Options.Create(new ContentTypeIconOptions
            {
                CachePath = partialDirectory
            });

            var moduleTable = new ModuleTable();
            var fileProvider = new PhysicalFileProvider(currentDirectory);
            var service = new ContentTypeIconService(options, fileProvider, new MemoryCache(new MemoryCacheOptions()), moduleTable);
            Controller = new ContentTypeIconController(service);
            Settings = new ContentTypeIconSettings
            {
                FontSize = ContentTypeIconOptions.DefaultFontSize,
                BackgroundColor = ContentTypeIconOptions.DefaultBackgroundColor,
                ForegroundColor = ContentTypeIconOptions.DefaultForegroundColor,
                Height = ContentTypeIconOptions.DefaultHeight,
                Width = ContentTypeIconOptions.DefaultWidth
            };
        }

        private static string SetCurrentDirectory()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var idx = currentDir.IndexOf("bin", StringComparison.InvariantCulture);
            var projectDir = currentDir.Substring(0, idx);
            Directory.SetCurrentDirectory(projectDir);
            return projectDir;
        }

        public void Dispose()
        {
            Directory.Delete(_temporaryDirectory, true);
        }
    }
}
