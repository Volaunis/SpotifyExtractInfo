using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Artists
{
    public class Cursors
    {
        [JsonPropertyName("after")]
        public string After { get; set; }
    }
}