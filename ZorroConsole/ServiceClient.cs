using System.Linq;
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

    public ServiceMessage Send(ServiceMessage request)
    {
      Client.DefaultRequestHeaders.Clear();

      foreach (var header in request.Context)
      {
        Client.DefaultRequestHeaders.Add(header.Key, header.Value);
      }


      using (HttpContent httpContent = new StringContent($"\"{request.Text}\"", Encoding.UTF8, "application/json"))
      {
        var result = Task.Run(() => Client.PostAsync(url, httpContent)).Result;

        result.EnsureSuccessStatusCode();

        var resultStr = Task.Run(() => result.Content.ReadAsStringAsync()).Result;

        var responseHeaders = result.Headers.ToDictionary(x => x.Key, y => y.Value);

        return new ServiceMessage(resultStr, responseHeaders);
      }
    }
  }
}
