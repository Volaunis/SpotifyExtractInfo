using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models.Spotify.Albums;
using SpotifyExtractInfo.Models.Spotify.Common;
using SpotifyExtractInfo.Models.Spotify.Playlist;

namespace SpotifyExtractInfo.Business
{
    public interface IFileStoreBusiness
    {
        Task SaveArtistsToJson(List<Artist> artists);
        Task SaveTracksToJson(List<Track> tracks);
        Task SaveAlbumsToJson(List<Album> albums);
        Task SavePlaylistsToJson(List<Playlist> playlists);

        Task SaveArtistsToHtml(List<Artist> artists);
        Task SaveTracksToHtml(List<Track> tracks);
        Task SaveAlbumsToHtml(List<Album> albums);
        Task SavePlaylistsToHtml(List<Playlist> playlists);
        Task<List<Artist>> ReadArtistsJson();
        Task<List<Track>> ReadtracksJson();
        Task<List<Album>> ReadalbumsJson();
        Task<List<Playlist>> ReadplaylistsJson();
    }

    public class FileStoreBusiness : IFileStoreBusiness
    {
        private readonly IFileHelper _fileHelper;
        private readonly ITemplateHelper _templateHelper;

        public FileStoreBusiness(IFileHelper fileHelper, ITemplateHelper templateHelper)
        {
            _fileHelper = fileHelper;
            _templateHelper = templateHelper;
        }

        #region SaveToJson
        public async Task SaveArtistsToJson(List<Artist> artists)
        {
            await _fileHelper.SaveJsonFile("Spotify-artists.json", artists);
        }

        public async Task SaveTracksToJson(List<Track> tracks)
        {
            await _fileHelper.SaveJsonFile("Spotify-tracks.json", tracks);
        }

        public async Task SaveAlbumsToJson(List<Album> albums)
        {
            await _fileHelper.SaveJsonFile("Spotify-albums.json", albums);
        }

        public async Task SavePlaylistsToJson(List<Playlist> playlists)
        {
            await _fileHelper.SaveJsonFile("Spotify-playlists.json", playlists);
        }
        #endregion

        #region SaveToHtml
        public async Task SaveArtistsToHtml(List<Artist> artists)
        {
            var bodyTemplate = await _templateHelper.GetTemplate("Artist-Body");
            var artistTemplate = await _templateHelper.GetTemplate("Artist-Artist");
            
            var artistList = new StringBuilder();
            foreach (var artist in artists)
            {
                artistList.AppendLine(_templateHelper.ProcessTemplate(artistTemplate, new Dictionary<string, string>
                {
                    {"ArtistName", artist.Name},
                    {"ImageUrl", artist.Images.FirstOrDefault()?.Url}
                }));
            }

            var body = _templateHelper.ProcessTemplate(bodyTemplate, new Dictionary<string, string>
            {
                {"Artists", artistList.ToString()}
            });
            
            await _fileHelper.SaveHtmlFile("Spotify-Artists.html", body);
        }

        public async Task SaveTracksToHtml(List<Track> tracks)
        {
            var bodyTemplate = await _templateHelper.GetTemplate("Track-Body");
            var trackTemplate = await _templateHelper.GetTemplate("Track-Track");


            var trackList = new StringBuilder();
            foreach (var track in tracks)
            {
                trackList.AppendLine(_templateHelper.ProcessTemplate(trackTemplate, new Dictionary<string, string>
                {
                    {"Title", track.Name},
                    {"Artists", string.Join(", ", track.Artists.Select(a => a.Name))},
                    {"AlbumTitle", track.Album.Name},
                    {"ImageUrl", track.Album.Images.LastOrDefault()?.Url}
                }));
            }

            var body = _templateHelper.ProcessTemplate(bodyTemplate, new Dictionary<string, string>
            {
                {"Tracks", trackList.ToString()}
            });

            await _fileHelper.SaveHtmlFile("Spotify-Tracks.html", body);
        }

