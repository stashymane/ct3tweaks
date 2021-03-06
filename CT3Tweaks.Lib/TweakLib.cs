﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CT3Tweaks.Lib
{
    public class TweakLib
    {
        private TweakProfile profile;

        public string ExePath;
        public string BackupPath;

        public TweakLib(string exePath, TweakProfile profile, string backupPath)
        {
            ExePath = Path.GetFullPath(exePath);
            BackupPath = Path.GetFullPath(backupPath);
            this.profile = profile;
        }

        public TweakLib(string exePath, TweakProfile profile) : this(exePath, profile, exePath + ".backup")
        {
        }

        public TweakLib(string exePath) : this(exePath, TweakProfile.GetByChecksum(exePath))
        {
        }

        public (uint Width, uint Height) Resolution
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(ExePath));
                r.BaseStream.Position = profile.Width.First();
                var w = r.ReadUInt32();
                r.BaseStream.Position = profile.Height.First();
                var h = r.ReadUInt32();
                return (w, h);
            }
            set
            {
                Backup();

                var aspect = (float) value.Width / value.Height;
                using var w = new BinaryWriter(File.OpenWrite(ExePath));
                foreach (var addr in profile.Width)
                {
                    w.Seek(addr, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(value.Width), 0, 2);
                }

                foreach (var addr in profile.Height)
                {
                    w.Seek(addr, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(value.Height), 0, 2);
                }

                foreach (var addr in profile.Aspect)
                {
                    w.Seek(addr, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(aspect), 0, 4);
                }
            }
        }

        public float Fov
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(ExePath));
                r.BaseStream.Position = profile.Fov.First();
                return r.ReadSingle();
            }
            set
            {
                Backup();

                using var w = new BinaryWriter(File.OpenWrite(ExePath));
                foreach (var addr in profile.Fov)
                {
                    w.Seek(addr, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(value), 0, 4);
                }
            }
        }

        public byte Fps
        {
            get
            {
                using var r = new BinaryReader(File.OpenRead(ExePath));
                r.BaseStream.Position = profile.Fps.First();
                return r.ReadByte();
            }
            set
            {
                Backup();

                using var w = new BinaryWriter(File.OpenWrite(ExePath));

                foreach (var addr in profile.Fps)
                {
                    w.Seek(addr, SeekOrigin.Begin);
                    w.Write(BitConverter.GetBytes(value), 0, 1);
                }
            }
        }

        public void Backup()
        {
            Backup(BackupPath);
        }

        public void Backup(string backupPath)
        {
            Backup(BackupPath, ExePath);
        }

        public static void Backup(string backupPath, string exePath)
        {
            if (!File.Exists(backupPath))
                File.Copy(exePath, backupPath);
        }

        public void Restore()
        {
            Restore(BackupPath);
        }

        public void Restore(string backupPath)
        {
            Restore(backupPath, ExePath);
        }

        public static void Restore(string backupPath, string restorePath)
        {
            if (File.Exists(restorePath))
                File.Delete(restorePath);
            File.Copy(backupPath, restorePath);
        }

        public void ResetDisplayMode()
        {
            ResetDisplayMode(Path.GetDirectoryName(ExePath) + @"\TAXI3.CFG");
        }

        public static void ResetDisplayMode(string configPath)
        {
            using var stream = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite) {Position = 00};
            stream.WriteByte(0x01);
        }
    }

    public struct TweakProfile
    {
        public IEnumerable<int> Width;
        public IEnumerable<int> Height;
        public IEnumerable<int> Aspect;
        public IEnumerable<int> Fov;
        public IEnumerable<int> Fps;

        public TweakProfile(IEnumerable<int> width, IEnumerable<int> height, IEnumerable<int> aspect, IEnumerable<int> fov, IEnumerable<int> fps)
        {
            Width = width;
            Height = height;
            Aspect = aspect;
            Fov = fov;
            Fps = fps;
        }

        public static TweakProfile GetByChecksum(string path)
        {
            using (var md5 = MD5.Create())
            {
                using var stream = File.OpenRead(path);
                var hash = BitConverter.ToString(
                    md5.ComputeHash(stream)
                ).Replace("-", "").ToLowerInvariant();
                return hash switch
                {
                    "76b991e486460599cf5f09e7b0902875" => Fairlight,
                    "15a1ea00012f0119c1affc43f6c07fc1" => Xplosiv,
                    _ => throw new ArgumentException("The md5sum provided does not match any supported executables."),
                };
            }
        }

        public static TweakProfile Fairlight = new TweakProfile(
            new[] { 0x7A89 },
            new[] { 0x7A93 },
            new[] { 0x3176A, 0x6ADC9 },
            new[] { 0x6ADCE },
            new[] { 0x7E4B }
        );

        public static TweakProfile Xplosiv => Fairlight;
    }
}
