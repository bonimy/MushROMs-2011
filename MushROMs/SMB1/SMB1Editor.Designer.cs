namespace MushROMs.SMB1
{
    partial class SMB1Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMB1Editor));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaletteEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGFXEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMap16Editor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmContents = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbObjectSelector = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPaletteEditor = new System.Windows.Forms.ToolStripButton();
            this.tsbGFXEditor = new System.Windows.Forms.ToolStripButton();
            this.tsbMap16Editor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.drwLevel = new MushROMs.Controls.DrawControl();
            this.hsbLevel = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblScroll = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.mnuMain.SuspendLayout();
            this.tlsMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmEdit,
            this.tsmEditors,
            this.tsmTools,
            this.tsmHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(984, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNew,
            this.tsmOpen,
            this.toolStripSeparator,
            this.tsmSave,
            this.tsmSaveAs,
            this.toolStripSeparator2,
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmNew
            // 
            this.tsmNew.Image = ((System.Drawing.Image)(resources.GetObject("tsmNew.Image")));
            this.tsmNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmNew.Name = "tsmNew";
            this.tsmNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmNew.Size = new System.Drawing.Size(146, 22);
            this.tsmNew.Text = "&New";
            // 
            // tsmOpen
            // 
            this.tsmOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsmOpen.Image")));
            this.tsmOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(146, 22);
            this.tsmOpen.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // tsmSave
            // 
            this.tsmSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmSave.Image")));
            this.tsmSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(146, 22);
            this.tsmSave.Text = "&Save";
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.Size = new System.Drawing.Size(146, 22);
            this.tsmSaveAs.Text = "Save &As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(146, 22);
            this.tsmExit.Text = "E&xit";
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUndo,
            this.tsmRedo,
            this.toolStripSeparator3,
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.toolStripSeparator4,
            this.tsmDelete,
            this.tsmSelectAll});
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(39, 20);
            this.tsmEdit.Text = "&Edit";
            // 
            // tsmUndo
            // 
            this.tsmUndo.Name = "tsmUndo";
            this.tsmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmUndo.Size = new System.Drawing.Size(164, 22);
            this.tsmUndo.Text = "&Undo";
            // 
            // tsmRedo
            // 
            this.tsmRedo.Name = "tsmRedo";
            this.tsmRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmRedo.Size = new System.Drawing.Size(164, 22);
            this.tsmRedo.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmCut
            // 
            this.tsmCut.Image = ((System.Drawing.Image)(resources.GetObject("tsmCut.Image")));
            this.tsmCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmCut.Size = new System.Drawing.Size(164, 22);
            this.tsmCut.Text = "Cu&t";
            // 
            // tsmCopy
            // 
            this.tsmCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsmCopy.Image")));
            this.tsmCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(164, 22);
            this.tsmCopy.Text = "&Copy";
            // 
            // tsmPaste
            // 
            this.tsmPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsmPaste.Image")));
            this.tsmPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(164, 22);
            this.tsmPaste.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmDelete.Size = new System.Drawing.Size(164, 22);
            this.tsmDelete.Text = "Delete";
            // 
            // tsmSelectAll
            // 
            this.tsmSelectAll.Name = "tsmSelectAll";
            this.tsmSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmSelectAll.Size = new System.Drawing.Size(164, 22);
            this.tsmSelectAll.Text = "Select &All";
            // 
            // tsmEditors
            // 
            this.tsmEditors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPaletteEditor,
            this.tsmGFXEditor,
            this.tsmMap16Editor});
            this.tsmEditors.Name = "tsmEditors";
            this.tsmEditors.Size = new System.Drawing.Size(55, 20);
            this.tsmEditors.Text = "E&ditors";
            // 
            // tsmPaletteEditor
            // 
            this.tsmPaletteEditor.CheckOnClick = true;
            this.tsmPaletteEditor.Image = global::MushROMs.SMB1.Resources.imgOpenPalette;
            this.tsmPaletteEditor.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmPaletteEditor.Name = "tsmPaletteEditor";
            this.tsmPaletteEditor.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.P)));
            this.tsmPaletteEditor.Size = new System.Drawing.Size(221, 22);
            this.tsmPaletteEditor.Text = "&Palette Editor";
            this.tsmPaletteEditor.ToolTipText = "Open Palette Editor...";
            this.tsmPaletteEditor.Click += new System.EventHandler(this.tsmPaletteEditor_Click);
            // 
            // tsmGFXEditor
            // 
            this.tsmGFXEditor.CheckOnClick = true;
            this.tsmGFXEditor.Image = global::MushROMs.SMB1.Resources.img8x8Editor;
            this.tsmGFXEditor.Name = "tsmGFXEditor";
            this.tsmGFXEditor.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.G)));
            this.tsmGFXEditor.Size = new System.Drawing.Size(221, 22);
            this.tsmGFXEditor.Text = "&GFX Editor";
            this.tsmGFXEditor.ToolTipText = "Open GFX Editor...";
            this.tsmGFXEditor.CheckedChanged += new System.EventHandler(this.tsmGFXEditor_CheckedChanged);
            // 
            // tsmMap16Editor
            // 
            this.tsmMap16Editor.CheckOnClick = true;
            this.tsmMap16Editor.Image = global::MushROMs.SMB1.Resources.imgMap16Editor;
            this.tsmMap16Editor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmMap16Editor.Name = "tsmMap16Editor";
            this.tsmMap16Editor.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.M)));
            this.tsmMap16Editor.Size = new System.Drawing.Size(221, 22);
            this.tsmMap16Editor.Text = "&Map16 Editor";
            this.tsmMap16Editor.ToolTipText = "Open Map16 Editor...";
            this.tsmMap16Editor.Click += new System.EventHandler(this.tsmMap16Editor_Click);
            // 
            // tsmTools
            // 
            this.tsmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCustomize,
            this.tsmOptions,
            this.tsmBuild});
            this.tsmTools.Name = "tsmTools";
            this.tsmTools.Size = new System.Drawing.Size(48, 20);
            this.tsmTools.Text = "&Tools";
            // 
            // tsmCustomize
            // 
            this.tsmCustomize.Name = "tsmCustomize";
            this.tsmCustomize.Size = new System.Drawing.Size(152, 22);
            this.tsmCustomize.Text = "&Customize";
            // 
            // tsmOptions
            // 
            this.tsmOptions.Name = "tsmOptions";
            this.tsmOptions.Size = new System.Drawing.Size(152, 22);
            this.tsmOptions.Text = "&Options";
            // 
            // tsmBuild
            // 
            this.tsmBuild.Name = "tsmBuild";
            this.tsmBuild.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.tsmBuild.Size = new System.Drawing.Size(152, 22);
            this.tsmBuild.Text = "Build";
            this.tsmBuild.ToolTipText = "Builds the ROM assembly data";
            this.tsmBuild.Click += new System.EventHandler(this.tsmBuild_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmContents,
            this.tsmIndex,
            this.tsmSearch,
            this.toolStripSeparator5,
            this.tsmAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmContents
            // 
            this.tsmContents.Name = "tsmContents";
            this.tsmContents.Size = new System.Drawing.Size(122, 22);
            this.tsmContents.Text = "&Contents";
            // 
            // tsmIndex
            // 
            this.tsmIndex.Name = "tsmIndex";
            this.tsmIndex.Size = new System.Drawing.Size(122, 22);
            this.tsmIndex.Text = "&Index";
            // 
            // tsmSearch
            // 
            this.tsmSearch.Name = "tsmSearch";
            this.tsmSearch.Size = new System.Drawing.Size(122, 22);
            this.tsmSearch.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(122, 22);
            this.tsmAbout.Text = "&About...";
            // 
            // tlsMain
            // 
            this.tlsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator8,
            this.tsbObjectSelector,
            this.toolStripSeparator7,
            this.tsbPaletteEditor,
            this.tsbGFXEditor,
            this.tsbMap16Editor,
            this.toolStripSeparator6,
            this.tsbHelp});
            this.tlsMain.Location = new System.Drawing.Point(0, 24);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(984, 25);
            this.tlsMain.TabIndex = 1;
            this.tlsMain.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "&New";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "&Open";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "&Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "C&ut";
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "&Copy";
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "&Paste";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbObjectSelector
            // 
            this.tsbObjectSelector.CheckOnClick = true;
            this.tsbObjectSelector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbObjectSelector.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectSelector.Image")));
            this.tsbObjectSelector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectSelector.Name = "tsbObjectSelector";
            this.tsbObjectSelector.Size = new System.Drawing.Size(23, 22);
            this.tsbObjectSelector.Text = "Open Object Selector";
            this.tsbObjectSelector.Click += new System.EventHandler(this.tsbObjectSelector_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPaletteEditor
            // 
            this.tsbPaletteEditor.CheckOnClick = true;
            this.tsbPaletteEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaletteEditor.Image = global::MushROMs.SMB1.Resources.imgOpenPalette;
            this.tsbPaletteEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaletteEditor.Name = "tsbPaletteEditor";
            this.tsbPaletteEditor.Size = new System.Drawing.Size(23, 22);
            this.tsbPaletteEditor.Text = "Open Palette Editor...";
            this.tsbPaletteEditor.CheckedChanged += new System.EventHandler(this.tsbPalette_CheckedChanged);
            // 
            // tsbGFXEditor
            // 
            this.tsbGFXEditor.CheckOnClick = true;
            this.tsbGFXEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGFXEditor.Image = global::MushROMs.SMB1.Resources.img8x8Editor;
            this.tsbGFXEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGFXEditor.Name = "tsbGFXEditor";
            this.tsbGFXEditor.Size = new System.Drawing.Size(23, 22);
            this.tsbGFXEditor.Text = "Open GFX Editor...";
            this.tsbGFXEditor.CheckedChanged += new System.EventHandler(this.tsbGFXEditor_CheckedChanged);
            // 
            // tsbMap16Editor
            // 
            this.tsbMap16Editor.CheckOnClick = true;
            this.tsbMap16Editor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMap16Editor.Image = global::MushROMs.SMB1.Resources.imgMap16Editor;
            this.tsbMap16Editor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMap16Editor.Name = "tsbMap16Editor";
            this.tsbMap16Editor.Size = new System.Drawing.Size(23, 22);
            this.tsbMap16Editor.Text = "Open Map16 Editor...";
            this.tsbMap16Editor.Click += new System.EventHandler(this.tsbMap16Editor_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "He&lp";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 574);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(984, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 2;
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(70, 17);
            this.tssMain.Text = "Editor ready";
            // 
            // drwLevel
            // 
            this.drwLevel.AutoScroll = true;
            this.drwLevel.BackColor = System.Drawing.Color.Black;
            this.drwLevel.Location = new System.Drawing.Point(0, 52);
            this.drwLevel.Name = "drwLevel";
            this.drwLevel.Size = new System.Drawing.Size(1000, 208);
            this.drwLevel.TabIndex = 3;
            this.drwLevel.Paint += new System.Windows.Forms.PaintEventHandler(this.drwLevel_Paint);
            this.drwLevel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwLevel_MouseMove);
            // 
            // hsbLevel
            // 
            this.hsbLevel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hsbLevel.LargeChange = 16;
            this.hsbLevel.Location = new System.Drawing.Point(0, 557);
            this.hsbLevel.Maximum = 512;
            this.hsbLevel.Name = "hsbLevel";
            this.hsbLevel.Size = new System.Drawing.Size(984, 17);
            this.hsbLevel.TabIndex = 5;
            this.hsbLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbLevel_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "DrawTime: ";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(90, 263);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(26, 13);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "0ms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Scroll Max: ";
            // 
            // lblScroll
            // 
            this.lblScroll.AutoSize = true;
            this.lblScroll.Location = new System.Drawing.Point(91, 280);
            this.lblScroll.Name = "lblScroll";
            this.lblScroll.Size = new System.Drawing.Size(25, 13);
            this.lblScroll.TabIndex = 9;
            this.lblScroll.Text = "512";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Scroll Value: ";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(91, 297);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(13, 13);
            this.lblValue.TabIndex = 11;
            this.lblValue.Text = "0";
            // 
            // SMB1Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 596);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblScroll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hsbLevel);
            this.Controls.Add(this.drwLevel);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.tlsMain);
            this.Controls.Add(this.mnuMain);
            this.Icon = global::MushROMs.SMB1.Resources.Icon;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "SMB1Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MushROMs - Super Mario Bros. 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMB1Editor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SMB1Editor_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SMB1Editor_KeyDown);
            this.Resize += new System.EventHandler(this.SMB1Editor_Resize);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmNew;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmTools;
        private System.Windows.Forms.ToolStripMenuItem tsmCustomize;
        private System.Windows.Forms.ToolStripMenuItem tsmOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmContents;
        private System.Windows.Forms.ToolStripMenuItem tsmIndex;
        private System.Windows.Forms.ToolStripMenuItem tsmSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmEditors;
        private System.Windows.Forms.ToolStripMenuItem tsmPaletteEditor;
        private System.Windows.Forms.ToolStrip tlsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbPaletteEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private System.Windows.Forms.ToolStripMenuItem tsmGFXEditor;
        private System.Windows.Forms.ToolStripButton tsbGFXEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmMap16Editor;
        private System.Windows.Forms.ToolStripButton tsbMap16Editor;
        private MushROMs.Controls.DrawControl drwLevel;
        private System.Windows.Forms.HScrollBar hsbLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblScroll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbObjectSelector;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmBuild;



    }
}