using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Artists
{
    public class Followers
    {
        [JsonPropertyName("href")]
        public object Href { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}