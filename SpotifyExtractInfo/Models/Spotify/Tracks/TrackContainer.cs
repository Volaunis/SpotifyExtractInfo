using System;
using System.Text.Json.Serialization;
using SpotifyExtractInfo.Models.Spotify.Common;

namespace SpotifyExtractInfo.Models.Spotify.Tracks
{
    public class TrackContainer
    {
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
        [JsonPropertyName("track")]
        public Track Track { get; set; }
    }
}
