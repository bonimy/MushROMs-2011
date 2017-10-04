using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using LC_Sharp;
using SNES;

namespace MushROMs.SMB1
{
    public unsafe partial class GFXEditor : EditorForm
    {
        #region strings
        private const string EditorName = "GFX Editor";
        private const string GBX_GFXEditor = "GFX Editor";
        private const string BTN_PrevPage = "Prev. Page";
        private const string BTN_NextPage = "Next Page";
        private const string BTN_PalUp = "Pal. Up";
        private const string BTN_PalDn = "Pal. Dn";
        private const string BTN_Undo = "Undo";
        private const string BTN_Redo = "Redo";
        private const string BTN_SaveAll = "Save All";
        private const string GBX_TileEditpr = "Tile Editor";
        private const string BTN_Rotate90 = "Rotate 90º";
        private const string BTN_FlipX = "Flip X";
        private const string BTN_FlipY = "Flip Y";
        private const string BTN_TileUndo = "Undo";
        private const string BTN_TileRedo = "Redo";
        private const string LBL_ColorX = "Edit Color ";

        private const string Status_DRW_GFX1 = "Left click (or Ctrl+C) to copy a tile and Right click (or Ctrl+V) to paste a tile.";
        private const string Status_BTN_PrevPage = "Pg. Up: Go back one page.";
        private const string Status_BTN_NextPage = "Pg. Dn: Go forward one page.";
        private const string Status_BTN_PalUp = "Home: Go back one Palette.";
        private const string Status_BTN_PalDn = "End: Go forward on Palette.";
        private const string Status_BTN_Undo = "Ctrl+Z: Undo last operation of GFX Editor.";
        private const string Status_BTN_Redo = "Ctrl+Z: Redo last operation of GFX Editor.";
        private const string Status_BTN_SaveAll = "Ctrl+S: Save all GFX.";
        private const string Status_BTN_Rotate90 = "Rotates the tile 90 degrees clockwise.";
        private const string Status_BTN_FlipX = "Flips the tile horizontally.";
        private const string Status_BTN_FlipY = "Flips the tile vertically.";
        private const string Status_BTN_TileUndo = "Ctrl+Shift+Z: Undo last operation to Tile Editor.";
        private const string Status_BTN_TileRedo = "Ctrl+Shift+Y: Redo last operation to Tile Editor.";
        private const string Status_DRW_Tile = "Use mouse buttons (or number keys and arrows) to draw pixels to tile.";
        private const string Status_DRW_ColorX = "Selected color from Palette: ";
        private const string Status_DRW_Palette = "Left click (or number 1-6): Select a color to draw with.";

        private const string Base = "\\GFX";
        private const string Static = "\\Tables\\Static.bin";
        private const string InitialGFX = "\\Default\\GFX";
        private const string GraphicsExt = "lz2";
        private const string TableExt = "bin";
        private const string SpriteFolder = "\\Tables\\Sprite";
        private const string ObjectFolder = "\\Tables\\Object";
        #endregion

        #region Constants
        /// <summary>
        /// The zoom factor of the GFXEditor.
        /// </summary>
        private const int GFXZoom = 2;
        /// <summary>
        /// The calculated width of a GFXEditor 8x8 tile with zoom.
        /// </summary>
        private const int TileWidth = GFX.TileWidth * GFXEditor.GFXZoom;
        /// <summary>
        /// The calculated height of a GFXEditor 8x8 tile with zoom.
        /// </summary>
        private const int TileHeight = GFX.TileHeight * GFXEditor.GFXZoom;
        /// <summary>
        /// The calculated width of the GFXEditor with zoom.
        /// </summary>
        private const int GFXWidth = GFX.ImageWidth * GFXEditor.GFXZoom;
        /// <summary>
        /// The calculated height of the GFXEditor with zoom.
        /// </summary>
        private const int GFXHeight = GFX.ImageHeight * GFXEditor.GFXZoom;
        /// <summary>
        /// The zoom factor of the TileEditor.
        /// </summary>
        private const int EditTileZoom = 0x10;
        /// <summary>
        /// The calculated width of the TileEditor with zoom.
        /// </summary>
        private const int EditTileWidth = GFX.TileWidth * GFXEditor.EditTileZoom;
        /// <summary>
        /// The calculated height of the TileEditor with zoom.
        /// </summary>
        private const int EditTileHeight = GFX.TileHeight * GFXEditor.EditTileZoom;
        /// <summary>
        /// GFX Index value indicating no index is chosed and should fallback to the default index.
        /// </summary>
        private const ushort NotSet = 0xFFFF;
        #endregion

