using System;
using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Albums
{
    public class AlbumContainer
    {
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
        [JsonPropertyName("album")]
        public Album Album { get; set; }
    }
}