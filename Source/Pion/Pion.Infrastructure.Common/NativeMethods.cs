using System;
using System.Runtime.InteropServices;

namespace Pion.Infrastructure.Common
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
    }
}
