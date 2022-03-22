using System;
using System.Threading.Tasks;
using SpotifyExtractInfo.Business;
using SpotifyExtractInfo.Models;

namespace SpotifyExtractInfo.Service
{
    public interface ISpotifyExtractInfoService
    {
        Task Process(ProcessTypeEnum processType, string token);
    }

    public class SpotifyExtractInfoService : ISpotifyExtractInfoService
    {
        private readonly IArtistBusiness _artistBusiness;
        private readonly ITrackBusiness _trackBusiness;
        private readonly IAlbumBusiness _albumBusiness;
        private readonly IPlaylistBusiness _playlistBusiness;
        private readonly IFileStoreBusiness _fileStoreBusiness;

        public SpotifyExtractInfoService(
            IArtistBusiness artistBusiness,
            ITrackBusiness trackBusiness,
            IAlbumBusiness albumBusiness,
            IPlaylistBusiness playlistBusiness,
            IFileStoreBusiness fileStoreBusiness)
        {
            _artistBusiness = artistBusiness;
            _trackBusiness = trackBusiness;
            _albumBusiness = albumBusiness;
            _playlistBusiness = playlistBusiness;
            _fileStoreBusiness = fileStoreBusiness;
        }


        public async Task Process(ProcessTypeEnum processType, string token)
        {
            // Fetch Artists
            Console.Write("Saving artists...");
            if (processType == ProcessTypeEnum.Spotify)
            {
                var artists = await _artistBusiness.GetArtists(token);
                await _fileStoreBusiness.SaveArtistsToJson(artists);
            }
            else if (processType == ProcessTypeEnum.HTML)
            {
                var artists = await _fileStoreBusiness.ReadArtistsJson();
                await _fileStoreBusiness.SaveArtistsToHtml(artists);
            }
            Console.WriteLine("DONE");

            // Fetch tracks
            Console.Write("Saving tracks...");
            if (processType == ProcessTypeEnum.Spotify)
            {
                var tracks = await _trackBusiness.GetTracks(token);
                await _fileStoreBusiness.SaveTracksToJson(tracks);
            }
            else if (processType == ProcessTypeEnum.HTML)
            {
                var tracks = await _fileStoreBusiness.ReadtracksJson();
                await _fileStoreBusiness.SaveTracksToHtml(tracks);
            }

            Console.WriteLine("DONE");

            // Fetch albums
            Console.Write("Saving albums...");
            if (processType == ProcessTypeEnum.Spotify)
            {
                var albums = await _albumBusiness.GetAlbums(token);
                await _fileStoreBusiness.SaveAlbumsToJson(albums);
            }
            else if (processType == ProcessTypeEnum.HTML)
            {
                var albums = await _fileStoreBusiness.ReadalbumsJson();
                await _fileStoreBusiness.SaveAlbumsToHtml(albums);
            }

            Console.WriteLine("DONE");

            // Fetch playlists
            Console.WriteLine("Saving playlists...");
            if (processType == ProcessTypeEnum.Spotify)
            {
                var playlists = await _playlistBusiness.GetPlaylists(token);
                await _fileStoreBusiness.SavePlaylistsToJson(playlists);
            }
            else if (processType == ProcessTypeEnum.HTML)
            {
                var playlists = await _fileStoreBusiness.ReadplaylistsJson();
                await _fileStoreBusiness.SavePlaylistsToHtml(playlists);
            }

            Console.WriteLine("DONE");
        }
    }
}
