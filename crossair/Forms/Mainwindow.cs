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
using System.Text.RegularExpressions;

namespace crossair {
	public partial class Overlay : Form {
		private bool OverlayOn = true;
		private bool ExitWithGame = true;
		private bool paused = false;
		private Bitmap Reticule = null;
		private Timer refresh = new System.Windows.Forms.Timer();
		private AsyncGlobalShortcuts test = new AsyncGlobalShortcuts();
		private Regex windowTitle = new Regex("WINDOWTITLE", RegexOptions.Compiled);
		private dataReader reader = new dataReader();

		private bool updateIcon = false;

		public Overlay() {
			InitializeComponent();

			loadImage();

			refresh.Interval = 1000;
			refresh.Tick += new EventHandler(onTick);
			refresh.Start();

			this.Opacity = 0;

			test.KeyPressed += new EventHandler<KeyPressedEventArgs>(hotkeyHandler);
			test.RegisterHotKey(Keys.F12);
			

			readOptions();
		}

		private void readOptions() {
			reader.deserialize();
			windowTitle = new Regex(reader.gameWindowTitle, RegexOptions.Compiled);
			ExitWithGame = reader.closeWithGame;
			exitToolStripMenuItem.Checked = ExitWithGame;
			toggleToolStripMenuItem.Checked = OverlayOn;
		}

		private void onExit() {
			TrayIcon.Visible = false;

			string title = windowTitle.ToString();
			if (title == null)
				title = "";
			reader.gameWindowTitle = title;
			reader.closeWithGame = ExitWithGame;
			reader.serialize();
			test.Dispose();
		}


		private void hotkeyHandler(object source, KeyPressedEventArgs e) {
			if (e.Key.First() == Keys.F12) {
				IntPtr test = Process.GetProcesses().FirstOrDefault().MainWindowHandle;
				Form lolwindow = Control.FromHandle(test) as Form;
				try {
					lolwindow.Opacity = 0.5;
				} catch (NullReferenceException) {
					MessageBox.Show("Null");
				}
			}
		}

		private void onTick(object source, EventArgs e) {
			IntPtr GameWindow = new IntPtr();

			try {
				GameWindow = Process.GetProcesses().FirstOrDefault(x => windowTitle.Match(x.MainWindowTitle).Success).MainWindowHandle; //change me

				if (updateIcon) {
					paused = false;
					if (OverlayOn) {
						TrayIcon.Icon = crossair.Properties.Resources.on;
						this.Opacity = 100;
					} else {
						TrayIcon.Icon = crossair.Properties.Resources.off;
						this.Opacity = 0;
					}

					paused = true;
					updateIcon = false;
				}
			} catch (System.NullReferenceException) {
				if (ExitWithGame) {
					this.Close();
					return;
				} else {
					if (!updateIcon) {
						TrayIcon.Icon = crossair.Properties.Resources.paused;
						this.Opacity = 0;
						paused = true;
						updateIcon = true;
					}
					return;
				}
			}

			if (!OverlayOn) return;

			if (WindowsUtil.GetForegroundWindow() != GameWindow) {
				this.Opacity = 0;
				return;
			} else {
				this.Opacity = 100;
				WindowsUtil.BringWindowToTop(this.Handle);
			}


			WindowsUtil.RECT tempSize = new WindowsUtil.RECT();
			WindowsUtil.GetWindowRect(GameWindow, ref tempSize);

			int newX = tempSize.Left + (tempSize.Right - tempSize.Left) / 2 - this.Size.Width/2;
			int newY = tempSize.Top + (tempSize.Bottom - tempSize.Top) / 2 - this.Size.Height/2;
			
			this.Location = new Point(newX, newY);
		}

		private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Clicks == 2) 
				loadImage();
			else if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2)
				toggle();
		}

		protected override void OnFormClosing(FormClosingEventArgs e) {
			TrayIcon.Visible = false;
			base.OnFormClosing(e);
		}

		private void toggle() {
			if (paused) return;

			OverlayOn = !OverlayOn;

			if (OverlayOn) {
				toggleToolStripMenuItem.Checked = true;
				TrayIcon.Icon = crossair.Properties.Resources.on;
				this.Opacity = 100;
			} else {
				toggleToolStripMenuItem.Checked = false;
				TrayIcon.Icon = crossair.Properties.Resources.off;
				this.Opacity = 0;
			}
		}

		private void loadImage() {
			if (Reticule != null)
				Reticule.Dispose();

			while (true) {
				try {
					Bitmap tempBMP = new Bitmap("ret.bmp");
					Reticule = new Bitmap(tempBMP);
					tempBMP.Dispose();

					this.Size = Reticule.Size;
					DrawSpace.Size = Reticule.Size;

					this.TransparencyKey = Reticule.GetPixel(0, 0);
					this.BackColor = this.TransparencyKey;
					DrawSpace.Image = Reticule;
					DrawSpace.Refresh();
					return;
				} catch (System.IO.FileNotFoundException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
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

		
		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			onExit();
			this.Close();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
			TextEnterBox temp = new TextEnterBox(windowTitle.ToString());
			temp.onClose += new TextEnterBox.test(getWinTitle);
			temp.Show();
		}

		private void getWinTitle(string t) {
			try {
				windowTitle = new Regex(t, RegexOptions.Compiled);
			} catch(System.ArgumentException) {
				System.Windows.Forms.DialogResult res = MessageBox.Show("ERROR: Invalid Regex! \n Please use a simple string or correct your regex.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
				if (res == System.Windows.Forms.DialogResult.Retry) {
					TextEnterBox temp = new TextEnterBox(windowTitle.ToString());
					temp.onClose += new TextEnterBox.test(getWinTitle);
					temp.Show();
				}
			}
		}

		private void exitWithGameToolStripMenuItem_Click(object sender, EventArgs e) {
			ExitWithGame = !ExitWithGame;
			exitWithGameToolStripMenuItem.Checked = ExitWithGame;
		}

		private void toggleToolStripMenuItem_Click(object sender, EventArgs e) {
			toggle();
		}

		private void ReloadImageMenuItem_Click(object sender, EventArgs e) {
			loadImage();
		}

		private void Overlay_FormClosing(object sender, FormClosingEventArgs e) {
			onExit();
		}


	}
}
