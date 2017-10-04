using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using LC_Sharp;

namespace SNES
{
    /// <summary>
    /// A 16x16 array representing a full SNES Palette.
    /// </summary>
    public unsafe class Palette
    {
        /// <summary>
        /// Represents the number of cells per row a Palette contains.
        /// </summary>
        public const int Rows = 0x10;
        /// <summary>
        /// Represents the number of columns a Palette contains.
        /// </summary>
        public const int Columns = Palette.Rows;
        /// <summary>
        /// Represents the total number of cells in a Palette.
        /// </summary>
        public const int ArraySize = Palette.Rows * Palette.Columns;
        /// <summary>
        /// Represents the data size (in bytes) for a standard Palette.
        /// </summary>
        public const int DataSize = Palette.ArraySize << 1;
        /// <summary>
        /// Represents the width in pixels a single Palette color will be drawn at.
        /// </summary>
        public const int ColorWidth = 0x10;
        /// <summary>
        /// Represents the height in pixels a single Palette color will be drawn at.
        /// </summary>
        public const int ColorHeight = Palette.ColorWidth;
        /// <summary>
        /// Represents the width in pixels the full Palette will be drawn at.
        /// </summary>
        public const int ImageWidth = Palette.ColorWidth * Palette.Columns;
        /// <summary>
        /// Represents the height in pixels the full Palette will be drawn at.
        /// </summary>
        public const int ImageHeight = Palette.ColorHeight * Palette.Rows;

        /// <summary>
        /// A 2-Dimensional array representing all cells of a Palette.
        /// </summary>
        public uint[,] Data;

        /// <summary>
        /// Initializes an empty Palette.
        /// </summary>
        public Palette()
        {
            this.Data = new uint[Palette.Rows, Palette.Columns];
            fixed (uint* ptr = this.Data)
            {
                int index = 0;
                for (int i = 0; i < Palette.Rows; i++, index += Palette.Columns)
                    for (int j = 1; j < Palette.Columns; j++)
                        ptr[index + j] = 0xFF000000;
            }
        }

        /// <summary>
        /// Initializes a Palette from a byte array.
        /// </summary>
        /// <param name="data">
        /// A byte array representing 15-bit SNES RGB values of at least 0x200 bytes.
        /// </param>
        public Palette(ref byte[] data)
        {
            this.Data = new uint[Palette.Rows, Palette.Columns];

            fixed (byte* p = data)
            fixed (uint* dest = this.Data)
            {
                ushort* src = (ushort*)p;
                for (int i = 0; i < Palette.ArraySize; i++)
                    dest[i] = LunarCompress.SNEStoPCRGB(src[i]) | 0xFF000000;

                for (int i = 0; i < Palette.ArraySize; i += Palette.Columns)
                    dest[i] &= 0xFFFFFF;
            }
        }

        /// <summary>
        /// Initializes a Palette copying data from an existing Palette.
        /// </summary>
        /// <param name="palette">
        /// A reference to an exisiting Palette.
        /// </param>
        public Palette(ref Palette palette)
        {
            this.Data = new uint[Palette.Rows, Palette.Columns];

            fixed (uint* src = palette.Data)
            fixed (uint* dest = this.Data)
                for (int i = 0; i < Palette.ArraySize; i++)
                    dest[i] = src[i];
        }

        /// <summary>
        /// Transforms the Palette data to a 0x200 byte array of 15-bit SNES RGB values.
        /// </summary>
        /// <returns>
        /// A byte array representing thr 15-bit SNES RGB values.
        /// </returns>
        public byte[] ToByteArray()
        {
            byte[] data = new byte[Palette.ArraySize << 1];

            fixed (byte* p = data)
            fixed (uint* src = this.Data)
            {
                ushort* dest = (ushort*)p;
                for (int i = 0; i < Palette.ArraySize; i++)
                    dest[i] = LunarCompress.PCtoSNESRGB(src[i]);
            }

            return data;
        }

