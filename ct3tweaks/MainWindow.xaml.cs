using ct3tweaks.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ct3tweaks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Resolution> resolutions = new List<Resolution>();

        public MainWindow()
        {
            InitializeComponent();
            ProfileManager.ProfileChange += ProfileManager_ProfileChange;
            ProfileManager.DirectoryChange += ProfileManager_DirectoryChange;
            LoadDirectory();
            LoadResolutions();
            LoadProfile();
            Updater.Check();
        }

        private void ProfileManager_DirectoryChange(string past, string present)
        {
            if (present.Equals(""))
                DirectoryString.Text = "Not set";
            else
                DirectoryString.Text = present;
        }

        private void ProfileManager_ProfileChange(Profile past, Profile present)
        {
            present.ResolutionChange += Present_ResolutionChange;
            present.FramerateChange += Present_FramerateChange;
            present.FOVChange += Present_FOVChange;
        }

        private void Present_FOVChange(float past, float present)
        {
            FovSlider.Value = present;
            FOVInput.Text = "" + present;
        }

        private void Present_FramerateChange(int past, int present)
        {
            FramerateSlider.Value = present;
            FramerateInput.Text = "" + present;
        }

        private void Present_ResolutionChange(Resolution past, Resolution present)
        {
            if (resolutions.Contains(present))
                ResolutionSlider.Value = resolutions.IndexOf(present);
            ResolutionInput.Text = present.w + "x" + present.h;
        }

        private void LoadDirectory()
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
                    try
                    {
                        ProfileManager.Directory = path;
                        break;
                    }
                    catch (FileNotFoundException)
                    {
                        continue;
                    }
                }
            }
            else
            {
                try
                {
                    ProfileManager.Directory = Properties.Settings.Default.Directory;
                }
                catch (FileNotFoundException)
                {
                    Properties.Settings.Default.Directory = "";
                    Properties.Settings.Default.Save();
                    ProfileManager.Directory = "";
                }
            }
        }

        private void LoadResolutions()
        {
            resolutions.Add(new Resolution(1280, 720));
            resolutions.Add(new Resolution(1920, 1080));
            ResolutionSlider.Maximum = resolutions.Count - 1;
        }

        private void LoadProfile()
        {
            ProfileManager.CurrentProfile = new Profile(new Resolution(1920, 1080), 60, 90);
            Present_ResolutionChange(null, ProfileManager.CurrentProfile.Resolution);
            Present_FramerateChange(0, ProfileManager.CurrentProfile.Framerate);
            Present_FOVChange(0, ProfileManager.CurrentProfile.FOV);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs r)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                if (!dialog.SelectedPath.Equals(""))
                {
                    try
                    {
                        ProfileManager.Directory = dialog.SelectedPath;
                    }
                    catch (FileNotFoundException e)
                    {
                        MessageBox.Show(e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ApplyAndLaunchButton_Click(object sender, RoutedEventArgs r)
        {
            try
            {
                ProfileManager.Apply();
                System.Diagnostics.Process.Start(ProfileManager.ExecutablePath);
            }
            catch (FileNotFoundException e)
            {
                FailedToApply(e);
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs r)
        {
            try
            {
                ProfileManager.Apply();
                MessageBox.Show("Profile successfully applied!");
            }
            catch (FileNotFoundException e)
            {
                FailedToApply(e);
            }
        }

        private void FailedToApply(FileNotFoundException e)
        {
            MessageBox.Show("Failed to apply profile:\n" + e.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void FramerateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FramerateSlider.IsMouseCaptured || FramerateSlider.IsFocused)
                ProfileManager.CurrentProfile.Framerate = (int) FramerateSlider.Value;
        }

        private void ResolutionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ResolutionSlider.IsMouseCaptured || ResolutionSlider.IsFocused)
                ProfileManager.CurrentProfile.Resolution = resolutions[(int) ResolutionSlider.Value];
        }

        private void FovSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FovSlider.IsMouseCaptured || FovSlider.IsFocused)
                ProfileManager.CurrentProfile.FOV = (float) FovSlider.Value;
        }

        private void DefaultsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TweakLib.RestoreBackup(ProfileManager.ExecutablePath, ProfileManager.BackupExecutablePath);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No backup available");
            }
        }
    }
}
