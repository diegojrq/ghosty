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

namespace ghosty.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            TBDistance.Text = Properties.Settings.Default.distance.ToString();
            TBSpeed.Text = Properties.Settings.Default.speed.ToString();
            TBInterval.Text = Properties.Settings.Default.interval.ToString();            
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.distance = Int32.Parse(TBDistance.Text);
            Properties.Settings.Default.speed = Int32.Parse(TBSpeed.Text);
            Properties.Settings.Default.interval = Int32.Parse(TBInterval.Text);

            Close();
        }
    }
}
