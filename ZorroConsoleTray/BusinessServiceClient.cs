using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZorroConsoleTray
{
  internal class BusinessServiceClient : IDisposable
  {
    readonly HttpClient _client = new HttpClient();

    public string GetAnswer(string url, string requestText)
    {
      var response = Task.Run(async () => await _client.PostAsync(url, new StringContent($"\"{requestText}\"", Encoding.UTF8, "application/json"))).Result;

      response.EnsureSuccessStatusCode();

      var responseText = Task.Run(async () => await response.Content.ReadAsStringAsync()).Result;

      return responseText;
    }

    public void Dispose()
    {
      _client.Dispose();
    }
  }
}
