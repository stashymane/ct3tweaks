using System;
using CT3Tweaks.Lib;
using ReactiveUI;

namespace CT3Tweaks.Gui.ViewModels
{
    public class TweakViewModel : ViewModelBase
    {
        public TweakLib tweaks;

        public TweakViewModel(MainWindowViewModel main)
        {
            main.WhenAnyValue(x => x.Path).Subscribe(p => { tweaks = new TweakLib(p); });
        }
    }
}