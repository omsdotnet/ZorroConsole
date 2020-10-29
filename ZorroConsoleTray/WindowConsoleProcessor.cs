using System.Runtime.InteropServices;
using System.Text;

namespace ZorroConsoleTray
{
  internal class WindowConsoleProcessor
  {
    public WindowConsoleSessionId CatchUserSession()
    {
      int foregroundWindowHandle = GetForegroundWindow();
      uint remoteThreadId = GetWindowThreadProcessId(foregroundWindowHandle, 0);
      uint currentThreadId = GetCurrentThreadId();

      AttachThreadInput(remoteThreadId, currentThreadId, true);
      int focused = GetFocus();
      AttachThreadInput(remoteThreadId, currentThreadId, false);

      return new WindowConsoleSessionId(remoteThreadId, focused);
    }

    public string GetSessionText(WindowConsoleSessionId session)
    {
      StringBuilder builder = new StringBuilder(500);

      SendMessage(session.WindowHandle, WM_GETTEXT, builder.Capacity, builder);

      return builder.ToString();
    }

    public void SetSessionText(WindowConsoleSessionId session, string text)
    {
      StringBuilder builder = new StringBuilder(text);

      SendMessage(session.WindowHandle, WM_SETTEXT, 0, builder);
    }

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(int hWnd, int ProcessId);

    [DllImport("user32.dll")]
    static extern int GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern int GetFocus();

    [DllImport("user32.dll")]
    static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern int SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);

    const int WM_SETTEXT = 12;
    const int WM_GETTEXT = 13;
  }
}
