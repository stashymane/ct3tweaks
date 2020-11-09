using System.IO;

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
                Images[i] = new ArtImage(this, r.ReadUInt32());

            r.Dispose();
        }
    }
}