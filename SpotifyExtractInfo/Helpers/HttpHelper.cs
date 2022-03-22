using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SpotifyExtractInfo.Helpers
{
    public interface IHttpHelper
    {
        Task<T> Get<T>(string url, string token);
    }

    public class HttpHelper : IHttpHelper
    {
        private HttpClient _client;

        public async Task<T> Get<T>(string url, string token)
        {
            var httpClient = GetClient(token);

            return await httpClient.GetFromJsonAsync<T>(url);
        }

        private HttpClient GetClient(string token)
        {
            if (_client != null) return _client;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            return _client;
        }
    }
}
