using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServer.Libraries;

namespace WebServer.Forms
{
    public partial class frmChangePort : Form
    {
        public event Action<int> OnSaved;

        public bool IsWaiting
        {
            get => !this.panMain.Enabled;
            set => this.panMain.Enabled = !value;
        }

        public int Port
        {
            set => this.txtPort.Text = value.ToString();
        }

        public frmChangePort()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Libraries.Forms.ChangeFont(this, Fonts.Get("Varela Round"));
            Libraries.Forms.SetGradientBackground(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPort.Text, out int new_port))
            {
                this.OnSaved?.Invoke(new_port);
            }
            else
            {
                Dialog.Error("New Port value must be a number");
                this.txtPort.Focus();
            }
        }

        private void txtPort_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(this.btnSave, EventArgs.Empty);
            }
        }
    }
}
