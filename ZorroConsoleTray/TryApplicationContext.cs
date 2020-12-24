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
          new MenuItem("Settings", Settings),
          new MenuItem("Exit", Exit)
        }),
        Visible = true
      };
      trayIcon.DoubleClick += Settings;
    }

    private void Settings(object sender, EventArgs e)
    {
      var form = new SettingsForm();
      form.startWordTextBox.Text = Program.StartWord;
      form.stopWordTextBox.Text = Program.StopWord;
      form.httpTextBox.Text = Program.ServiceUrl;

      var result = form.ShowDialog();

      if (result == DialogResult.OK)
      {
        Program.StartWord = form.startWordTextBox.Text;
        Program.StopWord = form.stopWordTextBox.Text;
        Program.ServiceUrl = form.httpTextBox.Text;

        Program.SaveSettings();
      }
    }

    void Exit(object sender, EventArgs e)
    {
      trayIcon.Visible = false;

      Application.Exit();
    }
  }
}
