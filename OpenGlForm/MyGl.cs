using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenGlForm
{
    public class MyGl
    {
        internal const int WM_ERASEBKGND = 0x0014;

        [DllImport("user32.dll")]
        internal static extern bool InvalidateRect(IntPtr nWnd, IntPtr lpRect, bool bErase);
        internal static Encoding RussianEncoding = Encoding.GetEncoding(1251);
    }
}
