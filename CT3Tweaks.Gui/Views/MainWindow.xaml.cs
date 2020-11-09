using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CT3Tweaks.Gui.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}