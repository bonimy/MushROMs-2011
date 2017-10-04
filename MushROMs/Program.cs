using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MushROMs.Controls;
using LC_Sharp;

namespace MushROMs
{
    static class Program
    {
        #region Constant Strings
        private const string WindowTitle = EditorForm.WindowTitle;
        private const string GameUnknown = "Unknown editor specified. Switching to SMAS Editor for safety.";
        private const string NoLunarCompress = "Could not load editor. Lunar Compress needed for functionality.";
        #endregion

        /// <summary>
        /// Specifies which game the editor will start with at run time.
        /// </summary>
        private static Games StartGame = Games.SMB1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static unsafe void Main(string[] args)
        {
            if (LunarCompress.CheckLunarDLL())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Starts the specific editor depending on which game is set at run time.
                switch (Program.StartGame)
                {
                    case Games.SMAS:
                        Application.Run(new MushROMs.SMASEditor(args));
                        break;
                    case Games.SMB1:
                        Application.Run(new MushROMs.SMB1.SMB1Editor(args));
                        break;
                    case Games.SMB2J:
                    case Games.SMB2U:
                    case Games.SMB3:
                    case Games.SMW:
                    default:
                        MessageBox.Show(Program.GameUnknown);
                        Program.StartGame = Games.SMAS;
                        Application.Run(new MushROMs.SMASEditor(args));
                        break;
                }
            }
            else
                MessageBox.Show(Program.NoLunarCompress, Program.WindowTitle);
        }
    }

    /// <summary>
    /// Specifies constants defining which All-Stars games is selected.
    /// </summary>
    public enum Games
    {
        /// <summary>
        /// Super Mario All-Stars. This normally encapsulates the rest of the game not specific to the games themselves.
        /// </summary>
        SMAS = 0,
        /// <summary>
        /// Super Mario Bros.
        /// </summary>
        SMB1 = 1,
        /// <summary>
        /// Super Mario Bros: The Lost Levels (or Super Mario Bros. 2 in Japan).
        /// </summary>
        SMB2J = 2,
        /// <summary>
        /// Super Mario Bros. 2 US.
        /// </summary>
        SMB2U = 3,
        /// <summary>
        /// Super Mario Bros. 3
        /// </summary>
        SMB3 = 4,
        /// <summary>
        /// Super Mario World (when applicable).
        /// </summary>
        SMW = 5
    }
}