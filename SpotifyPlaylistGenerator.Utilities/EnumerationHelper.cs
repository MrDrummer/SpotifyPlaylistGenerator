namespace SpotifyPlaylistGenerator.Utilities;

public static class EnumerationHelper
{
    // Have you looked at DistinctBy? It supports composite uniqueness you muppet! e.g. `new { pt.PlaylistId, pt.TrackId, pt.PositionIndex }`

    public static IEnumerable<TOutput> ChunkAndProcess<TInput, TOutput>(this IEnumerable<TInput> source, 
        int chunkSize, Func<IEnumerable<TInput>, IEnumerable<TOutput>> processFunc)
    {
        var chunks = source.Select((s, i) => new {Value = s, Index = i})
            .GroupBy(x => x.Index / chunkSize)
            .Select(grp => grp.Select(x => x.Value));
        
        foreach (var chunk in chunks)
        {
            foreach (var result in processFunc(chunk))
            {
                yield return result;
            }
        }
    }
    
    public static async Task<List<TOutput>> ChunkAndProcessAsync<TInput, TOutput>(this IEnumerable<TInput> source,
        int chunkSize, Func<IEnumerable<TInput>, Task<IEnumerable<TOutput>>> processFunc)
    {
        var chunks = source.Select((s, i) => new {Value = s, Index = i})
            .GroupBy(x => x.Index / chunkSize)
            .Select(grp => grp.Select(x => x.Value));
    
        var allResults = new List<TOutput>();

        foreach (var chunk in chunks)
        {
            var result = await processFunc(chunk);
            allResults.AddRange(result);
        }
  
        return allResults;
    }
    
    public static async IAsyncEnumerable<TOutput> ChunkAndProcessYieldAsync<TInput, TOutput>(this IEnumerable<TInput> source, 
        int chunkSize, Func<IEnumerable<TInput>, Task<IEnumerable<TOutput>>> processFunc)
    {
        var chunks = source.Select((s, i) => new {Value = s, Index = i})
            .GroupBy(x => x.Index / chunkSize)
            .Select(grp => grp.Select(x => x.Value));
    
        foreach (var chunk in chunks)
        {
            var result = await processFunc(chunk);
            foreach (var res in result)
            {
                yield return res;
            }
        }
    }
}