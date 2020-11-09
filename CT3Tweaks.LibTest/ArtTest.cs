using System;
using System.Linq;
using CT3Tweaks.Lib;
using NUnit.Framework;

namespace CT3Tweaks.LibTest
{
    public class ArtTest
    {
        private string Dir;

        [SetUp]
        public void Setup()
        {
            Dir = AppDomain.CurrentDomain.BaseDirectory + "/bank00.art";
        }

        [Test]
        public void ContainerTest()
        {
            var test = new ArtPackage(Dir);
            Assert.AreEqual(76, test.Images.Length);
            Assert.AreEqual(308, test.Images.First().Offset);
            Assert.AreEqual(128, test.Images.First().Width);
        }
    }
}