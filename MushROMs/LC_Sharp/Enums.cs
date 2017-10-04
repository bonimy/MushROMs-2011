
namespace LC_Sharp
{
    /// <summary>
    /// Specifies the compression algorithm to use.
    /// </summary>
    public enum CompressionFormats
    {
        /// <summary>
        /// LZ1 Compression Format. Found in Zelda3 and Japanese SMW.
        /// </summary>
        LZ1 = 0,
        /// <summary>
        /// LZ2 Compression Format. Found in SMW, YI, and Zelda3
        /// </summary>
        LZ2 = 1,
        /// <summary>
        /// LZ3 Compression Format. Found in Pokemon Gold and Silver.
        /// </summary>
        LZ3 = 2,
        /// <summary>
        /// LZ4 Compression Format. Found in Super Mario RPG.
        /// </summary>
        LZ4 = 3,
        /// <summary>
        /// LZ5 Compression Format. Found in Metroid3, Super Mario Kart, and Sim City.
        /// </summary>
        LZ5 = 4,
        /// <summary>
        /// LZ6 Compression Format. Found in Sailor Moon and Sailor Moon R.
        /// </summary>
        LZ6 = 5,
        /// <summary>
        /// LZ7 Compression Format. Found in Secret of Mana
        /// </summary>
        LZ7 = 6,
        /// <summary>
        /// LZ8 Compression Format. Found in Super Mario RPG.
        /// </summary>
        LZ8 = 7,
        /// <summary>
        /// LZ9 Compression Format. Found in Lufia1/2.
        /// </summary>
        LZ9 = 8,
        /// <summary>
        /// LZ10 Compression Format. Found in RoboTrek.
        /// </summary>
        LZ10 = 9,
        /// <summary>
        /// LZ11 Compression Format. Found in Harvest Moon.
        /// </summary>
        LZ11 = 10,
        /// <summary>
        /// LZ12 Compression Format. Found in Gradius 3.
        /// </summary>
        LZ12 = 11,
        /// <summary>
        /// LZ13 Compression Format. Found in Chrono Trigger.
        /// </summary>
        LZ13 = 12,
        /// <summary>
        /// LZ14 Compression Format. Found in Famicom Tentai/Detective Club 2.
        /// </summary>
        LZ14 = 13,
        /// <summary>
        /// LZ15 Compression Format. Found in Star Fox and Star Fox 2.
        /// </summary>
        LZ15 = 14,
        /// <summary>
        /// LZ16 Compression Format. Found in Yoshi's Island.
        /// </summary>
        LZ16 = 15,
        /// <summary>
        /// RLE1 Compression Format. Used in backgrounds of SMW.
        /// </summary>
        RLE1 = 100,
        /// <summary>
        /// RLE2 Compression Format. Used in overworld of SMW.
        /// </summary>
        RLE2 = 101,
        /// <summary>
        /// RLE3 Compression Format. Used in Mega Man X.
        /// </summary>
        RLE3 = 102,
        /// <summary>
        /// RLE4 Compression Format. Used in title screen of Radical Dreamers.
        /// </summary>
        RLE4 = 103
    }

    /// <summary>
    /// Specifies the expansion method to use for the ROM when expanding beyonf 4MB.
    /// </summary>
    public enum ExROMExpansionModes
    {
        /// <summary>
        /// 48 Mbit expansion.
        /// </summary>
        s48_ExHiROM_1 = 48,
        /// <summary>
        /// Higher compatibility, but uses up to 1 meg of the new
        /// space.  Do not use this unless the ROM doesn't load or
        /// has problems with the other options.
        /// </summary>
        s48_ExHiROM_2 = 0x148,
        /// <summary>
        /// 64 Mbit expansion.
        /// </summary>
        s64_ExHiROM_1 = 64,
        /// <summary>
        /// Higher compatibility, but uses up to 2 meg of the new
        /// space.  Do not use this unless the ROM doesn't load or
        /// has problems with the other options.
        /// </summary>
        s64_ExHiROM_2 = 0x164,
        /// <summary>
        /// For LoROMs that use the 00:8000-6F:FFFF map.
        /// </summary>
        s48_ExLoROM_1 = 0x1048,
        /// <summary>
        /// For LoROMs that use the 80:8000-FF:FFFF map.
        /// </summary>
        s48_ExLoROM_2 = 0x2048,
        /// <summary>
        /// Higher compatibility, but uses up most of the new space.
        /// Do not use this unless the ROM doesn't load or has
        /// problems with the other options.
        /// </summary>
        s48_ExLoROM_3 = 0x4048,
        /// <summary>
        /// For LoROMs that use the 00:8000-6F:FFFF map.
        /// </summary>
        s64_ExLoROM_1 = 0x1064,
        /// <summary>
        /// For LoROMs that use the 80:8000-FF:FFFF map.
        /// </summary>
        s64_ExLoROM_2 = 0x2064,
        /// <summary>
        /// Higher compatibility, but uses up most of the new space.
        /// Do not use this unless the ROM doesn't load or has
        /// problems with the other options.
        /// </summary>
        s64_ExLoROM_3 = 0x4064,
    }

