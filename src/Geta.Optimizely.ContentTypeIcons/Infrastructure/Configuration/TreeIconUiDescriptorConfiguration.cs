using System.Reflection;
using System.Text;
using EPiServer.Shell;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Microsoft.Extensions.Options;

namespace Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration
{
    internal class TreeIconUiDescriptorConfiguration
    {
        private readonly UIDescriptorRegistry _uiDescriptorRegistry;
        private readonly ContentTypeIconOptions _configuration;
        public static bool EnabledAndInUse { get; internal set; }
        public static bool FontAwesomeVersion4InUse { get; internal set; }
        public static bool FontAwesomeVersion5InUse { get; internal set; }

        public TreeIconUiDescriptorConfiguration(
            UIDescriptorRegistry uiDescriptorRegistry,
            IOptions<ContentTypeIconOptions> options)
        {
            _uiDescriptorRegistry = uiDescriptorRegistry;
            _configuration = options.Value;
        }

        public void Initialize()
        {
            EnabledAndInUse = false;
            
            foreach (var descriptor in _uiDescriptorRegistry.UIDescriptors)
            {
                EnrichDescriptorWithIconClass(descriptor);
            }
        }

        internal void EnrichDescriptorWithIconClass(UIDescriptor descriptor)
        {
            var contentTypeIconAttribute = descriptor.ForType.GetCustomAttribute<ContentTypeIconAttribute>(false);
            var treeIconAttribute = descriptor.ForType.GetCustomAttribute<TreeIconAttribute>(false);

            if (contentTypeIconAttribute == null && treeIconAttribute?.Icon == null) return;

            if ((_configuration.EnableTreeIcons && treeIconAttribute?.Ignore != true) || treeIconAttribute?.Icon != null)
            {
                descriptor.IconClass = BuildIconClassNames(contentTypeIconAttribute, treeIconAttribute);
                EnabledAndInUse = true;
            }
        }

        private static string BuildIconClassNames(
            ContentTypeIconAttribute contentTypeIconAttribute,
            TreeIconAttribute treeIconAttribute)
        {
            var icon = treeIconAttribute?.Icon ?? contentTypeIconAttribute?.Icon;
            if (icon == null) return string.Empty;

            var builder = new StringBuilder();
            var className = icon.ToString().ToDashCase().Replace("_", string.Empty);

            switch (icon)
            {
                case FontAwesome _:
                    builder.AppendFormat("fa fa-{0} ", className);
                    FontAwesomeVersion4InUse = true;
                    break;
                case FontAwesome5Brands _:
                    builder.AppendFormat("fab fa-{0} ", className);
                    FontAwesomeVersion5InUse = true;
                    break;
                case FontAwesome5Regular _:
                    builder.AppendFormat("far fa-{0} ", className);
                    FontAwesomeVersion5InUse = true;
                    break;
                case FontAwesome5Solid _:
                    builder.AppendFormat("fas fa-{0} ", className);
                    FontAwesomeVersion5InUse = true;
                    break;
            }

            var rotate = treeIconAttribute?.Rotate ?? contentTypeIconAttribute.Rotate;

            switch (rotate)
            {
                case Rotations.Rotate90:
                case Rotations.Rotate180:
                case Rotations.Rotate270:
                    builder.AppendFormat("fa-rotate-{0} ", (int)rotate);
                    break;
                case Rotations.FlipHorizontal:
                    builder.Append("fa-flip-horizontal ");
                    break;
                case Rotations.FlipVertical:
                    builder.Append("fa-flip-vertical ");
                    break;
            }

            builder.Append("fa-fw");

            return builder.ToString();
        }
    }
}
