using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure
{
    public class PhysicalPathResolver
    {
        private readonly string _contentRootPath;
        private readonly string _appDataPath;

        public PhysicalPathResolver(IWebHostEnvironment webHostEnvironment)
        {
            _contentRootPath = webHostEnvironment.ContentRootPath ?? string.Empty;
            _appDataPath = Path.Combine(_contentRootPath, "App_Data");
        }

        public string Rebase(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            if (path.StartsWith("[appDataPath]", StringComparison.OrdinalIgnoreCase))
            {
                var relativePath = path.Substring("[appDataPath]".Length).TrimStart(Path.DirectorySeparatorChar);
                path = Path.Combine(_appDataPath, relativePath);
            }

            path = Environment.ExpandEnvironmentVariables(path);

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(_contentRootPath, path);
            }

            return path;
        }
    }
}
