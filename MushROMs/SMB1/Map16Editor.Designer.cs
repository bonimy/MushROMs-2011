namespace MushROMs.SMB1
{
    partial class Map16Editor
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
                    Parent.Map16EditorVisible = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map16Editor));
            this.drwMap16 = new MushROMs.Controls.DrawControl();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbxProperties = new System.Windows.Forms.GroupBox();
            this.drawControl2 = new MushROMs.Controls.DrawControl();
            this.btnFlipY8 = new System.Windows.Forms.Button();
            this.btnFlipX8 = new System.Windows.Forms.Button();
            this.btnEdit8 = new System.Windows.Forms.Button();
            this.btnFlipY16 = new System.Windows.Forms.Button();
            this.btnFlipX16 = new System.Windows.Forms.Button();
            this.btnEdit16 = new System.Windows.Forms.Button();
            this.drawControl1 = new MushROMs.Controls.DrawControl();
            this.stsMain.SuspendLayout();
            this.gbxProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // drwMap16
            // 
            this.drwMap16.BackColor = System.Drawing.Color.Black;
            this.drwMap16.Location = new System.Drawing.Point(0, 0);
            this.drwMap16.Name = "drwMap16";
            this.drwMap16.Size = new System.Drawing.Size(256, 256);
            this.drwMap16.TabIndex = 0;
            this.drwMap16.Paint += new System.Windows.Forms.PaintEventHandler(this.drwMap16_Paint);
            this.drwMap16.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwMap16_MouseClick);
            this.drwMap16.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwMap16_MouseMove);
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 348);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(256, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 1;
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(16, 17);
            this.tssMain.Text = "...";
            // 
            // gbxProperties
            // 
            this.gbxProperties.Controls.Add(this.drawControl2);
            this.gbxProperties.Controls.Add(this.btnFlipY8);
            this.gbxProperties.Controls.Add(this.btnFlipX8);
            this.gbxProperties.Controls.Add(this.btnEdit8);
            this.gbxProperties.Controls.Add(this.btnFlipY16);
            this.gbxProperties.Controls.Add(this.btnFlipX16);
            this.gbxProperties.Controls.Add(this.btnEdit16);
            this.gbxProperties.Controls.Add(this.drawControl1);
            this.gbxProperties.Location = new System.Drawing.Point(0, 263);
            this.gbxProperties.Name = "gbxProperties";
            this.gbxProperties.Size = new System.Drawing.Size(256, 80);
            this.gbxProperties.TabIndex = 2;
            this.gbxProperties.TabStop = false;
            this.gbxProperties.Text = "Tile Properties";
            // 
            // drawControl2
            // 
            this.drawControl2.BackColor = System.Drawing.Color.Black;
            this.drawControl2.Location = new System.Drawing.Point(10, 57);
            this.drawControl2.Name = "drawControl2";
            this.drawControl2.Size = new System.Drawing.Size(8, 8);
            this.drawControl2.TabIndex = 7;
            // 
            // btnFlipY8
            // 
            this.btnFlipY8.Location = new System.Drawing.Point(202, 49);
            this.btnFlipY8.Name = "btnFlipY8";
            this.btnFlipY8.Size = new System.Drawing.Size(48, 23);
            this.btnFlipY8.TabIndex = 6;
            this.btnFlipY8.Text = "Flip Y";
            this.btnFlipY8.UseVisualStyleBackColor = true;
            // 
            // btnFlipX8
            // 
            this.btnFlipX8.Location = new System.Drawing.Point(148, 49);
            this.btnFlipX8.Name = "btnFlipX8";
            this.btnFlipX8.Size = new System.Drawing.Size(48, 23);
            this.btnFlipX8.TabIndex = 5;
            this.btnFlipX8.Text = "Flip X";
            this.btnFlipX8.UseVisualStyleBackColor = true;
            // 
            // btnEdit8
            // 
            this.btnEdit8.Location = new System.Drawing.Point(28, 49);
            this.btnEdit8.Name = "btnEdit8";
            this.btnEdit8.Size = new System.Drawing.Size(114, 23);
            this.btnEdit8.TabIndex = 4;
            this.btnEdit8.Text = "Edit 8x8 Attributes";
            this.btnEdit8.UseVisualStyleBackColor = true;
            // 
            // btnFlipY16
            // 
            this.btnFlipY16.Location = new System.Drawing.Point(202, 19);
            this.btnFlipY16.Name = "btnFlipY16";
            this.btnFlipY16.Size = new System.Drawing.Size(48, 23);
            this.btnFlipY16.TabIndex = 3;
            this.btnFlipY16.Text = "Flip Y";
            this.btnFlipY16.UseVisualStyleBackColor = true;
            // 
            // btnFlipX16
            // 
            this.btnFlipX16.Location = new System.Drawing.Point(148, 19);
            this.btnFlipX16.Name = "btnFlipX16";
            this.btnFlipX16.Size = new System.Drawing.Size(48, 23);
            this.btnFlipX16.TabIndex = 2;
            this.btnFlipX16.Text = "Flip X";
            this.btnFlipX16.UseVisualStyleBackColor = true;
            // 
            // btnEdit16
            // 
            this.btnEdit16.Location = new System.Drawing.Point(28, 19);
            this.btnEdit16.Name = "btnEdit16";
            this.btnEdit16.Size = new System.Drawing.Size(114, 23);
            this.btnEdit16.TabIndex = 1;
            this.btnEdit16.Text = "Edit 16x16 Attributes";
            this.btnEdit16.UseVisualStyleBackColor = true;
            // 
            // drawControl1
            // 
            this.drawControl1.BackColor = System.Drawing.Color.Black;
            this.drawControl1.Location = new System.Drawing.Point(6, 22);
            this.drawControl1.Name = "drawControl1";
            this.drawControl1.Size = new System.Drawing.Size(16, 16);
            this.drawControl1.TabIndex = 0;
            // 
            // Map16Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 370);
            this.Controls.Add(this.gbxProperties);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.drwMap16);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Map16Editor";
            this.ShowInTaskbar = false;
            this.Text = "Map16 Editor";
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.gbxProperties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MushROMs.Controls.DrawControl drwMap16;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private System.Windows.Forms.GroupBox gbxProperties;
        private Controls.DrawControl drawControl2;
        private System.Windows.Forms.Button btnFlipY8;
        private System.Windows.Forms.Button btnFlipX8;
        private System.Windows.Forms.Button btnEdit8;
        private System.Windows.Forms.Button btnFlipY16;
        private System.Windows.Forms.Button btnFlipX16;
        private System.Windows.Forms.Button btnEdit16;
        private Controls.DrawControl drawControl1;
    }
}