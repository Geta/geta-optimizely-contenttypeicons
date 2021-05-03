/*using System.IO;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Geta.Optimizely.ContentTypeIcons.ResourceProvider
{
    internal class AssemblyResourceVirtualFile : VirtualFile
    {
        private readonly string _path;

        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            _path = ServiceLocator.Current.GetInstance<IVirtualPathResolver>().ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            var resourcePath = AssemblyResourceProvider.GetResourcePath(_path);

            if (!string.IsNullOrEmpty(resourcePath))
            {
                var stream = AssemblyResourceProvider.ModuleAssembly.GetManifestResourceStream(resourcePath);

                return stream;
            }

            return null;
        }
    }
}*/