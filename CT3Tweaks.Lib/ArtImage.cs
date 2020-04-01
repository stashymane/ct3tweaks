using System;
using System.IO;

namespace CT3Tweaks.Lib
{
    public class ArtImage
    {
        public const uint Magic = 1481922631; //GXTX
        
        public ArtImage(ArtPackage package, uint offset)
        {
            Package = package;
            Offset = offset;

            using var r = new BinaryReader(File.OpenRead(Package.FilePath));
            r.BaseStream.Position = Offset;
            var m = r.ReadUInt32();
            if (m != Magic)
                throw new InvalidDataException($"Magic bytes not found at offset {Offset}. Value found: {m}");
            Width = r.ReadUInt16();
            Height = r.ReadUInt16();
            DataSize = r.ReadUInt32();
            CompressionType = r.ReadUInt16();
            DataOffset = r.BaseStream.Position;
            r.Dispose();
        }
        
        public ArtPackage Package;
        public uint Offset;
        public ushort Width;
        public ushort Height;
        public uint DataSize;
        public ushort CompressionType;

        public long DataOffset;
        
        private byte[] _imageData;

        public byte[] ImageData
        {
            get
            {
                if (_imageData != null) return _imageData;
                using var r = new BinaryReader(File.OpenRead(Package.FilePath));
                r.BaseStream.Position = DataOffset;
                _imageData = r.ReadBytes((int)DataSize);
                r.Dispose();
                return _imageData;
            }
            set => _imageData = value;
        }
    }
}