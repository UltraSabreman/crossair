using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

public struct HotKey {
	public Keys KeyCode;

	public HotKey(Keys k) {
		KeyCode = k;
	}
}

public sealed class AsyncGlobalShortcuts {
	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(Keys vKey);

	//private Window _window = new Window();
	static private List<HotKey> keys = new List<HotKey>();
	public event EventHandler<KeyPressedEventArgs> KeyPressed;
	private Thread Listner = null;
	static private bool flag = true;
	



	public AsyncGlobalShortcuts() {
		Listner = new Thread(new ThreadStart(this.AsyncListener));
		Listner.Name = "Async Global Shortcuts";
		Listner.Start();
	}

	private void AsyncListener() {
		while (flag) {
			try {
				foreach (HotKey k in keys) {
					if (isKeyPressed(k.KeyCode) && KeyPressed != null)
						KeyPressed(this, new KeyPressedEventArgs(k.KeyCode));

				}
			} catch (System.InvalidOperationException) {
				continue;
			}
		}
	}

	static private bool isKeyPressed(Keys code) {
		short result = GetAsyncKeyState(code);

		if (((result >> 0xF) & 1) == 1 || (((result << 1) >> 1) & 1) == 1) 
			return true;

		return false;
	}
	
	/*
	/// <summary>
	/// Represents the window that is used internally to get the messages.
	/// </summary>
	private class Window : NativeWindow, IDisposable {
		private static int WM_HOTKEY = 0x0312;

		public Window() {
			// create the handle for the window.
			this.CreateHandle(new CreateParams());
		}

		/// <summary>
		/// Overridden to get the notifications.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);

			// check if we got a hot key pressed.
			if (m.Msg == WM_HOTKEY) {
				// get the keys.
				Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
				ModKeys modifier = (ModKeys)((int)m.LParam & 0xFFFF);

				// invoke the event to notify the parent.
				if (KeyPressed != null)
					KeyPressed(this, new KeyPressedEventArgs(modifier, key));
			}
		}

		public event EventHandler<KeyPressedEventArgs> KeyPressed;

		#region IDisposable Members

		public void Dispose() {
			this.DestroyHandle();
		}

		#endregion
	}*/

	/// <summary>
	/// Registers a hot key in the system.
	/// </summary>
	/// <param name="modifier">The modifiers that are associated with the hot key.</param>
	/// <param name="key">The key itself that is associated with the hot key.</param>
	public void RegisterHotKey(Keys key) {
		keys.Add(new HotKey(key));
		HotKey temp = keys.Last();
	}

	/// <summary>
	/// Registers a hot key in the system.
	/// </summary>
	/// <param name="hotkey">A HotKey Struct</param>
	public void RegisterHotKey(HotKey hotkey) {
		keys.Add(hotkey);
	}

	/// <summary>
	/// Unregisters a hot key in the system.
	/// </summary>
	/// <param name="modifier">The modifiers that are associated with the hot key.</param>
	/// <param name="key">The key itself that is associated with the hot key.</param>
	public void UnregisterHotKey(Keys key) {
		foreach (HotKey k in keys) {
			if (k.KeyCode == key) {
				keys.Remove(k);
				return;
			}
		}
		throw new InvalidOperationException("Couldn’t unregister the hot key.");
	}

	/// <summary>
	/// Unregisters a hot key in the system.
	/// </summary>
	/// <param name="hotkey">A HotKey Struct</param>
	public void UnregisterHotKey(HotKey hotkey) {
		if (keys.Contains(hotkey)) {
			keys.Remove(hotkey);
		} else
			throw new InvalidOperationException("Couldn’t unregister the hot key.");
	}

	/// <summary>
	/// A hot key has been pressed.
	/// </summary>
	

	#region IDisposable Members

	public void Dispose() {
	/*	// unregister all the registered hot keys.
		foreach (HotKey k in keys)
			UnregisterHotKey(_window.Handle, k.ID);

		// dispose the inner native window.
		_window.Dispose();*/
	}

	#endregion
}

/// <summary>
/// Event Args for the event that is fired after the hot key has been pressed.
/// </summary>
public class KeyPressedEventArgs : EventArgs {
	private Keys _key;

	internal KeyPressedEventArgs(Keys key) {
		_key = key;
	}

	public Keys Key {
		get { return _key; }
	}
}
