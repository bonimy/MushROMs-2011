using System;
using System.Drawing;
using System.Drawing.Imaging;
using LC_Sharp;
using SNES;

namespace MushROMs.SMB1
{
    public unsafe partial class Layer1
    {
        private const string DataException = "Data too small to be valid level with header.";
        private const string DataIndexException = "Data too small at index to be valid level with header.";
        private const string DataSizeException = "Data too small at index to be valid level with header.";
        private const string PageSizeException = "Maximum page limit reached.";
        private const string LevelEndException = "Level data did not have a proper end byte.";

        public const int MaxObjects = 0x1000;
        public const int MaxPages = 0x20;
        public const int MaxWidth = Layer1.MaxPages * 0x10;
        public const int MaxHeight = 0x0D;
        public const int ImageHeight = Layer1.MaxHeight * Map16.TileHeight;
        public const int ImageWidth = Layer1.MaxWidth * Map16.TileWidth;
        private const ushort NotSet = Map.NotSet;

        public Object[] Objects;
        public int ObjectCount;
        public MapType Type;
        public bool PreviewNextLevel;
        public ushort Time;
        public byte StartX;
        public byte StartY;
        public byte Background;
        public Map Map;

        public Layer1()
        {
            this.Map = new Map(Layer1.MaxHeight, Layer1.MaxWidth);
            Objects = new Object[Layer1.MaxObjects];
            ObjectCount = 0;
            Type = MapType.Ground;
            PreviewNextLevel = false;
            Time = 0x400;
            StartX = 0;
            StartY = 0;
            Background = 8;
        }

        public Layer1(byte[] data)
        {
            if (data.Length < 6)
                throw new ArgumentException(Layer1.DataException);

            Objects = new Object[MaxObjects];

            fixed (byte* ptr = data)
                Initialize(ptr, data.Length);
        }

        public Layer1(byte[] data, int index)
        {
            if (data.Length - index < 6)
                throw new ArgumentException(Layer1.DataIndexException);

            Objects = new Object[MaxObjects];
            fixed (byte* ptr = data)
                Initialize(ptr + index, data.Length - index);
        }

        public Layer1(byte* data, int index, int length)
        {
            if (length - index < 6)
                throw new ArgumentException(Layer1.DataSizeException);

            Objects = new Object[MaxObjects];
            Initialize(data + index, length - index);
        }

