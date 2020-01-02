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

        public Resolution Resolution
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(Path));
                r.BaseStream.Position = ResBytes[0];
                var w = r.ReadUInt32();
                r.BaseStream.Position = ResBytes[1];
                var h = r.ReadUInt32();
                return new Resolution(w, h);
            }
            set
            {
                Backup();

                var aspect = (float) value.w / value.h;
                using var w = new BinaryWriter(File.OpenWrite(Path));

                w.Seek(ResBytes[0], SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value.w), 0, 2);

                w.Seek(ResBytes[1], SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value.h), 0, 2);

                foreach (var b in AspectBytes)
                {
                    w.Seek(b, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(aspect), 0, 4);
                }
            }
        }
        
        public float Fov
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(Path));
                r.BaseStream.Position = FovByte;
                return r.ReadSingle();
            }
            set
            {
                Backup();
                using var w = new BinaryWriter(File.OpenWrite(Path));
                w.Seek(FovByte, SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value), 0, 4);
            }
        }

        public byte Fps
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(Path));
                r.BaseStream.Position = FpsByte;
                return r.ReadByte();
            }
            set
            {
                Backup();
                using var w = new BinaryWriter(File.OpenWrite(Path));
                w.Seek(FpsByte, SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value), 0, 1);
            }
        }

        public void Backup()
        {
            Backup(Path + ".backup");
        }

        public void Backup(string backupPath)
        {
            if (!File.Exists(backupPath))
                File.Copy(Path, backupPath);
        }

        public void Restore()
        {
            Restore(Path + ".backup");
        }

        public void Restore(string backupPath)
        {
            if (File.Exists(Path))
                File.Delete(Path);
            File.Copy(backupPath, Path);
        }

        public void ResetDisplayMode()
        {
            ResetDisplayMode(System.IO.Path.GetDirectoryName(Path) + @"\TAXI3.CFG");
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
