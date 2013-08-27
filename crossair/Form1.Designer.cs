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
			this.DrawSpace = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.DrawSpace)).BeginInit();
			this.SuspendLayout();
			// 
			// TrayIcon
			// 
			this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
			this.TrayIcon.Visible = true;
			this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
			// 
			// DrawSpace
			// 
			this.DrawSpace.BackColor = System.Drawing.Color.Transparent;
			this.DrawSpace.ErrorImage = null;
			this.DrawSpace.InitialImage = null;
			this.DrawSpace.Location = new System.Drawing.Point(0, 0);
			this.DrawSpace.Name = "DrawSpace";
			this.DrawSpace.Size = new System.Drawing.Size(256, 256);
			this.DrawSpace.TabIndex = 0;
			this.DrawSpace.TabStop = false;
			// 
			// Overlay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(256, 256);
			this.ControlBox = false;
			this.Controls.Add(this.DrawSpace);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Overlay";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.DrawSpace)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon TrayIcon;
		private System.Windows.Forms.PictureBox DrawSpace;
	}
}

