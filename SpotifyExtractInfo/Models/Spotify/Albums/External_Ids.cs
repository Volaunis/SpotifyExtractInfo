using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Albums
{
    public class External_Ids
    {
        [JsonPropertyName("upc")]
        public string Upc { get; set; }
    }
}