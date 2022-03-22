using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models.Spotify.Albums;

namespace SpotifyExtractInfo.Business
{
    public interface IAlbumBusiness
    {
        Task<List<Album>> GetAlbums(string token);
    }

    public class AlbumBusiness : IAlbumBusiness
    {
        private readonly IHttpHelper _httpHelper;

        public AlbumBusiness(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<List<Album>> GetAlbums(string token)
        {

            var albums = new List<Album>();

            var next = "https://api.spotify.com/v1/me/albums?limit=50";

            AlbumCollection albumCollection;

            do
            {
                albumCollection = await _httpHelper.Get<AlbumCollection>(next, token);

                albums.AddRange(albumCollection.Items.Select(i => i.Album));

                next = albumCollection.Next;
            } while (albumCollection.Next != null);

            return albums;


        }
    }
}
