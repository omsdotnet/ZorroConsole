using System.Collections.Generic;

namespace ZorroConsole
{
  internal class ServiceMessage
  {
    public string Text { get; }

    public Dictionary<string, IEnumerable<string>> Context { get; }

    public ServiceMessage(string text, Dictionary<string, IEnumerable<string>> context)
    {
      Text = text;
      Context = context;
    }
  }
}