    /// <summary>
    /// Specifies options on how a file should be handled when opened.
    /// </summary>
    public enum FileModes
    {
        /// <summary>
        /// Opens an existing file in read-only mode.
        /// </summary>
        ReadOnly = 0,
        /// <summary>
        /// Opens an existing file in read and write mode.
        /// </summary>
        ReadWrite = 1,
        /// <summary>
        /// Creates a new files in read and write mode, ersasing the existing file (if any).
        /// </summary>
        CreateReadWrite = 2
    }

    /// <summary>
    /// Specifies options on how to handle a file's memory array size.
    /// </summary>
    public enum LockArraySizeOptions
    {
        /// <summary>
        /// Do not lock array.
        /// </summary>
        None = 0,
        /// <summary>
        /// Lock array size and do not allow expansion.
        /// </summary>
        LockAndNoExpansion = 4,
        /// <summary>
        /// Lock array size except for expansion.
        /// </summary>
        LockWithExpansion = 8
    }

    /// <summary>
    /// Specifies ROM addressing modes.
    /// </summary>
    public enum AddressFormats
    {
        /// <summary>
        /// For LoROM games up to 32 Mbit ROM sizes.
        /// </summary>
        LoROM = 1,
        /// <summary>
        /// For HiROM games up to 32 Mbit ROM sizes.
        /// </summary>
        HiROM = 2,
        /// <summary>
        /// For HiROM games above 32 Mbit.
        /// </summary>
        ExHiROM = 4,
        /// <summary>
        /// For LoROM games above 32 Mbit.
        /// </summary>
        ExLoROM = 8,
        /// <summary>
        /// For LoROM games up to 32 Mbit ROM sizes. Always uses 80:8000 map.
        /// </summary>
        LoROM2 = 0x10,
        /// <summary>
        /// For HiROM games up to 32 Mbit ROM sizes. Always uses 40:0000 map.
        /// </summary>
        HiROM2 = 0x20,
    }

    /// <summary>
    /// Specifies bank types and boundaries.
    /// </summary>
    public enum BankTypes
    {
        /// <summary>
        /// Ignore bank boundaries and header.
        /// </summary>
        NoBank = 0,
        /// <summary>
        /// LoROM
        /// </summary>
        LoROM = 1,
        /// <summary>
        /// HiROM
        /// </summary>
        HiROM = 2
    }

    /// <summary>
    /// Format of SNES graphics
    /// </summary>
    public enum GraphicsFormats
    {
        /// <summary>
        /// 1 bit per pixel. Uses color numbers 0-1.
        /// </summary>
        _1BPP = 1,
        /// <summary>
        /// 2 bits per pixel. Uses color numbers 0-3.
        /// </summary>
        _2BPP = 2,
        /// <summary>
        /// 3 bits per pixel. Uses color numbers 0-7.
        /// </summary>
        _3BPP = 3,
        /// <summary>
        /// 4 bits per pixel. Uses color numbers 0-15.
        /// </summary>
        _4BPP = 4,
        /// <summary>
        /// 5 bits per pixel. Uses color numbers 0-31.
        /// </summary>
        _5BPP = 5,
        /// <summary>
        /// 6 bits per pixel. Uses color numbers 0-63.
        /// </summary>
        _6BPP = 6,
        /// <summary>
        /// 7 bits per pixel. Uses color numbers 0-127.
        /// </summary>
        _7BPP = 7,
        /// <summary>
        /// 8 bits per pixel. Uses color numbers 0-255.
        /// </summary>
        _8BPP = 8,
        /// <summary>
        /// 4 bits per pixel. Uses color numbers 0-15. This format is intended for Game Boy Advanced.
        /// </summary>
        GBA_4BPP = 0x14
    }

