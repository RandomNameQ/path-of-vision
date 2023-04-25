using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Poe_show_buff
{
    public static class FormExtensions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static int SetWindowLong(IntPtr hwnd, int index, int value);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int LWA_ALPHA = 0x2;

        public static void ClickThrough(this Form form)
        {
            SetWindowLong(form.Handle, GWL_EXSTYLE, GetWindowLong(form.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetLayeredWindowAttributes(form.Handle, 0, 128, LWA_ALPHA);
        }
    }
}
