namespace MushROMs.SMB1
{
    partial class ObjectSelector
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
                    Parent.ObjectSelectorVisible = false;
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
            this.drwSelectedObject = new MushROMs.Controls.DrawControl();
            this.lbxObject = new System.Windows.Forms.ListBox();
            this.cbxObjectType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // drwSelectedObject
            // 
            this.drwSelectedObject.BackColor = System.Drawing.Color.Black;
            this.drwSelectedObject.Location = new System.Drawing.Point(0, 0);
            this.drwSelectedObject.Name = "drwSelectedObject";
            this.drwSelectedObject.Size = new System.Drawing.Size(256, 256);
            this.drwSelectedObject.TabIndex = 0;
            this.drwSelectedObject.Paint += new System.Windows.Forms.PaintEventHandler(this.drwSelectedObject_Paint);
            this.drwSelectedObject.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwSelectedObject_MouseMove);
            // 
            // lbxObject
            // 
            this.lbxObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxObject.FormattingEnabled = true;
            this.lbxObject.Location = new System.Drawing.Point(0, 280);
            this.lbxObject.Name = "lbxObject";
            this.lbxObject.Size = new System.Drawing.Size(256, 134);
            this.lbxObject.TabIndex = 1;
            this.lbxObject.SelectedIndexChanged += new System.EventHandler(this.lbxObject_SelectedIndexChanged);
            // 
            // cbxObjectType
            // 
            this.cbxObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxObjectType.FormattingEnabled = true;
            this.cbxObjectType.Items.AddRange(new object[] {
            "Standard Objects",
            "Direct Map16",
            "Expandable Objects (Linear)",
            "Expandable Objects (Rectangular)",
            "Ground Objects",
            "Misc. Objects",
            "Scenery Objects"});
            this.cbxObjectType.Location = new System.Drawing.Point(0, 256);
            this.cbxObjectType.Name = "cbxObjectType";
            this.cbxObjectType.Size = new System.Drawing.Size(256, 24);
            this.cbxObjectType.TabIndex = 2;
            this.cbxObjectType.SelectedIndexChanged += new System.EventHandler(this.cbxObjectType_SelectedIndexChanged);
            // 
            // ObjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 414);
            this.Controls.Add(this.cbxObjectType);
            this.Controls.Add(this.lbxObject);
            this.Controls.Add(this.drwSelectedObject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Object Selector";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private MushROMs.Controls.DrawControl drwSelectedObject;
        private System.Windows.Forms.ListBox lbxObject;
        private System.Windows.Forms.ComboBox cbxObjectType;
    }
}