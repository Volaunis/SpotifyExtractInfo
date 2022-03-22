using System.Text.Json.Serialization;

namespace SpotifyExtractInfo.Models.Spotify.Playlist
{
    public class VideoThumbnail
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
