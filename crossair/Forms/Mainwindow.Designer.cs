namespace crossair {
	partial class Overlay {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overlay));
			this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitWithGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ReloadImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TrayContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TrayIcon
			// 
			this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
			this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
			this.TrayIcon.Visible = true;
			this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
			// 
			// TrayContextMenu
			// 
			this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem,
            this.exitWithGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.ReloadImageMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.TrayContextMenu.Name = "TrayContextMenu";
			this.TrayContextMenu.Size = new System.Drawing.Size(175, 126);
			// 
			// toggleToolStripMenuItem
			// 
			this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
			this.toggleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.toggleToolStripMenuItem.Text = "Toggle Overlay";
			this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
			// 
			// exitWithGameToolStripMenuItem
			// 
			this.exitWithGameToolStripMenuItem.Name = "exitWithGameToolStripMenuItem";
			this.exitWithGameToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exitWithGameToolStripMenuItem.Text = "Exit With Game";
			this.exitWithGameToolStripMenuItem.Click += new System.EventHandler(this.exitWithGameToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.optionsToolStripMenuItem.Text = "Set Target Window";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// ReloadImageMenuItem
			// 
			this.ReloadImageMenuItem.Name = "ReloadImageMenuItem";
			this.ReloadImageMenuItem.Size = new System.Drawing.Size(174, 22);
			this.ReloadImageMenuItem.Text = "Reload Image";
			this.ReloadImageMenuItem.Click += new System.EventHandler(this.ReloadImageMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// Overlay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(256, 256);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Overlay";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Overlay_FormClosing);
			this.TrayContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon TrayIcon;
		private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
		private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitWithGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ReloadImageMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

