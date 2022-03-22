using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models.Spotify.Common;
using SpotifyExtractInfo.Models.Spotify.Tracks;

namespace SpotifyExtractInfo.Business
{
    public interface ITrackBusiness
    {
        Task<List<Track>> GetTracks(string token);
    }

    public class TrackBusiness : ITrackBusiness
    {
        private readonly IHttpHelper _httpHelper;

        public TrackBusiness(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<List<Track>> GetTracks(string token)
        {
            var tracks = new List<Track>();

            var next = "https://api.spotify.com/v1/me/tracks?limit=50";

            Tracks trackCollection;

            do
            {
                trackCollection = await _httpHelper.Get<Tracks>(next, token);

                tracks.AddRange(trackCollection.Items.Select(i => i.Track));

                next = trackCollection.Next;
            } while (trackCollection.Next != null);

            return tracks;
        }
    }
}
