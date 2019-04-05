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

        private void IgnoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(url);
            this.Close();
        }
    }
}
