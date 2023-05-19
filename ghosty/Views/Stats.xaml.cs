using ghosty.Actions.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ghosty.Views
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        private DispatcherTimer clockTimer = new DispatcherTimer();
        private DispatcherTimer lastInputTimeTimer = new DispatcherTimer();

        public Stats()
        {
            InitializeComponent();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            // seting pre-timer values
            LblClock.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss");
            LblLastInputTime.Content = "System Idle Time: " + SOInteraction.GetLastInputTime();
            
            clockTimer.Tick += new EventHandler(clockTimer_Tick);
            clockTimer.Interval = new TimeSpan(0, 0, 1);
            clockTimer.Start();

            lastInputTimeTimer.Tick += new EventHandler(lastInputTimeTimer_Tick);
            lastInputTimeTimer.Interval = new TimeSpan(0, 0, 1);
            lastInputTimeTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            var mainWindow = new MainWindow();

            this.Left = desktopWorkingArea.Right - mainWindow.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height - mainWindow.Height;
        }

        private void clockTimer_Tick(Object source, EventArgs e)
        {
            LblClock.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss");
        }

        private void lastInputTimeTimer_Tick(Object source, EventArgs e)
        {
            LblLastInputTime.Content = "System Idle Time: " + SOInteraction.GetLastInputTime();
        }

        private void BtnCloseStats_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
