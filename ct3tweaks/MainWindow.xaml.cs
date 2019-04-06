using ct3tweaks.Pages;
using Octokit;
using Semver;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ct3tweaks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static GitHubClient client = new GitHubClient(new ProductHeaderValue("ct3tweaks"));
        AdvancedSettings a = new AdvancedSettings();
        BasicSettings b = new BasicSettings();

        public MainWindow()
        {
            InitializeComponent();
            SetAdvancedMode(Properties.Settings.Default.AdvancedMode);
            UpdateDirectory();
            CheckForUpdates();
        }

        private void SetAdvancedMode(bool advanced)
        {
            Properties.Settings.Default.AdvancedMode = advanced;
            Properties.Settings.Default.Save();
            UserControl toLoad;
            if (advanced)
            {
                toLoad = a;
                this.SettingsModeToggleButton.Content = "Simple mode";
            }
            else
            {
                toLoad = b;
                this.SettingsModeToggleButton.Content = "Advanced mode";
            }
            this.Height = BrowseGrid.Height + AdvancedToggleGrid.Height + toLoad.Height + 40;
            SettingsGrid.Children.Clear();
            SettingsGrid.Children.Add(toLoad);
        }

        private void SettingsModeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SetAdvancedMode(!Properties.Settings.Default.AdvancedMode);
        }

        private void UpdateDirectory()
        {
            if (Properties.Settings.Default.Directory.Equals(""))
            {
                string[] common = new string[4];
                common[0] = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                common[1] = Environment.SpecialFolder.ProgramFiles + "/Crazy Taxi 3";
                common[2] = "C:/Games/Crazy Taxi 3";
                common[3] = "D:/Games/Crazy Taxi 3";
                foreach (string path in common)
                {
                    string exepath = path + "/CT3.exe";
                    if (File.Exists(exepath))
                        SetDirectory(path);
                }
            }
            else
                SetDirectory(Properties.Settings.Default.Directory);
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
                ShowGameNotFoundError();
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

        public static void ShowSuccessDialog()
        {
            MessageBoxResult result = MessageBox.Show("Successfully saved settings!",
                                          "Success",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
        }

        public static void ShowGameNotFoundError()
        {
            MessageBoxResult result = MessageBox.Show("Game not found in specified directory!",
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
        }

        public static async void CheckForUpdates()
        {
            if (DateTime.Now.Subtract(Properties.Settings.Default.LastUpdateCheck).TotalDays > 7)
            {
                try
                {
                    Properties.Settings.Default.LastUpdateCheck = DateTime.Now;
                    Release latest = await client.Repository.Release.GetLatest("stashymane", "ct3tweaks");

                    if (SemVersion.Parse(latest.TagName) > SemVersion.Parse(Assembly.GetExecutingAssembly().GetName().Version.ToString()))
                        new UpdateNotifier(latest.TagName, latest.Url).ShowDialog();
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
