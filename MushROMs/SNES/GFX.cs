using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using LC_Sharp;

namespace SNES
{
    public unsafe class GFX
    {
        /// <summary>
        /// Represents the number of rows of tiles that make up a GFX page.
        /// </summary>
        public const int Rows = 8;
        /// <summary>
        /// Represents the number of columns of tiles that make up a GFX page.
        /// </summary>
        public const int Columns = 0x10;
        /// <summary>
        /// Represents the number of tiles that make up a GFX page.
        /// </summary>
        public const int Tiles = GFX.Rows * GFX.Columns;
        /// <summary>
        /// Represents the width in pixels a GFX tile is.
        /// </summary>
        public const int TileWidth = 8;
        /// <summary>
        /// Represents the height in pixels a GFX tile.
        /// </summary>
        public const int TileHeight = GFX.TileWidth;
        /// <summary>
        /// Represents the number of pixels a GFX tile has.
        /// </summary>
        public const int TileSize = GFX.TileWidth * GFX.TileHeight;
        /// <summary>
        /// Represents the width in pixels a GFX page is.
        /// </summary>
        public const int ImageWidth = GFX.Columns * GFX.TileWidth;
        /// <summary>
        /// Represents the height in pixels a GFX page is.
        /// </summary>
        public const int ImageHeight = GFX.Rows * GFX.TileHeight;
        /// <summary>
        /// Represents the size of the array for a GFX page.
        /// </summary>
        public const int ArraySize = GFX.Tiles * GFX.TileSize;

        /// <summary>
        /// The GFX pixel index data.
        /// </summary>
        public byte[, , , ,] Data;
        /// <summary>
        /// The Graphics Format the GFX data is in.
        /// </summary>
        public GraphicsFormats GraphicsFormat;

        public GFX(int pages, GraphicsFormats gfxType)
        {
            this.Data = new byte[pages, GFX.Rows, GFX.Columns, GFX.TileWidth, GFX.TileHeight];
            this.GraphicsFormat = gfxType;
        }

        public void NewPage(int page, ref byte[] data)
        {
            fixed (byte* src = data)
            fixed (byte* dest = &this.Data[page, 0, 0, 0, 0])
                LunarCompress.CreatePixelMap(src, dest, GFX.Tiles, this.GraphicsFormat);
        }

        public void NewPage(int srcPage, int destPage, ref GFX gfx)
        {
            fixed (byte* src = &gfx.Data[srcPage, 0, 0, 0, 0])
            fixed (byte* dest = &this.Data[srcPage, 0, 0, 0, 0])
                for (int i = 0; i < GFX.ArraySize; i++)
                    dest[i] = src[i];
        }

        public byte[] ToByteArray(int page)
        {
            int length = (GFX.ArraySize * ((int)this.GraphicsFormat & 0x0F)) >> 3;
            byte[] data = new byte[length];
            fixed (byte* src = &this.Data[page, 0, 0, 0, 0])
            fixed (byte* dest = data)
                LunarCompress.CreateBPPMap(src, dest, GFX.Tiles, this.GraphicsFormat);
            return data;
        }

        public void SetTile(int page, int row, int column, ref GFX.Tile tile)
        {
            fixed (byte* dest = &this.Data[page, row, column, 0, 0])
            fixed (byte* src = tile.Data)
                for (int i = 0; i < GFX.TileSize; i++)
                    dest[i] = src[i];
        }

