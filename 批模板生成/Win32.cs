using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace 批模板生成
{
	internal  class Win32
	{
		

	}
	internal static class NativeMethods
	{
		// 引用系统钩子
		[DllImport("user32.dll")]
		internal static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll")]
		internal static extern IntPtr SetActiveWindow(IntPtr hwnd);

		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		internal static extern uint ReleaseCapture();
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	}
}
