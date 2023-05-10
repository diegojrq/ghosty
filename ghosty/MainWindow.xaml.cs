using ghosty.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = System.Drawing.Point;

namespace ghosty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer aTimer;
        public MainWindow()
        {
            InitializeTimers();
            InitializeComponent();
        }

        private void InitializeTimers()
        {
            System.Windows.Threading.DispatcherTimer clockTimer = new System.Windows.Threading.DispatcherTimer();
            clockTimer.Tick += new EventHandler(clockTimer_Tick);
            clockTimer.Interval = new TimeSpan(0, 0, 1);
            clockTimer.Start();

            System.Windows.Threading.DispatcherTimer lastInputTimeTimer = new System.Windows.Threading.DispatcherTimer();
            lastInputTimeTimer.Tick += new EventHandler(lastInputTimeTimer_Tick);
            lastInputTimeTimer.Interval = new TimeSpan(0, 0, 1);
            lastInputTimeTimer.Start();
        }

        private void clockTimer_Tick(Object source, EventArgs e)
        {
            LblClock.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss");
        }

        private void lastInputTimeTimer_Tick(Object source, EventArgs e)
        {
            LblLastInputTime.Content = GetLastInputTime();
        }

        static uint GetLastInputTime()
        {
            uint idleTime = 0;
            IOInput.LASTINPUTINFO lastInputInfo = new IOInput.LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = (uint)Environment.TickCount;

            if (IOInput.GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = (uint)lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }

            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            MoveMouse();
        }


        private void MoveMouse() {
            
            int distance = 200;
            int speed = 1;

            for (int i = 0; i < distance; i++)
            {
                Point point = new Point(0, -1);

                try
                {
                    var mi = new IOInput.MOUSE_INPUT
                    {
                        dx = point.X,
                        dy = point.Y,
                        mouseData = 0,
                        time = 0,
                        dwFlags = IOInput.MouseEventFlags.MOVE,
                        dwExtraInfo = UIntPtr.Zero
                    };
                    var input = new IOInput.INPUT
                    {
                        mi = mi,
                        type = Convert.ToInt32(IOInput.Win32Consts.INPUT_MOUSE)
                    };
                    IOInput.SendInput(1, ref input, Marshal.SizeOf(input));
                }
                catch (Exception ex)
                {
                    //StaticCode.Logger?.Here().Error(ex.Message);
                }

                Thread.Sleep(speed);
            }
            //lbl.Content = GetLastInputTime().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }
    }
}
