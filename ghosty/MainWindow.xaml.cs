using ghosty.Actions.Mouse;
using ghosty.Actions.System;
using ghosty.Operators;
using ghosty.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;
using Point = System.Drawing.Point;

namespace ghosty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer actionTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeTimers();
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void InitializeTimers()
        {
            actionTimer.Tick += new EventHandler(actionTimer_Tick);
            Properties.Settings.Default.isWorking = false;
        }

        private void actionTimer_Tick(object sender, EventArgs e)
        {
            MoveMouse();
        }

        private void MoveMouse() {
            Actions.Mouse.MouseMove.MoveUp(Properties.Settings.Default.distance, Properties.Settings.Default.speed);
        }

        private void BtnStartAction_Click(object sender, RoutedEventArgs e)
        {            
            if (Properties.Settings.Default.isWorking)
            {
                actionTimer.Stop();
                Properties.Settings.Default.isWorking = false;

                BtnStartAction.Content = new Image
                {
                    Source = new BitmapImage(new Uri("/Resources/Images/play-button.png", UriKind.Relative))
                };

            } else {             

                if (!Properties.Settings.Default.isWorking)
                {
                    actionTimer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.interval);
                    actionTimer.Start();
                    Properties.Settings.Default.isWorking = true;

                    BtnStartAction.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("/Resources/Images/stop-button.png", UriKind.Relative))
                    };
                }
            }
            
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
        }

        private void BtnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.GrdMainGrid.FindResource("PlayAnimationStoryboard") as Storyboard;
            Storyboard.SetTarget(sb, this.GrdMainGrid);
            sb.Completed += (sender, eArgs) => Application.Current.Shutdown();
            sb.Begin();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            Stats statsWindow = new Stats();
            statsWindow.Show();
        }
    }
}
