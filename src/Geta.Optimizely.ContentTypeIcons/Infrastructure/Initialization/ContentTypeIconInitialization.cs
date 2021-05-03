using System.IO;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization
{
    [ModuleDependency(typeof(EPiServer.Cms.Shell.InitializableModule))]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    internal class ContentTypeIconInitialization : IInitializableModule
    {
        private static bool _initialized;

        public void Initialize(InitializationEngine context)
        {
            if (_initialized || context.HostType != HostType.WebApplication)
            {
                return;
            }

            var options = context.Locate.Advanced.GetService<IOptions<ContentTypeIconOptions>>();
            var settings = options.Value;

            // verify cache directory exists
            var fullPath = VirtualPathUtilityEx.RebasePhysicalPath(settings.CachePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            _initialized = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
