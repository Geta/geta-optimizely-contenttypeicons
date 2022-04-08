using System;
using System.IO;
using EPiServer.Framework.Internal;

namespace Geta.Optimizely.ContentTypeIcons.Tests
{
    public class TestPhysicalPathResolver : IPhysicalPathResolver
    {
        private readonly string _currentDirectory;
        private readonly string _appDataPath;

        public TestPhysicalPathResolver(string currentDirectory)
        {
            _currentDirectory = currentDirectory;
            _appDataPath = Path.Combine(_currentDirectory, "App_Data");
        }

        public string Rebase(string path)
        {
            if (path.StartsWith("[appDataPath]", StringComparison.OrdinalIgnoreCase))
            {
                var pathLength = path.Length;
                var placeHolderLength = "[appDataPath]".Length;
                var length = pathLength - placeHolderLength;
                var relativePath = path.Substring(placeHolderLength, length).TrimStart('\\', '/');
                path = Path.Combine(_appDataPath, relativePath);
            }
            path = Environment.ExpandEnvironmentVariables(path);
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(_currentDirectory, path);
            }
            return path;
        }
    }
}