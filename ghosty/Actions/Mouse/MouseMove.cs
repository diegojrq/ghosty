﻿using ghosty.Actions.System;
using ghosty.Operators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ghosty.Actions.Mouse
{
    public static class MouseMove
    {
        public static void Move(Point point)
        {
            try
            {
                var mouseInput = new IODefiners.MOUSE_INPUT
                {
                    dx = point.X,
                    dy = point.Y,
                    mouseData = 0,
                    time = 0,
                    dwFlags = IODefiners.MouseEventFlags.MOVE,
                    dwExtraInfo = UIntPtr.Zero
                };

                var input = new IODefiners.INPUT
                {
                    mouseInput = mouseInput,
                    type = Convert.ToInt32(IODefiners.Win32Consts.INPUT_MOUSE)
                };

                IODefiners.SendInput(1, ref input, Marshal.SizeOf(input));
            }
            catch (Exception ex)
            {
                //StaticCode.Logger?.Here().Error(ex.Message);
            }
        }

        public static void MoveUp(int distance, int speed)
        {
            for (int i = 0; i < distance; i++)
            {
                Move(new Point(0, -1));
                Thread.Sleep(speed);
            }
        }
    }
}
