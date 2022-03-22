using System;
using System.Text.Json.Serialization;
using SpotifyExtractInfo.Models.Spotify.Common;

namespace SpotifyExtractInfo.Models.Spotify.Playlist
{
    public class PlaylistTrack
    {
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
        [JsonPropertyName("added_by")]
        public Owner AddedBy { get; set; }
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }
        [JsonPropertyName("track")]
        public Track Track { get; set; }
        [JsonPropertyName("video_thumbnail")]
        public VideoThumbnail VideoThumbnail { get; set; }

    }
}
