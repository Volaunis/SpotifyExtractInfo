using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Albums
{
    public class Copyright
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}