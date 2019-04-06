using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;

namespace ct3tweaks.Pages
{
    /// <summary>
    /// Interaction logic for AdvancedSettings.xaml
    /// </summary>
    public partial class AdvancedSettings : UserControl
    {
        public AdvancedSettings()
        {
            InitializeComponent();
        }

        private void CheckFovNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TweakLib.FilesExist())
            {
                string[] res = ResolutionPicker.Text.Split('x');
                TweakLib.ChangeResolution(Convert.ToInt32(res[0]), Convert.ToInt32(res[1]));
                TweakLib.ChangeRefreshRate((int) FramerateSlider.Value);
                TweakLib.ChangeFOV(Convert.ToSingle(FOVPickerSlider.Value));
                MainWindow.ShowSuccessDialog();
            }
            else
            {
                MainWindow.ShowGameNotFoundError();
            }
        }

        private void FOVPickerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FovLabel != null)
                FovLabel.Content = Math.Round(FOVPickerSlider.Value, 2);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FramerateLabel != null)
                FramerateLabel.Content = FramerateSlider.Value;
        }
    }
}
