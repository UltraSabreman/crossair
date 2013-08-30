using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace crossair {
	public partial class TextEnterBox : Form {
		public delegate void test(string lol);
		public event test onClose;
		private string text = "";

		public TextEnterBox(string t = "") {
			InitializeComponent();

			text = t;
			textBox1.Text = text;
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {
			text = textBox1.Text;
		}

		private void Ok_Click(object sender, EventArgs e) {
			if (onClose != null)
				onClose(text);

			this.Close();
		}

		private void Cancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				if (onClose != null)
					onClose(text);

				this.Close();
			} else if (e.KeyCode == Keys.Escape) {
				this.Close();
			}
		}	
	}
}
