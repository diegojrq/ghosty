using ghosty.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ghosty.Operators.IODefiners;

namespace ghosty.Actions.System
{
    public static class SOInteraction
    {

        public static uint GetLastInputTime()
        {
            uint idleTime = 0;
            IODefiners.LASTINPUTINFO lastInputInfo = new IODefiners.LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;
            uint envTicks = (uint)Environment.TickCount;

            if (IODefiners.GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = (uint)lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }

            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }
    }
}
