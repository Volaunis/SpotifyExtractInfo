using System.Text.Json.Serialization;
using SpotifyExtractInfo.Models.Spotify.Common;

namespace SpotifyExtractInfo.Models.Spotify.Albums
{
    public class Tracks: CommonTracks
    {
        [JsonPropertyName("items")]
        public Track[] Items { get; set; }
    }
}