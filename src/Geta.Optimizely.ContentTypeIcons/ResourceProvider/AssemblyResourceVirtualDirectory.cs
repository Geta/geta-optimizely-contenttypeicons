using System.Collections;
using System.Linq;

namespace Geta.Optimizely.ContentTypeIcons.ResourceProvider
{
    internal class AssemblyResourceVirtualDirectory : VirtualDirectory
    {
        public AssemblyResourceVirtualDirectory(string virtualDir)
            : base(virtualDir)
        {
        }

        public override IEnumerable Children => Enumerable.Empty<IEnumerable>();

        public override IEnumerable Directories => Enumerable.Empty<IEnumerable>();

        public override IEnumerable Files => Enumerable.Empty<IEnumerable>();
    }
}