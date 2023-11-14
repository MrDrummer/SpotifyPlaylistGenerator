namespace SpotifyPlaylistGenerator.Utilities;

public static class EnumerationHelper
{
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector)
    {
        return items.GroupBy(keySelector).Select(g => g.First());
    }
}