        /// <summary>
        /// Draws a Palette to a 256x256 Bitmap.
        /// </summary>
        /// <returns>
        /// A 256x256 bitmap containing a 16x16 array of the Palette colors.
        /// </returns>
        public Bitmap DrawPalette()
        {
            uint[,] pixels = new uint[Palette.ImageHeight, Palette.ImageWidth];
            fixed (uint* scan0 = pixels)
            {
                DrawPalette(scan0, Palette.ImageWidth, Palette.ImageHeight, 0, 0);
                return new Bitmap(Palette.ImageWidth, Palette.ImageHeight, Palette.ImageWidth * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        /// <summary>
        /// Draws Row of the Palette to a 256x16 Bitmap.
        /// </summary>
        /// <param name="row">
        /// The specified row to draw.
        /// </param>
        /// <returns>
        /// A 256x16 Bitmap of the 16 colors in the specified row of the Palette.
        /// </returns>
        public Bitmap DrawPaletteRow(int row)
        {
            uint[,] pixels = new uint[Palette.ColorHeight, Palette.ImageWidth];
            fixed (uint* scan0 = pixels)
            fixed (uint* src = &this.Data[row, 0])
            {
                Palette.DrawPaletteRow(scan0, Palette.ImageWidth, Palette.ColorHeight, 0, 0, src);
                return new Bitmap(Palette.ImageWidth, Palette.ColorHeight, Palette.ImageWidth * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        /// <summary>
        /// Draws a specified color to a 16x16 Bitmap.
        /// </summary>
        /// <param name="color">
        /// A 32-bit aaaaaaaa-rrrrrrrr-gggggggg-bbbbbbbb color.
        /// </param>
        /// <returns>
        /// A 16x16 bitmap of the solid color.
        /// </returns>
        public static Bitmap DrawSquare(uint color)
        {
            uint[,] pixels = new uint[Palette.ColorHeight, Palette.ColorWidth];
            fixed (uint* scan0 = pixels)
            {
                Palette.DrawSolidColor(scan0, Palette.ColorWidth, Palette.ColorHeight, 0, 0, Palette.ColorWidth, Palette.ColorHeight, color);
                return new Bitmap(Palette.ColorWidth, Palette.ColorHeight, Palette.ColorWidth * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        /// <summary>
        /// Writes the Palette's pixel data to a uint pointer representing the first scanline.
        /// </summary>
        /// <param name="scan0">
        /// A pointer to the first pixel of the bitmap data.
        /// </param>
        /// <param name="width">
        /// The width of the bitmap.
        /// </param>
        /// <param name="height">
        /// The height of the bitmap.
        /// </param>
        /// <param name="x">
        /// The x-coordinate to draw the Palette to.
        /// </param>
        /// <param name="y">
        /// The y-coordinate to draw the Palette to.
        /// </param>
        public void DrawPalette(uint* scan0, int width, int height, int x, int y)
        {
            fixed (uint* ptr = this.Data)
            {
                uint* src = ptr;
                for (int row = 0; row < Palette.Rows; row++, src += Palette.Columns)
                    Palette.DrawPaletteRow(scan0, width, height, x, y + (row * Palette.ColorWidth), src);
            }
        }

        /// <summary>
        /// Writes the pixel data of a specified row of the Palette to a uint pointer representing the first scanline.
        /// </summary>
        /// <param name="scan0">
        /// A pointer to the first pixel of the bitmap data.
        /// </param>
        /// <param name="width">
        /// The width of the bitmap.
        /// </param>
        /// <param name="height">
        /// The height of the bitmap.
        /// </param>
        /// <param name="x">
        /// The x-coordinate to draw the Palette to.
        /// </param>
        /// <param name="y">
        /// The y-coordinate to draw the Palette to.
        /// </param>
        /// <param name="src">
        /// A pointer to the array containing the Palette row colors.
        /// </param>
        public static void DrawPaletteRow(uint* scan0, int width, int height, int x, int y, uint* src)
        {
            for (int col = 0; col < Palette.Columns; col++, src++)
                Palette.DrawSolidColor(scan0, width, height, x + (col * Palette.ColorWidth), y, Palette.ColorWidth, Palette.ColorHeight, *src);
        }

        /// <summary>
        /// Writes the pixel data of a single color to a uint pointer representing the first scanline.
        /// </summary>
        /// <param name="scan0">
        /// A pointer to the first pixel of the bitmap data.
        /// </param>
        /// <param name="width">
        /// The width of the bitmap.
        /// </param>
        /// <param name="height">
        /// The height of the bitmap.
        /// </param>
        /// <param name="x">
        /// The x-coordinate to draw the color to.
        /// </param>
        /// <param name="y">
        /// The y-coordinate to draw the color to,
        /// </param>
        /// <param name="lengthX">
        /// The width of the color to be drawn.
        /// </param>
        /// <param name="lengthY">
        /// The height of the color to be drawn.
        /// </param>
        /// <param name="color">
        /// A 32-bit aaaaaaaa-rrrrrrrr-gggggggg-bbbbbbbb color.
        /// </param>
        public static void DrawSolidColor(uint* scan0, int width, int height, int x, int y, int lengthX, int lengthY, uint color)
        {
            int w = (x + lengthX) < width ? lengthX : (width - x);
            int h = (y + lengthY) < height ? lengthY : (height - y);

            uint* dest = scan0 + (y * width) + x;
            for (int i = 0; i < w; i++, dest += width)
                for (int j = 0; j < h; j++)
                    dest[j] = color;
        }

        /// <summary>
        /// Modifies the Palette data to create a gradient between two colors of specified coordinates.
        /// </summary>
        /// <param name="x1">
        /// The x-coordinate of the first color.
        /// </param>
        /// <param name="y1">
        /// The y-coordinate of the second color.
        /// </param>
        /// <param name="x2">
        /// The x-coordinate of the second color.
        /// </param>
        /// <param name="y2">
        /// The y-coordinate of the second color.
        /// </param>
        public void Gradient(int x1, int y1, int x2, int y2)
        {
            int s1 = (y1 * Palette.Columns) + x1;
            int s2 = (y2 * Palette.Columns) + x2;
            int min = s1 < s2 ? s1 : s2;
            int max = s1 > s2 ? s1 : s2;
            int delta = max - min;
            if (delta == 0)
                return;

            fixed (uint* ptr = this.Data)
            {
                uint c1 = *(ptr + min);
                int r1 = (int)(c1 >> 0x10) & 0xFF;
                int g1 = (int)(c1 >> 8) & 0xFF;
                int b1 = (int)(c1 & 0xFF);

                uint c2 = *(ptr + max);
                int r2 = (int)(c2 >> 0x10) & 0xFF;
                int g2 = (int)(c2 >> 8) & 0xFF;
                int b2 = (int)(c2 & 0xFF);

                for (int i = 0; i <= delta; i++)
                {
                    int a = ((i + min) & 0x0F) == 0 ? 0 : 0xFF;
                    int r = ((r1 * (delta - i)) + (r2 * i)) / delta;
                    int g = ((g1 * (delta - i)) + (g2 * i)) / delta;
                    int b = ((b1 * (delta - i)) + (b2 * i)) / delta;

                    ptr[min + i] = (uint)((a << 0x18) | (r << 0x10) | (g << 8) | b);
                }
            }
        }

        /// <summary>
        /// Converts a 32-bit color to a System.Drawing.Color.
        /// </summary>
        /// <param name="argb">
        /// A 32-bit aaaaaaaa-rrrrrrrr-gggggggg-bbbbbbbb color.
        /// </param>
        /// <returns>
        /// A System.Drawing.Color equivalent of the 32-bit color.
        /// </returns>
        public static Color PCToSystemColor(uint argb)
        {
            int a = (int)(argb >> 0x18);
            int r = (((int)argb >> 0x10) & 0xFF) + 4;
            int g = (((int)argb >> 8) & 0xFF) + 4;
            int b = ((int)argb & 0xFF) + 4;

            r = r > 0xFF ? 0xF8 : (r & 0xF8);
            g = g > 0xFF ? 0xF8 : (g & 0xF8);
            b = b > 0xFF ? 0xF8 : (b & 0xF8);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Converts a System.Drawing.Color to a 32-bit color.
        /// </summary>
        /// <param name="color">
        /// A system.Drawing.Color.
        /// </param>
        /// <returns>
        /// A 32-bit aaaaaaaa-rrrrrrrr-gggggggg-bbbbbbbb color.
        /// </returns>
        public static uint SystemToPCColor(Color color)
        {
            uint alpha = (uint)color.A << 0x18;
            uint red = (uint)color.R << 0x10;
            uint green = (uint)color.G << 8;
            uint blue = (uint)color.B;
            return alpha | red | green | blue;
        }
    }
}