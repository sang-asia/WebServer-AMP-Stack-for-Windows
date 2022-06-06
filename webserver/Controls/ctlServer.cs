using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebServer.Libraries;

namespace WebServer.Controls
{
    public partial class ctlServer : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click install button")]
        public event EventHandler OnInstallClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click uninstall button")]
        public event EventHandler OnUninstallClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click start button")]
        public event EventHandler OnStartClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click stop button")]
        public event EventHandler OnStopClick;

        private bool is_installed = false;
        private bool is_downloading = false;
        private bool is_running = false;
        private bool is_waiting = false;
        private bool has_options = false;

        public bool Downloading
        {
            get => is_downloading;
            set {
                is_downloading = value;
                prbPercent.Visible = is_downloading;
                lblPercent.Visible = is_downloading;
            }
        }

        public bool HasOptionsButton
        {
            get => has_options;
            set {
                has_options = value;
                CheckOptionsButton();
            }
        }

        public bool Installed
        {
            get => is_installed;
            set
            {
                is_installed = value;
                btnInstall.Visible = !value;
                panSub.Visible = value;
                Running = is_running;
                CheckOptionsButton();
            }
        }

        public ToolStripItemCollection MenuItems
        {
            set {
                this.ctxMenu.Items.Clear();
                this.ctxMenu.Items.AddRange(value);
            }
        }

        public string Message
        {
            get => lblMessage.Text;
            set => lblMessage.Text = value;
        }

        public string Label
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public bool Only64Bit
        {
            get => pic64bit.Visible;
            set => pic64bit.Visible = value;
        }

        public int Percent
        {
            get => prbPercent.Value;
            set {
                prbPercent.Value = value;
                lblPercent.Text = value.ToString() + "%";
            }
        }

        public int[] Port
        {
            get
            {
                string[] ports = lblPort.Text.Split(',');
                List<int> result = new List<int>();

                foreach (string p in ports)
                {
                    result.Add(int.Parse(p));
                }

                return result.ToArray();
            }
            set
            {
                lblPort.Text = string.Join(", ", value);
                lblPort.Visible = value.Length > 0;
                lblPortText.Visible = value.Length > 0;
            }
        }

        public bool Running
        {
            get => is_installed && is_running;
            set
            {
                is_running = value;
                btnControl.Visible = is_installed;

                if (!is_installed)
                {
                    lblStatus.Text = "Not Installed";
                    btnControl.Text = "...";
                }
                else
                {
                    lblStatus.Text = is_running ? "Running" : "Stopped";
                    lblStatus.ForeColor = is_running ? Color.SeaGreen : Color.Crimson;
                    btnControl.Text = is_running ? "Stop" : "Start";
                    btnControl.BackgroundImage = is_running 
                        ? global::WebServer.Properties.Resources.icons8_stop_circled_16
                        : global::WebServer.Properties.Resources.icons8_circled_play_16;
                }
            }
        }

        public bool Waiting
        {
            get => is_waiting;
            set {
                is_waiting = value;
                btnControl.Enabled = !is_waiting;
                btnInstall.Enabled = !is_waiting;
                btnActions.Enabled = !is_waiting;
            }
        }

        public ctlServer()
        {
            InitializeComponent();
        }

        private void ctlServer_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Height != 71)
            {
                this.Size = new Size(this.Size.Width, 71);
            }
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            if (is_running)
            {
                OnStopClick?.Invoke(this, e);
            }
            else
            {
                OnStartClick?.Invoke(this, e);
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!this.Installed)
            {
                this.OnInstallClick?.Invoke(this, e);
            }
        }

        private void ctxMenuUninstall_Click(object sender, EventArgs e)
        {
            if (this.Installed)
            {
                if (Dialog.Confirm("Are you sure you want to uninstall " + lblName.Text) == DialogResult.Yes)
                {
                    this.OnUninstallClick?.Invoke(this, e);
                }
            }
        }

        private void btnActions_Click(object sender, EventArgs e)
        {
            if (btnActions.ContextMenuStrip.Tag == null)
            {
                btnActions.ContextMenuStrip.Tag = "Shown";
                btnActions.ContextMenuStrip.Show(
                    btnActions.PointToScreen(
                        new Point(-(btnActions.ContextMenuStrip.Width - btnActions.Width), btnActions.Height + 5)
                    )
                );
            }
            else
            {
                btnActions.ContextMenuStrip.Close();
            }
        }

        private void CheckOptionsButton()
        {
            btnActions.Visible = is_installed && has_options;
        }

        private void ctxMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ctxMenu.Tag = null;
        }
    }
}
