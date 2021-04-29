using EPiServer.Framework.TypeScanner;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.FileProviders;

namespace Geta.Optimizely.ContentTypeIcons
{
    public class FontThumbnailModule : ShellModule
    {
        public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath)
            : base(name, routeBasePath, resourceBasePath)
        {
        }

        public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath, ITypeScannerLookup typeScannerLookup, IFileProvider virtualPathProvider)
            : base(name, routeBasePath, resourceBasePath, typeScannerLookup, virtualPathProvider)
        {
        }
    }
}