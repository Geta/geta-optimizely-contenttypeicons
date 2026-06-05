using SixLabors.ImageSharp;

namespace Geta.Optimizely.ContentTypeIcons.Caching
{
    public interface IIconCacheProvider
    {
        void Initialize();
        bool TryGet(string key, out Image image);
        void Set(string key, Image image);
    }
}
