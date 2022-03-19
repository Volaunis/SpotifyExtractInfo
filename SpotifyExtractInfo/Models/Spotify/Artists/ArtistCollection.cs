using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Artists
{

    public class ArtistCollection
    {
        [JsonPropertyName("artists")]
        public Artists Artists { get; set; }
    }
}
