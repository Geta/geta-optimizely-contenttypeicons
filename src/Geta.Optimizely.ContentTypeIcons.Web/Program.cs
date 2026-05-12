using Geta.Optimizely.ContentTypeIcons.Web;

var webProjectDir = AppContext.BaseDirectory.Contains("bin")
    ? Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"))
    : Directory.GetCurrentDirectory();

Host.CreateDefaultBuilder(args)
    .ConfigureCmsDefaults()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseContentRoot(Path.GetFullPath("../../sub/geta-foundation-core/src/Foundation"));
    })
    .ConfigureAppConfiguration((ctx, config) =>
    {
        // Add our web project's appsettings AFTER Foundation's so they win.
        config.AddJsonFile(Path.Combine(webProjectDir, "appsettings.json"), optional: true, reloadOnChange: true);
        config.AddJsonFile(Path.Combine(webProjectDir, $"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json"), optional: true, reloadOnChange: true);
    })
    .Build()
    .Run();
