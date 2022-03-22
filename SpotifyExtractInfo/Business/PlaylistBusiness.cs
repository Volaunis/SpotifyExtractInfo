using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models.Spotify.Playlist;

namespace SpotifyExtractInfo.Business
{
    public interface IPlaylistBusiness
    {
        Task<List<Playlist>> GetPlaylists(string token);
    }

    public class PlaylistBusiness : IPlaylistBusiness
    {
        private readonly IHttpHelper _httpHelper;

        public PlaylistBusiness(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<List<Playlist>> GetPlaylists(string token)
        {
            var playlists = new List<Playlist>();

            var next = "https://api.spotify.com/v1/me/playlists?limit=50";

            PlaylistCollection playlistCollection;

            do
            {
                playlistCollection = await _httpHelper.Get<PlaylistCollection>(next, token);

                playlists.AddRange(playlistCollection.Items);

                next = playlistCollection.Next;
            } while (playlistCollection.Next != null);

            for (var index = 0; index < playlists.Count; index++)
            {
                var playlist = playlists[index];
                Console.Write($"  {index}/{playlists.Count}: Saving tracks for playlist {playlist.Name}...");
                playlist.PlaylistTracks = await GetPlaylistTracks(playlist.TracksReference.Href, token);
                Console.WriteLine("DONE");
            }

            return playlists;
        }

        private async Task<List<PlaylistTrack>> GetPlaylistTracks(string tracksReference, string token)
        {
            var playlistTracks = new List<PlaylistTrack>();

            var next = tracksReference;

            PlaylistTrackCollection playlistTrackCollection;

            do
            {
                playlistTrackCollection = await _httpHelper.Get<PlaylistTrackCollection>(next, token);

                playlistTracks.AddRange(playlistTrackCollection.Items);

                next = playlistTrackCollection.Next;
            } while (playlistTrackCollection.Next != null);

            return playlistTracks;
        }
    }
}
