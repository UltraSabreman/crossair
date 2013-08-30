using System;
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
