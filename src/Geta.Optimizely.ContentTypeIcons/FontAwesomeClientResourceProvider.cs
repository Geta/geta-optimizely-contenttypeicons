using System.Collections.Generic;
using EPiServer.Framework.Web.Resources;
using EPiServer.Shell;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization;

namespace Geta.Optimizely.ContentTypeIcons
{
    /// <summary>
    /// Provides the required resources needed for the font
    /// </summary>
    [ClientResourceProvider]
    internal class FontAwesomeClientResourceProvider : IClientResourceProvider
    {
        public IEnumerable<ClientResource> GetClientResources()
        {
            // Only load the fonts when they're needed to save load time
            if (!TreeIconUiDescriptorInitialization.EnabledAndInUse) yield break;

            if (TreeIconUiDescriptorInitialization.FontAwesomeVersion4InUse)
            {
                var path = Paths.ToClientResource(Constants.ModuleName, "ClientResources/fa4/css/font-awesome.min.css");

                yield return new ClientResource
                {
                    Name = "epi.shell.ui",
                    Path = path,
                    ResourceType = ClientResourceType.Style
                };
            }

            if (TreeIconUiDescriptorInitialization.FontAwesomeVersion5InUse)
            {
                var path = Paths.ToClientResource(Constants.ModuleName, "ClientResources/fa5/css/all.min.css");

                yield return new ClientResource
                {
                    Name = "epi.shell.ui",
                    Path = path,
                    ResourceType = ClientResourceType.Style
                };
            }
        }
    }
}