using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure
{
    public class PhysicalPathResolver
    {
        private readonly string _appDataPath;

        public PhysicalPathResolver(IWebHostEnvironment webHostEnvironment)
        {
            _appDataPath = Path.Combine(webHostEnvironment.ContentRootPath ?? string.Empty, "App_Data");
        }

        public string Rebase(string path) =>
            path?.Replace("[appDataPath]", _appDataPath, StringComparison.OrdinalIgnoreCase) ?? string.Empty;
    }
}
