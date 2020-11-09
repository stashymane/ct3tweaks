using System.IO;
using OpenTK.Graphics.ES30;

namespace CT3Tweaks.Lib
{
    public class ArtImage
    {
        public const uint Magic = 1481922631; //GXTX

        public ArtPackage Package;
        public uint Offset;
        public ushort Width;
        public ushort Height;
        public uint DataSize;
        public uint Unknown;
        public ushort CompressionType;

        public long DataOffset;

        private byte[] _imageData;

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
            Unknown = r.ReadUInt16();
            CompressionType = r.ReadUInt16();
            DataOffset = r.BaseStream.Position;
            r.Dispose();
        }

        public byte[] ImageData
        {
            get
            {
                if (_imageData != null) return _imageData;
                using var r = new BinaryReader(File.OpenRead(Package.FilePath));
                r.BaseStream.Position = DataOffset;
                _imageData = r.ReadBytes((int) DataSize);
                r.Dispose();
                return _imageData;
            }
            set => _imageData = value;
        }

        public byte[] Decode()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, Width, Height);

            GL.CompressedTexImage2D(All.Texture2D, 0, All.CompressedRgbaS3tcDxt3Ext, Width, Height, 0,
                (int) DataSize, ImageData);

            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);

            var data = new byte[] { };
            GL.ReadPixels(0, 0, Width, Height, PixelFormat.Rgba, PixelType.Int, data);
            GL.Finish();
            return data;
        }
    }
}