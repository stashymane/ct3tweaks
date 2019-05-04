using ct3tweaks.Objects;
using System;
using System.IO;

namespace ct3tweaks
{
    static class TweakLib
    {
        static readonly Int32[] resBytes = new Int32[] { 0x7A89, 0x7A93 };
        static readonly Int32[] aspectBytes = new Int32[] { 0x3176A, 0x6ADC9 };
        static readonly Int32 fovByte = 0x6ADCE;
        static readonly Int32 fpsByte = 0x7E4B;

        public static void ResetDisplayMode(string configPath)
        {
            using (var stream = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 00;
                stream.WriteByte(0x01);
            }
        }

        public static void ChangeResolution(string gamePath, Resolution res)
        {
            BackupOriginal(gamePath);

            int w = res.w;
            int h = res.h;

            float aspect = (float) w / h;
            using (var stream = new FileStream(gamePath, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = resBytes[0];
                stream.Write(BitConverter.GetBytes(w), 0, 2);

                stream.Position = resBytes[1];
                stream.Write(BitConverter.GetBytes(h), 0, 2);

                foreach (Int32 b in aspectBytes)
                {
                    stream.Position = b;
                    stream.Write(BitConverter.GetBytes(aspect), 0, 4);
                }
            }
        }

        public static void ChangeFOV(string gamePath, float fov)
        {
            BackupOriginal(gamePath);
            using (var stream = new FileStream(gamePath, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = fovByte;
                stream.Write(BitConverter.GetBytes(fov), 0, 4);
            }
        }

        public static void ChangeFramerate(string gamePath, int fps)
        {
            BackupOriginal(gamePath);
            using (var stream = new FileStream(gamePath, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = fpsByte;
                stream.Write(BitConverter.GetBytes(fps), 0, 1);
            }
        }

        public static void BackupOriginal(string gamePath)
        {
            BackupOriginal(gamePath, gamePath.Replace(".exe", ".original.exe"));
        }

        public static void BackupOriginal(string gamePath, string backupPath)
        {
            if (!File.Exists(backupPath))
                File.Copy(gamePath, backupPath);
        }

        public static void RestoreBackup(string gamePath)
        {
            RestoreBackup(gamePath, gamePath.Replace(".exe", ".original.exe"));
        }

        public static void RestoreBackup(string gamePath, string backupPath)
        {
            if (File.Exists(gamePath))
                File.Delete(gamePath);
            File.Copy(backupPath, gamePath);
        }
    }
}
