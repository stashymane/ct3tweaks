using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CT3Tweaks.Gui.Views
{
    public class MusicTab : UserControl
    {
        public MusicTab()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
