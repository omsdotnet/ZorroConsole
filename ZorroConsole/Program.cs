using System;
using System.Linq;

namespace ZorroConsole
{
  internal class Program
  {
    static void Main(string[] args)
    {
      bool repeat;
      string userInput = "APPTEST START";
      const string stopWord = "APPTEST STOP";

      string serviceUrl = !string.IsNullOrEmpty(args.FirstOrDefault()) ? args.First() :
#if DEBUG
      "https://localhost:44370/appconsole/";
#else
     "https://zorrobackend.azurewebsites.net/appconsole/";
#endif

      var client = new ServiceClient(serviceUrl);

      Console.WriteLine(userInput);

      do
      {
        string response = client.Send(userInput);
        
        Console.WriteLine(response);

        repeat = !string.Equals(userInput, stopWord, StringComparison.OrdinalIgnoreCase);

        if (repeat)
        {
          userInput = Console.ReadLine();
        }
      } 
      while (repeat);
    }
  }
}
