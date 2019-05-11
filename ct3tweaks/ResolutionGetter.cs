using ct3tweaks.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class ResolutionGetter
{
    [DllImport("user32.dll")]
    public static extern bool EnumDisplaySettings(
          string deviceName, int modeNum, ref DEVMODE devMode);
    const int ENUM_CURRENT_SETTINGS = -1;

    const int ENUM_REGISTRY_SETTINGS = -2;

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
        private const int CCHDEVICENAME = 0x20;
        private const int CCHFORMNAME = 0x20;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public ScreenOrientation dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    public static List<Resolution> GetResolutions()
    {
        DEVMODE vDevMode = new DEVMODE();
        int i = 0;
        List<Resolution> res = new List<Resolution>();
        while (EnumDisplaySettings(null, i, ref vDevMode))
        {
            Resolution r = new Resolution(vDevMode.dmPelsWidth, vDevMode.dmPelsHeight);
            if (!res.Contains(r))
                res.Add(r);
            i++;
        }
        return res;
    }

}