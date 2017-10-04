using System.Drawing;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// An extension of the standard System.Windows.Forms.UserControl which optimizes settings for drawing.
    /// </summary>
    public class DrawControl : UserControl
    {
        /// <summary>
        /// Initializes an instance of a standard Draw Control
        /// </summary>
        public DrawControl()
        {
            this.DoubleBuffered = true;             //Removes flicker and is smarter than removing OnPaintBackground
            this.BackColor = Color.Black;           //Removes System.Control color, making it visible in the designer.
            this.Size = new Size(0x100, 0x100);     //Common size for things I draw.
        }
    }
}