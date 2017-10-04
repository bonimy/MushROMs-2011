using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MushROMs.Controls;
using SNES;
using LC_Sharp;

namespace MushROMs.SMB1
{
    public unsafe partial class ObjectSelector : EditorForm
    {
        private new SMB1Editor Parent;

        public Map Map;
        public Layer1.Object Object;
        public int[] SelectedIndexes;
        public int ObjectTypeIndex;

        public Palette Palette
        {
            get
            {
                return Parent.Palette;
            }
        }

        public GFX GFX
        {
            get
            {
                return Parent.GFX;
            }
        }

        public Map16 Map16
        {
            get
            {
                return Parent.Map16;
            }
        }

        public ObjectSelector(IntPtr hwnd)
        {
            InitializeComponent();
            this.Parent = (SMB1Editor)Control.FromHandle(hwnd);
            this.Map = new Map(0x10, 0x10);
            SelectedIndexes = new int[cbxObjectType.Items.Count];
            for (int i = 0; i < SelectedIndexes.Length; i++)
                SelectedIndexes[i] = 0;
            this.cbxObjectType.SelectedIndex = 0;
        }

        private void cbxObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndexes[this.ObjectTypeIndex] = this.lbxObject.SelectedIndex;
            this.ObjectTypeIndex = this.cbxObjectType.SelectedIndex;
            this.lbxObject.Items.Clear();
            this.lbxObject.Items.AddRange(this.ListItems[this.cbxObjectType.SelectedIndex]);
            this.lbxObject.SelectedIndex = this.SelectedIndexes[this.ObjectTypeIndex];
        }

        #region Objects list for listbox
        private string[][] ListItems = {
            new string[]{   //Standard Objects
                "Question block with Mushroom",
                "Question block with Coin",
                "Invisible block with Coin",
                "Used block",
                "Rope for axe",
                "Axe",
                "Brick with Mushroom",
                "Brick with Vine",
                "Brick with Star",
                "Brick with 10 coins",
                "Brick with 1UP",
                "Invisible block with 1UP",
                "Reverse L-Pipe",
                "Flag Pole"
            },
            new string[]{
                "Page 0x00",
                "Page 0x01",
                "Page 0x02",
                "Page 0x03",
                "Page 0x04",
                "Page 0x05",
                "Page 0x06",
                "Page 0x07",
                "Page 0x08",
                "Page 0x09",
                "Page 0x0A",
                "Page 0x0B",
                "Page 0x0C",
                "Page 0x0D",
                "Page 0x0E",
                "Page 0x0F",
            },
            new string[]{   //Expandable objects (Linear)
                "Canon",
                "Enterable Pipe",
                "Unenterable Pipe",
                "Bridge",
                "Grass (Scenery)",
                "Balance Rope"
            },
            new string[]{   //Expandable objects (Rectangular)
                "Breakable Brick",
                "Solid Block",
                "Cloud Ground",
                "Fence (Scenery)",
                "Solid Sea Block",
                "Solid Sea Vine",
                "Solid Castle Block",
                "Coin",
                "Water",
                "Lava",
                "Ground",
                "Sea Ground",
                "Castle Floor",
                "Green Tree Island",
                "Mushroom Island",
                "Long Length Ground"
            },
            new string[]{   //Ground objects
                "Ground with left ledge",
                "Ground with right ledge",
                "Ground with left and right ledges",
                "Castle floor with left ledge",
                "Castle floor with right ledge",
                "Castle floor with left and right ledges",
                "Castle ceiling"
            },
            new string[]{   //Misc. objects
                "",
                "Row of question blocks",
                "Bowser Bridge",
                "Ceiling Cap",
                "Lift\'s Vertical Rope",
                "Long Reverse L-Pipe",
                "Staircase"
            },
            new string[]{   //Scenery Objects
                "Small Tree (Scenery)",
                "Big Tree (Scenery)",
                "Small Castle",
                "Big Castle",
                "Descending Stairs"
            },
            new string[]{   //Sprite commands
                "Scroll Stop",
                "Scroll Stop (Warp Zone)",
                "Cheep Cheep Generator",
                "Bullet Bill Generator",
                "Bowser\'s Fire Generator"
            }};
        #endregion

        private void drwSelectedObject_Paint(object sender, PaintEventArgs e)
        {
            DrawObject(e.Graphics);
        }

        private void DrawObject(Graphics g)
        {
            int width = 0x100;
            int height = 0x100;

            uint[,] data = new uint[width, height];
            fixed (uint* palette = Palette.Data)
            fixed (byte* gfx = GFX.Data)
            fixed (Map8* tiles = Map16.Data)
            fixed (uint* scan0 = data)
            {
                Map.DrawMap(scan0, width, height, 0, 0, palette, gfx, tiles, Render8x8Flags.Draw);
                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0)), Point.Empty);
            }
        }

        public void RedrawObjectSelector()
        {
            drwSelectedObject.Invalidate();
        }

        private void lbxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteObject();
            RedrawObjectSelector();
        }

        private void WriteObject()
        {
            switch (cbxObjectType.SelectedIndex)
            {
                case 1:
                    fixed (ushort* ptr = this.Map.Tiles)
                    {
                        int index = lbxObject.SelectedIndex << 8;
                        for (int i = 0; i < 0x100; i++)
                            ptr[i] = (ushort)(index + i);
                        break;
                    }
            }
        }

        private void drwSelectedObject_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
