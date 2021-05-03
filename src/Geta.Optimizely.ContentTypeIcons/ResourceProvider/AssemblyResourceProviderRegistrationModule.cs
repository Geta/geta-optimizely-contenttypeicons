/*
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Initialization;

namespace Geta.Optimizely.ContentTypeIcons.ResourceProvider
{
    internal class AssemblyResourceProviderRegistrationModule : IVirtualPathProviderModule
    {
        public IEnumerable<VirtualPathProvider> CreateProviders(InitializationEngine context)
        {
            return context.HostType.Equals(HostType.WebApplication)
                ? new VirtualPathProvider[] { new AssemblyResourceProvider() } : Enumerable.Empty<VirtualPathProvider>();
        }
    }
}
*/
