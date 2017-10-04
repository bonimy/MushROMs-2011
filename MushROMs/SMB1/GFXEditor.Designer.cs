namespace MushROMs.SMB1
{
    partial class GFXEditor
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (Parent != null)
            {
                if (!Parent.Disposing)
                    Parent.GFXEditorVisible = false;
                else
                    base.Dispose(disposing);
            }
            else
                base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GFXEditor));
            this.drwGFX = new MushROMs.Controls.DrawControl();
            this.drwTile = new MushROMs.Controls.DrawControl();
            this.drwPalette = new MushROMs.Controls.DrawControl();
            this.drwColor1 = new MushROMs.Controls.DrawControl();
            this.drwColor3 = new MushROMs.Controls.DrawControl();
            this.drwColor2 = new MushROMs.Controls.DrawControl();
            this.drwColor4 = new MushROMs.Controls.DrawControl();
            this.drwColor5 = new MushROMs.Controls.DrawControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnRotate90 = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPalDn = new System.Windows.Forms.Button();
            this.btnPalUp = new System.Windows.Forms.Button();
            this.btnFlipX = new System.Windows.Forms.Button();
            this.btnFlipY = new System.Windows.Forms.Button();
            this.gbxGFXEditor = new System.Windows.Forms.GroupBox();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.gbxTileEditor = new System.Windows.Forms.GroupBox();
            this.btnTileRedo = new System.Windows.Forms.Button();
            this.btnTileUndo = new System.Windows.Forms.Button();
            this.lblColor1 = new System.Windows.Forms.Label();
            this.lblColor3 = new System.Windows.Forms.Label();
            this.lblColor2 = new System.Windows.Forms.Label();
            this.lblColor4 = new System.Windows.Forms.Label();
            this.lblColor5 = new System.Windows.Forms.Label();
            this.drwColor6 = new MushROMs.Controls.DrawControl();
            this.lblColor6 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.gbxGFXEditor.SuspendLayout();
            this.gbxTileEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // drwGFX
            // 
            this.drwGFX.BackColor = System.Drawing.SystemColors.Control;
            this.drwGFX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwGFX.Location = new System.Drawing.Point(0, 0);
            this.drwGFX.Name = "drwGFX";
            this.drwGFX.Size = new System.Drawing.Size(258, 258);
            this.drwGFX.TabIndex = 0;
            this.drwGFX.Paint += new System.Windows.Forms.PaintEventHandler(this.drwGFX_Paint);
            this.drwGFX.Enter += new System.EventHandler(this.drwControl_Refresh);
            this.drwGFX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drwGFX_KeyDown);
            this.drwGFX.Leave += new System.EventHandler(this.drwControl_Refresh);
            this.drwGFX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwGFX_MouseClick);
            this.drwGFX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwGFX_MouseMove);
            // 
            // drwTile
            // 
            this.drwTile.BackColor = System.Drawing.SystemColors.Control;
            this.drwTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwTile.Location = new System.Drawing.Point(0, 262);
            this.drwTile.Name = "drwTile";
            this.drwTile.Size = new System.Drawing.Size(130, 130);
            this.drwTile.TabIndex = 1;
            this.drwTile.Paint += new System.Windows.Forms.PaintEventHandler(this.drwTile_Paint);
            this.drwTile.Enter += new System.EventHandler(this.drwControl_Refresh);
            this.drwTile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drwTile_KeyDown);
            this.drwTile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.drwControl_KeyUp);
            this.drwTile.Leave += new System.EventHandler(this.drwControl_Refresh);
            this.drwTile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwTile_MouseEdit);
            this.drwTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwTile_MouseEdit);
            // 
            // drwPalette
            // 
            this.drwPalette.BackColor = System.Drawing.SystemColors.Control;
            this.drwPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwPalette.Location = new System.Drawing.Point(0, 396);
            this.drwPalette.Name = "drwPalette";
            this.drwPalette.Size = new System.Drawing.Size(258, 18);
            this.drwPalette.TabIndex = 2;
            this.drwPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.drwPalette_Paint);
            this.drwPalette.Enter += new System.EventHandler(this.drwControl_Refresh);
            this.drwPalette.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drwPalette_KeyDown);
            this.drwPalette.KeyUp += new System.Windows.Forms.KeyEventHandler(this.drwControl_KeyUp);
            this.drwPalette.Leave += new System.EventHandler(this.drwControl_Refresh);
            this.drwPalette.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseClick);
            this.drwPalette.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseMove);
            // 
            // drwColor1
            // 
            this.drwColor1.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor1.Enabled = false;
            this.drwColor1.Location = new System.Drawing.Point(134, 262);
            this.drwColor1.Name = "drwColor1";
            this.drwColor1.Size = new System.Drawing.Size(16, 16);
            this.drwColor1.TabIndex = 0;
            this.drwColor1.Visible = false;
            this.drwColor1.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor3
            // 
            this.drwColor3.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor3.Enabled = false;
            this.drwColor3.Location = new System.Drawing.Point(134, 306);
            this.drwColor3.Name = "drwColor3";
            this.drwColor3.Size = new System.Drawing.Size(16, 16);
            this.drwColor3.TabIndex = 0;
            this.drwColor3.Visible = false;
            this.drwColor3.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor2
            // 
            this.drwColor2.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor2.Enabled = false;
            this.drwColor2.Location = new System.Drawing.Point(134, 284);
            this.drwColor2.Name = "drwColor2";
            this.drwColor2.Size = new System.Drawing.Size(16, 16);
            this.drwColor2.TabIndex = 0;
            this.drwColor2.Visible = false;
            this.drwColor2.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor4
            // 
            this.drwColor4.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor4.Enabled = false;
            this.drwColor4.Location = new System.Drawing.Point(134, 328);
            this.drwColor4.Name = "drwColor4";
            this.drwColor4.Size = new System.Drawing.Size(16, 16);
            this.drwColor4.TabIndex = 0;
            this.drwColor4.Visible = false;
            this.drwColor4.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor5
            // 
            this.drwColor5.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor5.Enabled = false;
            this.drwColor5.Location = new System.Drawing.Point(134, 350);
            this.drwColor5.Name = "drwColor5";
            this.drwColor5.Size = new System.Drawing.Size(16, 16);
            this.drwColor5.TabIndex = 0;
            this.drwColor5.Visible = false;
            this.drwColor5.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(352, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(16, 17);
            this.tssMain.Text = "...";
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Location = new System.Drawing.Point(6, 19);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(75, 23);
            this.btnPrevPage.TabIndex = 0;
            this.btnPrevPage.Text = "Prev. Page";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnRotate90
            // 
            this.btnRotate90.Location = new System.Drawing.Point(6, 19);
            this.btnRotate90.Name = "btnRotate90";
            this.btnRotate90.Size = new System.Drawing.Size(75, 23);
            this.btnRotate90.TabIndex = 0;
            this.btnRotate90.Text = "Rotate 90º";
            this.btnRotate90.UseVisualStyleBackColor = true;
            this.btnRotate90.Click += new System.EventHandler(this.btnRotate90_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(6, 48);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 1;
            this.btnNextPage.Text = "Next Page";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPalDn
            // 
            this.btnPalDn.Location = new System.Drawing.Point(6, 106);
            this.btnPalDn.Name = "btnPalDn";
            this.btnPalDn.Size = new System.Drawing.Size(75, 23);
            this.btnPalDn.TabIndex = 2;
            this.btnPalDn.Text = "Pal. Dn";
            this.btnPalDn.UseVisualStyleBackColor = true;
            this.btnPalDn.Click += new System.EventHandler(this.btnPalDn_Click);
            // 
            // btnPalUp
            // 
            this.btnPalUp.Location = new System.Drawing.Point(6, 77);
            this.btnPalUp.Name = "btnPalUp";
            this.btnPalUp.Size = new System.Drawing.Size(75, 23);
            this.btnPalUp.TabIndex = 3;
            this.btnPalUp.Text = "Pal. Up";
            this.btnPalUp.UseVisualStyleBackColor = true;
            this.btnPalUp.Click += new System.EventHandler(this.btnPalUp_Click);
            // 
            // btnFlipX
            // 
            this.btnFlipX.Location = new System.Drawing.Point(6, 48);
            this.btnFlipX.Name = "btnFlipX";
            this.btnFlipX.Size = new System.Drawing.Size(75, 23);
            this.btnFlipX.TabIndex = 2;
            this.btnFlipX.Text = "FlipX";
            this.btnFlipX.UseVisualStyleBackColor = true;
            this.btnFlipX.Click += new System.EventHandler(this.btnFlipX_Click);
            // 
            // btnFlipY
            // 
            this.btnFlipY.Location = new System.Drawing.Point(6, 77);
            this.btnFlipY.Name = "btnFlipY";
            this.btnFlipY.Size = new System.Drawing.Size(75, 23);
            this.btnFlipY.TabIndex = 1;
            this.btnFlipY.Text = "FlipY";
            this.btnFlipY.UseVisualStyleBackColor = true;
            this.btnFlipY.Click += new System.EventHandler(this.btnFlipY_Click);
            // 
            // gbxGFXEditor
            // 
            this.gbxGFXEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxGFXEditor.Controls.Add(this.btnSaveAll);
            this.gbxGFXEditor.Controls.Add(this.btnRedo);
            this.gbxGFXEditor.Controls.Add(this.btnUndo);
            this.gbxGFXEditor.Controls.Add(this.btnPrevPage);
            this.gbxGFXEditor.Controls.Add(this.btnNextPage);
            this.gbxGFXEditor.Controls.Add(this.btnPalDn);
            this.gbxGFXEditor.Controls.Add(this.btnPalUp);
            this.gbxGFXEditor.Location = new System.Drawing.Point(262, 0);
            this.gbxGFXEditor.Name = "gbxGFXEditor";
            this.gbxGFXEditor.Size = new System.Drawing.Size(90, 225);
            this.gbxGFXEditor.TabIndex = 3;
            this.gbxGFXEditor.TabStop = false;
            this.gbxGFXEditor.Text = "GFX Editor";
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(6, 193);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAll.TabIndex = 7;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(6, 164);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(75, 23);
            this.btnRedo.TabIndex = 5;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(6, 135);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // gbxTileEditor
            // 
            this.gbxTileEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTileEditor.Controls.Add(this.btnTileRedo);
            this.gbxTileEditor.Controls.Add(this.btnTileUndo);
            this.gbxTileEditor.Controls.Add(this.btnRotate90);
            this.gbxTileEditor.Controls.Add(this.btnFlipY);
            this.gbxTileEditor.Controls.Add(this.btnFlipX);
            this.gbxTileEditor.Location = new System.Drawing.Point(262, 242);
            this.gbxTileEditor.Name = "gbxTileEditor";
            this.gbxTileEditor.Size = new System.Drawing.Size(90, 169);
            this.gbxTileEditor.TabIndex = 4;
            this.gbxTileEditor.TabStop = false;
            this.gbxTileEditor.Text = "Tile Editor";
            // 
            // btnTileRedo
            // 
            this.btnTileRedo.Location = new System.Drawing.Point(6, 135);
            this.btnTileRedo.Name = "btnTileRedo";
            this.btnTileRedo.Size = new System.Drawing.Size(75, 23);
            this.btnTileRedo.TabIndex = 4;
            this.btnTileRedo.Text = "Redo";
            this.btnTileRedo.UseVisualStyleBackColor = true;
            this.btnTileRedo.Click += new System.EventHandler(this.btnTileRedo_Click);
            // 
            // btnTileUndo
            // 
            this.btnTileUndo.Location = new System.Drawing.Point(6, 106);
            this.btnTileUndo.Name = "btnTileUndo";
            this.btnTileUndo.Size = new System.Drawing.Size(75, 23);
            this.btnTileUndo.TabIndex = 3;
            this.btnTileUndo.Text = "Undo";
            this.btnTileUndo.UseVisualStyleBackColor = true;
            this.btnTileUndo.Click += new System.EventHandler(this.btnTileUndo_Click);
            // 
            // lblColor1
            // 
            this.lblColor1.AutoSize = true;
            this.lblColor1.Location = new System.Drawing.Point(156, 262);
            this.lblColor1.Name = "lblColor1";
            this.lblColor1.Size = new System.Drawing.Size(61, 13);
            this.lblColor1.TabIndex = 0;
            this.lblColor1.Text = "Edit Color 1";
            // 
            // lblColor3
            // 
            this.lblColor3.AutoSize = true;
            this.lblColor3.Location = new System.Drawing.Point(156, 306);
            this.lblColor3.Name = "lblColor3";
            this.lblColor3.Size = new System.Drawing.Size(61, 13);
            this.lblColor3.TabIndex = 0;
            this.lblColor3.Text = "Edit Color 3";
            this.lblColor3.Visible = false;
            // 
            // lblColor2
            // 
            this.lblColor2.AutoSize = true;
            this.lblColor2.Location = new System.Drawing.Point(156, 284);
            this.lblColor2.Name = "lblColor2";
            this.lblColor2.Size = new System.Drawing.Size(61, 13);
            this.lblColor2.TabIndex = 0;
            this.lblColor2.Text = "Edit Color 2";
            // 
            // lblColor4
            // 
            this.lblColor4.AutoSize = true;
            this.lblColor4.Location = new System.Drawing.Point(156, 328);
            this.lblColor4.Name = "lblColor4";
            this.lblColor4.Size = new System.Drawing.Size(61, 13);
            this.lblColor4.TabIndex = 0;
            this.lblColor4.Text = "Edit Color 4";
            this.lblColor4.Visible = false;
            // 
            // lblColor5
            // 
            this.lblColor5.AutoSize = true;
            this.lblColor5.Location = new System.Drawing.Point(156, 350);
            this.lblColor5.Name = "lblColor5";
            this.lblColor5.Size = new System.Drawing.Size(61, 13);
            this.lblColor5.TabIndex = 0;
            this.lblColor5.Text = "Edit Color 5";
            this.lblColor5.Visible = false;
            // 
            // drwColor6
            // 
            this.drwColor6.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor6.Enabled = false;
            this.drwColor6.Location = new System.Drawing.Point(134, 372);
            this.drwColor6.Name = "drwColor6";
            this.drwColor6.Size = new System.Drawing.Size(16, 16);
            this.drwColor6.TabIndex = 0;
            this.drwColor6.Visible = false;
            this.drwColor6.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // lblColor6
            // 
            this.lblColor6.AutoSize = true;
            this.lblColor6.Location = new System.Drawing.Point(156, 372);
            this.lblColor6.Name = "lblColor6";
            this.lblColor6.Size = new System.Drawing.Size(61, 13);
            this.lblColor6.TabIndex = 0;
            this.lblColor6.Text = "Edit Color 6";
            this.lblColor6.Visible = false;
            // 
            // GFXEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 436);
            this.Controls.Add(this.lblColor6);
            this.Controls.Add(this.drwColor6);
            this.Controls.Add(this.lblColor5);
            this.Controls.Add(this.lblColor4);
            this.Controls.Add(this.lblColor2);
            this.Controls.Add(this.lblColor3);
            this.Controls.Add(this.lblColor1);
            this.Controls.Add(this.gbxTileEditor);
            this.Controls.Add(this.gbxGFXEditor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.drwColor5);
            this.Controls.Add(this.drwColor4);
            this.Controls.Add(this.drwColor2);
            this.Controls.Add(this.drwColor3);
            this.Controls.Add(this.drwColor1);
            this.Controls.Add(this.drwPalette);
            this.Controls.Add(this.drwTile);
            this.Controls.Add(this.drwGFX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GFXEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GFXEditor";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GFXEditor_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbxGFXEditor.ResumeLayout(false);
            this.gbxTileEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MushROMs.Controls.DrawControl drwGFX;
        private MushROMs.Controls.DrawControl drwTile;
        private MushROMs.Controls.DrawControl drwPalette;
        private MushROMs.Controls.DrawControl drwColor1;
        private MushROMs.Controls.DrawControl drwColor3;
        private MushROMs.Controls.DrawControl drwColor2;
        private MushROMs.Controls.DrawControl drwColor4;
        private MushROMs.Controls.DrawControl drwColor5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnRotate90;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPalDn;
        private System.Windows.Forms.Button btnPalUp;
        private System.Windows.Forms.Button btnFlipX;
        private System.Windows.Forms.Button btnFlipY;
        private System.Windows.Forms.GroupBox gbxGFXEditor;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.GroupBox gbxTileEditor;
        private System.Windows.Forms.Label lblColor1;
        private System.Windows.Forms.Label lblColor3;
        private System.Windows.Forms.Label lblColor2;
        private System.Windows.Forms.Label lblColor4;
        private System.Windows.Forms.Label lblColor5;
        private MushROMs.Controls.DrawControl drwColor6;
        private System.Windows.Forms.Label lblColor6;
        private System.Windows.Forms.Button btnTileRedo;
        private System.Windows.Forms.Button btnTileUndo;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
    }
}