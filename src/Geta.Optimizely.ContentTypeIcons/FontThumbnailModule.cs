using EPiServer.Framework.TypeScanner;

namespace Geta.Optimizely.ContentTypeIcons
{
    public class FontThumbnailModule : ShellModule
    {
        public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath) : base(name, routeBasePath, resourceBasePath)
        {
        }

        public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath, ITypeScannerLookup typeScannerLookup, VirtualPathProvider virtualPathProvider) : base(name, routeBasePath, resourceBasePath, typeScannerLookup, virtualPathProvider)
        {
        }
    }
}
