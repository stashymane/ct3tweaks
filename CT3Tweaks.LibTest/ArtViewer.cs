using System;
using CT3Tweaks.Lib;
using NUnit.Framework;
using OpenTK.Windowing.Desktop;

namespace CT3Tweaks.LibTest
{
    public class ArtViewer : GameWindow
    {
        public ArtViewer() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            RenderFrequency = 60;
        }
    }

    public class ArtView
    {
        private string Dir = AppDomain.CurrentDomain.BaseDirectory + "/bank00.art";

        [Test]
        public void Run()
        {
            var v = new ArtViewer();
            var p = new ArtPackage(Dir);
            var data = p.Images[0].Decode();
        }
    }
}