using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZorroConsoleTray
{
  public partial class SettingsForm : Form
  {
    public SettingsForm()
    {
      InitializeComponent();
    }

    private void SettingsForm_Load(object sender, EventArgs e)
    {
      var screen = Screen.FromPoint(this.Location);
      this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
    }
  }
}
