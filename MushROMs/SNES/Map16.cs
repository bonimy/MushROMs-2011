using System;
using System.Drawing;
using System.Drawing.Imaging;
using LC_Sharp;

namespace SNES
{
    public unsafe class Map16
    {
        public const int TileHeight = Map8.TileHeight * 2;
        public const int TileWidth = Map8.TileWidth * 2;
        public const int Rows = 0x10;
        public const int Columns = 0x10;
        public const int PageSize = Map16.Rows * Map16.Columns;
        public const int NumTiles = 4;
        public const int DataSize = Map16.NumTiles * Map8.DataSize;
        public const int ImageHeight = Map16.TileHeight * Map16.Rows;
        public const int ImageWidth = Map16.TileWidth * Map16.Columns;

        public Map8[,] Data;

        public Map16()
        {
            Data = new Map8[Map16.PageSize, Map16.NumTiles];
            fixed (Map8* ptr = Data)
                for (int i = 0; i < Map16.PageSize; i++)
                    ptr[i] = new Map8();
        }

        public Map16(byte[] data)
        {
            Data = new Map8[data.Length / Map16.DataSize, Map16.NumTiles];
            int length = data.Length / Map8.DataSize;

            fixed (byte* ptr = data)
            fixed (Map8* dest = Data)
            {
                ushort* src = (ushort*)ptr;
                for (int i = 0; i < length; i++)
                    dest[i].Data = src[i];
            }
        }

        public Map8* ToPointer()
        {
            fixed (Map8* ptr = Data)
                return ptr;
        }

        public void FlipX(int tile)
        {
            Map8 dummy;

            dummy.Data = Data[tile, 0].Data;
            Data[tile, 0].Data = Data[tile, 2].Data;
            Data[tile, 2].Data = Data[tile, 0].Data;

            dummy.Data = Data[tile, 1].Data;
            Data[tile, 1].Data = Data[tile, 3].Data;
            Data[tile, 3].Data = Data[tile, 1].Data;

            for (int i = 0; i < Map16.NumTiles; i++)
                Data[tile, i].FlipVertical();
        }

        public void FlipY(int tile)
        {
            Map8 dummy;

            dummy.Data = Data[tile, 0].Data;
            Data[tile, 0].Data = Data[tile, 2].Data;
            Data[tile, 2].Data = Data[tile, 0].Data;

            dummy.Data = Data[tile, 1].Data;
            Data[tile, 1].Data = Data[tile, 3].Data;
            Data[tile, 3].Data = Data[tile, 1].Data;

            for (int i = 0; i < Map16.NumTiles; i++)
                Data[tile, i].FlipHorizontal();
        }

        public Bitmap DrawMap16Page(GFX gfx, Palette palette, int page)
        {
            uint[,] data = new uint[Map16.ImageHeight, Map16.ImageWidth];
            fixed (uint* scan0 = data)
            fixed (byte* pixels = gfx.Data)
            fixed (uint* colors = palette.Data)
            fixed (Map8* tiles = &this.Data[page * Map16.PageSize, 0])
            {
                DrawMap16Page(scan0, Map16.ImageWidth, Map16.ImageHeight, 0, 0, pixels, colors, tiles, Render8x8Flags.Draw);
                return new Bitmap(Map16.ImageWidth, Map16.ImageHeight, Map16.ImageWidth * 4, PixelFormat.Format32bppArgb, new IntPtr(scan0));
            }
        }

        public Bitmap DrawMap16Page(GFX gfx, Palette palette, int page, int x, int y)
        {
            uint[,] data = new uint[Map16.ImageHeight, Map16.ImageWidth];
            fixed (uint* scan0 = data)
            fixed (byte* pixels = gfx.Data)
            fixed (uint* colors = palette.Data)
            fixed (Map8* tiles = &this.Data[page * Map16.PageSize, 0])
            {
                DrawMap16Page(scan0, Map16.ImageWidth, Map16.ImageHeight, 0, 0, pixels, colors, tiles, Render8x8Flags.Draw);

                uint* square = scan0 + (y << 8) + x;
                for (int i = 0; i < Map16.TileHeight; i++, square += Map16.ImageWidth)
                    for (int j = 0; j < Map16.TileWidth; j++)
                        square[j] ^= 0xFFFFFF;
                return new Bitmap(Map16.ImageWidth, Map16.ImageHeight, Map16.ImageWidth * 4, PixelFormat.Format32bppArgb, new IntPtr(scan0));
            }
        }

        public void DrawMap16Page(uint* scan0, int width, int height, int x, int y, byte* gfx, uint* palette, Map8* tiles, Render8x8Flags flags)
        {
            for (int i = 0; i < Map16.ImageHeight; i += Map16.TileHeight)
                for (int j = 0; j < Map16.ImageWidth; j += Map16.TileWidth)
                    DrawSquare(scan0, width, height, x + j, y + i, palette, gfx, tiles, i + (j / Map16.TileWidth), flags);
        }

        public static void DrawSquare(uint* scan0, int width, int height, int x, int y, uint* palette, byte* gfx, Map8* tiles, int map16, Render8x8Flags flags)
        {
            int offset = map16 * Map16.NumTiles;

            tiles[offset].DrawTiles(scan0, width, height, x, y, gfx, palette, flags);
            tiles[offset + 1].DrawTiles(scan0, width, height, x, y + Map8.TileHeight, gfx, palette, flags);
            tiles[offset + 2].DrawTiles(scan0, width, height, x + Map8.TileWidth, y, gfx, palette, flags);
            tiles[offset + 3].DrawTiles(scan0, width, height, x + Map8.TileWidth, y + Map8.TileHeight, gfx, palette, flags);
        }
    }
}