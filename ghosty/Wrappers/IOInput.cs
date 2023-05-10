using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ghosty.Wrappers
{
    internal class IOInput
    {
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            HWHEEL = 0x00001000,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSE_INPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public MOUSE_INPUT mi;
        }

        [Flags]
        public enum Win32Consts
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }

        [DllImport("user32.dll")]
        public static extern uint SendInput(
            uint nInputs,
            ref INPUT pInputs,
            int cbSize);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            public uint cbSize;
            public int dwTime;
        }
    }
}
