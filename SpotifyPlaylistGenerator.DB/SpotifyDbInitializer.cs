using Microsoft.Extensions.DependencyInjection;

namespace SpotifyPlaylistGenerator.DB;

public class SpotifyDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SpotifyDbContext>();

        await context.Database.EnsureCreatedAsync();
    }
}