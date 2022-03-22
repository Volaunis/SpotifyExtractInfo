using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Playlist
{
    public class TracksReference
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
