using System;
using ReactiveUI;

namespace CT3Tweaks.Gui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _path = @"C:\Games\Crazy Taxi 3";

        public string Path
        {
            get => _path;
            set => this.RaiseAndSetIfChanged(ref _path, value);
        }

        public MainWindowViewModel()
        {
            MusicViewModel = new MusicViewModel();
            this.WhenAnyValue(x => x.Path).Subscribe(Console.WriteLine);
        }

        public MusicViewModel MusicViewModel { get; }
    }
}