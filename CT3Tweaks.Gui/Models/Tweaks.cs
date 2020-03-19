using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.Platform;
using CT3Tweaks.Lib;
using DynamicData;
using OpenToolkit.Graphics.ES11;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.GraphicsLibraryFramework;
using ReactiveUI;
using GL = OpenToolkit.Graphics.ES30.GL;


namespace CT3Tweaks.Gui.Models
{
    class Tweaks
    {
        public (uint Width, uint Height) Resolution;
        public float Fov;
        public byte Fps;

        public List<(uint Width, uint Height)> Resolutions = new List<(uint, uint)>();
        public List<float> Fpses = new List<float>() {30, 60};

        public Tweaks()
        {
            if (GLFW.Init())
            {
                unsafe {
                    foreach (var mode in GLFW.GetVideoModes(GLFW.GetPrimaryMonitor()))
                    {
                        (uint Width, uint Height) res = ((uint) mode.Width, (uint) mode.Height);
                        if (!Resolutions.Contains(res))
                            Resolutions.Add(res);
                    }
                }
                GLFW.Terminate();
            }
            else
            {
                Resolutions.Add((1280, 720));
                Resolutions.Add((1920, 1080));
                Resolutions.Add((3840, 2160));
            }
        }
    }
}