        #region Variables
        /// <summary>
        /// The SMB1Editor that owns this GFXEditor.
        /// </summary>
        private new SMB1Editor Parent;
        /// <summary>
        /// The current GFX of the GFXEditor.
        /// </summary>
        public GFX GFX;
        /// <summary>
        /// The current GFX.Tile of the GFXEditor.
        /// </summary>
        private GFX.Tile Tile;
        /// <summary>
        /// Array of indexes describing which GFX file is set for each respective location.
        /// </summary>
        private ushort[] CurrentIndexes;
        /// <summary>
        /// When CurrentIndexes are not set, the editor falls back to these Indexes.
        /// </summary>
        private ushort[] DefaultIndexes;
        /// <summary>
        /// A two-dimensional array containg all indexes for all levels.
        /// </summary>
        private ushort[,] AllIndexes;
        /// <summary>
        /// Gets the current level of the SMB1Editor.
        /// </summary>
        private int Level
        {
            get
            {
                return Parent.Level;
            }
        }
        /// <summary>
        /// Gets the current Palette of the SMB1Editor.
        /// </summary>
        private Palette Palette
        {
            get
            {
                return Parent.Palette;
            }
        }
        private int _paletteIndex;
        /// <summary>
        /// Gets or sets an index value determining which palette row to use for drawing.
        /// </summary>
        private int PaletteIndex
        {
            get
            {
                return _paletteIndex;
            }
            set
            {
                _paletteIndex = value;
                RedrawForm(true);
            }
        }
        private int _paletteX;
        /// <summary>
        /// X-coordinate in the Palette view.
        /// </summary>
        private int PaletteX
        {
            get
            {
                return _paletteX;
            }
            set
            {
                _paletteX = value;
                RedrawPalette();
            }
        }
        /// <summary>
        /// Gets the Map16Editor owned by the Parent.
        /// </summary>
        private Map16Editor Map16Editor
        {
            get
            {
                return Parent.Map16Editor;
            }
        }
        /// <summary>
        /// Array of x-indexes for selectable colors in the Tile Editor.
        /// </summary>
        private int[] EditColorIndexes;
        private int _page;
        /// <summary>
        /// Gets or sets the current page visible in the GFXEditor.
        /// </summary>
        private int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
                RedrawGFX(false);
            }
        }
        /// <summary>
        /// Current X-coordinate of the GFX Editor.
        /// </summary>
        private int X;
        /// <summary>
        /// Current Y-coordinate of the GFX Editor.
        /// </summary>
        private int Y;
        /// <summary>
        /// Current X-coordinate of the GFX Tile Editor.
        /// </summary>
        private int EditX;
        /// <summary>
        /// Current Y-coordinate of the GFX Tile Editor.
        /// </summary>
        private int EditY;
        /// <summary>
        /// Keeps track of which number on the keyboard the user is holding.
        /// </summary>
        private int EditNumDown;
        /// <summary>
        /// Mouse X-coordinate in the current editor.
        /// </summary>
        private int MouseX;
        /// <summary>
        /// Mouse y-coordinate in the current editor.
        /// </summary>
        private int MouseY;
        /// <summary>
        /// Array of the Draw Control colors.
        /// </summary>
        private DrawControl[] drwColor;
        /// <summary>
        /// Array of the Labels for the Draw Control colors.
        /// </summary>
        private Label[] lblColor;
        /// <summary>
        /// Saves the GFXHistoryIndex value since the last save.
        /// </summary>
        private int SaveIndex;
        /// <summary>
        /// An index for where in the Tile History Data the user is.
        /// </summary>
        private int GFXHistoryIndex;
        /// <summary>
        /// A list of all previous actions done by the user to the GFX Editor (for Undo).
        /// </summary>
        private List<GFXSaveData> GFXPrev;
        /// <summary>
        /// A list of all future actions the user did to the GFX Editor before undoing (for Redo).
        /// </summary>
        private List<GFXSaveData> GFXNext;
        /// <summary>
        /// An index for where in the Pixel History Data the user is.
        /// </summary>
        private int TileHistoryIndex;
        /// <summary>
        /// A list of all previous actions done by the user to the Tile Editor (for Undo).
        /// </summary>
        private List<TileSaveData> TilePrev;
        /// <summary>
        /// A list of all future actions the user did to the Tile Editor before undoing (for Redo).
        /// </summary>
        private List<TileSaveData> TileNext;
        #endregion

        #region Functions
        /// <summary>
        /// Initializes a new instance of a GFXEditor.
        /// </summary>
        /// <param name="hwnd">
        /// Handle to the SMB1Editor Form.
        /// </param>
        public GFXEditor(IntPtr hwnd)
        {
            //Form related stuff.
            InitializeComponent();
            Parent = (SMB1Editor)Control.FromHandle(hwnd);
            this.BaseDirectory = Parent.BaseDirectory + GFXEditor.Base;

            this.drwColor = new DrawControl[] { this.drwColor1, this.drwColor2, this.drwColor3, this.drwColor4, this.drwColor5, this.drwColor6 };
            this.lblColor = new Label[] { this.lblColor1, this.lblColor2, this.lblColor3, this.lblColor4, this.lblColor5, this.lblColor6 };

            #region const string assignment
            this.Text = GFXEditor.EditorName;
            this.gbxGFXEditor.Text = GFXEditor.GBX_GFXEditor;
            this.btnPrevPage.Text = GFXEditor.BTN_PrevPage;
            this.btnNextPage.Text = GFXEditor.BTN_NextPage;
            this.btnPalDn.Text = GFXEditor.BTN_PalUp;
            this.btnPalUp.Text = GFXEditor.BTN_PalDn;
            this.btnUndo.Text = GFXEditor.BTN_Undo;
            this.btnRedo.Text = GFXEditor.BTN_Redo;
            this.btnSaveAll.Text = GFXEditor.BTN_SaveAll;
            this.gbxTileEditor.Text = GFXEditor.GBX_TileEditpr;
            this.btnRotate90.Text = GFXEditor.BTN_Rotate90;
            this.btnFlipX.Text = GFXEditor.BTN_FlipX;
            this.btnFlipY.Text = GFXEditor.BTN_FlipY;
            this.btnTileUndo.Text = GFXEditor.BTN_TileUndo;
            this.btnTileRedo.Text = GFXEditor.BTN_TileRedo;
            for (int i = 0; i < this.lblColor.Length; i++)
                lblColor[i].Text = GFXEditor.LBL_ColorX + (i + 1).ToString();

            this.drwGFX.Tag = GFXEditor.Status_DRW_GFX1;
            this.btnPrevPage.Tag = GFXEditor.Status_BTN_PrevPage;
            this.btnNextPage.Tag = GFXEditor.Status_BTN_NextPage;
            this.btnPalDn.Tag = GFXEditor.Status_BTN_PalUp;
            this.btnPalUp.Tag = GFXEditor.Status_BTN_PalDn;
            this.btnUndo.Tag = GFXEditor.Status_BTN_Undo;
            this.btnRedo.Tag = GFXEditor.Status_BTN_Redo;
            this.btnSaveAll.Tag = GFXEditor.Status_BTN_SaveAll;
            this.btnRotate90.Tag = GFXEditor.Status_BTN_Rotate90;
            this.btnFlipX.Tag = GFXEditor.Status_BTN_FlipX;
            this.btnFlipY.Tag = GFXEditor.Status_BTN_FlipY;
            this.btnTileUndo.Tag = GFXEditor.Status_BTN_TileUndo;
            this.btnTileRedo.Tag = GFXEditor.Status_BTN_TileRedo;
            this.drwTile.Tag = GFXEditor.Status_DRW_Tile;
            this.drwPalette.Tag = GFXEditor.Status_DRW_Palette;
            #endregion

            //Set up data for GFX
            GetDefaultIndexes();
            GetAllIndexes();
            SetCurrentIndexes();
            InitializeGFX();

            //Set up data for GFX Tile Editor
            this.EditNumDown = -1;
            Tile = new GFX.Tile();
            this.EditColorIndexes = new int[this.drwColor.Length];
            SetEditColor(0, 0);
            SetEditColor(1, 1);

            //Tags for keyboard use.
            for (int i = 0; i < this.drwColor.Length; i++)
                this.drwColor[i].Tag = i;

            this.ResetHistory();

            this.btnPrevPage.Enabled =
            this.btnPalUp.Enabled = false;

            this.ActiveControl = drwGFX;
        }
        /// <summary>
        /// Gets the default index values from an included file from the SMB1 Project.
        /// </summary>
        private void GetDefaultIndexes()
        {
            //To do: change to load from ROM or file when needed.
            byte[] data = File.ReadAllBytes(this.BaseDirectory + GFXEditor.Static);
            this.DefaultIndexes = new ushort[data.Length >> 1];
            
            fixed (byte* ptr = data)
            fixed (ushort* dest = this.DefaultIndexes)
            {
                ushort* src = (ushort*)ptr;
                for (int i = 0; i < this.DefaultIndexes.Length; i++)
                    dest[i] = src[i];
            }
        }
        /// <summary>
        /// Gets all index values defined in an included file from the SMB1 project.
        /// </summary>
        private void GetAllIndexes()
        {
            //To do: change to load from ROM or file when needed.
            CurrentIndexes = new ushort[this.DefaultIndexes.Length];
            AllIndexes = new ushort[9, SMB1Editor.MaxLevels];
            fixed (ushort* dest = AllIndexes)
            {
                for (int i = 0; i < 9; i++)
                {
                    int index = i * SMB1Editor.MaxLevels;

                    StringBuilder sb = new StringBuilder(this.BaseDirectory);

                    //Indexes for sprites
                    if (i > 4)
                    {
                        sb.Append(GFXEditor.SpriteFolder);
                        sb.Append(i - 4);
                    }
                    //Indexes for objects.
                    else
                    {
                        sb.Append(GFXEditor.ObjectFolder);
                        sb.Append(i + 1);
                    }
                    sb.Append('.');
                    sb.Append(GFXEditor.TableExt);
                    string path = sb.ToString();

                    if (File.Exists(path))
                    {
                        byte[] data = File.ReadAllBytes(path);
                        if (data.Length == SMB1Editor.MaxLevels << 1)
                        {
                            fixed (byte* ptr = data)
                            {
                                ushort* src = (ushort*)ptr;
                                for (int j = 0; j < SMB1Editor.MaxLevels; j++)
                                    dest[index + j] = src[j];
                            }
                        }
                    }
                    //Allows to use Default GFX.
                    else
                        for (int j = 0; j < SMB1Editor.MaxLevels; j++)
                            dest[index + j] = GFXEditor.NotSet;
                }
            }
        }
        /// <summary>
        /// Reinitializes all data in the GFXEditor.
        /// </summary>
        /// <param name="draw">
        /// If set, will redraw the GFX Editor.
        /// </param>
        public void Reset(bool draw)
        {
            SetCurrentIndexes();
            InitializeGFX();
            ResetHistory();

            if (draw)
                RedrawGFX(true);
        }
        /// <summary>
        /// Resets all History done in the GFX Editor.
        /// </summary>
        private void ResetHistory()
        {
            //Initialize Unod/Redo history
            this.SaveIndex = 0;
            this.GFXHistoryIndex = 0;
            this.GFXPrev = new List<GFXSaveData>();
            this.GFXNext = new List<GFXSaveData>();
            this.TileHistoryIndex = 0;
            this.TilePrev = new List<TileSaveData>();
            this.TileNext = new List<TileSaveData>();

            this.btnUndo.Enabled =
            this.btnRedo.Enabled =
            this.btnSaveAll.Enabled =
            this.btnTileUndo.Enabled =
            this.btnTileRedo.Enabled = false;
        }
        /// <summary>
        /// Assigns to CurrentIndexes values from AllIndexes depending on the Level.
        /// </summary>
        private void SetCurrentIndexes()
        {
            //To do: Draw (possibly)
            for (int i = 0; i < 9; i++)
            {
                ushort value = this.AllIndexes[i, this.Level];
                if (value == GFXEditor.NotSet)
                    this.CurrentIndexes[i] = this.DefaultIndexes[i];
                else
                    this.CurrentIndexes[i] = value;
            }

            int l = this.DefaultIndexes.Length;
            for (int i = 9; i < l; i++)
                this.CurrentIndexes[i] = DefaultIndexes[i];
        }
        /// <summary>
        /// Inializes and sets GFX from files depending on the current index values of the level.
        /// </summary>
        private void InitializeGFX()
        {
            int l = CurrentIndexes.Length;
            GFX = new GFX(l, GraphicsFormats._4BPP);
            for (int i = 0; i < l; i++)
            {
                StringBuilder sb = new StringBuilder(this.BaseDirectory);
                sb.Append(GFXEditor.InitialGFX);
                sb.Append(CurrentIndexes[i].ToString("X2"));
                sb.Append('.');
                sb.Append(GFXEditor.GraphicsExt);

                string path = sb.ToString();
                if (File.Exists(path))
                {
                    byte[] rom = File.ReadAllBytes(path);
                    LunarCompress.OpenRAMFile(ref rom);
                    byte[] data = LunarCompress.Decompress(CompressionFormats.LZ2, 0x10000);
                    if (data.Length != 0)
                        GFX.NewPage(i, ref data);
                    LunarCompress.CloseFile();
                }
            }
        }
        /// <summary>
        /// Redraws all visible editors in the GFXEditor Form.
        /// </summary>
        /// <param name="branch">
        /// If set, will draw other controls beyond the GFX Editor.
        /// </param>
        public void RedrawForm(bool branch)
        {
            RedrawGFX(branch);
            RedrawPalette();
            RedrawTile();
            RedrawEditColors();
        }
        private void CheckPastGFXData()
        {
            if (this.GFXHistoryIndex < this.GFXPrev.Count)
            {
                this.GFXPrev.RemoveRange(this.GFXHistoryIndex, this.GFXPrev.Count - this.GFXHistoryIndex);
                this.GFXNext = new List<GFXSaveData>();
            }

            this.GFXHistoryIndex++;

            if (!btnSaveAll.Enabled)
                this.btnSaveAll.Enabled = true;
            if (!btnUndo.Enabled)
                this.btnUndo.Enabled = true;
            if (btnRedo.Enabled)
                this.btnRedo.Enabled = false;
        }
        private void SetGFXUndoHistory(int page, int y, int x)
        {
            CheckPastGFXData();
            this.GFXPrev.Add(new GFXSaveData(page, x, y, ref this.GFX));
        }
        private void SetGFXUndoHistory(int page)
        {
            CheckPastGFXData();
            this.GFXPrev.Add(new GFXSaveData(page, ref this.GFX));
        }
        private void SetGFXRedoHistory()
        {
            int h = this.GFXHistoryIndex;
            if (this.GFXPrev.Count - h - 1 == this.GFXNext.Count)
            {
                int page = this.GFXPrev[h].Page;
                if (!this.GFXPrev[h].IsPage)
                {
                    int x = this.GFXPrev[h].X;
                    int y = this.GFXPrev[h].Y;
                    this.GFXNext.Add(new GFXSaveData(page, x, y, ref this.GFX));
                }
                else
                    this.GFXNext.Add(new GFXSaveData(page, ref this.GFX));
            }
        }
        private void GFXUndo()
        {
            if (this.GFXHistoryIndex > 0)
            {
                this.GFXHistoryIndex--;
                SetGFXRedoHistory();

                int h = this.GFXHistoryIndex;
                if (!this.GFXPrev[h].IsPage)
                {
                    int x = this.GFXPrev[h].X;
                    int y = this.GFXPrev[h].Y;
                    this.GFX.SetTile(this.GFXPrev[h].Page, y, x, ref this.GFXPrev[h].Tile);
                }
                else
                    this.GFX.NewPage(0, this.GFXPrev[h].Page, ref this.GFXPrev[h].GFX);

                RedrawGFX(true);

                if (btnSaveAll.Enabled && this.GFXHistoryIndex == this.SaveIndex)
                    this.btnSaveAll.Enabled = false;
                else if (!btnSaveAll.Enabled && this.GFXHistoryIndex != this.SaveIndex)
                    this.btnSaveAll.Enabled = true;

                if (btnUndo.Enabled && this.GFXHistoryIndex == 0)
                    this.btnUndo.Enabled = false;
                if (!btnRedo.Enabled)
                    btnRedo.Enabled = true;
            }
        }
        private void GFXRedo()
        {
            if (this.GFXHistoryIndex < this.GFXPrev.Count)
            {
                int h = this.GFXPrev.Count - this.GFXHistoryIndex++ - 1;
                if (!this.GFXNext[h].IsPage)
                {
                    int x = this.GFXNext[h].X;
                    int y = this.GFXNext[h].Y;
                    this.GFX.SetTile(this.GFXNext[h].Page, y, x, ref this.GFXNext[h].Tile);
                }
                else
                    this.GFX.NewPage(0, this.GFXNext[h].Page, ref this.GFXNext[h].GFX);

                RedrawGFX(true);

                //Disable Save button if at last save. Otherwise, enable.
                if (btnSaveAll.Enabled && this.GFXHistoryIndex == this.SaveIndex)
                    btnSaveAll.Enabled = false;
                else if (!btnSaveAll.Enabled && this.GFXHistoryIndex != this.SaveIndex)
                    btnSaveAll.Enabled = true;

                //Disable Redo when at last change.
                if (btnRedo.Enabled && this.GFXHistoryIndex == GFXPrev.Count)
                    btnRedo.Enabled = false;
                if (!btnUndo.Enabled)
                    btnUndo.Enabled = true;
            }
        }
        private void CheckPastTileData()
        {

            if (this.TileHistoryIndex < this.TilePrev.Count)
            {
                this.TilePrev.RemoveRange(this.TileHistoryIndex, this.TilePrev.Count - this.TileHistoryIndex);
                this.TileNext = new List<TileSaveData>();
            }

            this.TileHistoryIndex++;

            if (!btnTileUndo.Enabled)
                this.btnTileUndo.Enabled = true;
            if (btnTileRedo.Enabled)
                this.btnTileRedo.Enabled = false;
        }
        private void SetTileUndoHistory(int x, int y)
        {
            CheckPastTileData();
            this.TilePrev.Add(new TileSaveData(x, y, this.Tile.Data[y, x]));
        }
        private void SetTileUndoHistory(RotateFlipType rf)
        {
            CheckPastTileData();
            this.TilePrev.Add(new TileSaveData(rf));
        }
        private void SetTileRedoHistory()
        {
            int h = this.TileHistoryIndex;
            if (this.TilePrev.Count - h - 1 == this.TileNext.Count)
            {
                RotateFlipType rf = this.TilePrev[h].RotateFlip;
                if (rf == RotateFlipType.RotateNoneFlipNone)
                {
                    int x = this.TilePrev[h].X;
                    int y = this.TilePrev[h].Y;
                    this.TileNext.Add(new TileSaveData(x, y, (byte)this.Tile.Data[y, x]));
                }
                else
                    this.TileNext.Add(new TileSaveData(rf));
            }
        }
        private void TileUndo()
        {
            if (this.TileHistoryIndex > 0)
            {
                this.TileHistoryIndex--;
                SetTileRedoHistory();

                int h = this.TileHistoryIndex;
                switch (this.TilePrev[h].RotateFlip)
                {
                    case RotateFlipType.RotateNoneFlipNone:
                    {
                        int x = this.TilePrev[h].X;
                        int y = this.TilePrev[h].Y;
                        this.Tile.Data[y, x] = this.TilePrev[h].Color;
                        break;
                    }
                    case RotateFlipType.RotateNoneFlipX:
                        this.Tile.FlipX();
                        break;
                    case RotateFlipType.RotateNoneFlipY:
                        this.Tile.FlipY();
                        break;
                    case RotateFlipType.Rotate90FlipNone:
                        this.Tile.Rotate270();
                        break;
                }

                RedrawTile();

                if (btnTileUndo.Enabled && this.TileHistoryIndex == 0)
                    this.btnTileUndo.Enabled = false;
                if (!btnTileRedo.Enabled)
                    btnTileRedo.Enabled = true;
            }
        }
        private void TileRedo()
        {
            if (this.TileHistoryIndex < this.TilePrev.Count)
            {
                int h = this.TilePrev.Count - this.TileHistoryIndex++ - 1;

                switch (this.TileNext[h].RotateFlip)
                {
                    case RotateFlipType.RotateNoneFlipNone:
                        {
                            int x = this.TileNext[h].X;
                            int y = this.TileNext[h].Y;
                            this.Tile.Data[y, x] = this.TileNext[h].Color;
                            break;
                        }
                    case RotateFlipType.RotateNoneFlipX:
                        this.Tile.FlipX();
                        break;
                    case RotateFlipType.RotateNoneFlipY:
                        this.Tile.FlipY();
                        break;
                    case RotateFlipType.Rotate90FlipNone:
                        this.Tile.Rotate90();
                        break;
                }

                RedrawTile();

                if (btnTileRedo.Enabled && this.TileHistoryIndex == TilePrev.Count)
                    btnTileRedo.Enabled = false;
                if (!btnTileUndo.Enabled)
                    btnTileUndo.Enabled = true;
            }
        }
        /// <summary>
        /// Controls the logic for redrawing the GFXEditor.
        /// </summary>
        /// <param name="branch">
        /// Will draw other controls beyond the GFX Editor.
        /// </param>
        private void RedrawGFX(bool branch)
        {
            drwGFX.Height = (Page == 4 ? GFXEditor.GFXHeight : (GFXEditor.GFXHeight << 1));
            drwGFX.Invalidate();
            if (branch)
                Map16Editor.RedrawMap16();
        }
        /// <summary>
        /// Controls the logic for redrawing the Palette.
        /// </summary>
        private void RedrawPalette()
        {
            drwPalette.Invalidate();
        }
        /// <summary>
        /// Controls the logic for redrawing the EditTile.
        /// </summary>
        private void RedrawTile()
        {
            drwTile.Invalidate();
        }
        /// <summary>
        /// Controls the logic for redrawing all EditColors.
        /// </summary>
        private void RedrawEditColors()
        {
            drwColor1.Invalidate();
            drwColor2.Invalidate();
            drwColor3.Invalidate();
            drwColor4.Invalidate();
            drwColor5.Invalidate();
            drwColor6.Invalidate();
        }
        /// <summary>
        /// Goes to the next page in the GFXEditor.
        /// </summary>
        private void NextPage()
        {
            if (Page < 13)
            {
                Page += Page == 4 ? 1 : 2;
                if (!btnPrevPage.Enabled)
                    this.btnPrevPage.Enabled = true;
            }
            else
                this.btnNextPage.Enabled = false;
        }
        /// <summary>
        /// Goes to the previous page in the GFXEditor.
        /// </summary>
        private void PreviousPage()
        {
            if (Page > 0)
            {
                Page -= Page == 5 ? 1 : 2;
                if (!btnNextPage.Enabled)
                    this.btnNextPage.Enabled = true;
            }
            else
                this.btnPrevPage.Enabled = false;
        }
        /// <summary>
        /// Uses the next row of Palette colors.
        /// </summary>
        private void NextPalette()
        {
            if (PaletteIndex < Palette.Rows - 1)
            {
                PaletteIndex++;
                if (!btnPalUp.Enabled)
                    this.btnPalUp.Enabled = true;
            }
            else
                this.btnPalDn.Enabled = false;
        }
        /// <summary>
        /// Uses the previous row of Palette colors.
        /// </summary>
        private void PreviousPalette()
        {
            if (PaletteIndex > 0)
            {
                PaletteIndex--;
                if (!btnPalDn.Enabled)
                    this.btnPalDn.Enabled = true;
            }
            else
                this.btnPalUp.Enabled = false;
        }
        /// <summary>
        /// Sets the index of the palette color to the specified EditColor index.
        /// </summary>
        /// <param name="index">
        /// Specifies which EditColor to assign to.
        /// </param>
        /// <param name="pIndex">
        /// Specifies which Palette index to assign.
        /// </param>
        private void SetEditColor(int index, int pIndex)
        {
            //Set the color to the appropriate index.
            this.EditColorIndexes[index] = pIndex;

            //Array creation to easily switch between the draw colors.
            DrawControl[] c = { drwColor1, drwColor2, drwColor3, drwColor4, drwColor5, drwColor6 };
            Label[] l = { lblColor1, lblColor2, lblColor3, lblColor4, lblColor5, lblColor6 };
            if (!c[index].Enabled)
                c[index].Enabled = 
                c[index].Visible = 
                l[index].Enabled = 
                l[index].Visible = true;
            switch (index)
            {
                case 0:
                    drwColor1.Invalidate();
                    break;
                case 1:
                    drwColor2.Invalidate();
                    break;
                case 2:
                    drwColor3.Invalidate();
                    break;
                case 3:
                    drwColor4.Invalidate();
                    break;
                case 4:
                    drwColor5.Invalidate();
                    break;
                case 5:
                    drwColor6.Invalidate();
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="active"></param>
        private void DrawGFXEditor(ref Graphics g, int x, int y, bool active)
        {
            fixed (uint* p = this.Palette.Data)
            {
                g.DrawImage(GFX.DrawGFXPage(this.Page, p, this.PaletteIndex, GFXEditor.GFXZoom), Point.Empty);
                if (Page != 4)
                    g.DrawImage(GFX.DrawGFXPage(this.Page + 1, p, this.PaletteIndex, GFXEditor.GFXZoom), new Point(0, GFXEditor.GFXHeight));
            }

            if (active)
            {
                Pen p = new Pen(Color.Black, 1);
                p.DashStyle = DashStyle.Dash;
                g.DrawRectangle(p, new Rectangle(x, y, GFXEditor.TileWidth - 1, GFXEditor.TileHeight - 1));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="active"></param>
        private void DrawEditTile(Graphics g, int x, int y, bool active)
        {
            //I make the full call to the Palette editor to bypass errors I get in Visual Studio.
            //I don't like it, but using ref in a draw function is preferable.
            g.DrawImageUnscaled(this.Tile.DrawTile(ref this.Parent.PaletteEditor.Palette, this.PaletteIndex, GFXEditor.EditTileZoom), Point.Empty);

            if (active)
            {
                Pen p = new Pen(Color.Black, 1);
                p.DashStyle = DashStyle.Dash;
                g.DrawRectangle(p, new Rectangle(x * GFXEditor.EditTileZoom, y * GFXEditor.EditTileZoom, GFXEditor.EditTileZoom - 1, GFXEditor.EditTileZoom - 1));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="active"></param>
        private void DrawPalette(Graphics g, int x, bool active)
        {
            g.DrawImageUnscaled(Palette.DrawPaletteRow(this.PaletteIndex), Point.Empty);

            if (active)
            {
                uint pc = this.Palette.Data[this.PaletteIndex, x] | 0xFF000000;
                ExpandedColor c = Palette.PCToSystemColor(pc);
                Pen p = new Pen(c.GetMaximumContrast(), 1);
                p.DashStyle = DashStyle.Dash;
                g.DrawRectangle(p, new Rectangle(x * Palette.ColorWidth, 0, Palette.ColorWidth - 1, Palette.ColorHeight - 1));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetCoordinates(int x, int y)
        {
            if (x == -1)
                x = GFX.Columns - 1;
            else if (x == GFX.Columns)
                x = 0;

            int yMax = this.Page == 4 ? GFX.Rows : (GFX.Rows << 1);
            if (y == -1)
                y = yMax - 1;
            else if (y == yMax)
                y = 0;

            if (x != -2)
                this.X = x;
            if (y != -2)
                this.Y = y;

            RedrawGFX(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CopyTile(int page, int x, int y)
        {
            if (!drwTile.Visible)
                drwTile.Visible = drwTile.Enabled = true;

            int row = y & 7;
            page += ((y & 8) > 0 ? 1 : 0);
            Tile = new GFX.Tile(ref this.GFX, page, row, x);
            RedrawTile();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void PasteTile(int page, int x, int y)
        {
            if (drwTile.Visible)
            {
                int row = y & 7;
                page += ((y & 8) > 0 ? 1 : 0);
                SetGFXUndoHistory(page, row, x);
                this.GFX.SetTile(page, row, x, ref this.Tile);
                RedrawGFX(true);
            }
        }
        private void EditTile(int x, int y, int index)
        {
            SetTileUndoHistory(x, y);
            this.Tile.Data[y, x] = (byte)this.EditColorIndexes[index];
        }
        private void FlipY()
        {
            SetTileUndoHistory(RotateFlipType.RotateNoneFlipY);
            this.Tile.FlipY();
            RedrawTile();
        }
        private void FlipX()
        {
            SetTileUndoHistory(RotateFlipType.RotateNoneFlipX);
            this.Tile.FlipX();
            RedrawTile();
        }
        private void Rotate90()
        {
            SetTileUndoHistory(RotateFlipType.Rotate90FlipNone);
            this.Tile.Rotate90();
            RedrawTile();
        }
        private void SaveAll()
        {
            if (btnSaveAll.Enabled)
            {
                int c = CurrentIndexes.Length;
                byte[] data = new byte[(GFX.ArraySize * (int)this.GFX.GraphicsFormat) >> 3];
                int l = data.Length;
                for (int i = 0; i < c; i++)
                {
                    StringBuilder sb = new StringBuilder(this.BaseDirectory);
                    sb.Append("\\Default\\GFX");
                    sb.Append(CurrentIndexes[i].ToString("X2"));
                    sb.Append(".lz2");
                    string path = sb.ToString();

                    fixed (byte* dest = data)
                    fixed (byte* src = &this.GFX.Data[i, 0, 0, 0, 0])
                        LunarCompress.CreateBPPMap(src, dest, GFX.Tiles, this.GFX.GraphicsFormat);
                    File.WriteAllBytes(path, LunarCompress.Recompress(CompressionFormats.LZ2, 0x10000, ref data));
                }

                this.SaveIndex = this.GFXHistoryIndex;
                this.btnSaveAll.Enabled = false;
            }
        }
        #endregion

        #region Events
        private void drwGFX_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGFXEditor(ref g, this.X * GFXEditor.TileWidth, this.Y * GFXEditor.TileHeight, this.ActiveControl == (Control)sender);
        }

        private void GFXEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.PageUp:
                        PreviousPage();
                        break;
                    case Keys.PageDown:
                        NextPage();
                        break;
                    case Keys.Home:
                        PreviousPalette();
                        break;
                    case Keys.End:
                        NextPalette();
                        break;
                }
            }
        }

        private void drwGFX_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseX != e.X || this.MouseY != e.Y)
            {
                this.MouseX = e.X;
                this.MouseY = e.Y;

                if (EditorForm.IsInBounds(e.Location, drwGFX.ClientRectangle))
                {
                    this.ActiveControl = (Control)sender;
                    SetCoordinates(e.X / GFXEditor.TileWidth, e.Y / GFXEditor.TileHeight);
                }
            }
        }

        private void drwGFX_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    CopyTile(this.Page, this.X, this.Y);
                    break;
                case MouseButtons.Right:
                    PasteTile(this.Page, this.X, this.Y);
                    break;
            }
        }

        private void drwPalette_Paint(object sender, PaintEventArgs e)
        {
            DrawPalette(e.Graphics, this.PaletteX, this.ActiveControl == (Control)sender);
        }

        private void drwTile_Paint(object sender, PaintEventArgs e)
        {
            DrawEditTile(e.Graphics, this.EditX, this.EditY, this.ActiveControl == (Control)sender);
        }

        private void drwPalette_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInBounds(e.Location, drwPalette.ClientRectangle))
            {
                int set = -1;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        set = 0;
                        break;
                    case MouseButtons.Right:
                        set = 1;
                        break;
                    case MouseButtons.Middle:
                        set = 2;
                        break;
                    case MouseButtons.XButton1:
                        set = 3;
                        break;
                    case MouseButtons.XButton2:
                        set = 4;
                        break;
                    default:
                        break;
                }
                if (set != -1)
                    SetEditColor(set, this.PaletteX);
            }
        }

        private void drwColor_Paint(object sender, PaintEventArgs e)
        {
            int index = (int)((Control)sender).Tag;
            uint color = this.Palette.Data[this.PaletteIndex, this.EditColorIndexes[index]] | 0xFF000000;
            Graphics g = e.Graphics;
            g.DrawImage(Palette.DrawSquare(color), Point.Empty);
        }

        private void drwTile_MouseEdit(object sender, MouseEventArgs e)
        {
            if (IsInBounds(e.Location, drwTile.ClientRectangle))
            {
                int x = e.X / GFXEditor.EditTileZoom;
                int y = e.Y / GFXEditor.EditTileZoom;

                this.ActiveControl = (Control)sender;
                int index = -1;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        index = 0;
                        break;
                    case MouseButtons.Right:
                        index = 1;
                        break;
                    case MouseButtons.Middle:
                        if (drwColor3.Visible)
                            index = 2;
                        break;
                    case MouseButtons.XButton1:
                        if (drwColor4.Visible)
                            index = 3;
                        break;
                    case MouseButtons.XButton2:
                        if (drwColor5.Visible)
                            index = 4;
                        break;
                }

                if (index != -1)
                    EditTile(this.EditX, this.EditY, index);
                RedrawTile();
            }
        }

        private void btnFlipY_Click(object sender, EventArgs e)
        {
            FlipY();
        }

        private void btnFlipX_Click(object sender, EventArgs e)
        {
            FlipX();
        }

        private void btnRotate90_Click(object sender, EventArgs e)
        {
            Rotate90();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void btnPalDn_Click(object sender, EventArgs e)
        {
            NextPalette();
        }

        private void btnPalUp_Click(object sender, EventArgs e)
        {
            PreviousPalette();
        }

        private void drwPalette_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsInBounds(e.Location, drwPalette.ClientRectangle))
            {
                this.ActiveControl = (Control)sender;
                this.PaletteX = e.X >> 4;
            }
        }

        private void drwControl_Refresh(object sender, EventArgs e)
        {
            ((Control)sender).Invalidate();
        }

        private void drwGFX_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        SetCoordinates(this.X, this.Y - 1);
                        break;
                    case Keys.Down:
                        SetCoordinates(this.X, this.Y + 1);
                        break;
                    case Keys.Left:
                        SetCoordinates(this.X - 1, this.Y);
                        break;
                    case Keys.Right:
                        SetCoordinates(this.X + 1, this.Y);
                        break;
                }
            }
            else if (e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        CopyTile(this.Page, this.X, this.Y);
                        break;
                    case Keys.V:
                        PasteTile(this.Page, this.X, this.Y);
                        break;
                }
            }
        }

        private void drwTile_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    if (drwColor1.Enabled)
                        EditNumDown = 0;
                    break;
                case Keys.D2:
                    if (drwColor2.Enabled)
                        EditNumDown = 1;
                    break;
                case Keys.D3:
                    if (drwColor3.Enabled)
                        EditNumDown = 2;
                    break;
                case Keys.D4:
                    if (drwColor4.Enabled)
                        EditNumDown = 3;
                    break;
                case Keys.D5:
                    if (drwColor5.Enabled)
                        EditNumDown = 4;
                    break;
                case Keys.D6:
                    if (drwColor6.Enabled)
                        EditNumDown = 5;
                    break;
                case Keys.Up:
                    this.EditY--;
                    break;
                case Keys.Down:
                    this.EditY++;
                    break;
                case Keys.Left:
                    this.EditX--;
                    break;
                case Keys.Right:
                    this.EditX++;
                    break;
            }

            if (this.EditX >= GFX.TileWidth)
                this.EditX = 0;
            else if (this.EditX < 0)
                this.EditX = GFX.TileWidth - 1;

            if (this.EditY >= GFX.TileHeight)
                this.EditY = 0;
            else if (this.EditY < 0)
                this.EditY = GFX.TileHeight - 1;

            if (EditNumDown != -1)
                EditTile(this.EditX, this.EditY, EditNumDown);
            RedrawTile();
        }

        private void drwPalette_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    EditNumDown = 0;
                    break;
                case Keys.D2:
                    EditNumDown = 1;
                    break;
                case Keys.D3:
                    EditNumDown = 2;
                    break;
                case Keys.D4:
                    EditNumDown = 3;
                    break;
                case Keys.D5:
                    EditNumDown = 4;
                    break;
                case Keys.D6:
                    EditNumDown = 5;
                    break;
                case Keys.Left:
                    this._paletteX--;
                    break;
                case Keys.Right:
                    this._paletteX++;
                    break;
            }

            if (this._paletteX < 0)
                this._paletteX = Palette.Columns - 1;
            else if (this._paletteX >= Palette.Columns)
                this._paletteX = 0;

            if (this.EditNumDown != -1)
                SetEditColor(this.EditNumDown, this.PaletteX);

            RedrawPalette();
        }

        private void drwControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                    EditNumDown = -1;
                    break;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            GFXUndo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            GFXRedo();
        }

        private void btnTileUndo_Click(object sender, EventArgs e)
        {
            TileUndo();
        }

        private void btnTileRedo_Click(object sender, EventArgs e)
        {
            TileRedo();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        #endregion

        private class GFXSaveData
        {
            public int Page;
            public int X;
            public int Y;
            public bool IsPage;
            public GFX.Tile Tile;
            public GFX GFX;

            public GFXSaveData(int page, int x, int y, ref GFX gfx)
            {
                this.Page = page;
                this.X = x;
                this.Y = y;
                this.Tile = new GFX.Tile(ref gfx, page, y, x);
                this.IsPage = false;
            }

            public GFXSaveData(int page, ref GFX gfx)
            {
                this.Page = page;
                this.GFX = new GFX(1, gfx.GraphicsFormat);
                this.GFX.NewPage(page, 0, ref gfx);
            }
        }

        private class TileSaveData
        {
            public int X;
            public int Y;
            public RotateFlipType RotateFlip;
            public byte Color;

            public TileSaveData(int x, int y, byte color)
            {
                this.X = x;
                this.Y = y;
                this.Color = color;
                this.RotateFlip = RotateFlipType.RotateNoneFlipNone;
            }

            public TileSaveData(RotateFlipType rf)
            {
                this.RotateFlip = rf;
            }
        }        
    }
}