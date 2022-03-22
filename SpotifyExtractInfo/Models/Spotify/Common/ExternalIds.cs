using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Common
{
    public class ExternalIds
    {
        [JsonPropertyName("upc")]
        public string Upc { get; set; }
    }
}