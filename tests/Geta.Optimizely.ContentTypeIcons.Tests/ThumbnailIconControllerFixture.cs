using System;
using System.IO;
using FakeItEasy;
using Geta.Optimizely.ContentTypeIcons.Controllers;
using Geta.Optimizely.ContentTypeIcons.Infrastructure;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Settings;
using Microsoft.AspNetCore.Hosting;
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
            var currentDirectory = GetProjectDirectory();
            var appDataPath = Path.Combine(currentDirectory, "App_Data");
            var guid = Guid.NewGuid().ToString();
            var cachePath = $"[appDataPath]/thumb_cache/{guid}/";
            _temporaryDirectory = Path.Combine(appDataPath, "thumb_cache", guid);
            Directory.CreateDirectory(_temporaryDirectory);

            var fakeEnv = A.Fake<IWebHostEnvironment>();
            A.CallTo(() => fakeEnv.ContentRootPath).Returns(currentDirectory);

            var options = Options.Create(new ContentTypeIconOptions
            {
                CachePath = cachePath
            });

            var fileProvider = new PhysicalFileProvider(currentDirectory);
            var pathResolver = new PhysicalPathResolver(fakeEnv);
            var service = new ContentTypeIconService(
                options,
                fileProvider,
                pathResolver,
                new MemoryCache(new MemoryCacheOptions()));
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

        private static string GetProjectDirectory()
        {
            var currentDir = AppContext.BaseDirectory;
            var idx = currentDir.IndexOf("bin", StringComparison.InvariantCulture);
            return currentDir.Substring(0, idx);
        }

        public void Dispose()
        {
            Directory.Delete(_temporaryDirectory, true);
            GC.SuppressFinalize(this);
        }
    }
}
