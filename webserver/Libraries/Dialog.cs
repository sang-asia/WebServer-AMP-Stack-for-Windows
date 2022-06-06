using System.Windows.Forms;

namespace WebServer.Libraries
{
    class Dialog
    {
        /// <summary>
        /// Show confirmation dialog with Yes/No buttons
        /// </summary>
        public static DialogResult Confirm(string msg, string title = "Confirmation")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Show info dialog with OK button
        /// </summary>
        public static void Info(string msg, string title = "Notice")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Show info dialog with OK button
        /// </summary>
        public static void Error(string msg, string title = "Error")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
