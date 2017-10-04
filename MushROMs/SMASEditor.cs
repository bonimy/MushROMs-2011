using System;
using MushROMs.Controls;

namespace MushROMs
{
    public partial class SMASEditor : EditorForm
    {
        /// <summary>
        /// The SMB1 Editor Form.
        /// </summary>
        SMB1.SMB1Editor SMB1Editor;

        /// <summary>
        /// Initializes a new instance of a MushROMs.SMASEditor with the given command line arguments
        /// </summary>
        /// <param name="args">
        /// Command line arguments from Main().
        /// </param>
        public SMASEditor(string[] args)
        {
            InitializeComponent();
        }

        private void btnSMB1_Click(object sender, EventArgs e)
        {
            this.SMB1Editor = new MushROMs.SMB1.SMB1Editor(this.Handle);
            this.SMB1Editor.Show();
            this.Hide();
        }
    }
}