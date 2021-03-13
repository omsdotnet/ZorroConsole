using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ZorroConsole
{
  internal class Program
  {
    static void Main(string[] args)
    {
      bool repeat;
      string userInput = "APPTEST START";
      const string stopWord = "APPTEST STOP";

      const string clearCommand = "clrscr";
      const string poolingCommand = "pooling";
      const string writeCommand = "write";
      const string formatedCommand = "formated";

      string serviceUrl = !string.IsNullOrEmpty(args.FirstOrDefault()) ? args.First() :
#if DEBUG
      "https://localhost:44370/appconsole/";
#else
     "https://zorrobackend.azurewebsites.net/appconsole/";
#endif

      var requestHeaders = new Dictionary<string, IEnumerable<string>>();
      var client = new ServiceClient(serviceUrl);

      Console.WriteLine(serviceUrl);
      Console.WriteLine(userInput);

      do
      {
        var response = client.Send(new ServiceMessage(userInput, requestHeaders));

        if (response.Context.ContainsKey(clearCommand))
        {
          Console.Clear();
        }

        if (response.Context.ContainsKey(writeCommand))
        {
          Console.Write(response.Text);
        }
        else if (response.Context.ContainsKey(formatedCommand))
        {
          ColorConsole.WriteEmbeddedColorLine(response.Text);
        }
        else
        {
          Console.WriteLine(response.Text);
        }

        repeat = !string.Equals(userInput, stopWord, StringComparison.OrdinalIgnoreCase);

        if (repeat)
        {
          if (response.Context.ContainsKey(poolingCommand))
          {
            var timeout = Convert.ToInt32(response.Context[poolingCommand].First());
            Thread.Sleep(timeout);
            userInput = string.Empty;
          }
          else
          {
            userInput = Console.ReadLine();
          }

          requestHeaders.Clear();
          foreach (var header in response.Context)
          {
            requestHeaders.Add(header.Key, header.Value);
          }
        }
      } 
      while (repeat);
    }
  }
}
