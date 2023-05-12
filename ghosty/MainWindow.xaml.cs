using ghosty.Actions.Mouse;
using ghosty.Operators;
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
using System.Windows.Threading;
using Point = System.Drawing.Point;

namespace ghosty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeTimers();
            InitializeComponent();
        }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer clockTimer = new DispatcherTimer();
        private DispatcherTimer lastInputTimeTimer = new DispatcherTimer();

        private void InitializeTimers()
        {
            clockTimer.Tick += new EventHandler(clockTimer_Tick);
            clockTimer.Interval = new TimeSpan(0, 0, 1);
            clockTimer.Start();

            lastInputTimeTimer.Tick += new EventHandler(lastInputTimeTimer_Tick);
            lastInputTimeTimer.Interval = new TimeSpan(0, 0, 1);
            lastInputTimeTimer.Start();
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);            
        }

        private void clockTimer_Tick(Object source, EventArgs e)
        {
            LblClock.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss");
        }

        private void lastInputTimeTimer_Tick(Object source, EventArgs e)
        {
            LblLastInputTime.Content = "System Idle Time: " + GetLastInputTime();
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
            MoveMouse();
        }

        private void MoveMouse() {

            int distance = 200;
            int speed = 1;

            Actions.Mouse.MouseMove.MoveUp(distance, speed);
        }

        private void BtnStartAction_Click(object sender, RoutedEventArgs e)
        {            
            if ((BtnStartAction.Content as string) == "Stop")
            {
                BtnStartAction.Content = "Start";
                dispatcherTimer.Stop();
            
            } else { 
            
                if ((BtnStartAction.Content as string) == "Start")
                {
                    dispatcherTimer.Interval = new TimeSpan(0, 0, Int32.Parse(TBInterval.Text));
                    dispatcherTimer.Start();
                    BtnStartAction.Content = "Stop";
                }
            }
            
        }
    }
}
