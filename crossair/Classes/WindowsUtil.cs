using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace crossair {
	class WindowsUtil {
		private const int GWL_STYLE = -16;
		private const int GWL_EX_STYLE = -20;

		private const int WS_EX_LAYERED = 0x80000;
		private const int WS_EX_TRANSPARENT = 0x20;
		private const int WS_EX_TOPMOST = 0x8; // Set using SetWindowPos!

		const int HWND_BOTTOM = 1; // Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
		const int HWND_NOTOPMOST = -2; // Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
		const int HWND_TOP = 0; // Places the window at the top of the Z order.
		const int HWND_TOPMOST = -1; // Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.

		const int SW_HIDE = 0;
		const int SW_SHOWNORMAL = 1;
		const int SW_SHOWMINIMIZED = 2;
		const int SW_SHOWMAXIMIZED = 3;
		const int SW_RESTORE = 9;

		const uint SWP_NOMOVE = 0x0002; // Retains the current position (ignores X and Y parameters).
		const uint SWP_NOSIZE = 0x0001; // Retains the current size (ignores the cx and cy parameters).
		const uint SWP_NOZORDER = 0x0004; // Retains the current Z order (ignores the hWndInsertAfter parameter).
    


		[DllImport("dwmapi.dll")]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);


		[Flags]
		public enum DWM_BB {
			Enable = 1,
			BlurRegion = 2,
			TransitionOnMaximized = 4
		}

		public struct DWM_BLURBEHIND {
			public DWM_BB dwFlags;
			public bool  fEnable;
			public IntPtr hRgnBlur;
			public bool fTransitionOnMaximized;
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
		
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, UInt32 flags);

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT {
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		public static IntPtr SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong) {
			/*if (IntPtr.Size == 4) {
				return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			} else {
				return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);*/
			//}
			return IntPtr.Zero;
		}

		[DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLong")]
		[SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return", Justification = "This declaration is not used on 64-bit Windows.")]
		[SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2", Justification = "This declaration is not used on 64-bit Windows.")]
		public static extern long SetWindowLongPtr32(IntPtr hWnd, Int32 nIndex, long dwNewLong);

		[DllImport("user32.dll", SetLastError = true, EntryPoint = "GetWindowLong")]

		public static extern long GetWindowLongPtr32(IntPtr hWnd, Int32 nIndex);

		[DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtr")]
		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "Entry point does exist on 64-bit Windows.")]
		private static extern long SetWindowLongPtr64(IntPtr hWnd, Int32 nIndex, long dwNewLong);


	}

}