    /// <summary>
    /// Specifies flags for rendering 8x8 tiles. This flags can be combined.
    /// </summary>
    public enum Render8x8Flags
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,
        /// <summary>
        /// Invert the transparent area of the tile.
        /// </summary>
        InvertTransparent = 1,
        /// <summary>
        ///  Invert the non-transparent area of the tile.
        /// </summary>
        InvertOpaque = 2,
        /// <summary>
        /// Invert the tile colors. Combination of InvertTranparent and InvertOpaque.
        /// </summary>
        Invert = InvertTransparent | InvertOpaque,
        /// <summary>
        /// Highlight the transparent area of the tile red.
        /// </summary>
        RedTransparent = 4,
        /// <summary>
        /// Highlight the non-transparent area of the tile red.
        /// </summary>
        RedOpaque = 8,
        /// <summary>
        /// Hightlight the tile red. Combination of RedTransparent and RedOpaque.
        /// </summary>
        Red = RedTransparent | RedOpaque,
        /// <summary>
        /// Highlight the transparent area of the tile green.
        /// </summary>
        GreenTransparent = 0x10,
        /// <summary>
        /// Highlight the non-transparent area of the tile green.
        /// </summary>
        GreenOpaque = 0x20,
        /// <summary>
        /// Highlight the tile green. Combination of GreenTransparent and GreenOpaque.
        /// </summary>
        Green = GreenTransparent | GreenOpaque,
        /// <summary>
        /// Highlight the transparent area of the tile blue.
        /// </summary>
        BlueTransparent = 0x40,
        /// <summary>
        /// Highlight the non-transparent area of the tile blue.
        /// </summary>
        BlueOpaque = 0x80,
        /// <summary>
        /// Highlight the tile blue. Combination of BlueTransparent and BlueOpaque".
        /// </summary>
        Blue = BlueTransparent | BlueOpaque,
        /// <summary>
        /// Make the tile translucent
        /// </summary>
        Translucent = 0x100,
        /// <summary>
        /// Half-color mode.
        /// </summary>
        HalfColor = 0x200,
        /// <summary>
        /// SNES sub-screen addition mode.
        /// </summary>
        ScreenAdd = 0x400,
        /// <summary>
        /// SNES sub-screen subtraction mode.
        /// </summary>
        ScreenSub = 0x800,
        /// <summary>
        /// Draw tile only if Priority==0.
        /// </summary>
        Priority0 = 0x1000,
        /// <summary>
        /// Draw tile only if Priority==1.
        /// </summary>
        Priority1 = 0x2000,
        /// <summary>
        /// Draw tile only if Priority==2 (valid for Sprite only).
        /// </summary>
        Priority2 = 0x4000,
        /// <summary>
        /// Draw tile only if Priority==3 (valid for Sprite only).
        /// </summary>
        Priority3 = 0x8000,
        /// <summary>
        /// Draw the tile.
        /// </summary>
        Draw = Priority0 | Priority1 | Priority2 | Priority3,
        /// <summary>
        /// Draw transparent area of tile opaque.
        /// </summary>
        Opaque = 0x10000,
        /// <summary>
        /// Uses sprite format for Map8Tile, adds 8 to palette #.
        /// </summary>
        Sprite = 0x20000,
        /// <summary>
        /// Make tile translucent only if palette>=12.
        /// </summary>
        SpriteTranslucent = 0x40000,
        /// <summary>
        /// Draw tile using 2bpp mode (default is 4bpp).
        /// </summary>
        GFX2BPP = 0x80000,
        /// <summary>
        /// Draw 16x16 tile (default is 8x8).
        /// </summary>
        Tile16 = 0x100000,
        /// <summary>
        /// Draw 32x32 tile (valid for Sprite only).
        /// </summary>
        Tile32 = 0x200000,
        /// <summary>
        /// Draw 64x64 tile (valid for Sprite only).
        /// </summary>
        Tile64 = 0x400000
    }

    /// <summary>
    /// Specifes flags to set when doing RATS-related functions. Multiple flags can be used with OR operations.
    /// </summary>
    public enum RATSFunctionFlags
    {
        /// <summary>
        /// Avoid storing data at positions that cross LoROM bank boundaries.
        /// </summary>
        LoROM = 0x100,
        /// <summary>
        /// Avoid storing data at positions that cross LoROM bank boundaries.
        /// </summary>
        HiROM = 0x200,
        /// <summary>
        /// Similar to <see cref="LoROM"/>, but also avoids writing above 7F:0000.
        /// </summary>
        ExLoROM = 0x10000,
        /// <summary>
        /// Similar to <see cref="HiROM"/>, but also voids the 0-7FFF area of banks $70-$77 (SRAM), or writing above 7E:0000.
        /// </summary>
        ExHiROM = 0x400,
        /// <summary>
        /// Don't write a RATS tag for the data.
        /// </summary>
        NoWriteRAT = 0x2000,
        /// <summary>
        /// Don't write the data in the RAT tag.
        /// </summary>
        NoWriteData = 0x8000,
        /// <summary>
        /// If the data cannot be stored in the ROM, Lunar Compress.dll will expand the ROM to 1, 2, 3, or 4MB and try again.
        /// </summary>
        ExpandROM = 0x20000,
        /// <summary>
        /// Don't scan the supplied data and remove embedded RATs within it (done by setting the bytes to 0).
        /// </summary>
        NoScanData = 0x40000,
        /// <summary>
        /// Writes neither the RATS tag nor the data.
        /// </summary>
        NoWrite = 0x80000,
        /// <summary>
        /// Allow data storage across bank boundaries, but avoid inaccessible ExHiROM areas.
        /// </summary>
        ExHiROMRange = 0x100000,
        /// <summary>
        /// Allow data storage across bank boundaries, but avoid inaccessible ExLoROM areas.
        /// </summary>
        ExLoROMRange = 0x200000
    }

    /// <summary>
    /// Used when determining data with no RATS tags which may be compressed data. These flags can be combines with OR operations.
    /// </summary>
    public enum RATSCompressFlags
    {
        /// <summary>
        /// If no RATS tag is found, attempt to get size by decompressing the data.
        /// </summary>
        Compressed = 0x800,
        /// <summary>
        /// Don't erase the RATS tag for the data (if it exists).
        /// </summary>
        NoEraseRAT = 0x1000,
        /// <summary>
        /// Don't erase the data.
        /// </summary>
        NoEraseData = 0x4000,
    }
}