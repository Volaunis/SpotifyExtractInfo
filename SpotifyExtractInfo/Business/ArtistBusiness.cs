using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models.Spotify.Artists;
using SpotifyExtractInfo.Models.Spotify.Common;

namespace SpotifyExtractInfo.Business
{
    public interface IArtistBusiness
    {
        Task<List<Artist>> GetArtists(string token);
    }

    public class ArtistBusiness : IArtistBusiness
    {
        private readonly IHttpHelper _httpHelper;

        public ArtistBusiness(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<List<Artist>> GetArtists(string token)
        {
            var artists = new List<Artist>();

            var next = "https://api.spotify.com/v1/me/following?type=artist&limit=50";

            ArtistCollection artistCollection;

            do
            {
                artistCollection = await _httpHelper.Get<ArtistCollection>(next, token);

                artists.AddRange(artistCollection.Artists.Items);

                next = artistCollection.Artists.Next;
            } while (artistCollection.Artists.Next != null);

            return artists;
        }
    }
}
