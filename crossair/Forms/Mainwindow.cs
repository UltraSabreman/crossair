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

using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace crossair {
	public partial class Overlay : Form {
		private bool enabled = false;
		private bool softPause = false;
		private Timer refresh = new System.Windows.Forms.Timer();
		private AsyncGlobalShortcuts keyListner = new AsyncGlobalShortcuts();
		private Regex windowTitle = null;

		private Sprite reticule = null;
		private RenderWindow mainWindow = null;
		private DataReader reader = new DataReader();
		private Config configs = new Config();

		public Overlay() {
			InitializeComponent();

			init();

			loadImage();

			windowTitle = new Regex(configs.TargetWindowTitle, RegexOptions.Compiled);
			
			refresh.Interval = 1000;
			refresh.Tick += new EventHandler(onTick);
			refresh.Start();

			keyListner.KeyPressed += new EventHandler<KeyPressedEventArgs>(hotkeyHandler);
			keyListner.RegisterHotKey(configs.ShowHideReticule);

			while (mainWindow.IsOpen()) {
				mainWindow.DispatchEvents();
				mainWindow.Clear(new SFML.Graphics.Color(0, 0, 0, 0));

				if (enabled && !softPause)
					mainWindow.Draw(reticule);

				mainWindow.Display();
			}
		}

		private void init() {
			// Create the main window
			mainWindow = new RenderWindow(new VideoMode(500, 500), "SFML window with OpenGL", Styles.None);

			// Make it the active window for OpenGL calls
			mainWindow.SetActive();
			//mainWindow.Closed += new EventHandler(OnClosed);

			WindowsUtil.DWM_BLURBEHIND t = new WindowsUtil.DWM_BLURBEHIND();
			t.dwFlags = WindowsUtil.DWM_BB.Enable;
			t.fEnable = true;
			t.hRgnBlur = new IntPtr();
			WindowsUtil.DwmEnableBlurBehindWindow(mainWindow.SystemHandle, ref t);
			WindowsUtil.EnableWindow(mainWindow.SystemHandle, false);

			mainWindow.SetVisible(false);



			/*// 0x00000020 = WS_EX_TRANSPARENT
			// 0x80 = WS_EX_TOOLWINDOW
			//-20 = GWL_EXSTYLE
			WindowsUtil.SetWindowLongPtr32(mainWindow.SystemHandle, -20, 0x00000020 | 0x80);
			//-16 = GWL_STYLE
			//0x80000000 == WS_POPUP
			//IntPtr test = new IntPtr(0x80000000L);
			WindowsUtil.SetWindowLongPtr32(mainWindow.SystemHandle, -16, 0x80000000L);
			//WindowsUtil.SetWindowPos(mainWindow.SystemHandle, 0, 0, 0, 0, 0, 0);*/
			//long test = WindowsUtil.GetWindowLongPtr32(mainWindow.SystemHandle, -20);
			//WindowsUtil.SetWindowLongPtr32(mainWindow.SystemHandle, -20, test | 0x00080000L);

			//TODO: Look at bookmark for implimentation details on making this shit click through.
			//Will most likely have to grab windows and actualy make them into classes.


			

			try {
				reader.Deserialize(configs);
			} catch (FileNotFoundException) { }

			exitToolStripMenuItem.Checked = configs.ExitWithProgram;
			toggleToolStripMenuItem.Checked = enabled;
		}

		private void onExit() {
			TrayIcon.Visible = false;

			string title = windowTitle.ToString();
			if (title == null)
				title = "";

			reader.Serialize(configs);
			//Window window = (Window)sender;
			//window.Close();
			mainWindow.Close();
			//keyListner.Dispose();
		}


		private void hotkeyHandler(object source, KeyPressedEventArgs e) {
			if (e.Key.First() == configs.ShowHideReticule.First()) {
				IntPtr targetWindow = Process.GetProcesses().FirstOrDefault().MainWindowHandle;

				enabled = !enabled;
			}
		}

		private void onTick(object source, EventArgs e) {
			IntPtr GameWindow = new IntPtr();

			try {
				GameWindow = Process.GetProcesses().FirstOrDefault(x => windowTitle.Match(x.MainWindowTitle).Success).MainWindowHandle;

				if (enabled) {
					TrayIcon.Icon = crossair.Properties.Resources.on;
				} else {
					TrayIcon.Icon = crossair.Properties.Resources.off;
				}
			
			} catch (System.NullReferenceException) {
				if (configs.ExitWithProgram) {
					this.Close();
					return;
				} else {
					TrayIcon.Icon = crossair.Properties.Resources.paused;
					return;
				}
			}

			//if (!enabled) return;

			if (WindowsUtil.GetForegroundWindow() != GameWindow) {
				softPause = true;
				return;
			} else {
				softPause = false;
				//WindowsUtil.BringWindowToTop(mainWindow.SystemHandle);
			}
			
			WindowsUtil.RECT tempSize = new WindowsUtil.RECT();
			WindowsUtil.GetWindowRect(GameWindow, ref tempSize);

			int newX = tempSize.Left + (tempSize.Right - tempSize.Left) / 2 - (int)mainWindow.Size.X/2;
			int newY = tempSize.Top + (tempSize.Bottom - tempSize.Top) / 2 - (int)mainWindow.Size.Y/2;

			//-1 == HWND_TOP											 SWP_SHOWWINDOW | SWP_NOSIZE
			WindowsUtil.SetWindowPos(mainWindow.SystemHandle, -1, newX, newY, (int)mainWindow.Size.X, (int)mainWindow.Size.Y, 0x0040 | 0x0001);
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
			if (softPause) return;

			enabled = !enabled;

			if (enabled) {
				toggleToolStripMenuItem.Checked = true;
				TrayIcon.Icon = crossair.Properties.Resources.on;
			} else {
				toggleToolStripMenuItem.Checked = false;
				TrayIcon.Icon = crossair.Properties.Resources.off;
			}
		}

		private void loadImage() {
			bool tryLoading = true;
			while (tryLoading) {
				try {
					reticule = new Sprite(new Texture(configs.ReticulePath) { Smooth = true });
					mainWindow.Size = reticule.Texture.Size;

					/*Vector2f size = new Vector2f(reticule.Texture.Size.X, reticule.Texture.Size.Y);
					Vector2f offset = new Vector2f(reticule.Texture.Size.X / 2, reticule.Texture.Size.Y / 2);

					mainWindow.SetView(new SFML.Graphics.View() { Size = size, Center = offset });*/
					tryLoading = false;

				} catch (SFML.LoadingFailedException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
				} catch (NullReferenceException) {
					if (MessageBox.Show("NULL, Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
				}
			}
		}


		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				//cp.ExStyle |= 0x80; //WS_EX_TOOLWINDOW
				////cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
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
			configs.ExitWithProgram = !configs.ExitWithProgram;
			exitWithGameToolStripMenuItem.Checked = configs.ExitWithProgram;
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
