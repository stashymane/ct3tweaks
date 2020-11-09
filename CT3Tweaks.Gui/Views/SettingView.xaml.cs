using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CT3Tweaks.Gui.ViewModels;

namespace CT3Tweaks.Gui.Views
{
    public class SettingView : UserControl
    {
        private MainWindowViewModel main => DataContext as MainWindowViewModel;

        public SettingView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void BrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var d = new OpenFolderDialog {Title = "Locate the Crazy Taxi 3 installation folder..."};
            var a = await d.ShowAsync(VisualRoot as Window);
            if (a == "")
                return;

            main.Path = a;
        }

        private void SubmitFolderPath(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                main.Path = (sender as TextBox)?.Text;
        }
    }
}