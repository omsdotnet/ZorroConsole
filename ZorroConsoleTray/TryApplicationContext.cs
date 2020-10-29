using System;
using System.Windows.Forms;
using ZorroConsoleTray.Properties;

namespace ZorroConsoleTray
{
  internal class TryApplicationContext : ApplicationContext
  {
    private NotifyIcon trayIcon;

    public TryApplicationContext()
    {
      trayIcon = new NotifyIcon()
      {
        Icon = Resources.AppIcon,
        Text = "This is ZorroConsole tray app",
        ContextMenu = new ContextMenu(new[] {
          new MenuItem("Exit", Exit)
        }),
        Visible = true
      };
    }

    void Exit(object sender, EventArgs e)
    {
      trayIcon.Visible = false;

      Application.Exit();
    }
  }
}
