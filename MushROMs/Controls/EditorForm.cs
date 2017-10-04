using System.Drawing;
using System.Windows.Forms;
using LC_Sharp;

namespace MushROMs.Controls
{
    public class EditorForm : Form
    {
        public const string WindowTitle = "MushROMs";

        /// <summary>
        /// Path for finding Lunar Compress.dll
        /// </summary>
        public const string LunarDLLPath = LunarCompress.LunarDLLPath;
        /// <summary>
        /// Editors and sub-editors may work with file. This string specifies the common directory for said files.
        /// </summary>
        public string BaseDirectory;

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Allow keyboard support for certain keys.
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                    return false;
                default:
                    return base.ProcessDialogKey(keyData);
            }
        }

        /// <summary>
        /// Examines a point and determines if it is in the specified boundary.
        /// </summary>
        /// <param name="location">
        /// The location to examine.
        /// </param>
        /// <param name="bounds">
        /// A rectangular boundary.
        /// </param>
        /// <returns>
        /// True if the point is in the boundary. False otherwise.
        /// </returns>
        public static bool IsInBounds(Point location, Rectangle bounds)
        {
            return location.X >= bounds.X && location.X < bounds.Width && location.Y >= bounds.Y && location.Y < bounds.Height;
        }
    }
}