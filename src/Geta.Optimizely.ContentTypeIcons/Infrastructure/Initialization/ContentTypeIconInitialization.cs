using System.IO;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Framework.Internal;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization
{
    [ModuleDependency(typeof(EPiServer.Cms.Shell.InitializableModule))]
    [ModuleDependency(typeof(InitializationModule))]
    [InitializableModule]
    internal class ContentTypeIconInitialization : IInitializableModule
    {
        private static bool Initialized;

        public void Initialize(InitializationEngine context)
        {
            if (Initialized || context.HostType != HostType.WebApplication)
            {
                return;
            }

            var options = context.Locate.Advanced.GetRequiredService<IOptions<ContentTypeIconOptions>>();
            var settings = options.Value;
            var physicalPathResolver = context.Locate.Advanced.GetRequiredService<IPhysicalPathResolver>();
            
            // verify cache directory exists
            var fullPath = physicalPathResolver.Rebase(settings.CachePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            Initialized = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
            // Nothing to uninitialize
        }
    }
}
