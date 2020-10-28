using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZorroConsoleTray
{
  class KeboardPressKeyProcessor
  {
    private List<char> buffer = new List<char>();
    
    public void Save(int pressKeyCode)
    {
      buffer.Add(Convert.ToChar(pressKeyCode));
    }

    public bool IsWord(string word)
    {
      var charArray = buffer.ToArray();
      var str = new string(charArray);
      buffer.Clear();

      Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {str}");

      return str == word;
    }
  }
}
