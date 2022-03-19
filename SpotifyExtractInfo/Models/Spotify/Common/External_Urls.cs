using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Common
{
    public class ExternalUrls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
    }
}