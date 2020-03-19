using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CT3Tweaks.Gui.Views
{
    public class SettingsTab : UserControl
    {
        public SettingsTab()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
