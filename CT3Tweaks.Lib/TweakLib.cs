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

        public string ExePath;

        public TweakLib(string exePath)
        {
            ExePath = Path.GetFullPath(exePath);
        }

        public Resolution Resolution
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(ExePath));
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
                using var w = new BinaryWriter(File.OpenWrite(ExePath));

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
                using var r = new BinaryReader(File.OpenRead(ExePath));
                r.BaseStream.Position = FovByte;
                return r.ReadSingle();
            }
            set
            {
                Backup();
                using var w = new BinaryWriter(File.OpenWrite(ExePath));
                w.Seek(FovByte, SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value), 0, 4);
            }
        }

        public byte Fps
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(ExePath));
                r.BaseStream.Position = FpsByte;
                return r.ReadByte();
            }
            set
            {
                Backup();
                using var w = new BinaryWriter(File.OpenWrite(ExePath));
                w.Seek(FpsByte, SeekOrigin.Begin);
                w.Write(BitConverter.GetBytes(value), 0, 1);
            }
        }

        public void Backup()
        {
            Backup(ExePath + ".backup");
        }

        public void Backup(string backupPath)
        {
            if (!File.Exists(backupPath))
                File.Copy(ExePath, backupPath);
        }

        public void Restore()
        {
            Restore(ExePath + ".backup");
        }

        public void Restore(string backupPath)
        {
            if (File.Exists(ExePath))
                File.Delete(ExePath);
            File.Copy(backupPath, ExePath);
        }

        public void ResetDisplayMode()
        {
            ResetDisplayMode(System.IO.Path.GetDirectoryName(ExePath) + @"\TAXI3.CFG");
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