        public async Task SaveAlbumsToHtml(List<Album> albums)
        {
            var bodyTemplate = await _templateHelper.GetTemplate("Album-Body");
            var albumTemplate = await _templateHelper.GetTemplate("Album-Album");
            var sideTemplate = await _templateHelper.GetTemplate("Album-Side");
            var trackTemplate = await _templateHelper.GetTemplate("Album-Track");

            var albumList = new StringBuilder();

            foreach (var album in albums)
            {
                var sideList = new StringBuilder();

                var numberOfDisks = album.Tracks.Items.Select(i => i.DiscNumber).Distinct().Count();

                for (var i = 1; i <= numberOfDisks; i++)
                {
                    var tracks = album.Tracks.Items.Where(item => item.DiscNumber == i).ToList();

                    var trackList = new StringBuilder();

                    foreach (var track in tracks)
                    {
                        trackList.AppendLine(_templateHelper.ProcessTemplate(trackTemplate, new Dictionary<string, string>
                        {
                            {"TrackNumber", track.TrackNumber.ToString()},
                            {"TrackTitle", track.Name}
                        }));
                    }

                    sideList.AppendLine(_templateHelper.ProcessTemplate(sideTemplate, new Dictionary<string, string>
                    {
                        {"SideNumber", i.ToString()},
                        {"Tracks", trackList.ToString()}
                    }));
                }

                albumList.AppendLine(_templateHelper.ProcessTemplate(albumTemplate, new Dictionary<string, string>
                {
                    {"AlbumTitle", album.Name},
                    {"Artists", string.Join(", ", album.Artists.Select(a => a.Name))},
                    {"Sides", sideList.ToString()},
                    {"ImageUrl", album.Images.FirstOrDefault()?.Url}
                }));
            }

            var body = _templateHelper.ProcessTemplate(bodyTemplate, new Dictionary<string, string>
            {
                {"Albums", albumList.ToString()}
            });

            await _fileHelper.SaveHtmlFile("Spotify-Albums.html", body);
        }

        public async Task SavePlaylistsToHtml(List<Playlist> playlists)
        {
            var bodyTemplate = await _templateHelper.GetTemplate("Playlist-Body");
            var playlistTemplate = await _templateHelper.GetTemplate("Playlist-Playlist");
            var trackTemplate = await _templateHelper.GetTemplate("Playlist-Track");
            
            var playlistList = new StringBuilder();

            foreach (var playlist in playlists)
            {
                var trackList = new StringBuilder();
                for (var index = 0; index < playlist.PlaylistTracks.Count; index++)
                {
                    var playlistTrack = playlist.PlaylistTracks[index];
                    var track = playlistTrack.Track;

                    if (track != null)
                    {
                        trackList.Append(_templateHelper.ProcessTemplate(trackTemplate, new Dictionary<string, string>
                        {
                            {"TrackNumber", (index + 1).ToString()},
                            {"TrackName", track.Name},
                            {"AlbumName", track.Album.Name},
                            {"Artists", string.Join(", ", track.Artists.Select(a => a.Name))}
                        }));
                    }
                    else
                    {
                        trackList.Append(_templateHelper.ProcessTemplate(trackTemplate, new Dictionary<string, string>
                        {
                            {"TrackNumber", (index + 1).ToString()},
                            {"TrackName", "Unknown"},
                            {"AlbumName", ""},
                            {"Artists", ""}
                        }));
                    }
                }

                playlistList.Append(_templateHelper.ProcessTemplate(playlistTemplate, new Dictionary<string, string>
                {
                    {"PlaylistTitle", playlist.Name},
                    {"Tracks", trackList.ToString()}
                }));
            }

            var body = _templateHelper.ProcessTemplate(bodyTemplate, new Dictionary<string, string>
            {
                {"Playlists", playlistList.ToString()}
            });

            await _fileHelper.SaveHtmlFile("Spotify-Playlists.html", body);
        }
        #endregion

        #region ReadJson
        public Task<List<Artist>> ReadArtistsJson()
        {
            return _fileHelper.ReadJsonFile<List<Artist>>("Spotify-artists.json");
        }

        public Task<List<Track>> ReadtracksJson()
        {
            return _fileHelper.ReadJsonFile<List<Track>>("Spotify-tracks.json");
        }

        public Task<List<Album>> ReadalbumsJson()
        {
            return _fileHelper.ReadJsonFile<List<Album>>("Spotify-albums.json");
        }

        public Task<List<Playlist>> ReadplaylistsJson()
        {
            return _fileHelper.ReadJsonFile<List<Playlist>>("Spotify-playlists.json");
        }
        #endregion
    }
}
