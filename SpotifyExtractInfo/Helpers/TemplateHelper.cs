using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SpotifyExtractInfo.Helpers
{
    public interface ITemplateHelper
    {
        Task<string> GetTemplate(string templateName);
        string ProcessTemplate(string template, Dictionary<string, string> fields);
    }

    public class TemplateHelper : ITemplateHelper
    {
        public async Task<string> GetTemplate(string templateName)
        {
            return await File.ReadAllTextAsync($"Templates\\{templateName}.html");
        }

        public string ProcessTemplate(string template, Dictionary<string, string> fields)
        {
            var ret = template;
            foreach (var field in fields)
                ret = ret.Replace($"{{{field.Key}}}", field.Value);
            return ret;
        }
    }
}
