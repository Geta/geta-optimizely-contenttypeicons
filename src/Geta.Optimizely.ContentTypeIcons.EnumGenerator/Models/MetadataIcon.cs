using System.Collections.Generic;

namespace Geta.Optimizely.ContentTypeIcons.EnumGenerator.Models
{
    public class MetadataIcon
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Unicode { get; set; }
        public IList<string> Styles { get; set; }
        public IList<string> Changes { get; set; }
        public Search Search { get; set; }
        public bool Private { get; set; }
    }
}