        public Bitmap DrawGFXPage(int page, uint* palette, int row, int zoom)
        {
            int height = GFX.ImageHeight * zoom;
            int width = GFX.ImageWidth * zoom;

            uint[,] pixels = new uint[height, width];
            fixed (uint* scan0 = pixels)
            {
                DrawGFXPage(scan0, width, height, 0, 0, page, palette, row, zoom);
                return new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        public void DrawGFXPage(uint* scan0, int width, int height, int x, int y, int page, uint* palette, int row, int zoom)
        {
            palette += row * GFX.TileWidth;
            fixed (byte* src = &this.Data[page, 0, 0, 0, 0])
            {
                byte* gfx = src;
                for (int _row = 0; _row < GFX.Rows; _row++)
                    for (int col = 0; col < GFX.Columns; col++, gfx += GFX.TileSize)
                        GFX.DrawSquare(scan0, width, height, x + (col * GFX.TileWidth * zoom), y + (_row * GFX.TileHeight * zoom), gfx, palette, zoom);
            }
        }

        public Bitmap DrawSquare(byte* tile, ref Palette palette, int row, int zoom)
        {
            int height = GFX.TileHeight * zoom;
            int width = GFX.TileWidth * zoom;

            uint[,] pixels = new uint[height, width];
            fixed (uint* scan0 = pixels)
            fixed (uint* _pal = &palette.Data[row, 0])
            {
                GFX.DrawSquare(scan0, width, height, 0, 0, tile, _pal, zoom);
                return new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        public static void DrawSquare(uint* scan0, int width, int height, int x, int y, byte* gfx, uint* palette, int zoom)
        {
            for (int i = 0; i < GFX.TileHeight; i++)
                for (int j = 0; j < GFX.TileWidth; j++, gfx++)
                    Palette.DrawSolidColor(scan0, width, height, x + (j * zoom), y + (i * zoom), zoom, zoom, *(palette + *gfx));
        }

        public class Tile
        {
            public byte[,] Data;

            public Tile()
            {
                this.Data = new byte[GFX.TileHeight, GFX.TileWidth];
            }

            public Tile(ref byte[,] data)
            {
                this.Data = new byte[GFX.TileHeight, GFX.TileWidth];

                fixed (byte* src = data)
                fixed (byte* dest = this.Data)
                    for (int i = 0; i < GFX.TileSize; i++)
                        dest[i] = src[i];
            }

            public Tile(ref GFX gfx, int page, int row, int column)
            {
                this.Data = new byte[GFX.TileHeight, GFX.TileWidth];

                fixed (byte* src = &gfx.Data[page, row, column, 0, 0])
                fixed (byte* dest = this.Data)
                    for (int i = 0; i < GFX.TileSize; i++)
                        dest[i] = src[i];
            }

            public Bitmap DrawTile(ref Palette palette, int row, int zoom)
            {
                int height = GFX.TileHeight * zoom;
                int width = GFX.TileWidth * zoom;

                uint[,] pixels = new uint[height, width];
                fixed (uint* scan0 = pixels)
                fixed (uint* _pal = &palette.Data[row, 0])
                fixed (byte* src = this.Data)
                {
                    GFX.DrawSquare(scan0, width, height, 0, 0, src, _pal, zoom);
                    return new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
                }
            }

            public void FlipX()
            {
                byte[,] temp = new byte[GFX.TileHeight, GFX.TileWidth];
                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        temp[i, j] = this.Data[i, GFX.TileWidth - 1 - j];

                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        this.Data[i, j] = temp[i, j];
            }

            public void FlipY()
            {
                byte[,] temp = new byte[GFX.TileHeight, GFX.TileWidth];
                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        temp[i, j] = this.Data[GFX.TileHeight - 1 - i, j];

                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        this.Data[i, j] = temp[i, j];
            }

            public void Rotate90()
            {
                byte[,] temp = new byte[GFX.TileHeight, GFX.TileWidth];
                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        temp[i, j] = this.Data[GFX.TileHeight - 1 - j, i];

                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        this.Data[i, j] = temp[i, j];
            }

            public void Rotate180()
            {
                byte[,] temp = new byte[GFX.TileHeight, GFX.TileWidth];
                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        temp[i, j] = this.Data[GFX.TileWidth - 1 - i, GFX.TileHeight - 1 - j];

                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        this.Data[i, j] = temp[i, j];
            }

            public void Rotate270()
            {
                byte[,] temp = new byte[GFX.TileHeight, GFX.TileWidth];
                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        temp[i, j] = this.Data[j, GFX.TileWidth - 1 - i];

                for (int i = 0; i < GFX.TileHeight; i++)
                    for (int j = 0; j < GFX.TileWidth; j++)
                        this.Data[i, j] = temp[i, j];
            }
        }
    }
}