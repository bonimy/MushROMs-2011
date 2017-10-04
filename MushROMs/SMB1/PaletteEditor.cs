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
    public unsafe partial class PaletteEditor : EditorForm
    {
        #region strings
        private const string EditorName = "Palette Editor";
        private const string GBX_Status = "Status";
        private const string GBX_Player = "Player Palette";
        private const string GBX_Actions = "Actions";
        private const string LBLPalette = "Palette";
        private const string LBL_Color = "Color";
        private const string LBL_PC = "PC Value";
        private const string LBL_SNES = "SNES Value";
        private const string RDB_Mario = "Mario";
        private const string RDB_Luigi = "Luigi";
        private const string RDB_FireMario = "Fire Mario";
        private const string RDB_FireLuigi = "Fire Luigi";
        private const string BTN_SavePalette = "Save Palette";
        private const string BTN_LoadFile = "Load From File";
        private const string BTN_SaveToFile = "Save To File";
        private const string BTN_Undo = "Undo";
        private const string BTN_Redo = "Redo";

        private const string Status_RDB_Player = "Page Up/Page Down: Cycle Player Palettes in the editor.";
        private const string Status_BTN_Save = "Ctrl + S: Save the Palette to its standard file.";
        private const string Status_BTN_SaveAS = "Ctrl + Shift + S: Save the Palette to a new file.";
        private const string Status_BTN_Open = "Ctrl + O: Open a new Palette from a file.";
        private const string Status_BTN_Undo = "Ctrl + Z: Undo last action in the Palette editor.";
        private const string Status_BTN_Redo = "Ctrl + Y: Redo last action in the palette editor.";
        private const string Status_DRW_Color = "Ctrl + Shift + E: Edit clipboard color.";
        private const string Status_DRWPalette1 = "Hold Ctrl, then click to a cell to make a gradient.";

        private const string Base = "\\Palette";
        private const string DefaultFolder = "\\Default\\Palette";
        private const string DefaultExt = "bin";
        private const string PlayerFile = "\\Default\\Player.bin";
        private const string PaletteFilter = "Binary (BIN)|*.bin|Tile Layer Pro Palette|*.tpl|Palette File|*.pal|Lunar Magic Palette|*.mwl|All files|*.*";
        private const string InitialDirectory = "SMB1\\Palette\\Default";
        private const string OpenText = "Open Palette file.";
        private const string SaveText = "Save Palette file.";
        private const string Previewing = "Previewing ";
        private const string BadFileSize = "Bad file size.";
        private const string DragError = "You can only drag one file.";
        private const string NewPaletteLoaded = "New Palette loaded.";
        #endregion

        #region Constants
        /// <summary>
        /// The number of Player Palettes available.
        /// </summary>
        public const int NumPlayers = 4;
        /// <summary>
        /// The number of cells for all Player Palettes.
        /// </summary>
        private const int PlayerData = PaletteEditor.NumPlayers * Palette.Columns;
        #endregion

        #region Variables
        /// <summary>
        /// The SMB1Editor that owns this PaletteEditor.
        /// </summary>
        private new SMB1Editor Parent;
        /// <summary>
        /// Gets the GFXEditor owned by the Parent.
        /// </summary>
        private GFXEditor GFXEditor
        {
            get
            {
                return this.Parent.GFXEditor;
            }
        }
        /// <summary>
        /// The current Palette of the PaletteEditor
        /// </summary>
        public Palette Palette;
        /// <summary>
        /// An array representing the possible Player Palettes.
        /// </summary>
        private uint[,] PlayerPalettes;     //The use of an array like this concerns me and I contemplate modifying the Palette class.
        /// <summary>
        /// A copy of the initial Player Palettes (used for reloading the editor).
        /// </summary>
        private uint[,] Player_copy;
        private int _playerIndex;
        /// <summary>
        /// Index used to determine which Player Palette to use.
        /// </summary>
        public int PlayerIndex
        {
            get
            {
                return this._playerIndex;
            }
            set
            {
                fixed (uint* p1 = this.PlayerPalettes)
                fixed (uint* p2 = &this.Palette.Data[Palette.Rows - 1, 0])
                {
                    //Copy current Palette data to Player Palettes (at row of last Player Index).
                    uint* dest = p1 + this._playerIndex * Palette.Rows;
                    for (int i = 0; i < Palette.Columns; i++)
                        dest[i] = p2[i];

                    //Copy current Player Palette data (at row of given Player Index) to Palette data.
                    uint* src = p1 + value * Palette.Rows;
                    for (int i = 0; i < Palette.Columns; i++)
                        p2[i] = src[i];

                    this._playerIndex = value;
                }
            }
        }
        /// <summary>
        /// Gets the current level of the SMB1Editor.
        /// </summary>
        private int Level
        {
            get
            {
                return this.Parent.Level;
            }
        }
        /// <summary>
        /// X-coordinate in the Palette Bitmap.
        /// </summary>
        private int X;
        /// <summary>
        /// Y-coordinate in the Palette Bitmap.
        /// </summary>
        private int Y;
        /// <summary>
        /// X-coordinate of the gradient start location in the Palette Bitmap.
        /// </summary>
        private int GradX;
        /// <summary>
        /// Y-coordinate of the gradient start location in the Palette Bitmap.
        /// </summary>
        private int GradY;
        /// <summary>
        /// Returns true if the Ctrl button is held in the PaletteEditor, otherwise false.
        /// </summary>
        private bool CtrlDown;
        /// <summary>
        /// Mouse X-coordinate in the Palette Bitmap.
        /// </summary>
        private int MouseX;
        /// <summary>
        /// Mouse y-coordinate in the Palette Bitmap.
        /// </summary>
        private int MouseY;
        private uint _color;
        /// <summary>
        /// Gets or sets the current color in the clipboard on the PaletteEditor.
        /// </summary>
        private uint CurrentColor
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
                RedrawColor();
            }
        }
        /// <summary>
        /// Saves the current Palette when previewing a Drag and Drop Palette.
        /// </summary>
        private Palette DragPalette;
        /// <summary>
        /// Array of Radio Buttons for the players.
        /// </summary>
        private RadioButton[] rdbPlayers;
        /// <summary>
        /// Saves the HistoryIndex of value since the last save.
        /// </summary>
        private int SaveIndex;
        /// <summary>
        /// An index for where in the History Data the user is.
        /// </summary>
        private int HistoryIndex;
        /// <summary>
        /// A list of all previous actions taken by the user (for Undo).
        /// </summary>
        private List<SaveData> Prev;
        /// <summary>
        /// A list of all future actions the user did before undoing (for Redo).
        /// </summary>
        private List<SaveData> Next;
        #endregion

        #region Functions
        /// <summary>
        /// Initializes a new instance of a PaletteEditor.
        /// </summary>
        /// <param name="parent">
        /// Handle to SMB1Editor Form.
        /// </param>
        public PaletteEditor(IntPtr parent)
        {
            InitializeComponent();
            this.rdbPlayers = new RadioButton[] { this.rdbMario, this.rdbLuigi, this.rdbFireMario, this.rdbFireLuigi };

            #region const string assignment
            this.Text = PaletteEditor.EditorName;
            this.gbxStatus.Text = PaletteEditor.GBX_Status;
            this.gbxPlayer.Text = PaletteEditor.GBX_Player;
            this.gbxActions.Text = PaletteEditor.GBX_Actions;
            this.label1.Text = PaletteEditor.LBLPalette;
            this.label4.Text = PaletteEditor.LBL_Color;
            this.label3.Text = PaletteEditor.LBL_PC;
            this.label2.Text = PaletteEditor.LBL_SNES;
            this.rdbMario.Text = PaletteEditor.RDB_Mario;
            this.rdbLuigi.Text = PaletteEditor.RDB_Luigi;
            this.rdbFireMario.Text = PaletteEditor.RDB_FireMario;
            this.rdbFireLuigi.Text = PaletteEditor.RDB_FireLuigi;
            this.btnSave.Text = PaletteEditor.BTN_SavePalette;
            this.btnSaveToFile.Text = PaletteEditor.BTN_SaveToFile;
            this.btnLoadFromFile.Text = PaletteEditor.BTN_LoadFile;
            this.btnUndo.Text = PaletteEditor.BTN_Undo;
            this.btnRedo.Text = PaletteEditor.BTN_Redo;
            
            this.btnSave.Tag = PaletteEditor.Status_BTN_Save;
            this.btnSaveToFile.Tag = PaletteEditor.Status_BTN_SaveAS;
            this.btnLoadFromFile.Tag = PaletteEditor.Status_BTN_Open;
            this.btnUndo.Tag = PaletteEditor.Status_BTN_Undo;
            this.btnRedo.Tag = PaletteEditor.Status_BTN_Redo;
            this.drwColor.Tag = PaletteEditor.Status_DRW_Color;
            this.drwPalette.Tag = PaletteEditor.Status_DRWPalette1;
            #endregion

            //The tags represent the Player Index.
            for (int i = 0; i < this.rdbPlayers.Length; i++)
                this.rdbPlayers[i].Tag = i;

            //Things to do to the PaletteEditor Form
            this.Parent = (SMB1Editor)Control.FromHandle(parent);
            this.BaseDirectory = this.Parent.BaseDirectory + PaletteEditor.Base;

            //Initializes drawing data.
            InitializePlayerPalettes();
            Reset(false);
            this.X = this.Y = 0;
            UpdateStatus(this.X, this.Y);
        }
        /// <summary>
        /// Resets all data in the Form to the last save.
        /// </summary>
        /// <param name="draw">
        /// If true, the form will redraw the Palette.
        /// </param>
        public void Reset(bool draw)
        {
            //StringBuilder used to save (method may be called several times if user is scrolling through levels)
            StringBuilder sb = new StringBuilder(this.BaseDirectory);
            sb.Append(PaletteEditor.DefaultFolder);
            sb.Append(this.Level.ToString("X2"));
            sb.Append('.');
            sb.Append(PaletteEditor.DefaultExt);
            string path = sb.ToString();

            //This only checks if a file exists. No checks for security issues come up, but it's doubtful they ever will.
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                if (data.Length >= Palette.DataSize)
                    this.Palette = new Palette(ref data);
                else
                    this.Palette = new Palette();        //If the file cannot be used, default to empty palette
            }
            else
                this.Palette = new Palette();            //If the file does not exist, default to empty palette

            //Write the Player Palette data to the game Palette.
            fixed (uint* p = &this.Palette.Data[Palette.Rows - 1, 0])
            fixed (uint* src = this.Player_copy)
            fixed (uint* dest = this.PlayerPalettes)
            {
                for (int i = 0; i < PaletteEditor.PlayerData; i++)
                    dest[i] = src[i];

                uint* p2 = src + this.PlayerIndex * Palette.Columns;
                for (int i = 0; i < Palette.Columns; i++)
                    p[i] = p2[i];
            }

            //Resetting the palette means all other graphic will be changed, so we want those redrawn too.
            if (draw)
                RedrawPalette(true);

            //Initialize Undo/Redo History
            this.SaveIndex = 0;
            this.HistoryIndex = 0;
            this.Prev = new List<SaveData>();
            this.Next = new List<SaveData>();

            //Turn off these buttons on initialization.
            this.btnUndo.Enabled =
            this.btnRedo.Enabled =
            this.btnSave.Enabled = false;
        }
        /// <summary>
        /// Adds to the undo history data starting at the specified coordinate to a specified number of elements.
        /// </summary>
        /// <param name="x">
        /// X-coordinate to begin adding data at.
        /// </param>
        /// <param name="y">
        /// Y-coordinate to begin adding data at.
        /// </param>
        /// <param name="length">
        /// Number of elements to add to the undo clipboard.
        /// </param>
        private void SetUndoHistory(int x, int y, int length)
        {
            //A new color was drawn while in the undo history.
            if (this.HistoryIndex < this.Prev.Count)
            {
                //Remove all actions in the undo history that come after the current history index
                this.Prev.RemoveRange(this.HistoryIndex, this.Prev.Count - this.HistoryIndex);
                this.Next = new List<SaveData>();
            }

            this.HistoryIndex++;

            //Copy Palette data at specified location and length to arbitrary array.
            uint[] data = new uint[length];
            fixed (uint* src = &this.Palette.Data[y, x])
            fixed (uint* dest = data)
                for (int i = 0; i < length; i++)
                    dest[i] = src[i];

            Prev.Add(new SaveData(x, y, ref data, this.PlayerIndex));
        }
        /// <summary>
        /// Adds to the undo history clipboard all data between the two specified coordinates.
        /// </summary>
        /// <param name="x1">
        /// X-coordinate of the first element in the Palette.
        /// </param>
        /// <param name="y1">
        /// Y-coordinate of the first element in the Palette.
        /// </param>
        /// <param name="x2">
        /// X-coordinate of the last element in the Palette.
        /// </param>
        /// <param name="y2">
        /// Y-coordinate of the last element in the Palette.
        /// </param>
        private void SetUndoHistory(int x1, int y1, int x2, int y2)
        {
            //This method will simply calculate the length, then call the other function
            int s1 = (y1 * Palette.Columns) + x1;
            int s2 = (y2 * Palette.Columns) + x2;
            int min = s1 < s2 ? s1 : s2;
            int max = s1 > s2 ? s1 : s2;
            SetUndoHistory(s1 < s2 ? x1 : x2, s1 < s2 ? y1 : y2, 1 + max - min);
        }
        /// <summary>
        /// Sets the next redo history item.
        /// </summary>
        private void SetRedoHistory()
        {
            int h = this.HistoryIndex;
            if (this.Prev.Count - h - 1 == this.Next.Count)
            {
                //Use of local variables to read better and help performance
                int l = this.Prev[h].Length;
                int x = this.Prev[h].X;
                int y = this.Prev[h].Y;
                int p = this.Prev[h].PlayerIndex;

                //Switch Player Index to what was set in the History Index.
                int p_save = this.PlayerIndex;
                this.PlayerIndex = p;

                //Copy History Data to Palette Data.
                uint[] data = new uint[l];
                fixed (uint* src = &this.Palette.Data[y, x])
                fixed (uint* dest = data)
                    for (int i = 0; i < l; i++)
                        dest[i] = src[i];

                //Assign Player Index to original value.
                this.PlayerIndex = p_save;

                this.Next.Add(new SaveData(x, y, ref data, p));
            }
        }
        /// <summary>
        /// Undoes the last action performed in the PaletteEditor
        /// </summary>
        private void Undo()
        {
            if (this.HistoryIndex > 0)
            {
                this.HistoryIndex--;
                SetRedoHistory();

                //Use of local variable to read better and help performance.
                int h = this.HistoryIndex;
                int l = this.Prev[h].Length;
                int x = this.Prev[h].X;
                int y = this.Prev[h].Y;

                //Switch Player Index to what was set in the History Index.
                int o = this.PlayerIndex;
                this.PlayerIndex = this.Prev[h].PlayerIndex;

                //Copy History Data to Palette Data.
                fixed (uint* src = this.Prev[h].Data)
                fixed (uint* dest = &this.Palette.Data[y, x])
                    for (int i = 0; i < l; i++)
                        dest[i] = src[i];

                //Assign Player Index to original value.
                this.PlayerIndex = o;

                //Change to the actual Palette has taken place, so redraw all game graphics.
                RedrawPalette(true);

                //Disable save button if at last Save.
                if (btnSave.Enabled && this.HistoryIndex == this.SaveIndex)
                    btnSave.Enabled = false;
                else if (!btnSave.Enabled && this.HistoryIndex != this.SaveIndex)
                    btnSave.Enabled = true;

                //Disable Undo when History Index is 0. Redo will always need to be set.
                if (btnUndo.Enabled && this.HistoryIndex == 0)
                    btnUndo.Enabled = false;
                if (!btnRedo.Enabled)
                    btnRedo.Enabled = true;
            }
            
        }
        /// <summary>
        /// Redoes the next action that was performed in the PaletteEditor.
        /// </summary>
        private void Redo()
        {
            if (this.HistoryIndex < this.Prev.Count)
            {
                //Use of local variable to read better and help performance.
                int h = this.Prev.Count - this.HistoryIndex++ - 1;
                int l = this.Next[h].Length;
                int x = this.Next[h].X;
                int y = this.Next[h].Y;

                //Switch Player Index to what was set in the History Index.
                int o = this.PlayerIndex;
                this.PlayerIndex = this.Next[h].PlayerIndex;

                //Copy History Data to Palette Data.
                fixed (uint* src = this.Next[h].Data)
                fixed (uint* dest = &this.Palette.Data[y, x])
                    for (int i = 0; i < l; i++)
                        dest[i] = src[i];

                //Assign Player Index to original value.
                this.PlayerIndex = o;

                //Change to the actual Palette has taken place, so redraw all game graphics.
                RedrawPalette(true);

                //Disable Save button if at last save. Otherwise, enable.
                if (btnSave.Enabled && this.HistoryIndex == this.SaveIndex)
                    btnSave.Enabled = false;
                else if (!btnSave.Enabled && this.HistoryIndex != this.SaveIndex)
                    btnSave.Enabled = true;

                //Disable Redo when at last change.
                if (btnRedo.Enabled && this.HistoryIndex == Prev.Count)
                    btnRedo.Enabled = false;
                if (!btnUndo.Enabled)
                    btnUndo.Enabled = true;
            }
        }

        /// <summary>
        /// Sets a single color to the Palette at a specified coordinate.
        /// </summary>
        /// <param name="x">
        /// The x-coordinate to set the color.
        /// </param>
        /// <param name="y">
        /// The y-coordinate to set the color.
        /// </param>
        /// <param name="color">
        /// The color to set.
        /// </param>
        private void SetPalette(int x, int y, uint color)
        {
            this.SetUndoHistory(x, y, 1);
            this.Palette.Data[y, x] = color;
            this.PaletteWritten();

            this.CurrentColor = color;
        }
        /// <summary>
        /// Sets a gradient between two coordinates.
        /// </summary>
        /// <param name="x1">
        /// The x-coordinate of the first location
        /// </param>
        /// <param name="y1">
        /// The y-coordinate of the first location.
        /// </param>
        /// <param name="x2">
        /// The x-coordinate of the second location.
        /// </param>
        /// <param name="y2">
        /// The y-coordinate of the second location.
        /// </param>
        private void SetPalette(int x1, int y1, int x2, int y2)
        {
            this.SetUndoHistory(x1, y1, x2, y2);
            this.Palette.Gradient(x1, y1, x2, y2);
            this.PaletteWritten();
        }
        /// <summary>
        /// Sets a full Palette over the existing one.
        /// </summary>
        /// <param name="palette">
        /// The new Palette to use.
        /// </param>
        private void SetPalette(ref byte[] data)
        {
            this.SetUndoHistory(0, 0, Palette.ArraySize);
            this.Palette = new Palette(ref data);
            this.PaletteWritten();
        }
        /// <summary>
        /// Actions to take after the Palette is written to.
        /// </summary>
        private void PaletteWritten()
        {
            this.RedrawPalette(true);
            if (!btnSave.Enabled)
                this.btnSave.Enabled = true;
            if (!btnUndo.Enabled)
                this.btnUndo.Enabled = true;
            if (btnRedo.Enabled)
                this.btnRedo.Enabled = false;
        }
        /// <summary>
        /// Updates all of the information if the Status GroupBox
        /// </summary>
        /// <param name="x">
        /// X-coordinate to reference
        /// </param>
        /// <param name="y">
        /// Y-coordinate to reference
        /// </param>
        private void UpdateStatus(int x, int y)
        {
            //Coordinate text
            this.lblPalette.Text = y.ToString("X");
            this.lblColor.Text = x.ToString("X");

            //Color text
            int color = (int)this.Palette.Data[y, x] & 0xFFFFFF;
            this.lblPC.Text = color.ToString("X6");
            this.lblSNES.Text = LunarCompress.PCtoSNESRGB(color).ToString("X4");

            //Color component text
            this.lblRed.Text = (color >> 0x10).ToString();
            this.lblGreen.Text = ((color >> 8) & 0xFF).ToString();
            this.lblBlue.Text = (color & 0xFF).ToString();
        }
        /// <summary>
        /// Edits a color in the palette (or several for a gradient)
        /// </summary>
        /// <param name="x">
        /// X-coordinate of the color to edit
        /// </param>
        /// <param name="y">
        /// Y-coordinate of the color to edit.
        /// </param>
        /// <param name="gradX">
        /// Terminal gradient x-location (for gradients).
        /// </param>
        /// <param name="gradY">
        /// Terminal gradient y-location (for gradients).
        /// </param>
        /// <param name="gradient">
        /// If true, a gradient will be made between the two supplied points.
        /// </param>
        private void EditPaletteColor(int x, int y, int gradX, int gradY, bool gradient)
        {
            if (!gradient)
            {
                ColorDialog dlg = new ColorDialog();
                dlg.AllowFullOpen = true;
                dlg.Color = Palette.PCToSystemColor(Palette.Data[y, x]);
                dlg.FullOpen = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                    SetPalette(x, y, Palette.SystemToPCColor(dlg.Color));
            }
            else
                SetPalette(x, y, gradX, gradY);
        }
        /// <summary>
        /// Copies a color from the Palette at a specified location to the CurrentColor
        /// </summary>
        /// <param name="x">
        /// X-coordinate to copy from.
        /// </param>
        /// <param name="y">
        /// Y-coordinate to copy from.
        /// </param>
        private void PaletteToCurrentColor(int x, int y)
        {
            CurrentColor = Palette.Data[y, x];
        }
        /// <summary>
        /// Opens a ColorDialog to edit the CurrentColor.
        /// </summary>
        private void EditCurrentColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.AllowFullOpen = true;
            dlg.Color = Palette.PCToSystemColor(CurrentColor);
            dlg.FullOpen = true;
            if (dlg.ShowDialog() == DialogResult.OK)
                CurrentColor = Palette.SystemToPCColor(dlg.Color);
        }
        /// <summary>
        /// Controls the logic for redrawing the Palette.
        /// </summary>
        public void RedrawPalette(bool branch)
        {
            drwPalette.Invalidate();

            //Update the Player Palette data.
            for (int i = 0; i < Palette.Columns; i++)
                PlayerPalettes[this.PlayerIndex, i] = Palette.Data[Palette.Rows - 1, i];

            //Branhcing is intended to occur when actual data to the palette is changed, rather than, say, a visual change to the editor.
            if (branch)
                GFXEditor.RedrawForm(true);
        }
        /// <summary>
        /// Controls the logic for redrawing the CurrentColor.
        /// </summary>
        public void RedrawColor()
        {
            drwColor.Invalidate();
        }
        /// <summary>
        /// Draws the PaletteEditor Bitmap with all functionality currently set within it.
        /// </summary>
        /// <param name="g">
        /// The Graphics context of the device to draw to.
        /// </param>
        /// <param name="x1">
        /// Initial X-coordinate of the color the mouse is over.
        /// </param>
        /// <param name="y1">
        /// Initial Y-coordinate of the color the mouse is over.
        /// </param>
        /// <param name="x2">
        /// Terminal X-coordinate for the gradient.
        /// </param>
        /// <param name="y2">
        /// Terminal Y-coordinate for the gradient.
        /// </param>
        /// <param name="gradient">
        /// If true, the function draws logic for selecting a gradient.
        /// </param>
        /// <param name="active">
        /// If true, the function draws logic for an active border.
        /// </param>
        private void DrawPaletteEditor(Graphics g, int x1, int y1, int x2, int y2, bool gradient, bool active)
        {
            //Draw Palette.
            long t1 = DateTime.Now.Ticks;
            g.DrawImageUnscaled(Palette.DrawPalette(), Point.Empty);
            long t2 = DateTime.Now.Ticks;
            this.Text = ((t2 - t1) / 10000).ToString() + "ms";

            //Add visual aids only if the control is active.
            if (active)
            {
                //Get current color.
                uint pc = Palette.Data[y1, x1] | 0xFF000000;
                ExpandedColor color = Palette.PCToSystemColor(pc);

                //Create a dash pen with best contrast for color.
                Pen p = new Pen(color.GetMaximumContrast(), 1);
                p.DashStyle = DashStyle.Dash;

                //Gradient (with different coordinates)
                if (gradient && !(x1 == x2 && y1 == y2))
                {
                    //Keep it to a static color or it will keep changing depening on where the gradient goes to.
                    p.Color = Color.LightGray;

                    //Ensures the coordinates are ordered properly.
                    int s1 = (y1 * Palette.Columns) + x1;
                    int s2 = (y2 * Palette.Columns) + x2;
                    int minX = (s1 < s2 ? x1 : x2) * Palette.ColorWidth;
                    int minY = (s1 < s2 ? y1 : y2) * Palette.ColorHeight;
                    int maxX = (s1 > s2 ? x1 : x2) * Palette.ColorWidth;
                    int maxY = (s1 > s2 ? y1 : y2) * Palette.ColorHeight;

                    //If the y-coorsinates are different, then we need to draw an irregular shape.
                    if (maxY > minY)
                    {
                        //Gradient area consists of 8 points (9th is the first point to enclose the shape.).
                        Point[] a = new Point[9];
                        a[0] = new Point(minX, minY);
                        a[1] = new Point(0x100 - 1, minY);
                        a[2] = new Point(0x100 - 1, maxY - 1);
                        a[3] = new Point((maxX + 0x10) - 1, maxY - 1);
                        a[4] = new Point((maxX + 0x10) - 1, (maxY + 0x10) - 1);
                        a[5] = new Point(0, (maxY + 0x10) - 1);
                        a[6] = new Point(0, minY + 0x10);
                        a[7] = new Point(minX, minY + 0x10);
                        a[8] = a[0];
                        
                        //Draw shape
                        g.DrawLines(p, a);
                    }
                    //If the y-coordinate are the same, a simple rectangle can be drawn
                    else
                        g.DrawRectangle(p, minX, minY, maxX - minX + Palette.ColorWidth - 1, Palette.ColorHeight - 1);

                    //Used for first and last colors of gradient.
                    p.DashStyle = DashStyle.Dot;

                    //Border for first color
                    pc = Palette.Data[minY / Palette.ImageHeight, minX / Palette.ImageWidth] | 0xFF000000;
                    color = ExpandedColor.FromColor(Palette.PCToSystemColor(pc));
                    p.Color = color.GetMaximumContrast().ToColor();
                    g.DrawRectangle(p, minX + 2, minY + 2, Palette.ColorWidth - 5, Palette.ColorHeight - 5);

                    //Border for last color
                    pc = Palette.Data[maxY / Palette.ImageHeight, maxX / Palette.ImageWidth] | 0xFF000000;
                    color = ExpandedColor.FromColor(Palette.PCToSystemColor(pc));
                    p.Color = color.GetMaximumContrast().ToColor();
                    g.DrawRectangle(p, maxX + 2, maxY + 2, Palette.ColorWidth - 5, Palette.ColorHeight - 5);
                }
                //For non-gradients, just draw a rectangle around the selected color.
                else
                    g.DrawRectangle(p, new Rectangle(x1 * Palette.ColorWidth, y1 * Palette.ColorHeight, Palette.ColorWidth - 1, Palette.ColorHeight - 1));    //Simply draws a dash square 
            }
            UpdateStatus(this.X, this.Y);
        }
        /// <summary>
        /// Initializes the PlayerPalette data from external files from the ROM.
        /// </summary>
        private void InitializePlayerPalettes()
        {
            this.PlayerPalettes = new uint[PaletteEditor.NumPlayers, Palette.Columns];
            this.Player_copy = new uint[PaletteEditor.NumPlayers, Palette.Columns];

            //This is also temporary until the editor starts working better.
            string path = this.BaseDirectory + PaletteEditor.PlayerFile;
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                if (data.Length >= PaletteEditor.PlayerData << 1)
                {
                    fixed (byte* ptr = data)
                    fixed (uint* dest = this.PlayerPalettes)
                    fixed (uint* dest2 = this.Player_copy)
                    {
                        ushort* src = (ushort*)ptr;
                        for (int i = 0; i < PaletteEditor.PlayerData; i++)
                            dest[i] = dest2[i] = LunarCompress.SNEStoPCRGB(src[i]);
                    }
                }
            }
        }
        /// <summary>
        /// Opens an OpenFileDialog to open a new Palette from a file.
        /// </summary>
        private void Open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = PaletteEditor.DefaultExt;
            dlg.Filter = PaletteEditor.PaletteFilter;
            dlg.FilterIndex = 0;
            dlg.InitialDirectory = PaletteEditor.InitialDirectory;
            dlg.Title = PaletteEditor.OpenText;
            if (dlg.ShowDialog() == DialogResult.OK)
                OpenPaletteFromFile(dlg.FileName);
        }
        /// <summary>
        /// Opens a SaveFileDialog to save the Palette to a file.
        /// </summary>
        private void SaveAs()
        {
            //Note this does not account for different extensions yet.

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = PaletteEditor.DefaultExt;
            dlg.Filter = PaletteEditor.PaletteFilter;
            dlg.FilterIndex = 0;
            dlg.InitialDirectory = PaletteEditor.InitialDirectory;
            dlg.Title = PaletteEditor.SaveText;
            if (dlg.ShowDialog() == DialogResult.OK)
                SavePaletteToFile(dlg.FileName);
        }
        /// <summary>
        /// Opens a palette from a selected file path.
        /// </summary>
        /// <param name="path">
        /// The path of the file to open.
        /// </param>
        private void OpenPaletteFromFile(string path)
        {
            //Note this does not account for different extension yet.

            SetUndoHistory(0, 0, Palette.ArraySize);
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                if (data.Length >= Palette.DataSize)
                    this.Palette = new Palette(ref data);
                else
                    this.Palette = new Palette();        //If the file cannot be used, default to empty palette
            }
            else
                this.Palette = new Palette();            //If the file does not exist, default to empty palette
            PaletteWritten();
        }
        /// <summary>
        /// Saves the Palette to its appropriate file destination relative to the current level.
        /// </summary>
        private void SavePalette()
        {
            if (btnSave.Enabled)
            {
                StringBuilder sb = new StringBuilder(this.BaseDirectory);
                sb.Append(PaletteEditor.DefaultFolder);
                sb.Append(this.Level.ToString("X2"));
                sb.Append('.');
                sb.Append(PaletteEditor.DefaultExt);
                SavePaletteToFile(sb.ToString());

                this.SaveIndex = this.HistoryIndex;
                btnSave.Enabled = false;
            }
        }
        /// <summary>
        /// Saves the palette to a specified path.
        /// </summary>
        /// <param name="path">
        /// Path to save the Palette to.
        /// </param>
        private void SavePaletteToFile(string path)
        {
            File.WriteAllBytes(path, this.Palette.ToByteArray());
        }
        /// <summary>
        /// Sets the current edit coordinate of the PaletteEditor
        /// </summary>
        /// <param name="x">
        /// X-coordinate to be set to (-2 for no change).
        /// </param>
        /// <param name="y">
        /// Y-coordinate to be set to (-2 for no change).
        /// </param>
        private void SetCoordinates(int x, int y)
        {
            //Allows x-coordinate to wrap around editor.
            if (x == -1)
                x = 0x0F;
            if (x == 0x10)
                x = 0;

            //Allows y-coordinate to wrap aorund editor.
            if (y == -1)
                y = 0x0F;
            if (y == 0x10)
                y = 0;

            //Values of -2 mean it should not be changed.
            if (x != -2)
                this.X = x;
            if (y != -2)
                this.Y = y;

            RedrawPalette(false);
            UpdateStatus(this.X, this.Y);
        }
        #endregion

        #region Events
        private void drwPalette_Paint(object sender, PaintEventArgs e)
        {
            DrawPaletteEditor(e.Graphics, this.X, this.Y, this.GradX, this.GradY, this.CtrlDown, this.ActiveControl == (Control)sender);
        }

        private void PaletteEditor_KeyDown(object sender, KeyEventArgs e)
        {
            //Manage control key and its related variables/
            if (e.Control && !this.CtrlDown)
            {
                this.CtrlDown = true;
                this.GradX = X;
                this.GradY = Y;
            }
            else if (!e.Control)
                this.CtrlDown = false;

            //Fulle support for Palette Editor with the keyboard.
            if (e.Control && e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.P:
                        this.Close();
                        break;
                    case Keys.E:
                        EditCurrentColor();
                        break;
                    default:
                        break;
                }
            }
            if (e.Control && !e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        SaveAs();
                        break;
                    default:
                        break;
                }
            }
            if (!e.Shift && !e.Alt)    //e.Control invariant
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        this.ActiveControl = this.drwPalette;
                        SetCoordinates(X - 1, -2);
                        break;
                    case Keys.Right:
                        this.ActiveControl = this.drwPalette;
                        SetCoordinates(X + 1, -2);
                        break;
                    case Keys.Up:
                        this.ActiveControl = this.drwPalette;
                        SetCoordinates(-2, Y - 1);
                        break;
                    case Keys.Down:
                        this.ActiveControl = this.drwPalette;
                        SetCoordinates(-2, Y + 1);
                        break;
                    default:
                        break;
                }
            }
            if (e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        SavePalette();
                        break;
                    case Keys.O:
                        Open();
                        break;
                    case Keys.C:
                        PaletteToCurrentColor(X, Y);
                        break;
                    case Keys.V:
                        SetPalette(X, Y, CurrentColor);
                        break;
                    case Keys.E:
                        EditPaletteColor(X, Y, GradX, GradY, CtrlDown);
                        break;
                    case Keys.Z:
                        Undo();
                        break;
                    case Keys.Y:
                        Redo();
                        break;
                    default:
                        break;
                }
            }
            if (!e.Control && !e.Shift && !e.Alt)
            {
                //Managing the Player Index is kinda weak. Not sure if I should fix it. At least it works.
                int p = this.PlayerIndex;

                switch (e.KeyCode)
                {
                    case Keys.PageDown:
                        p++;
                        break;
                    case Keys.PageUp:
                        p--;
                        break;
                    default:
                        break;
                }

                if (p != this.PlayerIndex)
                {
                    if (p > 3)
                        this.PlayerIndex = 0;
                    else if (p < 0)
                        this.PlayerIndex = 3;
                    else
                        this.PlayerIndex = p;

                    rdbPlayers[this.PlayerIndex].Checked = true;
                    RedrawPalette(true);
                }
            }
        }

        private void drwPalette_MouseMove(object sender, MouseEventArgs e)
        {
            //Checks if the mouse is actually moving. This makes sure keyboard use isn't overridden.
            if (e.X != this.MouseX || e.Y != this.MouseY)
            {
                this.ActiveControl = (Control)sender;
                this.MouseX = e.X;
                this.MouseY = e.Y;

                //Mouse events can still occur when the cursor leaves the form, which can crash the program.
                if (IsInBounds(e.Location, ((Control)sender).ClientRectangle))
                {
                    int _y = this.Y;
                    int _x = this.X;
                    this.X = e.X / Palette.ColorWidth;
                    this.Y = e.Y / Palette.ColorHeight;

                    //Save from constant redrawing when not necessary.
                    if (this.X != _x || this.Y != _y)
                    {
                        UpdateStatus(this.X, this.Y);
                        RedrawPalette(false);
                    }
                }
            }
        }

        private void drwPalette_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInBounds(e.Location, ((Control)sender).ClientRectangle))
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        EditPaletteColor(this.X, this.Y, this.GradX, this.GradY, this.CtrlDown);
                        break;
                    case MouseButtons.Middle:
                        PaletteToCurrentColor(this.X, this.Y);
                        break;
                    case MouseButtons.Right:
                        SetPalette(this.X, this.Y, this.CurrentColor);
                        break;
                }
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void drwPalette_Enter(object sender, EventArgs e)
        {
            RedrawPalette(false);
            Control_Enter(sender, e);
        }

        private void drwColor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Bitmap bmp = Palette.DrawSquare(this.CurrentColor);
            g.DrawImageUnscaled(bmp, Point.Empty);
        }

        private void drwColor_Click(object sender, EventArgs e)
        {
            EditCurrentColor();
        }

        private void PaletteEditor_KeyUp(object sender, KeyEventArgs e)
        {
            this.CtrlDown = e.Control;
            RedrawPalette(false);
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            Reset(true);
        }

        private void drwPalette_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length == 1)
                {
                    byte[] data = File.ReadAllBytes(files[0]);
                    if (data.Length == Palette.ArraySize << 1)
                    {
                        e.Effect = DragDropEffects.All;
                        this.DragPalette = new Palette(ref this.Palette);
                        this.Palette = new Palette(ref data);
                        RedrawPalette(true);
                        this.tssMain.Text = PaletteEditor.Previewing + Path.GetFileName(files[0]);
                    }
                    else
                        this.tssMain.Text = PaletteEditor.BadFileSize;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    this.tssMain.Text = PaletteEditor.DragError;
                }
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void drwPalette_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.All)
            {
                //A new color was drawn while in the undo history.
                if (this.HistoryIndex < this.Prev.Count)
                {
                    //Remove all actions in the undo history that come after the current history index
                    this.Prev.RemoveRange(this.HistoryIndex, this.Prev.Count - this.HistoryIndex);
                    this.Next = new List<SaveData>();
                }

                this.HistoryIndex++;

                //Copy Palette data at specified location and length to arbitrary array.
                uint[] data = new uint[Palette.ArraySize];
                fixed (uint* src = this.DragPalette.Data)
                fixed (uint* dest = data)
                    for (int i = 0; i < Palette.ArraySize; i++)
                        dest[i] = src[i];

                Prev.Add(new SaveData(0, 0, ref data, this.PlayerIndex));

                PaletteWritten();

                this.tssMain.Text = PaletteEditor.NewPaletteLoaded;
            }
        }

        private void drwPalette_DragLeave(object sender, EventArgs e)
        {
            this.Palette = new Palette(ref this.DragPalette);
            RedrawPalette(true);
            this.tssMain.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePalette();
        }

        private void rdbPlayer_CheckedChanged(object sender, EventArgs e)
        {
            this.PlayerIndex = (int)((Control)sender).Tag;
            RedrawPalette(true);
        }

        private void Control_Enter(object sender, EventArgs e)
        {
            object o = ((Control)sender).Tag;
            try
            {
                tssMain.Text = (string)((Control)sender).Tag;
            }
            //The radio buttons use integers as Tags for the PlayerIndex.
            catch
            {
                tssMain.Text = PaletteEditor.Status_RDB_Player;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        #endregion

        /// <summary>
        /// A class containing the elements of data needed to make Undo/Redo history.
        /// </summary>
        private class SaveData
        {
            /// <summary>
            /// The start X-coordinate of the ClipData.
            /// </summary>
            public int X;
            /// <summary>
            /// The start Y-coordinate of the ClipData.
            /// </summary>
            public int Y;
            /// <summary>
            /// The PlayerIndex when the ClipData was set.
            /// </summary>
            public int PlayerIndex;
            /// <summary>
            /// An array of the colors that were changed.
            /// </summary>
            public uint[] Data;
            /// <summary>
            /// Gets the number of colors in the ClipData.
            /// </summary>
            public int Length
            {
                get
                {
                    return Data.Length;
                }
            }

            /// <summary>
            /// Creates a ClipData element from given components.
            /// </summary>
            /// <param name="x">
            /// The start x-coordinate.
            /// </param>
            /// <param name="y">
            /// The start y-coordinate
            /// </param>
            /// <param name="data">
            /// An array of the color data.
            /// </param>
            /// <param name="pIndex">
            /// The PlayerIndex
            /// </param>
            /// <returns>
            /// A ClipData element with the given components.
            /// </returns>
            public SaveData (int x, int y, ref uint[] data, int pIndex)
            {
                this.X = x;
                this.Y = y;
                this.PlayerIndex = pIndex;

                int length = data.Length;
                this.Data = new uint[length];
                fixed (uint* src = data)
                fixed (uint* dest = this.Data)
                    for (int i = 0; i < length; i++)
                        dest[i] = src[i];
            }
        }
    }
}