        private void Initialize(byte* src, int length)
        {
            fixed (Object* ptr = this.Objects)
            {
                Object* dest = ptr;
                GetHeader(src);     //The header data uses five bytes
                src += 5;

                int page = 0;
                GetNumObjects(src, length);     //Calculate how many objects there actually are beforehand

                for (int z = 0; z < ObjectCount; src += dest->ByteSize, dest->Width++, dest->Height++, dest++->Z = z++)
                {
                    byte coordinates = *src;
                    byte command = *(src + 1);

                    if ((command & 0x80) != 0)      //Gets the page skip command and checks to make sure we did not exceed.
                        page += 0x10;
                    if (page >= MaxWidth)
                        throw new ArgumentException(Layer1.PageSizeException);

                    command &= 0x7F;
                    if ((coordinates & 0x0F) != 0x0F)        //Standard object definitions
                    {
                        dest->X = page + (coordinates >> 4);      //X-coordinate of the object

                        coordinates &= 0x0F;
                        if (coordinates < 0x0D)     //Objects defined in the accessible level region.
                        {
                            dest->Y = coordinates & 0x0F;      //Y-coordinate of the object
                            if (command < 0x0C)     //Objects of just 1 tile.
                            {
                                dest->ByteSize = 2;
                                dest->Data = ObjectType.SingleTile;
                                dest->Value = command;
                            }
                            else if (command < 0x0F)     //Objects with non-changing dimensions and multiple tiles.
                            {
                                dest->ByteSize = 2;
                                dest->Data = ObjectType.StaticObject;
                                dest->Value = command - 0x0C;
                            }
                            else if (command == 0x0F)   //Direct map16 tile insertion.
                            {
                                dest->ByteSize = 5;
                                dest->Data = ObjectType.Map16Direct;
                                dest->Value = *((ushort*)(src + 2));
                                dest->Height = dest->Y + (*(src + 4) >> 4);
                                dest->Width = dest->X + (*(src + 4) & 0x0F);
                            }
                            else if (command < 0x40)    //Objects with vertical extension
                            {
                                dest->ByteSize = 2;
                                dest->Data = ObjectType.Vertical;
                                dest->Value = (command - 0x10) >> 4;
                                int height = *(src + 1);
                                if (dest->Value != 0 && height >= 8)
                                {
                                    height &= 7;
                                    dest->Value += 2;
                                }
                                dest->Height = dest->Y + (height & 0x0F);
                            }
                            else if (command < 0x70)    //Objects with horizontal extension
                            {
                                dest->ByteSize = 2;
                                dest->Data = ObjectType.Horizontal;
                                dest->Value = (command - 0x40) >> 4;
                                dest->Width = dest->X + (*(src + 1) & 0x0F);
                            }
                            else if (command < 0x7F)    //Objects with rectangular extension
                            {
                                dest->ByteSize = 3;
                                dest->Data = ObjectType.Rectangular;
                                dest->Value = command - 0x70;
                                dest->Height = dest->Y + (*(src + 2) >> 4);
                                dest->Width = dest->X + (*(src + 2) & 0x0F);
                            }
                            else    //Objects with long horizontal extension
                            {
                                dest->ByteSize = 3;
                                dest->Data = ObjectType.LongHorizontal;
                                dest->Value = 0;
                                dest->Width = dest->X + *(src + 2);
                                dest->Height = dest->Y + 4;
                            }
                        }
                        else if (coordinates == 0x0D)   //Objects mostly related to ground tiles
                        {
                            dest->ByteSize = 3;     //All objects are three bytes, so increment here
                            dest->Y = (command << 4) >> 4;     //Get Y-coordinate

                            if (command < 0x70)     //Ground objects
                            {
                                dest->Data = ObjectType.GroundObject;
                                dest->Value = command >> 4;
                                dest->Height = dest->Y + (*(src + 2) & 0x0F);
                                dest->Width = dest->X + (*(src + 2) >> 4);
                            }
                            else    //The remaing options have even more options! Such insanity.
                            {
                                command = (byte)(*(src + 2) >> 4);      //Get the new command byte.
                                if (command < 9)    //Objects for castles.
                                {
                                    dest->Data = ObjectType.CastleTileset;
                                    dest->Value = command;
                                    dest->Height = dest->Y + (*(src + 2) & 0x0F);
                                }
                                else if (command < 0x0C)    //More horizontally extendable objects.
                                {
                                    dest->Data = ObjectType.HorizontalExtra;
                                    dest->Value = command - 9;
                                    dest->Width = dest->X + (*(src + 2) & 0x0F);
                                }
                                else if (command < 0x0F)    //More vertically extendable objects.
                                {
                                    dest->Data = ObjectType.VerticalExtra;
                                    dest->Value = command - 0x0C;
                                    dest->Height = dest->Y + (*(src + 2) & 0x0F);
                                }
                                else    //The end-of-level staircase
                                {
                                    dest->Data = ObjectType.Staircase;
                                    dest->Value = 0;
                                    dest->Width = dest->X + (*(src + 2) & 0x0F);
                                }
                            }
                        }
                        else if (coordinates == 0x0E)   //More extra objects
                        {
                            dest->ByteSize = 2;     //All two-byte objects
                            if (command < 0x50)     //More objects with non-changing sizes of multiple tiles
                            {
                                dest->Data = ObjectType.StaticObjectExtra;
                                dest->Value = command >> 4;
                                dest->Y = command & 0x0F;
                            }
                            else    //Page skip. Sets the current page to the value (not really on object).
                                page = (command - 0x50) << 8;
                        }
                    }
                    else if (coordinates == 0x0F)   //Objects which are actually commands (like generators)
                    {
                        dest->ByteSize = 2;
                        dest->Data = ObjectType.Command;
                        dest->Value = command >> 4;
                        dest->X = page + (command & 0x0F);
                    }
                }
            }
            WriteMap();
        }

