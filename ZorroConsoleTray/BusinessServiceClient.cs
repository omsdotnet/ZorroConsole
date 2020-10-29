using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ZorroConsoleTray
{
  internal class BusinessServiceClient : IDisposable
  {
    readonly HttpClient _client = new HttpClient();

    public string GetAnswer(string url, string requestText)
    {
      WebRequest request = WebRequest.Create(url);
      request.Method = "POST";

      byte[] byteArray = Encoding.UTF8.GetBytes(requestText);

      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = byteArray.Length;

      Stream dataStream = request.GetRequestStream();
      dataStream.Write(byteArray, 0, byteArray.Length);
      dataStream.Close();

      WebResponse response = request.GetResponse();

      Debug.WriteLine(((HttpWebResponse)response).StatusDescription);

      string responseFromServer;

      using (dataStream = response.GetResponseStream())
      {
        StreamReader reader = new StreamReader(dataStream);
        responseFromServer = reader.ReadToEnd();
      }

      response.Close();

      return responseFromServer;
    }

    public void Dispose()
    {
      _client.Dispose();
    }
  }
}
