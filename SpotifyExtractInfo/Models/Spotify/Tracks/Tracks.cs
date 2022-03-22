using System.Text.Json.Serialization;
using SpotifyExtractInfo.Models.Spotify.Common;

namespace SpotifyExtractInfo.Models.Spotify.Tracks
{
    public class Tracks: CommonTracks
    {
        [JsonPropertyName("items")]
        public TrackContainer[] Items { get; set; }
    }
}