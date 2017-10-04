using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using LC_Sharp;
using SNES;

namespace MushROMs.SMB1
{
    public unsafe partial class SMB1Editor : EditorForm
    {
        #region strings
        private new const string WindowTitle = EditorForm.WindowTitle + " - Super Mario Bros. 1";
        private const string NowEditing = "Now editing level ";
        private const string Base = "\\SMB1";
        private const string Map16Base = "\\Maps\\Default\\Map25.bin";

        private const string Menu_File = "&File";
        private const string Menu_New = "&New";
        private const string Menu_Open = "&Open";
        private const string Menu_Save = "&Save";
        private const string Menu_SaveAs = "Save &As";
        private const string Menu_Exit = "E&xit";
        private const string Menu_Edit = "&Edit";
        private const string Menu_Undo = "&Undo";
        private const string Menu_Redo = "&Redo";
        private const string Menu_Cut = "Cu&t";
        private const string Menu_Copy = "&Copy";
        private const string Menu_Paste = "&Paste";
        private const string Menu_Delete = "&Delete";
        private const string Menu_SelectAll = "Select &All";
        private const string Menu_Editors = "E&ditors";
        private const string Menu_Palette = "&Palette Editor";
        private const string Menu_GFX = "&GFX Editor";
        private const string Menu_Map16 = "&Map16 Editor";
        private const string Menu_Tools = "&Tools";
        private const string Menu_Options = "&Options";
        private const string Menu_Customize = "&Customize";
        private const string Menu_Help = "&Help";
        private const string Menu_Contents = "&Contents";
        private const string Menu_Index = "&Index";
        private const string Menu_Search = "&Search";
        private const string Menu_About = "&About";

        private const string ToolTip_New = "Creates a new SMB1 project.";
        private const string ToolTip_Open = "Opens an SMB1 project for editing.";
        private const string ToolTip_Save = "Saves the current SMB1 project.";
        private const string ToolTip_SaveAs = "Saves the current SMB1 project as a new project.";
        private const string ToolTip_Exit = "Exits the application and prompts for save.";
        private const string ToolTip_Undo = "Undoes the last operation performed in the level editor.";
        private const string ToolTip_Redo = "Redoes the next operation to be performed in the level editor.";
        private const string ToolTip_Cut = "Cuts the visible tiles in the selected region.";
        private const string ToolTip_Copy = "Copies the visible tiles in the selected region.";
        private const string ToolTip_Paste = "Pastes the tile selection of the clipboard to the level editor.";
        private const string ToolTip_Delete = "Deletes the visible tiles in the selected region.";
        private const string ToolTip_SelectAll = "Selects all visible tiles in the level editor.";
        private const string ToolTip_PaletteEditor = "Toggles the Palette Editor.";
        private const string ToolTip_GFXEditor = "Toggles the GFX Editor.";
        private const string ToolTip_Map16Editor = "Toggles the Map16 Editor.";
        private const string ToolTip_Customize = "";
        private const string ToolTip_Options = "Modify various settings of the SMB1 Editor.";
        private const string ToolTip_Contents = "Opens the SMB1 Editor Help Menu.";
        private const string ToolTip_Index = "";
        private const string ToolTip_Search = "";
        private const string ToolTip_About = "Learn about MushROMs.";

        private const string Status_Ready = "Editor ready";
        private const string Status_New = "Successfully created ";
        private const string Status_Open = "Successfully opened ";
        private const string Status_Save = "Successfully saved ";
        private const string Status_SaveAs = "Successfully saved as ";
        private const string Status_PaletteOpen = "Palette editor opened.";
        private const string Status_PaletteClosed = "Palette editor closed.";
        private const string Status_GFXOpen = "GFX Editor opened.";
        private const string Status_GFXClosed = "GFX Editor closed.";
        private const string Status_Map16Open = "Map16 Editor opened.";
        private const string Status_Map16Closed = "Map16 Editor closed.";
        private const string Status_ObjectOpen = "Object window opened.";
        private const string Status_ObjectClosed = "Object window closed.";
        #endregion

        /// <summary>
        /// Maximum allowed number of levels for the SMB1Editor.
        /// </summary>
        public const int MaxLevels = 0x100;

        #region Editors
        protected new SMASEditor Parent;
        public PaletteEditor PaletteEditor;
        public GFXEditor GFXEditor;
        public Map16Editor Map16Editor;
        public ObjectSelector ObjectSelector;
        #endregion

        #region Editor variables
        private int _level;
        private int _pages;
        private Layer1 Map;

