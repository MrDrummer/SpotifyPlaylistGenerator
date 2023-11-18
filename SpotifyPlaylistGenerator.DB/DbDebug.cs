namespace SpotifyPlaylistGenerator.DB;

public class DbDebug
{
    private readonly SpotifyDbContext _context;
    
    public DbDebug(SpotifyDbContext context)
    {
        _context = context;
        
    }
    
    public async Task GetTrackedEntities()
    {
        var trackedEntities = _context.ChangeTracker.Entries();
        foreach(var entry in trackedEntities)
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }
    } 
}