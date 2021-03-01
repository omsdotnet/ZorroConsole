using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZorroConsole
{
  internal class ServiceClient
  {
    private string url;
    private static readonly HttpClient Client = new HttpClient();

    public ServiceClient(string url)
    {
      this.url = url;
    }

    public string Send(string userInput)
    {
      using (HttpContent httpContent = new StringContent($"\"{userInput}\"", Encoding.UTF8, "application/json"))
      {
        var result = Task.Run(() => Client.PostAsync(url, httpContent)).Result;

        result.EnsureSuccessStatusCode();

        var resultStr = Task.Run(() => result.Content.ReadAsStringAsync()).Result;

        return resultStr;
      }
    }
  }
}
