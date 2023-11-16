using System.Globalization;

namespace SpotifyPlaylistGenerator.Utilities;

public static class DateHelper
{
    public static DateTime? ConvertToDateTime(string dateString)
    {
        if (DateTime.TryParseExact(dateString, new[] { "yyyy", "yyyy-MM", "yyyy-MM-dd" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            return date;
        }

        return null;
        // throw new ArgumentException("Invalid date format", nameof(dateString));
    }
}