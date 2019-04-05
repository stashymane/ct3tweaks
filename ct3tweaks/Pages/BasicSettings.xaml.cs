using System;
using System.Windows;
using System.Windows.Controls;

namespace ct3tweaks.Pages
{
    /// <summary>
    /// Interaction logic for BasicSettings.xaml
    /// </summary>
    public partial class BasicSettings : UserControl
    {
        public BasicSettings()
        {
            InitializeComponent();
        }

        private int[] GetOptimalResolution()
        {
            return new int[] { Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight) };
        }

        private int GetOptimalRefreshRate()
        {
            return 60;
        }

        private double GetOptimalFov()
        {
            int[] res = GetOptimalResolution();
            return ((double) res[0] / (double) res[1]) * 45;
        }

        private void OptimisedResolution_Initialized(object sender, EventArgs e)
        {
            int[] res = GetOptimalResolution();
            OptimisedResolution.Text = res[0] + "x" + res[1];
        }

        private void OptimisedFramerate_Initialized(object sender, EventArgs e)
        {
            //TODO get monitor refresh rate
            OptimisedFramerate.Text = GetOptimalRefreshRate() + "fps";
        }

        private void OptimiseButton_Click(object sender, RoutedEventArgs e)
        {
            TweakLib.ResetDisplayMode();
            TweakLib.ChangeRefreshRate(GetOptimalRefreshRate());
            int[] res = GetOptimalResolution();
            TweakLib.ChangeResolution(res[0], res[1]);
            TweakLib.ChangeFOV((float) GetOptimalFov());
            MainWindow.ShowSuccessDialog();
        }

        private void OptimisedFov_Initialized(object sender, EventArgs e)
        {
            OptimisedFov.Text = Convert.ToInt32(GetOptimalFov()) + " FOV";
        }
    }
}
