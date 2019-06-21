using System;

namespace ct3tweaks.Objects
{
    public class Resolution : IEquatable<Resolution>
    {
        public int w;
        public int h;

        public Resolution(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public bool Equals(Resolution other)
        {
            return this.w == other.w && this.h == other.h;
        }

        override public string ToString()
        {
            return this.w + "x" + this.h;
        }
    }
}
