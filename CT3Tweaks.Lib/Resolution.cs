using System;

namespace CT3Tweaks.Lib
{
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
