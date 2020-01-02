using System;
using System.IO;

namespace CT3Tweaks.Lib
{
    public class TweakLib
    {
        private static readonly int[] ResBytes = { 0x7A89, 0x7A93 };
        private static readonly int[] AspectBytes = { 0x3176A, 0x6ADC9 };
        private const int FovByte = 0x6ADCE;
        private const int FpsByte = 0x7E4B;

        public string Path;

        public TweakLib(string path)
        {
            Path = path;
        }

        //TODO get variables from bin
        public Resolution Resolution
        {
            //get => new Resolution(640, 480);
            set
            {
                Backup();

                var aspect = (float) value.w / value.h;
                using var stream = new FileStream(Path, FileMode.Open, FileAccess.Write);

                stream.Position = ResBytes[0];
                stream.Write(BitConverter.GetBytes(value.w), 0, 2);

                stream.Position = ResBytes[1];
                stream.Write(BitConverter.GetBytes(value.h), 0, 2);

                foreach (var b in AspectBytes)
                {
                    stream.Position = b;
                    stream.Write(BitConverter.GetBytes(aspect), 0, 4);
                }
            }
        }
        
        public float Fov
        {
            get => 60f;
            set
            {
                Backup();
                using var stream = new FileStream(Path, FileMode.Open, FileAccess.Write)
                    {Position = FovByte};
                stream.Write(BitConverter.GetBytes(value), 0, 4);
            }
        }

        public byte Fps
        {
            get => 30;
            set
            {
                Backup();
                using var stream = new FileStream(Path, FileMode.Open, FileAccess.Write)
                    {Position = FpsByte};
                stream.Write(BitConverter.GetBytes(value), 0, 1);
            }
        }

        public void Backup()
        {
            Backup(Path.Replace(".exe", ".original.exe"));
        }

        public void Backup(string backupPath)
        {
            if (!File.Exists(backupPath))
                File.Copy(Path, backupPath);
        }

        public void Restore()
        {
            Restore(Path.Replace(".exe", ".original.exe"));
        }

        public void Restore(string backupPath)
        {
            if (File.Exists(Path))
                File.Delete(Path);
            File.Copy(backupPath, Path);
        }
        
        public static void ResetDisplayMode(string configPath)
        {
            using var stream = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite) {Position = 00};
            stream.WriteByte(0x01);
        }
    }

    public struct Resolution : IEquatable<Resolution>
    {
        public uint w;
        public uint h;

        public Resolution(uint w, uint h)
        {
            this.w = w;
            this.h = h;
        }

        public bool Equals(Resolution other) => w == other.w && h == other.h;

        public override string ToString() => w + "x" + h;
    }
}
