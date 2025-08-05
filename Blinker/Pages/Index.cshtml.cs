using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Blinker.Pages
{
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        public string Path = "http://tzer0m.duckdns.org:7000/led";

        public HttpClient Client { get; set; }

        public List<string> Colours { get; set; }

        public IndexModel()
        {
            Client = new();
            Colours = JsonConvert.DeserializeObject<ColourSet>(Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, Path)).Result.Content.ReadAsStringAsync().Result)?.Colours!;
        }

        public async void OnGet(string? colour)
        {
            if (colour is not null)
            {
                await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, Path + "?colour=" + colour));
            }
        }
    }
}