using System;
using LC_Sharp;

namespace SNES
{
    public unsafe struct Map8
    {
        public const int TileHeight = 8;
        public const int TileWidth = 8;
        public const int TileSize = Map8.TileHeight * Map8.TileWidth;
        public const int DataSize = 2;

        public ushort Data;
        public bool Sprite;

        public int GetTile()
        {
            if (this.Sprite)
                return this.Data & 0x3FF;
            else
                return this.Data & 0x3FF;
        }

        public void SetTile(int value)
        {
            if (this.Sprite)
            {
                this.Data &= 0xFE00;
                this.Data |= (ushort)(value & 0x1FF);
            }
            else
            {
                this.Data &= 0xFC00;
                this.Data |= (ushort)(value & 0x3FF);
            }
        }

        public int GetPaletteIndex()
        {
            return (this.Data >> (this.Sprite ? 9 : 10)) & 7;
        }

        public void SetPaletteIndex(int value)
        {
            if (this.Sprite)
                this.Data &= 0xF1FF;
            else
                this.Data &= 0xE3FF;
            this.Data |= (ushort)((value & 7) << (this.Sprite ? 9 : 10));
        }

        public LayerPriority GetLayerPriority()
        {
            if (this.Sprite)
            {
                switch (this.Data & 0x3000)
                {
                    case 0x0000:
                        return LayerPriority.Priority0;
                    case 0x1000:
                        return LayerPriority.Priority1;
                    case 0x2000:
                        return LayerPriority.Priority2;
                    case 0x3000:
                        return LayerPriority.Priority3;
                    default:
                        return (LayerPriority)0;
                }
            }
            else
                return (this.Data & 0x2000) == 0 ? LayerPriority.Priority0 : LayerPriority.Priority1;
        }

        public void SetLayerPriority(LayerPriority value)
        {
            if (this.Sprite)
            {
                this.Data &= 0xCFFF;
                switch (value)
                {
                    case LayerPriority.Priority0:
                        return;
                    case LayerPriority.Priority1:
                        this.Data |= 0x1000;
                        return;
                    case LayerPriority.Priority2:
                        this.Data |= 0x2000;
                        return;
                    case LayerPriority.Priority3:
                        this.Data |= 0x3000;
                        return;
                    default:
                        return;
                }
            }
            else
            {
                if (value == LayerPriority.Priority0)
                    this.Data &= 0xDFFF;
                else
                    this.Data |= 0x2000;
            }
        }

        public bool GetFlipX()
        {
            return (this.Data & 0x4000) > 0;
        }

        public void SetFlipX(bool value)
        {
            if (value)
                this.Data |= 0x4000;
            else
                this.Data &= 0xBFFF;
        }

        public bool GetFlipY()
        {
            return (this.Data & 0x8000) > 0;
        }

        public void SetFlipY(bool value)
        {
            if (value)
                this.Data |= 0x8000;
            else
                this.Data &= 0x7FFF;
        }

        public void FlipVertical()
        {
            this.Data ^= 0x4000;
        }

        public void FlipHorizontal()
        {
            this.Data ^= 0x8000;
        }

        public void DrawTiles(uint* scan0, int width, int height, int x, int y, byte* gfx, uint* palette, Render8x8Flags flags)
        {
            LunarCompress.Render8x8(scan0, width, height, x, y, gfx, palette, this.Data, flags);
        }

        public enum LayerPriority
        {
            Priority0 = Render8x8Flags.Priority0,
            Priority1 = Render8x8Flags.Priority1,
            Priority2 = Render8x8Flags.Priority2,
            Priority3 = Render8x8Flags.Priority3
        }
    }
}