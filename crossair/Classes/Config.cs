using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace crossair{
	class Config {
		public bool ExitWithProgram = true;
		public String ReticulePath = "ret.png";
		public HotKey ShowHideReticule = new HotKey(Keys.F4);
	}
}
