struct Image {
	u32 magic;
	u16 width;
	u16 height;
	u32 size;
	u16 unknown;
	u16 compressionType;
	u16 data[width * height];
};

struct ImagePointer {
	Image *image: u32;
};

struct ArtFile {
	u32 imageCount;
//	u32 offsets[imageCount];
	ImagePointer pointers[imageCount];
};

ArtFile art @ 0x00;
