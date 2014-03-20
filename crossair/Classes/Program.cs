using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Window;
using SFML.Graphics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace crossair {
	static class Program {
		static Sprite reticule = null;
		static RenderWindow mainWindow = null;
		static DataReader reader = new DataReader();
		static Config configs = new Config();

		static void Main() {
			init();

			bool tryLoading = true;
			while (tryLoading) {
				try {
					reticule = new Sprite(new Texture("ret.png") { Smooth = true });
					mainWindow.Size = reticule.Texture.Size;
					tryLoading = false;
				} catch (SFML.LoadingFailedException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
				} catch (NullReferenceException) {
					if (MessageBox.Show("Reticule File Not Found, press OK to retry, Cancel to quit", "Error Loading File", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
						System.Environment.Exit(0);
				}
			}

			while (mainWindow.IsOpen()) {
				mainWindow.DispatchEvents();
				mainWindow.Clear(new Color(0, 0, 0, 0));

				mainWindow.Draw(reticule);
				mainWindow.Display();
			}
		}

		static void init() {
			// Create the main window
			mainWindow = new RenderWindow(new VideoMode(640, 480), "SFML window with OpenGL", Styles.None);

			// Make it the active window for OpenGL calls
			mainWindow.SetActive();
			mainWindow.Closed += new EventHandler(OnClosed);

			WindowsUtil.DWM_BLURBEHIND t = new WindowsUtil.DWM_BLURBEHIND();
			t.dwFlags = WindowsUtil.DWM_BB.Enable;
			t.fEnable = true;
			t.hRgnBlur = new IntPtr();
			WindowsUtil.DwmEnableBlurBehindWindow(mainWindow.SystemHandle, ref t);
			try {
				reader.Deserialize(configs);
			} catch (FileNotFoundException ) { }
		}

		/// <summary>
		/// Function called when the window is closed
		/// </summary>
		static void OnClosed(object sender, EventArgs e) {
			Window window = (Window)sender;
			window.Close();
		}
	}


}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace crossair {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(resolveDll);
			Application.Run(new Overlay());
		}

		static Assembly resolveDll(object sender, ResolveEventArgs args) {
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("crossair.Newtonsoft.Json.dll")) {
				byte[] assemblyData = new byte[stream.Length];
				stream.Read(assemblyData, 0, assemblyData.Length);
				return Assembly.Load(assemblyData);
			}
		}
	}
}
*/