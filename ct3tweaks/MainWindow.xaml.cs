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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ct3tweaks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetAdvancedMode(Properties.Settings.Default.AdvancedMode);
        }

        private void SetAdvancedMode(bool advanced)
        {
            Properties.Settings.Default.AdvancedMode = advanced;
            if (advanced)
                this.SettingsModeToggleButton.Content = "Simple mode";
            else
                this.SettingsModeToggleButton.Content = "Advanced mode";
        }

        private void SettingsModeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SetAdvancedMode(!Properties.Settings.Default.AdvancedMode);
        }
    }
}
