using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

/// <summary>
/// Hot key struct, holds the actual key, and all the modifiers.
/// </summary>
public struct HotKey {
	/// <summary>
	/// Contians the hotkey and all modifiers (the first entery is the hotkey)
	/// </summary>
	public List<Keys> Keys;

	public HotKey(List<Keys> mod) {
		Keys = mod;
	}
}

public sealed class AsyncGlobalShortcuts : IDisposable {
	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(Keys vKey);

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
					bool allPressed = true;
					foreach (Keys modKey in k.Keys) {
						if (!isKeyPressed(modKey)) {
							allPressed = false;
							break;
						}
					}

					if (allPressed && KeyPressed != null)
						KeyPressed(this, new KeyPressedEventArgs(k));
					
				}
			} catch (System.InvalidOperationException) {
				continue;
			}
		}
	}

	static private bool isKeyPressed(Keys code) {
		short result = GetAsyncKeyState(code);

		return (((result >> 0xF) & 1) == 1 || (((result << 1) >> 1) & 1) == 1);
	}

	/// <summary>
	/// Registers a hot key in the system with a variable amount of modifiers.
	/// </summary>
	/// <param name="hotkeys">Any and all modifiers you wish to add</param>
	public void RegisterHotKey(params object[] hotkeys) {
		List<Keys> temp = new List<Keys>();
		foreach (Keys k in hotkeys)
			temp.Add(k);

		keys.Add(new HotKey(temp));
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
	/// <param name="hotkey">A HotKey Struct</param>
	public void UnregisterHotKey(HotKey hotkey) {
		if (keys.Contains(hotkey)) {
			keys.Remove(hotkey);
		} else
			throw new InvalidOperationException("Couldn’t unregister the hot key.");
	}

	#region IDisposable Members

	public void Dispose() {
		flag = false;
		Listner.Abort();
		keys.Clear();
	}

	#endregion
}

/// <summary>
/// Event Args for the event that is fired after the hot key has been pressed.
/// </summary>
public class KeyPressedEventArgs : EventArgs {
	private HotKey _key;

	internal KeyPressedEventArgs(HotKey key) {
		_key = key;
	}

	public HotKey Key {
		get { return _key; }
	}
}
