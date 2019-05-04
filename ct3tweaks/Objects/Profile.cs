using ct3tweaks.Objects;
using System;

namespace ct3tweaks
{
    class Profile
    {
        private Resolution resolution;
        private int framerate;
        private float fov;

        public event OnResolutionChange ResolutionChange;
        public delegate void OnResolutionChange(Resolution past, Resolution present);

        public event OnFramerateChange FramerateChange;
        public delegate void OnFramerateChange(int past, int present);

        public event OnFOVChange FOVChange;
        public delegate void OnFOVChange(float past, float present);

        public Resolution Resolution
        {
            get
            {
                return resolution;
            }
            set
            {
                ResolutionChange?.Invoke(resolution, value);
                resolution = value;
            }
        }
        public int Framerate
        {
            get
            {
                return framerate;
            }
            set
            {
                FramerateChange?.Invoke(framerate, value);
                framerate = value;
            }
        }
        public float FOV
        {
            get
            {
                return fov;
            }
            set
            {
                FOVChange?.Invoke(fov, value);
                fov = value;
            }
        }

        public Profile(Resolution resolution, int framerate, float fov)
        {
            Resolution = resolution;
            Framerate = framerate;
            FOV = fov;
        }
    }
}
