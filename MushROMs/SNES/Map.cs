using System;
using System.Drawing;
using System.Drawing.Imaging;
using LC_Sharp;

namespace SNES
{
    public unsafe struct Map
    {
        public const ushort NotSet = 0xFFFF;

        public int Height;
        public int Width;
        public ushort[,] Tiles;
        public Render8x8Flags[,] Flags;

        public Map(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.Tiles = new ushort[height, width];
            this.Flags = new Render8x8Flags[height, width];
            
            int area = height * width;
            fixed (ushort* ptr = this.Tiles)
                for (int i = 0; i < area; i++)
                    ptr[i] = Map.NotSet;
        }

        public Bitmap DrawMap(Palette palette, GFX gfx, Map16 tiles, Render8x8Flags flags)
        {
            int height = this.Height * Map16.TileHeight;
            int width = this.Width * Map16.TileWidth;
            uint[,] pixels = new uint[height, width];

            fixed (uint* scan0 = pixels)
            fixed (uint* _pal = palette.Data)
            fixed (byte* _gfx = gfx.Data)
            fixed (Map8* _tiles = tiles.Data)
            fixed (ushort* src = this.Tiles)
            {
                DrawMap(scan0, width, height, 0, 0, _pal, _gfx, _tiles, flags);
                return new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0));
            }
        }

        public void DrawMap(uint* scan0, int width, int height, int x, int y, uint* palette, byte* gfx, Map8* tiles, Render8x8Flags flags)
        {
            fixed (ushort* src = this.Tiles)
            fixed (Render8x8Flags* draw = this.Flags)
            {
                for (int i = 0; i < this.Height; i++)
                {
                    int index = i * this.Width;
                    int _y = y + (i * Map16.TileHeight);

                    for (int j = 0; j < this.Width; j++)
                    {
                        ushort map16 = src[index + j];
                        if (map16 != Map.NotSet)
                            Map16.DrawSquare(scan0, width, height, x + (j * Map16.TileWidth), _y, palette, gfx, tiles, map16, draw[index + j] | flags);
                    }
                }
            }
        }
    }
}