        private void GetHeader(byte* src)
        {
            this.Type = (MapType)(*src >> 6);
            this.PreviewNextLevel = (*src & 0x20) > 0;
            this.Time = (ushort)((*src & 0x0F) << 8);
            this.Time |= *(src + 1);
            this.StartX = *(src + 3);
            this.StartY = *(src + 4);
            this.Background = *(src + 5);
        }

        private void GetNumObjects(byte* src, int length)
        {
            this.ObjectCount = 0;
            int index = 0;
            int page = 0;
            for (; *(src + index) != 0xFF && index != length; ObjectCount++)
            {
                byte y = (byte)(*(src + index) & 0x0F);
                byte o = (byte)(*(src + index + 1) & 0x7F);

                if ((o & 0x80) > 0)
                    page++;
                if (page >= MaxPages)
                    throw new ArgumentException(Layer1.PageSizeException);

                if (y < 0x0D)
                {
                    if (o < 0x0F)
                        index += 2;
                    else if (o == 0x0F)
                        index += 5;
                    else if (o < 0x70)
                        index += 2;
                    else
                        index += 3;
                }
                else if (y == 0x0D)
                    index += 3;
                else
                    index += 2;
            }
            if (index == length && *(src + index) != 0xFF)
                throw new ArgumentOutOfRangeException(Layer1.LevelEndException);
        }

        public void WriteMap()
        {
            Map = new Map(Layer1.MaxHeight, Layer1.MaxWidth);
            fixed (Object* src = Objects)
            fixed (ushort* dest = Map.Tiles)
            fixed (Render8x8Flags* flags = Map.Flags)
            {
                for (int i = 0; i < MaxWidth * MaxHeight; i++)
                    *(dest + i) = 0;

                for (int z = 0; z < ObjectCount; z++)
                {
                    int i;
                    for (i = 0; i < ObjectCount; i++)   //Render objects in the correct Z-order.
                        if ((*(src + i)).Z == z)
                            break;

                    src[i].WriteObject(dest, MaxPages << 4, MaxHeight, flags, this.Type);
                }
            }
        }

        public unsafe struct Object
        {
            public int Value;
            public int X;
            public int Y;
            public int Height;
            public int Width;
            public int Z;
            public int ByteSize;
            public ObjectType Data;

            private void WriteTile(ushort* dest, int width, int height, int x, int y, ushort map16, Render8x8Flags* flags)
            {
                if (x < width && y < height)
                {
                    int offset = (y * width) + x;
                    *(dest + offset) = map16;
                }
            }

