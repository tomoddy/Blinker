using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Blinker.Pages
{
    public class IndexModel : PageModel
    {
        public string Path = "http://192.168.1.169:7000/led";

        public List<string> Colours { get; set; }

        public IndexModel()
        {
            HttpClient client = new();
            Colours = JsonConvert.DeserializeObject<ColourSet>(client.SendAsync(new HttpRequestMessage(HttpMethod.Get, Path)).Result.Content.ReadAsStringAsync().Result)?.Colours!;
        }

        public void OnGet() { }
    }
}