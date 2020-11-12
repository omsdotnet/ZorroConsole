namespace ZorroConsoleTray
{
  partial class SettingsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.startWordTextBox = new System.Windows.Forms.TextBox();
      this.startWordLabel = new System.Windows.Forms.Label();
      this.stopWordTextBox = new System.Windows.Forms.TextBox();
      this.stopWordLabel = new System.Windows.Forms.Label();
      this.httpTextBox = new System.Windows.Forms.TextBox();
      this.httpLabel = new System.Windows.Forms.Label();
      this.applyButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // startWordTextBox
      // 
      this.startWordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.startWordTextBox.Location = new System.Drawing.Point(123, 3);
      this.startWordTextBox.Multiline = true;
      this.startWordTextBox.Name = "startWordTextBox";
      this.startWordTextBox.Size = new System.Drawing.Size(392, 20);
      this.startWordTextBox.TabIndex = 1;
      // 
      // startWordLabel
      // 
      this.startWordLabel.AutoSize = true;
      this.startWordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.startWordLabel.Location = new System.Drawing.Point(3, 0);
      this.startWordLabel.Name = "startWordLabel";
      this.startWordLabel.Size = new System.Drawing.Size(114, 26);
      this.startWordLabel.TabIndex = 0;
      this.startWordLabel.Text = "Start Word";
      this.startWordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // stopWordTextBox
      // 
      this.stopWordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.stopWordTextBox.Location = new System.Drawing.Point(123, 29);
      this.stopWordTextBox.Multiline = true;
      this.stopWordTextBox.Name = "stopWordTextBox";
      this.stopWordTextBox.Size = new System.Drawing.Size(392, 20);
      this.stopWordTextBox.TabIndex = 3;
      // 
      // stopWordLabel
      // 
      this.stopWordLabel.AutoSize = true;
      this.stopWordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.stopWordLabel.Location = new System.Drawing.Point(3, 26);
      this.stopWordLabel.Name = "stopWordLabel";
      this.stopWordLabel.Size = new System.Drawing.Size(114, 26);
      this.stopWordLabel.TabIndex = 2;
      this.stopWordLabel.Text = "Stop Word";
      this.stopWordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // httpTextBox
      // 
      this.httpTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.httpTextBox.Location = new System.Drawing.Point(123, 55);
      this.httpTextBox.Multiline = true;
      this.httpTextBox.Name = "httpTextBox";
      this.httpTextBox.Size = new System.Drawing.Size(392, 21);
      this.httpTextBox.TabIndex = 5;
      // 
      // httpLabel
      // 
      this.httpLabel.AutoSize = true;
      this.httpLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.httpLabel.Location = new System.Drawing.Point(3, 52);
      this.httpLabel.Name = "httpLabel";
      this.httpLabel.Size = new System.Drawing.Size(114, 27);
      this.httpLabel.TabIndex = 4;
      this.httpLabel.Text = "Web Service (Post)";
      this.httpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // applyButton
      // 
      this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.applyButton.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.applyButton.Location = new System.Drawing.Point(0, 79);
      this.applyButton.Name = "applyButton";
      this.applyButton.Size = new System.Drawing.Size(518, 38);
      this.applyButton.TabIndex = 0;
      this.applyButton.Text = "Apply";
      this.applyButton.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.ColumnCount = 2;
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel.Controls.Add(this.startWordLabel, 0, 0);
      this.tableLayoutPanel.Controls.Add(this.stopWordLabel, 0, 1);
      this.tableLayoutPanel.Controls.Add(this.httpLabel, 0, 2);
      this.tableLayoutPanel.Controls.Add(this.startWordTextBox, 1, 0);
      this.tableLayoutPanel.Controls.Add(this.stopWordTextBox, 1, 1);
      this.tableLayoutPanel.Controls.Add(this.httpTextBox, 1, 2);
      this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 3;
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel.Size = new System.Drawing.Size(518, 79);
      this.tableLayoutPanel.TabIndex = 1;
      // 
      // SettingsForm
      // 
      this.AcceptButton = this.applyButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(518, 117);
      this.Controls.Add(this.tableLayoutPanel);
      this.Controls.Add(this.applyButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SettingsForm";
      this.Text = "Settings";
      this.Load += new System.EventHandler(this.SettingsForm_Load);
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label startWordLabel;
    private System.Windows.Forms.Label stopWordLabel;
    private System.Windows.Forms.Label httpLabel;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    public System.Windows.Forms.TextBox startWordTextBox;
    public System.Windows.Forms.TextBox stopWordTextBox;
    public System.Windows.Forms.TextBox httpTextBox;
  }
}

