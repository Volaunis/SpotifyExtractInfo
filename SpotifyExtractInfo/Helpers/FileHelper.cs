using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyExtractInfo.Helpers
{
    public interface IFileHelper
    {
        Task<T> ReadJsonFile<T>(string filename);
        Task SaveJsonFile<T>(string filename, T item);
        Task SaveHtmlFile(string filename, string html);
    }

    public class FileHelper : IFileHelper
    {
        public async Task<T> ReadJsonFile<T>(string filename)
        {
            var json = await File.ReadAllTextAsync(filename);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SaveJsonFile<T>(string filename, T item)
        {
            var json = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true});
            await File.WriteAllTextAsync(filename, json);
        }

        public async Task SaveHtmlFile(string filename, string html)
        {
            await File.WriteAllTextAsync(filename, html);
        }
    }
}
