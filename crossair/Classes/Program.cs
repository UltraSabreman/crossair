using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Window;
using SFML.Graphics;
using System.Windows.Forms;

//using Tao.OpenGl;

namespace crossair {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() {
			// Request a 32-bits depth buffer when creating the window
			ContextSettings contextSettings = new ContextSettings();
			contextSettings.DepthBits = 32;

			// Create the main window
			RenderWindow window = new RenderWindow(new VideoMode(640, 480), "SFML window with OpenGL", Styles.None);


			// Make it the active window for OpenGL calls
			window.SetActive();

			// Setup event handlers
			window.Closed += new EventHandler(OnClosed);
			window.KeyPressed += new EventHandler<SFML.Window.KeyEventArgs>(OnKeyPressed);
			window.Resized += new EventHandler<SizeEventArgs>(OnResized);
			CircleShape shape = new CircleShape(100);
			shape.FillColor = new Color(0,255,0,150);

			//SetWindowLong(window.SystemHandle, )



			WindowsUtil.DWM_BLURBEHIND t = new WindowsUtil.DWM_BLURBEHIND();
			t.dwFlags = WindowsUtil.DWM_BB.Enable;
			t.fEnable = true;
			t.hRgnBlur = new IntPtr();
			WindowsUtil.DwmEnableBlurBehindWindow(window.SystemHandle, ref t);



			// Start the game loop
			while (window.IsOpen()) {
				// Process events
				window.DispatchEvents();
				window.Clear(new Color(0,0,0,0));
				//Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);


				// Finally, display the rendered frame on screen
				window.Draw(shape);
				window.Display();
			}
		}

		/// <summary>
		/// Function called when the window is closed
		/// </summary>
		static void OnClosed(object sender, EventArgs e) {
			Window window = (Window)sender;
			window.Close();
		}

		/// <summary>
		/// Function called when a key is pressed
		/// </summary>
		static void OnKeyPressed(object sender, SFML.Window.KeyEventArgs e) {
			Window window = (Window)sender;
			if (e.Code == Keyboard.Key.Escape)
				window.Close();
		}

		/// <summary>
		/// Function called when the window is resized
		/// </summary>
		static void OnResized(object sender, SizeEventArgs e) {
			//Gl.glViewport(0, 0, (int)e.Width, (int)e.Height);
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