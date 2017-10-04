namespace MushROMs.SMB1
{
    partial class PaletteEditor
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
                    Parent.PaletteEditorVisible = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteEditor));
            this.drwPalette = new MushROMs.Controls.DrawControl();
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblPC = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSNES = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPalette = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxActions = new System.Windows.Forms.GroupBox();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbxPlayer = new System.Windows.Forms.GroupBox();
            this.rdbFireLuigi = new System.Windows.Forms.RadioButton();
            this.rdbFireMario = new System.Windows.Forms.RadioButton();
            this.rdbLuigi = new System.Windows.Forms.RadioButton();
            this.rdbMario = new System.Windows.Forms.RadioButton();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.drwColor = new MushROMs.Controls.DrawControl();
            this.gbxStatus.SuspendLayout();
            this.gbxActions.SuspendLayout();
            this.gbxPlayer.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // drwPalette
            // 
            this.drwPalette.AllowDrop = true;
            this.drwPalette.BackColor = System.Drawing.SystemColors.Control;
            this.drwPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwPalette.Location = new System.Drawing.Point(0, 0);
            this.drwPalette.Name = "drwPalette";
            this.drwPalette.Size = new System.Drawing.Size(258, 258);
            this.drwPalette.TabIndex = 0;
            this.drwPalette.DragDrop += new System.Windows.Forms.DragEventHandler(this.drwPalette_DragDrop);
            this.drwPalette.DragEnter += new System.Windows.Forms.DragEventHandler(this.drwPalette_DragEnter);
            this.drwPalette.DragLeave += new System.EventHandler(this.drwPalette_DragLeave);
            this.drwPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.drwPalette_Paint);
            this.drwPalette.Enter += new System.EventHandler(this.drwPalette_Enter);
            this.drwPalette.Leave += new System.EventHandler(this.drwPalette_Enter);
            this.drwPalette.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseClick);
            this.drwPalette.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseMove);
            // 
            // gbxStatus
            // 
            this.gbxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxStatus.Controls.Add(this.lblColor);
            this.gbxStatus.Controls.Add(this.label4);
            this.gbxStatus.Controls.Add(this.lblBlue);
            this.gbxStatus.Controls.Add(this.lblGreen);
            this.gbxStatus.Controls.Add(this.lblRed);
            this.gbxStatus.Controls.Add(this.lblPC);
            this.gbxStatus.Controls.Add(this.label3);
            this.gbxStatus.Controls.Add(this.lblSNES);
            this.gbxStatus.Controls.Add(this.label2);
            this.gbxStatus.Controls.Add(this.lblPalette);
            this.gbxStatus.Controls.Add(this.label1);
            this.gbxStatus.Location = new System.Drawing.Point(264, 12);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(159, 102);
            this.gbxStatus.TabIndex = 999;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Status";
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.Location = new System.Drawing.Point(82, 35);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(16, 16);
            this.lblColor.TabIndex = 0;
            this.lblColor.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Color: ";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlue.ForeColor = System.Drawing.Color.Blue;
            this.lblBlue.Location = new System.Drawing.Point(82, 83);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(32, 16);
            this.lblBlue.TabIndex = 0;
            this.lblBlue.Text = "255";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreen.ForeColor = System.Drawing.Color.Green;
            this.lblGreen.Location = new System.Drawing.Point(44, 83);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(32, 16);
            this.lblGreen.TabIndex = 0;
            this.lblGreen.Text = "255";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRed.ForeColor = System.Drawing.Color.Red;
            this.lblRed.Location = new System.Drawing.Point(6, 83);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(32, 16);
            this.lblRed.TabIndex = 0;
            this.lblRed.Text = "255";
            // 
            // lblPC
            // 
            this.lblPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPC.AutoSize = true;
            this.lblPC.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPC.Location = new System.Drawing.Point(82, 51);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(72, 16);
            this.lblPC.TabIndex = 0;
            this.lblPC.Text = "0x000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "PC Value: ";
            // 
            // lblSNES
            // 
            this.lblSNES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSNES.AutoSize = true;
            this.lblSNES.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSNES.Location = new System.Drawing.Point(82, 67);
            this.lblSNES.Name = "lblSNES";
            this.lblSNES.Size = new System.Drawing.Size(56, 16);
            this.lblSNES.TabIndex = 0;
            this.lblSNES.Text = "0x0000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "SNES Value: ";
            // 
            // lblPalette
            // 
            this.lblPalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalette.AutoSize = true;
            this.lblPalette.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalette.Location = new System.Drawing.Point(82, 19);
            this.lblPalette.Name = "lblPalette";
            this.lblPalette.Size = new System.Drawing.Size(16, 16);
            this.lblPalette.TabIndex = 0;
            this.lblPalette.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Palette: ";
            // 
            // gbxActions
            // 
            this.gbxActions.Controls.Add(this.btnRedo);
            this.gbxActions.Controls.Add(this.btnUndo);
            this.gbxActions.Controls.Add(this.btnSaveToFile);
            this.gbxActions.Controls.Add(this.btnLoadFromFile);
            this.gbxActions.Controls.Add(this.btnSave);
            this.gbxActions.Location = new System.Drawing.Point(12, 264);
            this.gbxActions.Name = "gbxActions";
            this.gbxActions.Size = new System.Drawing.Size(411, 48);
            this.gbxActions.TabIndex = 3;
            this.gbxActions.TabStop = false;
            this.gbxActions.Text = "Actions";
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(347, 19);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(58, 23);
            this.btnRedo.TabIndex = 4;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(289, 19);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(52, 23);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(196, 19);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(87, 23);
            this.btnSaveToFile.TabIndex = 2;
            this.btnSaveToFile.Text = "Save To File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            this.btnSaveToFile.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(102, 19);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(88, 23);
            this.btnLoadFromFile.TabIndex = 1;
            this.btnLoadFromFile.Text = "Load From File";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            this.btnLoadFromFile.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Palette";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // gbxPlayer
            // 
            this.gbxPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxPlayer.Controls.Add(this.rdbFireLuigi);
            this.gbxPlayer.Controls.Add(this.rdbFireMario);
            this.gbxPlayer.Controls.Add(this.rdbLuigi);
            this.gbxPlayer.Controls.Add(this.rdbMario);
            this.gbxPlayer.Location = new System.Drawing.Point(264, 120);
            this.gbxPlayer.Name = "gbxPlayer";
            this.gbxPlayer.Size = new System.Drawing.Size(159, 66);
            this.gbxPlayer.TabIndex = 1;
            this.gbxPlayer.TabStop = false;
            this.gbxPlayer.Text = "Player Palette:";
            // 
            // rdbFireLuigi
            // 
            this.rdbFireLuigi.AutoSize = true;
            this.rdbFireLuigi.Location = new System.Drawing.Point(86, 43);
            this.rdbFireLuigi.Name = "rdbFireLuigi";
            this.rdbFireLuigi.Size = new System.Drawing.Size(67, 17);
            this.rdbFireLuigi.TabIndex = 3;
            this.rdbFireLuigi.Text = "Fire Luigi";
            this.rdbFireLuigi.UseVisualStyleBackColor = true;
            this.rdbFireLuigi.CheckedChanged += new System.EventHandler(this.rdbPlayer_CheckedChanged);
            this.rdbFireLuigi.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // rdbFireMario
            // 
            this.rdbFireMario.AutoSize = true;
            this.rdbFireMario.Location = new System.Drawing.Point(6, 43);
            this.rdbFireMario.Name = "rdbFireMario";
            this.rdbFireMario.Size = new System.Drawing.Size(71, 17);
            this.rdbFireMario.TabIndex = 2;
            this.rdbFireMario.Text = "Fire Mario";
            this.rdbFireMario.UseVisualStyleBackColor = true;
            this.rdbFireMario.CheckedChanged += new System.EventHandler(this.rdbPlayer_CheckedChanged);
            this.rdbFireMario.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // rdbLuigi
            // 
            this.rdbLuigi.AutoSize = true;
            this.rdbLuigi.Location = new System.Drawing.Point(86, 20);
            this.rdbLuigi.Name = "rdbLuigi";
            this.rdbLuigi.Size = new System.Drawing.Size(47, 17);
            this.rdbLuigi.TabIndex = 1;
            this.rdbLuigi.Text = "Luigi";
            this.rdbLuigi.UseVisualStyleBackColor = true;
            this.rdbLuigi.CheckedChanged += new System.EventHandler(this.rdbPlayer_CheckedChanged);
            this.rdbLuigi.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // rdbMario
            // 
            this.rdbMario.AutoSize = true;
            this.rdbMario.Checked = true;
            this.rdbMario.Location = new System.Drawing.Point(7, 20);
            this.rdbMario.Name = "rdbMario";
            this.rdbMario.Size = new System.Drawing.Size(51, 17);
            this.rdbMario.TabIndex = 0;
            this.rdbMario.TabStop = true;
            this.rdbMario.Text = "Mario";
            this.rdbMario.UseVisualStyleBackColor = true;
            this.rdbMario.CheckedChanged += new System.EventHandler(this.rdbPlayer_CheckedChanged);
            this.rdbMario.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 315);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(435, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 0;
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(112, 17);
            this.tssMain.Text = "Palette Editor Ready";
            // 
            // drwColor
            // 
            this.drwColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drwColor.BackColor = System.Drawing.SystemColors.Control;
            this.drwColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor.Location = new System.Drawing.Point(264, 240);
            this.drwColor.Name = "drwColor";
            this.drwColor.Size = new System.Drawing.Size(18, 18);
            this.drwColor.TabIndex = 2;
            this.drwColor.Click += new System.EventHandler(this.drwColor_Click);
            this.drwColor.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            this.drwColor.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // PaletteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 337);
            this.Controls.Add(this.drwColor);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.gbxPlayer);
            this.Controls.Add(this.gbxActions);
            this.Controls.Add(this.gbxStatus);
            this.Controls.Add(this.drwPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Palette Editor";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaletteEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PaletteEditor_KeyUp);
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.gbxActions.ResumeLayout(false);
            this.gbxPlayer.ResumeLayout(false);
            this.gbxPlayer.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MushROMs.Controls.DrawControl drwPalette;
        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSNES;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPalette;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.GroupBox gbxPlayer;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private MushROMs.Controls.DrawControl drwColor;
        private System.Windows.Forms.RadioButton rdbFireMario;
        private System.Windows.Forms.RadioButton rdbLuigi;
        private System.Windows.Forms.RadioButton rdbMario;
        private System.Windows.Forms.RadioButton rdbFireLuigi;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
    }
}