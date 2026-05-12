using EPiServer.Framework.Hosting;
using EPiServer.Web.Hosting;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization;

namespace Geta.Optimizely.ContentTypeIcons.Web;

public class Startup
{
    private readonly Foundation.Startup _foundationStartup;

    public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _foundationStartup = new Foundation.Startup(webHostEnvironment, configuration);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        _foundationStartup.ConfigureServices(services);

        services.AddContentTypeIcons(x =>
        {
            x.EnableTreeIcons = true;
            x.ForegroundColor = "#ffffff";
            x.BackgroundColor = "#02423F";
            x.FontSize = 40;
            x.CachePath = "[appDataPath]\\thumb_cache\\";
            x.CustomFontPath = "[appDataPath]\\fonts\\";
        });

        const string moduleName = "Geta.Optimizely.ContentTypeIcons";
        var fullPath = Path.GetFullPath($"../../src/{moduleName}/module");

        services.Configure<CompositeFileProviderOptions>(options =>
        {
            options.BasePathFileProviders.Add(new MappingPhysicalFileProvider(
                $"/Optimizely/{moduleName}",
                string.Empty,
                fullPath));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        _foundationStartup.Configure(app, env);
        app.UseContentTypeIcons();
    }
}