            public void WriteObject(ushort* dest, int width, int height, Render8x8Flags* flags, MapType mapType)
            {
                switch (this.Data)
                {
                    case ObjectType.SingleTile:
                        {
                            ushort map16;
                            if (this.Value >= 7 && mapType == MapType.Ground)
                                map16 = Layer1.SingleTileObject[this.Value + 5];   //Brick items have different tiles on ground levels.
                            else
                                map16 = Layer1.SingleTileObject[this.Value];
                            WriteTile(dest, width, height, this.X, this.Y, map16, flags);
                            break;
                        }
                    case ObjectType.StaticObject:
                        switch (this.Value)
                        {
                            case 0:     //Reverse L-Pipe
                                fixed (byte* data = Layer1.LPipe)
                                {
                                    byte* src = data;
                                    for (int x = this.X; x < this.X + 4; x++)
                                        for (int y = this.Y; y < this.Y + 4; y++)
                                            WriteTile(dest, width, height, x, y, *src++, flags);
                                }
                                break;
                            case 1:     //Flag pole
                                if (this.Y >= Layer1.MaxHeight - 5)
                                    this.Y = Layer1.MaxHeight - 5;

                                WriteTile(dest, width, height, this.X, this.Y, Layer1.FlagPoleBall, flags);

                                for (int y = this.Y + 1; y < Layer1.MaxHeight - 3; y++)
                                    WriteTile(dest, width, height, this.X, y, Layer1.FlagPoleBar, flags);

                                WriteTile(dest, width, height, this.X, Layer1.MaxHeight - 3, Layer1.FlagPoleBase, flags);
                                break;
                            case 2:     //Spring board
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.SpringBoardTop, flags);
                                WriteTile(dest, width, height, this.X, this.Y + 1, Layer1.SpringBoardBottom, flags);
                                break;
                        }
                        break;
                    case ObjectType.Map16Direct:    //Insert direct map16 tile
                        for (int x = this.X; x < this.Width; x++)
                            for (int y = this.Y; y < this.Height; y++)
                                WriteTile(dest, width, height, x, y, (ushort)this.Value, flags);
                        break;
                    case ObjectType.Vertical:
                        switch (this.Value)
                        {
                            case 0:     //Canon
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.CanonHead, flags);
                                if (this.Height - this.Y >= 2)
                                {
                                    WriteTile(dest, width, height, this.X, this.Y + 1, Layer1.CanonNeck, flags);
                                    for (int y = this.Y + 2; y < this.Height; y++)
                                        WriteTile(dest, width, height, this.X, y, Layer1.CanonBody, flags);
                                }
                                break;
                            case 1:     //Standard pipes
                            case 2:
                            case 3:
                            case 4:
                                {
                                    int add = ((this.Value - 1) & 1) << 1;
                                    WriteTile(dest, width, height, this.X, this.Y, Layer1.Pipes[add], flags);
                                    WriteTile(dest, width, height, this.X + 1, this.Y, Layer1.Pipes[add + 1], flags);

                                    ushort left = Layer1.Pipes[add + 4];
                                    ushort right = Layer1.Pipes[add + 5];
                                    for (int y = this.Y + 1; y < this.Height; y++)
                                    {
                                        WriteTile(dest, width, height, this.X, y, left, flags);
                                        WriteTile(dest, width, height, this.X + 1, y, right, flags);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.Horizontal:     //All horizontal objects
                        {
                            if (this.Value != 0 || this.Width - this.X > 1)
                            {
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.HorizontalObjects[this.Value + 3], flags);
                                if (this.Width - this.X >= 2)
                                {
                                    WriteTile(dest, width, height, this.Width - 1, this.Y, Layer1.HorizontalObjects[this.Value + 6], flags);
                                    ushort map16 = Layer1.HorizontalObjects[this.Value];
                                    for (int x = this.X + 1; x < this.Width - 1; x++)
                                        WriteTile(dest, width, height, x, this.Y, map16, flags);
                                }
                            }
                        }
                        if (this.Value == 0)
                            for (int x = this.X; x < this.Width; x++)
                                WriteTile(dest, width, height, x, this.Y + 1, Layer1.BridgeTile, flags);
                        break;
                    case ObjectType.Rectangular:
                        switch (this.Value)
                        {
                            case 0:     //Rectangular objects of 1 tile
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                {
                                    ushort map16 = Layer1.Rectangular[this.Value];
                                    for (int x = this.X; x < this.Width; x++)
                                        for (int y = this.Y; y < this.Height; y++)
                                            WriteTile(dest, width, height, x, y, map16, flags);
                                }
                                break;
                            case 7:     //Coin
                                {
                                    ushort map16 = mapType != MapType.Underwater ? Layer1.StandardCoin : Layer1.WaterCoin;
                                    for (int x = this.X; x < this.Width; x++)
                                        for (int y = this.Y; y < this.Height; y++)
                                            WriteTile(dest, width, height, x, y, map16, flags);
                                }
                                break;
                            case 8:     //Rectangular objects of two tiles
                            case 9:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                                {
                                    ushort top = Layer1.Rectangular[this.Value + 4];
                                    ushort bottom = Layer1.Rectangular[this.Value - 1];
                                    for (int x = this.X; x < this.Width; x++)
                                    {
                                        WriteTile(dest, width, height, x, this.Y, top, flags);
                                        for (int y = this.Y + 1; y < this.Height; y++)
                                            WriteTile(dest, width, height, x, y, bottom, flags);
                                    }
                                }
                                break;
                            case 0x0D:      //Green Tree Island
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.IslandCap[2], flags);
                                if (this.Width - this.X >= 2)
                                {
                                    ushort middle = Layer1.IslandCap[0];
                                    for (int x = this.X + 1; x < this.Width - 1; x++)
                                        WriteTile(dest, width, height, x, this.Y, middle, flags);
                                    WriteTile(dest, width, height, this.Width - 1, this.Y, Layer1.IslandCap[4], flags);

                                    if (this.Height - this.Y >= 2)
                                    {
                                        int x = this.X + 1;
                                        int y = this.Y + 1;
                                        if (this.Width - this.X > 3)    //stem is more than 1 tile wide 
                                        {
                                            WriteTile(dest, width, height, x, y, Layer1.GreenIslandNeckLeft, flags);
                                            for (x = this.X + 2; x < this.Width - 2; x++)
                                                WriteTile(dest, width, height, x, y, Layer1.GreenIslandNeckTile, flags);
                                            WriteTile(dest, width, height, x, y, Layer1.GreenIslandNeckRight, flags);

                                            for (y = this.Y + 2; y < this.Height; y++, x = this.X + 1)
                                            {
                                                WriteTile(dest, width, height, x++, y, Layer1.GreenIslandBodyLeft, flags);
                                                for (; x < this.Width - 2; x++)
                                                    WriteTile(dest, width, height, x, y, Layer1.GreenIslandBodyTile, flags);
                                                WriteTile(dest, width, height, x, y, Layer1.GreenIslandBodyRight, flags);
                                            }
                                        }
                                        else    //stem is exactly 1 tile wide
                                        {
                                            WriteTile(dest, width, height, x, y, Layer1.GreenIslandNeckSingle, flags);

                                            for (y = this.Y + 2; y < this.Height; y++)
                                                WriteTile(dest, width, height, x, y, Layer1.GreenIslandBodySingle, flags);
                                        }
                                    }
                                }
                                break;
                            case 0x0E:      //Red Mushroom Island
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.IslandCap[3], flags);

                                if (this.Width - this.X >= 2)
                                {
                                    WriteTile(dest, width, height, this.Width - 1, this.Y, Layer1.IslandCap[5], flags);
                                    for (int x = this.X + 1; x < this.Width - 1; x++)
                                        WriteTile(dest, width, height, x, this.Y, Layer1.IslandCap[1], flags);
                                    if ((this.Width - this.X >= 3) && (this.Height - this.Y >= 2))
                                    {
                                        int x = (this.X + this.Width - 1) >> 1;
                                        if ((x & 1) > 0)
                                            x -= 1;
                                        WriteTile(dest, width, height, x, this.Y + 1, Layer1.RedIslandStemNeck, flags);
                                        for (int y = this.Y + 2; y < this.Height; y++)
                                            WriteTile(dest, width, height, x, y, Layer1.RedIslandStemBody, flags);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.LongHorizontal:     //Maybe one day it can support more stuff, but for now, just the long, vertical ground object
                        for (int x = this.X; x < this.Width; x++)
                        {
                            WriteTile(dest, width, height, x, this.Y, Layer1.GroundTop, flags);
                            for (int y = this.Y + 1; y < this.Height; y++)
                                WriteTile(dest, width, height, x, y, Layer1.GroundBottom, flags);
                        }
                        break;
                    case ObjectType.GroundObject:
                        break;
                    case ObjectType.CastleTileset:
                        break;
                    case ObjectType.HorizontalExtra:    //Row of question blocks (with coin) or Bowser Bridge
                        {
                            ushort map16 = this.Value == 0 ? Layer1.QuestionBlockRowTile : Layer1.BowserBridgeTile;
                            for (int x = this.X; x < this.Width; x++)
                                WriteTile(dest, width, height, x, this.Y, map16, flags);
                            break;
                        }
                    case ObjectType.VerticalExtra:
                        switch (this.Value)
                        {
                            case 0:     //Ceiling cap
                                for (int y = this.Y; y < this.Height; y += 2)
                                    WriteTile(dest, width, height, this.X, y, Layer1.CeilingCapTile, flags);
                                break;
                            case 1:     //Lift Rope
                                for (int y = this.Y; y < this.Height; y++)
                                    WriteTile(dest, width, height, this.X, y, Layer1.VerticalLiftRopeTile, flags);
                                for (int y = this.Height; y < Layer1.MaxHeight; y++)
                                    WriteTile(dest, width, height, this.X, y, Layer1.VerticalLiftRopeAir, flags);
                                break;
                            case 2:     //Long Reverse L-Pipe
                                fixed (byte* tiles = Layer1.LongLPipe)
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        int x = this.X + i;
                                        int y;
                                        for (y = this.Y; y < this.Height - 2; y++)
                                            WriteTile(dest, width, height, x, y, *(tiles + i), flags);

                                        WriteTile(dest, width, height, x, y, *(tiles + 4 + i), flags);
                                        WriteTile(dest, width, height, x, y + 1, *(tiles + 8 + i), flags);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.Staircase:
                        {
                            int w = this.Width - this.Y;
                            int max = w < Layer1.StaircaseHeight ? w : Layer1.StaircaseHeight;
                            for (int x = 0; x < max; x++)
                                for (int y = max - (x + 1); y < max; y++)
                                    WriteTile(dest, width, height, this.X + x, this.Y + y, Layer1.StaircaseTile, flags);
                            if (w > Layer1.StaircaseHeight)
                                for (int y = 0; y < Layer1.StaircaseHeight; y++)
                                    WriteTile(dest, width, height, this.X + Layer1.StaircaseHeight, this.Y + y, Layer1.StaircaseTile, flags);
                        }
                        break;
                    case ObjectType.StaticObjectExtra:
                        switch (this.Value)
                        {
                            case 0:     //Small tree
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.SmallTreeHead, flags);
                                WriteTile(dest, width, height, this.X, this.Y + 1, Layer1.TreeStemTile, flags);
                                break;
                            case 1:     //Big tree
                                WriteTile(dest, width, height, this.X, this.Y, Layer1.BigTreeHeadTop, flags);
                                WriteTile(dest, width, height, this.X, this.Y + 1, Layer1.BigTreeHeadBottom, flags);
                                WriteTile(dest, width, height, this.X, this.Y + 2, Layer1.TreeStemTile, flags);
                                break;
                            case 2:     //Small castle
                                break;
                            case 3:     //Big castle
                                break;
                            case 4:     //Descending stairs of castle
                                fixed (byte* tiles = Layer1.CastleStairs)
                                {
                                    byte[] offset = { 0, 0, 0, 5, 4, 3 };
                                    for (int i = 0; i < offset.Length; i++)
                                    {
                                        for (int j = 0; j < Layer1.CastleStairsHeight; j++)
                                        {
                                            ushort map16 = tiles[offset[i] + j];
                                            if (map16 != 0)
                                                WriteTile(dest, width, height, this.X + i, this.Y + j, map16, flags);
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    case ObjectType.Command:
                        break;
                }
            }
        }

        public enum ObjectType
        {
            SingleTile,
            StaticObject,
            Map16Direct,
            Vertical,
            Horizontal,
            Rectangular,
            LongHorizontal,
            GroundObject,
            CastleTileset,
            HorizontalExtra,
            VerticalExtra,
            Staircase,
            StaticObjectExtra,
            Command,
        }

        public enum MapType
        {
            Underwater,
            Ground,
            Underground,
            Castle
        }
    }
}