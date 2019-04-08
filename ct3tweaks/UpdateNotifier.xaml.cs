using System;
using System.Windows;

namespace ct3tweaks
{
    /// <summary>
    /// Interaction logic for UpdateNotifier.xaml
    /// </summary>
    public partial class UpdateNotifier : Window
    {
        string version;
        string url;

        public UpdateNotifier(string version, string url)
        {
            InitializeComponent();
            this.version = version;
            this.url = url;
            VersionLabel.Content = version;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(url);
            this.Close();
        }

        private void LaterButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NextCheck = DateTime.Now.AddDays(7);
            this.Close();
        }
    }
}
