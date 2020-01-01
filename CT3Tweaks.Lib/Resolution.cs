using System;

namespace CT3Tweaks.Lib
{
    public struct Resolution : IEquatable<Resolution>
    {
        public int w;
        public int h;

        public Resolution(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public bool Equals(Resolution other) => w == other.w && h == other.h;

        public override string ToString() => w + "x" + h;
    }
}
