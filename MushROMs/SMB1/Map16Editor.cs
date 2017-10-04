using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using MushROMs.Controls;
using LC_Sharp;
using SNES;

namespace MushROMs.SMB1
{
    public unsafe partial class Map16Editor : EditorForm
    {
        #region strings
        private const string Base = "\\Map16";
        private const string DataFile1 = "\\Default\\Map16G.bin";
        private const string DataFile2 = "\\Default\\Map16.bin";
        #endregion

        public const int MaxPages = 0x10;

        /// <summary>
        /// The SMB1Editor that owns this Map16Editor.
        /// </summary>
        private new SMB1Editor Parent;
        public Map16 Map16;
        public ushort[] ActsLike;
        private Color Map16BG;
        private int X;
        private int Y;
        private int Page;

        private GFX GFX
        {
            get
            {
                return Parent.GFX;
            }
        }
        private Palette Palette
        {
            get
            {
                return Parent.Palette;
            }
        }

        public Map16Editor(IntPtr hwnd)
        {
            InitializeComponent();
            Parent = (SMB1Editor)Control.FromHandle(hwnd);
            this.BaseDirectory = Parent.BaseDirectory + Map16Editor.Base;
            GetMap16Data();
            GetActsLikeData();
            Map16BG = Color.Gray;
            this.Page = 0;
        }

        private void GetMap16Data()
        {
            //ToDo: Make this work correctly when the editor gets better.
            string path = this.BaseDirectory + Map16Editor.DataFile1;
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                if (data.Length == MaxPages << 0x0B)
                    Map16 = new Map16(data);
                else
                    Map16 = new Map16();    //Instead, maybe supply a default resource?
            }
            else
                Map16 = new Map16();    //Lame fix. Should think of something better.
        }

        private void GetActsLikeData()
        {
            //ToDo: Make this work correctly
            string path = this.BaseDirectory + Map16Editor.DataFile2;
            ActsLike = new ushort[MaxPages << 8];
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                if (data.Length == MaxPages << 9)
                {
                    fixed (byte* ptr = data)
                    fixed (ushort* dest = ActsLike)
                    {
                        ushort* src = (ushort*)ptr;
                        int max = MaxPages << 0x10;
                        for (int i = 0; i < MaxPages << 8; i++)
                            *(dest + i) = *(src + i);
                    }
                }
            }
        }

        public void RedrawMap16()
        {
            drwMap16.Invalidate();
            Parent.RedrawLevelEditor();
        }

        private void drwMap16_Paint(object sender, PaintEventArgs e)
        {
            DrawMap16Editor(e.Graphics);
        }

        private void DrawMap16Editor(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Map16BG), new Rectangle(0, 0, 0x100, 0x100));
            g.DrawImageUnscaled(Map16.DrawMap16Page(GFX, Palette, 0, this.X << 4, this.Y << 4), Point.Empty);

        }

        private void drwMap16_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditorForm.IsInBounds(e.Location, this.ClientRectangle))
            {
                int x = e.X >> 4;
                int y = e.Y >> 4;
                if (x != this.X || y != this.Y)
                {
                    this.X = x;
                    this.Y = y;
                    RedrawMap16();
                }
            }
        }

        private void drwMap16_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}