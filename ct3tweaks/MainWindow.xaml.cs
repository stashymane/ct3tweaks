﻿using System.IO;
using System.Windows;

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
            SetDirectory(Properties.Settings.Default.Directory);
        }

        private void SetAdvancedMode(bool advanced)
        {
            Properties.Settings.Default.AdvancedMode = advanced;
            Properties.Settings.Default.Save();
            if (advanced)
                this.SettingsModeToggleButton.Content = "Simple mode";
            else
                this.SettingsModeToggleButton.Content = "Advanced mode";
        }

        private void SettingsModeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SetAdvancedMode(!Properties.Settings.Default.AdvancedMode);
        }

        private void SetDirectory(string directory)
        {
            if (File.Exists(directory + "/CT3.exe"))
            {
                Properties.Settings.Default.Directory = directory;
                Properties.Settings.Default.Save();
            }
            else if (!directory.Equals(""))
            {
                MessageBoxResult result = MessageBox.Show("Game not found in specified directory!\n" + directory,
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Asterisk);
                Properties.Settings.Default.Directory = "";
                Properties.Settings.Default.Save();
            }
            UpdateDirectoryString();
        }

        private void UpdateDirectoryString()
        {
            string location = Properties.Settings.Default.Directory;
            if (!location.Equals(""))
                this.GameLocationString.Text = location;
            else
                this.GameLocationString.Text = "Not set";
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                if (!dialog.SelectedPath.Equals(""))
                    SetDirectory(dialog.SelectedPath);
            }
        }
    }
}