using ct3tweaks.Interface.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ct3tweaks.Interface
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        TweaksTab tweaks = new TweaksTab();
        ProfileTab profiles = new ProfileTab();

        public Main()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void DragArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TweaksTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfilesTabButton.IsChecked == true)
                ProfilesTabButton.IsChecked = false;
            else
                TweaksTabButton.IsChecked = true;
        }

        private void ProfilesTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (TweaksTabButton.IsChecked == true)
                TweaksTabButton.IsChecked = false;
            else
                ProfilesTabButton.IsChecked = true;
        }
    }
}
