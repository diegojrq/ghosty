using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ghosty.Operators
{
    internal class IOInput
    {
        [Flags]
        public enum MouseEventFlags
        {
            ABSOLUTE = 0x00008000,
            MOVE = 0x00000001,
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            XUP = 0x00000100,
            XDOWN = 0x00000080,
            WHEEL = 0x00000800,
            HWHEEL = 0x00001000
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
            public MOUSE_INPUT mouseInput;
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
