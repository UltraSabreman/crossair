using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace crossair {
	public partial class Overlay : Form {
		private bool OverlayOn = true;
		private Bitmap Reticule = null;
		private Timer refresh = new Timer();


		public Overlay() {
			InitializeComponent();

			loadImage();

			refresh.Interval = 1000;
			refresh.Tick += new EventHandler(onTick);
			refresh.Start();

			this.Opacity = 0;
		}

		private void onTick(object source, EventArgs e) {
			IntPtr GameWindow = new IntPtr();
			try {
				GameWindow = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle.Contains("Planetside")).MainWindowHandle; //change me
			} catch (NullReferenceException) {
				this.Close();
				return;
			}

			if (!OverlayOn) return;

			BringWindowToTop(this.Handle);
			if (GetForegroundWindow() != GameWindow) {
				this.Opacity = 0;
				return;
			} else
				this.Opacity = 100;
			

			RECT tempSize = new RECT();
			GetWindowRect(GameWindow, ref tempSize);

			int newX = tempSize.Left + (tempSize.Right - tempSize.Left) / 2 - this.Size.Width/2;
			int newY = tempSize.Top + (tempSize.Bottom - tempSize.Top) / 2 - this.Size.Height/2;
			
			this.Location = new Point(newX, newY);
		}

		private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Clicks == 2) 
				loadImage();
			else if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2) {
				OverlayOn = !OverlayOn;

				if (OverlayOn) {
					TrayIcon.Icon = crossair.Properties.Resources.on;
					this.Opacity = 100;
				} else {
					TrayIcon.Icon = crossair.Properties.Resources.off;
					this.Opacity = 0;
				}
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e) {
			TrayIcon.Visible = false;
			//TrayIcon.Dispose();
			base.OnFormClosing(e);
		}


		private void loadImage() {
			if (Reticule != null)
				Reticule.Dispose();

			while (true) {
				try {
					Reticule = new Bitmap("ret.bmp");
					this.Size = Reticule.Size;
					DrawSpace.Size = Reticule.Size;

					this.TransparencyKey = Reticule.GetPixel(0, 0);
					this.BackColor = this.TransparencyKey;
					DrawSpace.Image = Reticule;
					DrawSpace.Refresh();

					return;
				} catch (System.IO.FileNotFoundException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel) {
						System.Environment.Exit(0);
						
					}
				} catch (System.ArgumentException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
				}
			}
		}


		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x80; //WS_EX_TOOLWINDOW
				cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
				return cp;
			}
		}
		protected override bool ShowWithoutActivation {
			get { return true; }
		}

		/////////////////////
		// invoked dll bs

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool BringWindowToTop(IntPtr hWnd);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}


	}
}
