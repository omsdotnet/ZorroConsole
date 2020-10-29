using System;
using System.Collections.Generic;

namespace ZorroConsoleTray
{
  internal class KeboardPressKeyProcessor
  {
    private List<char> buffer = new List<char>();
    
    public void Save(int pressKeyCode)
    {
      buffer.Add(Convert.ToChar(pressKeyCode));
    }

    public string GetTypedWord()
    {
      var charArray = buffer.ToArray();
      var ret = new string(charArray);
      buffer.Clear();

      return ret;
    }
  }
}
