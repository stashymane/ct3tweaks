using System;
using System.IO;
using CT3Tweaks.Lib;
using NUnit.Framework;

namespace CT3Tweaks.LibTest
{
    public class Tests
    {
        private TweakLib l;
        private string Dir;
        private const string Source = "Sample";
        private const string Dest = "Sample_temp";

        [SetUp]
        public void Setup()
        {
            Dir = AppDomain.CurrentDomain.BaseDirectory;
            File.Copy(Dir + Source, Dir + Dest);
            l = new TweakLib(Dir + Dest, TweakProfile.Fairlight);
        }

        [Test]
        public void ResolutionTest()
        {
            (uint Width, uint Height) defaultResolution = (640, 480);
            Assert.AreEqual(defaultResolution, l.Resolution);

            (uint Width, uint Height) newResolution = (1920, 1080);
            l.Resolution = newResolution;
            Assert.AreEqual(newResolution, l.Resolution);
        }

        [Test]
        public void FovTest()
        {
            const float defaultFov = 60f;
            Assert.AreEqual(defaultFov, l.Fov);

            const float newFov = 90f;
            l.Fov = newFov;
            Assert.AreEqual(newFov, l.Fov);
        }

        [Test]
        public void FpsTest()
        {
            const int defaultFps = 30;
            Assert.AreEqual(defaultFps, l.Fps);

            const byte newFps = 60;
            l.Fps = newFps;
            Assert.AreEqual(newFps, l.Fps);
        }

        [Test]
        public void BackupTest()
        {

        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(Dir + Dest);
        }
    }
}