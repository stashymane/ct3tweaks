using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ct3tweaks
{
    class TweakLib
    {
        public static void ResetDisplayMode()
        {
            String path = Properties.Settings.Default.Directory + "/TAXI3.CFG";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 00;
                stream.WriteByte(0x01);
            }
        }

        public static void ChangeRefreshRate(int rate)
        {
            BackupOriginal();
            String path = Properties.Settings.Default.Directory + "/CT3.exe";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 0x7E4B;
                stream.WriteByte(BitConverter.GetBytes(rate)[0]);
            }
        }

        public static void BackupOriginal()
        {
            string dir = Properties.Settings.Default.Directory;
            if (!File.Exists(dir + "/CT3.original.exe"))
                File.Copy(dir + "/CT3.exe", dir + "/CT3.original.exe");
        }
    }
}
