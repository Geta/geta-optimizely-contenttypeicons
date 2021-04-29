using System.Collections.Generic;
using Geta.Optimizely.ContentTypeIcons.Initialization;

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
            if (!TreeIconUIDescriptorInitialization.EnabledAndInUse)
                yield break;

            if (TreeIconUIDescriptorInitialization.FontAwesomeVersion4InUse)
            {
                var path = Paths.ToClientResource("Geta.Epi.FontThumbnail", "ClientResources/fa4/css/font-awesome.min.css");

                yield return new ClientResource
                {
                    Name = "epi.shell.ui",
                    Path = path,
                    ResourceType = ClientResourceType.Style
                };
            }

            if (TreeIconUIDescriptorInitialization.FontAwesomeVersion5InUse)
            {
                var path = Paths.ToClientResource("Geta.Epi.FontThumbnail", "ClientResources/fa5/css/all.min.css");

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
