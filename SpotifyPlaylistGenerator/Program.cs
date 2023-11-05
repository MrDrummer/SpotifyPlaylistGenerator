using SpotifyPlaylistGenerator.DB;

namespace SpotifyPlaylistGenerator;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var serviceProvider = host.Services;
        await SpotifyDbInitializer.InitializeAsync(serviceProvider);

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddJsonFile("spotify.secrets.json", optional: true, reloadOnChange: true);
                config.AddJsonFile("postgres.secrets.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration((_, builder) =>
            {
                builder.AddUserSecrets<Program>();
            });
}