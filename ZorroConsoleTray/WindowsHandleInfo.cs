using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZorroConsoleTray
{
  internal class WindowHandleInfo
  {
    private int _MainHandle;

    public WindowHandleInfo(int handle)
    {
      _MainHandle = handle;
    }

    public string GetCaption()
    {
      var strTitle = string.Empty;
      var intLength = GetWindowTextLength(_MainHandle) + 1;
      var stringBuilder = new StringBuilder(intLength);
      if (GetWindowText(_MainHandle, stringBuilder, intLength) > 0)
      {
        strTitle = stringBuilder.ToString();
      }

      return strTitle;
    }

    public List<IntPtr> GetAllChildHandles()
    {
      List<IntPtr> childHandles = new List<IntPtr>();

      GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
      IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

      try
      {
        EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
        EnumChildWindows(this._MainHandle, childProc, pointerChildHandlesList);
      }
      finally
      {
        gcChildhandlesList.Free();
      }

      return childHandles;
    }

    private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
    {
      GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

      if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
      {
        return false;
      }

      List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
      childHandles.Add(hWnd);

      return true;
    }

    private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

    [DllImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumChildWindows(int window, EnumWindowProc callback, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowText(int hWnd, StringBuilder text, int count);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowTextLength(int hWnd);
  }
}
