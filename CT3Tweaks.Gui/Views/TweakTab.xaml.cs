﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CT3Tweaks.Gui.Models;

namespace CT3Tweaks.Gui.Views
{
    public class TweakTab : UserControl
    {
        public TweakTab()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