        public int Level
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = value & 0xFF;      //Might need to be changeable, but we should be good for now.
                this.PaletteEditor.Reset(false);
                this.PaletteEditor.RedrawPalette(false);
                this.GFXEditor.Reset(false);
                this.GFXEditor.RedrawForm(false);
                this.RedrawLevelEditor();
                this.tssMain.Text = SMB1Editor.NowEditing + value.ToString("X");
            }
        }

        private int Pages
        {
            get
            {
                return this._pages;
            }
            set
            {
                this._pages = value;
            }
        }

        public Palette Palette
        {
            get
            {
                return this.PaletteEditor.Palette;
            }
        }

        public GFX GFX
        {
            get
            {
                return this.GFXEditor.GFX;
            }
        }

        public Map16 Map16
        {
            get
            {
                return this.Map16Editor.Map16;
            }
        }

        private Color _bgColor;
        private Color BGColor
        {
            get
            {
                return this._bgColor;
            }
            set
            {
                this._bgColor = value;
                RedrawLevelEditor();
            }
        }
        #endregion

        #region SubEditor variables
        /// <summary>
        /// Gets or sets the visibility of the Palette Editor window.
        /// </summary>
        public bool PaletteEditorVisible
        {
            get
            {
                return this.PaletteEditor.Visible;
            }
            set
            {
                this.PaletteEditor.Visible = value;
                this.tsmPaletteEditor.Checked = value;
                this.tsbPaletteEditor.Checked = value;
                
                if (value)
                    this.tssMain.Text = SMB1Editor.Status_PaletteOpen;
                else
                    this.tssMain.Text = SMB1Editor.Status_PaletteClosed;
            }
        }

        /// <summary>
        /// Gets or sets visibility of the GFX Editor window.
        /// </summary>
        public bool GFXEditorVisible
        {
            get
            {
                return this.GFXEditor.Visible;
            }
            set
            {
                this.GFXEditor.Visible = value;
                this.tsmGFXEditor.Checked = value;
                this.tsbGFXEditor.Checked = value;

                if (value)
                    this.tssMain.Text = SMB1Editor.Status_GFXOpen;
                else
                    this.tssMain.Text = SMB1Editor.Status_GFXClosed;
            }
        }

        /// <summary>
        /// Gets or sets visibility of the Map16 Editor window.
        /// </summary>
        public bool Map16EditorVisible
        {
            get
            {
                return this.Map16Editor.Visible;
            }
            set
            {
                this.Map16Editor.Visible = value;
                this.tsmMap16Editor.Checked = value;
                this.tsbMap16Editor.Checked = value;

                if (value)
                    this.tssMain.Text = SMB1Editor.Status_Map16Open;
                else
                    this.tssMain.Text = SMB1Editor.Status_Map16Closed;
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the Object Selector window.
        /// </summary>
        public bool ObjectSelectorVisible
        {
            get
            {
                return this.ObjectSelector.Visible;
            }
            set
            {
                this.ObjectSelector.Visible = value;
                this.tsbObjectSelector.Checked = value;

                if (value)
                    this.tssMain.Text = SMB1Editor.Status_ObjectOpen;
                else
                    this.tssMain.Text = SMB1Editor.Status_ObjectClosed;
            }
        }
        #endregion

        #region Constructors
        public SMB1Editor(IntPtr hwnd)
        {
            this.Parent = (SMASEditor)Control.FromHandle(hwnd);
            this.Location = Parent.Location;
            InitializeForm();
        }

        public SMB1Editor(string[] args)
        {
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeComponent();

            #region const string assignment
            this.Text = SMB1Editor.WindowTitle;
            this.tsmFile.Text = SMB1Editor.Menu_File;
            this.tsmNew.Text = SMB1Editor.Menu_New;
            this.tsmOpen.Text = SMB1Editor.Menu_Open;
            this.tsmSave.Text = SMB1Editor.Menu_Save;
            this.tsmSaveAs.Text = SMB1Editor.Menu_SaveAs;
            this.tsmExit.Text = SMB1Editor.Menu_Exit;
            this.tsmEdit.Text = SMB1Editor.Menu_Edit;
            this.tsmUndo.Text = SMB1Editor.Menu_Undo;
            this.tsmRedo.Text = SMB1Editor.Menu_Redo;
            this.tsmCut.Text = SMB1Editor.Menu_Cut;
            this.tsmCopy.Text = SMB1Editor.Menu_Copy;
            this.tsmPaste.Text = SMB1Editor.Menu_Paste;
            this.tsmSelectAll.Text = SMB1Editor.Menu_SelectAll;
            this.tsmEditors.Text = SMB1Editor.Menu_Editors;
            this.tsmPaletteEditor.Text = SMB1Editor.Menu_Palette;
            this.tsmGFXEditor.Text = SMB1Editor.Menu_GFX;
            this.tsmMap16Editor.Text = SMB1Editor.Menu_Map16;
            this.tsmTools.Text = SMB1Editor.Menu_Tools;
            this.tsmOptions.Text = SMB1Editor.Menu_Options;
            this.tsmCustomize.Text = SMB1Editor.Menu_Customize;
            this.tsmHelp.Text = SMB1Editor.Menu_Help;
            this.tsmContents.Text = SMB1Editor.Menu_Contents;
            this.tsmIndex.Text = SMB1Editor.Menu_Index;
            this.tsmSearch.Text = SMB1Editor.Menu_Search;
            this.tsmAbout.Text = SMB1Editor.Menu_About;
            this.tsmNew.ToolTipText =
            this.tsbNew.ToolTipText =SMB1Editor.ToolTip_New;
            this.tsmOpen.ToolTipText = 
            this.tsbOpen.ToolTipText =SMB1Editor.ToolTip_Open;
            this.tsmSave.ToolTipText =
            this.tsbSave.ToolTipText = SMB1Editor.ToolTip_Save;
            this.tsmSaveAs.ToolTipText =SMB1Editor.ToolTip_SaveAs;
            this.tsmExit.ToolTipText = SMB1Editor.ToolTip_Exit;
            this.tsmUndo.ToolTipText = SMB1Editor.ToolTip_Undo;
            this.tsmRedo.ToolTipText = SMB1Editor.ToolTip_Redo;
            this.tsmCut.ToolTipText =
            this.tsbCut.ToolTipText = SMB1Editor.ToolTip_Cut;
            this.tsmCopy.ToolTipText =
            this.tsbCopy.ToolTipText = SMB1Editor.ToolTip_Copy;
            this.tsmPaste.ToolTipText =
            this.tsbPaste.ToolTipText = SMB1Editor.ToolTip_Paste;
            this.tsmDelete.ToolTipText = SMB1Editor.ToolTip_Delete;
            this.tsmSelectAll.ToolTipText = SMB1Editor.ToolTip_SelectAll;
            this.tsmPaletteEditor.ToolTipText =
            this.tsbPaletteEditor.ToolTipText = SMB1Editor.ToolTip_PaletteEditor;
            this.tsmGFXEditor.ToolTipText =
            this.tsbGFXEditor.ToolTipText = SMB1Editor.ToolTip_GFXEditor;
            this.tsmMap16Editor.ToolTipText =
            this.tsbMap16Editor.ToolTipText = SMB1Editor.ToolTip_Map16Editor;
            this.tsmCustomize.ToolTipText = SMB1Editor.ToolTip_Customize;
            this.tsmOptions.ToolTipText = SMB1Editor.ToolTip_Options;
            this.tsmContents.ToolTipText =
            this.tsbHelp.ToolTipText = SMB1Editor.ToolTip_Contents;
            this.tsmIndex.ToolTipText = SMB1Editor.ToolTip_Index;
            this.tsmSearch.ToolTipText = SMB1Editor.ToolTip_Search;
            this.tsmAbout.ToolTipText = SMB1Editor.ToolTip_About;
            #endregion

            this.BaseDirectory = @"C:\Users\nrgarcia\Dropbox\Private\Emulation\Dev\Super Mario All-Stars\Project" + SMB1Editor.Base;     //change this when we work with actual files.

            this.PaletteEditor = new PaletteEditor(this.Handle);
            this.GFXEditor = new GFXEditor(this.Handle);
            this.Map16Editor = new Map16Editor(this.Handle);
            this.ObjectSelector = new ObjectSelector(this.Handle);

            this.AddOwnedForm(this.PaletteEditor);
            this.AddOwnedForm(this.GFXEditor);
            this.AddOwnedForm(this.Map16Editor);
            this.AddOwnedForm(this.ObjectSelector);

            InitializeMaps();
            this._level = 0;
            this._pages = 0x20;
            this._bgColor = Color.FromArgb(120, 176, 200);
            RedrawLevelEditor();
            
            this.tssMain.Text = SMB1Editor.Status_Ready;
        }
        #endregion

        #region Functions
        private void InitializeMaps()
        {
            string path = this.BaseDirectory + SMB1Editor.Map16Base;
            if (File.Exists(path))
                this.Map = new Layer1(File.ReadAllBytes(path));
            else
                this.Map = new Layer1();
        }

        public void RedrawLevelEditor()
        {
            this.drwLevel.Invalidate();
        }

        private void DrawLevelEditor(Graphics g)
        {
            long a = DateTime.Now.Ticks;
            int width = this.Width;
            int x = this.hsbLevel.Value * Map16.TileWidth;

            uint[,] data = new uint[width, Layer1.ImageHeight];
            fixed (uint* palette = this.Palette.Data)
            fixed (byte* gfx = this.GFX.Data)
            fixed (Map8* tiles = this.Map16.Data)
            fixed (uint* scan0 = data)
            {
                Map.Map.DrawMap(scan0, width, Layer1.ImageHeight, -x, 0, palette, gfx, tiles, Render8x8Flags.Draw);
                g.DrawImageUnscaled(new Bitmap(width, Layer1.ImageHeight, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0)), Point.Empty);
            }

            long c = DateTime.Now.Ticks;
            lblSpeed.Text = ((c - a) / 10000).ToString() + "ms";
        }
        #endregion

        #region Events
        private void SMB1Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Parent != null)
            {
                this.PaletteEditorVisible =
                this.GFXEditorVisible =
                this.Map16EditorVisible =
                this.ObjectSelectorVisible = false;
            }
        }

        private void SMB1Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Parent != null)
                this.Parent.Show();
        }

        private void tsmPaletteEditor_Click(object sender, EventArgs e)
        {
            this.PaletteEditorVisible = this.tsmPaletteEditor.Checked;
        }

        private void tsbPalette_CheckedChanged(object sender, EventArgs e)
        {
            this.PaletteEditorVisible = this.tsbPaletteEditor.Checked;
        }

        private void tsmGFXEditor_CheckedChanged(object sender, EventArgs e)
        {
            this.GFXEditorVisible = this.tsmGFXEditor.Checked;
        }

        private void tsbGFXEditor_CheckedChanged(object sender, EventArgs e)
        {
            this.GFXEditorVisible = this.tsbGFXEditor.Checked;
        }

        private void tsmMap16Editor_Click(object sender, EventArgs e)
        {
            this.Map16EditorVisible = this.tsmMap16Editor.Checked;
        }

        private void tsbMap16Editor_Click(object sender, EventArgs e)
        {
            this.Map16EditorVisible = this.tsbMap16Editor.Checked;
        }

        private void tsbObjectSelector_Click(object sender, EventArgs e)
        {
            this.ObjectSelectorVisible = this.tsbObjectSelector.Checked;
        }

        private void SMB1Editor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    this.Level = (this.Level + 1) & 0xFF;
                    break;
                case Keys.PageDown:
                    this.Level = (this.Level - 1) & 0xFF;
                    break;
            }
        }

        private void drwLevel_Paint(object sender, PaintEventArgs e)
        {
            DrawLevelEditor(e.Graphics);
        }

        private void drwLevel_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsInBounds(e.Location, this.ClientRectangle))
            {
                RedrawLevelEditor();

                int _x = e.X / Map16.TileWidth + this.hsbLevel.Value;
                int p = _x >> 4;
                int x = _x & 0x0F;
                int y = e.Y / Map16.TileHeight;

                StringBuilder sb = new StringBuilder("{");
                sb.Append(p.ToString("X"));
                sb.Append(" : ");
                sb.Append(y.ToString("X"));
                sb.Append(", ");
                sb.Append(x.ToString("X"));
                sb.Append(" - ");
                sb.Append(this.Map.Map.Tiles[y, _x].ToString("X4"));
                sb.Append("}");
                tssMain.Text = sb.ToString();
            }
        }

        private void SMB1Editor_Resize(object sender, EventArgs e)
        {
            this.drwLevel.Width = this.Width;
            int blocks = (this.Width + 8) / Map16.TileWidth;   //Gets the number of blocks visible on screen.
            this.hsbLevel.Maximum = Layer1.MaxWidth - blocks;
            this.lblScroll.Text = this.hsbLevel.Maximum.ToString();
            this.lblValue.Text = this.hsbLevel.Value.ToString();
        }

        private void hsbLevel_Scroll(object sender, ScrollEventArgs e)
        {
            this.lblValue.Text = this.hsbLevel.Value.ToString();
            RedrawLevelEditor();
        }
        #endregion

        private void tsmBuild_Click(object sender, EventArgs e)
        {
        }
    }

    /// <summary>
    /// Specifies constants defining which player is selected.
    /// </summary>
    public enum Players
    {
        /// <summary>
        /// Small Mario is active.
        /// </summary>
        SmallMario = 1,
        /// <summary>
        /// Small Luigi is active.
        /// </summary>
        SmallLuigi = 2,
        /// <summary>
        /// Mario is active.
        /// </summary>
        Mario = 3,
        /// <summary>
        /// Luigi is active.
        /// </summary>
        Luigi = 4,
        /// <summary>
        /// Fire Mario is active.
        /// </summary>
        FireMario = 5,
        /// <summary>
        /// Fire Luigi is active.
        /// </summary>
        FireLuigi = 6
    }
}