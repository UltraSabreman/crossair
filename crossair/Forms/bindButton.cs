using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace crossair {
	class bindButton : System.Windows.Forms.Button {
		public List<Keys> keyBind = new List<Keys>();
		private List<Keys> tempKeys = new List<Keys>();
		public string controlString = "Bind";


		public delegate void KeysDone(List<Keys> keys);
		public event KeysDone OnKeyAccept;

		private bool enterKeys = false;
		private bool holdEscape = false;

		private Timer escapeDelay = new Timer();

		public bindButton() {
			keyBind = new List<Keys>();

			updateText();

			escapeDelay.Interval = 2000;
			escapeDelay.Tick += new EventHandler(escape);
		}

		public bindButton(List<Keys> inkeys) {
			if (inkeys != null)
				keyBind = inkeys;
			else
				keyBind = new List<Keys>();

			updateText();

			escapeDelay.Interval = 2000;
			escapeDelay.Tick += new EventHandler(escape);
		}

		private void escape(object source, EventArgs e) {
			keyBind.Clear();
			tempKeys.Clear();

			updateText();
		}

		protected override void OnClick(EventArgs e) {
			tempKeys.Clear();
			foreach (Keys k in keyBind)
				tempKeys.Add(k);

			controlString = "Hit ESC to Cancel, Hold to Clear";
			this.Refresh();
			base.OnClick(e);
		}


		protected void updateText(bool tempOrNot = false) {
			List<Keys> temp = (tempOrNot ? tempKeys : keyBind);

			controlString = "";
			if (temp == null || temp.Count == 0)
				controlString = "No Bind";
			else {
				int ind = 0;
				foreach (Keys k in temp) {
					controlString += k.ToString();
					if (ind < temp.Count - 1)
						controlString += " + ";
					ind++;
				}
			}

			this.Refresh();
		}
		protected override void OnKeyDown(KeyEventArgs kevent) {
			if (!enterKeys) {
				if (kevent.KeyCode == Keys.Escape && !holdEscape) {
					holdEscape = true;
					escapeDelay.Start();
				} else if (kevent.KeyCode != Keys.Enter && kevent.KeyCode != Keys.Escape) {
					enterKeys = true;
					tempKeys.Clear();

					tempKeys.Add(kevent.KeyCode);
					updateText(true);
				}
			} else if (enterKeys && !tempKeys.Contains(kevent.KeyCode)) {
				tempKeys.Add(kevent.KeyCode);
				updateText(true);
			}

			base.OnKeyDown(kevent);
		}

		protected override void OnKeyUp(KeyEventArgs kevent) {
			if (enterKeys) {
				keyBind.Clear();
				foreach (Keys k in tempKeys)
					keyBind.Add(k);

				tempKeys.Clear();

				if (OnKeyAccept != null)
					OnKeyAccept(keyBind);

				holdEscape = false;
				enterKeys = false;
				updateText(false);
			} else {
				if (kevent.KeyCode == Keys.Escape) {
					escapeDelay.Stop();
					enterKeys = false;
					holdEscape = false;
				}
				updateText(true);
			}

			base.OnKeyUp(kevent);
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			Pen p = new Pen(Color.Black, 1f);
			Brush b = new SolidBrush(Color.White);
			Brush fontBrush = new SolidBrush(Color.Black);
			Font f = new Font("Arial", 8, FontStyle.Regular);
			SizeF txtSize = TextRenderer.MeasureText(controlString, f);

			e.Graphics.FillRectangle(b, new Rectangle(new Point(0, 0), this.Size));
			e.Graphics.DrawRectangle(p, new Rectangle(new Point(0, 0), this.Size - new Size(1, 1)));
			e.Graphics.DrawString(controlString, f, fontBrush, this.Size.Width / 2 - txtSize.Width / 2, this.Size.Height / 2 - txtSize.Height / 2);

			f.Dispose();
			b.Dispose();
			p.Dispose();
		}
	}
}
