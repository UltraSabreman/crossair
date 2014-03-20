using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace crossair{
	class Config {
		public bool ExitWithProgram = false;
		public String ReticulePath = "ret.png";
		public String TargetWindowTitle = "Calculator";
		public HotKey ShowHideReticule = new HotKey(Keys.F4);
	}
}
