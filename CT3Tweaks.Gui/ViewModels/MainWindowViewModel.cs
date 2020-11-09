using System;
using System.IO;
using ReactiveUI;

namespace CT3Tweaks.Gui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _path = @"C:\Games\Crazy Taxi 3";

        public string Path
        {
            get => _path;
            set
            {
                if (!File.Exists(System.IO.Path.Combine(value, "ct3.exe")))
                    throw new FileNotFoundException("CT3.exe was not found in this directory.");
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }

        public MainWindowViewModel()
        {
            MusicViewModel = new MusicViewModel();
            this.WhenAnyValue(x => x.Path).Subscribe(Console.WriteLine);
            try
            {
                _path = Directory.GetCurrentDirectory();
            }
            catch (FileNotFoundException)
            {
            }

            //TODO save/load path
        }

        public MusicViewModel MusicViewModel { get; }
    }
}