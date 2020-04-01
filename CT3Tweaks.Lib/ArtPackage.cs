using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CT3Tweaks.Lib
{
    public class ArtPackage
    {
        public string FilePath;
        public ArtImage[] Images;

        public ArtPackage(string path)
        {
            FilePath = Path.GetFullPath(path);
            using var r = new BinaryReader(File.OpenRead(FilePath));
            var n = r.ReadUInt32();
            Images = new ArtImage[n];
            for (var i = 0; i < n; i++)
            {
                ArtImage img = new ArtImage(this, r.ReadUInt32());
                Images[i] = img;
            }
            r.Dispose();
        }
    }
}