using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServer.Forms
{
    public partial class frmWaiting : Form
    {
        private Task _task;

        public frmWaiting()
        {
            InitializeComponent();

            this.Cursor = Cursors.WaitCursor;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            Libraries.Forms.SetGradientBackground(this);
        }

        public void SetMessage(string message)
        {
            this.lblMessage.Text = message;
        }

        public void SetTask(Task task)
        {
            this._task = task;
        }

        private void frmWaiting_Shown(object sender, EventArgs e)
        {
            this.lblMessage.Text = "Processing... Please wait a moment...";
        }

        /// <summary>
        /// Prevent close form by hotkey
        /// </summary>
        private void frmWaiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
