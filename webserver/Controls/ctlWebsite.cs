using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WebServer.Controls
{
    public partial class ctlWebsite : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click to edit website")]
        public event EventHandler OnEdit;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user click to delete website")]
        public event EventHandler OnDelete;

        private string[] _alias = { };
        private string _domain = "";
        private string _path = "";
        private int _port = 0;
        private int _ssl_port = 0;

        public string[] Alias
        {
            get => this._alias;
            set {
                this._alias = value;
                this.CheckAlias();
            }
        }

        public string Domain
        {
            get => this._domain;
            set
            {
                this._domain = value;
                this.CheckDomainPort();
            }
        }

        public string Path
        {
            get => this._path;
            set
            {
                this._path = value;

                if (value.StartsWith(Program.DIR_WEBSITES))
                {
                    this.lblPath.Text = value.Replace(Program.DIR_WEBSITES, "").Trim(new char[] { '\\', '/' });
                }
                else
                {
                    this.lblPath.Text = value;
                }

                if (this.lblPath.Text.Length > 34)
                {
                    this.lblPath.Text = "..." + this.lblPath.Text.Substring(this.lblPath.Text.Length - 31);
                }
            }
        }

        public string PHPVersion
        {
            get => this.lblPHPVersion.Text;
            set => this.lblPHPVersion.Text = value;
        }

        public int Port
        {
            get => this._port;
            set
            {
                this._port = value;
                this.CheckDomainPort();
            }
        }

        public int SslPort
        {
            get => this._ssl_port;
            set
            {
                this._ssl_port = value;
                this.CheckDomainPort();
            }
        }

        public ctlWebsite()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Libraries.Forms.SetGradientBackground(this);

            this.CheckAlias();
        }

        private void CheckAlias()
        {
            lblAlias.Text = String.Join(", ", this._alias);
            lblAlias.Visible = this._alias.Length > 0;
        }

        private void CheckDomainPort()
        {
            lblDomain.Text = this.GetDomain();
            this.picSSL.Visible = this._ssl_port > 0;
        }

        private string GetDomain()
        {
            if (this._ssl_port > 0)
            {
                return this._domain + (this._ssl_port != 443 ? ":" + this._ssl_port.ToString() : "");
            }
            else
            {
                return this._domain + (this._port != 80 ? ":" + this._port.ToString() : "");
            }
        }

        private string GetUrl()
        {
            if (this._ssl_port > 0)
            {
                return "https://" + this.GetDomain();
            }
            else
            {
                return "http://" + this.GetDomain();
            }
        }

        private void panMain_ClientSizeChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.panMain.Controls)
            {
                c.Width = this.panMain.ClientSize.Width;
            }

            this.Height = panBottom.Top + panBottom.Height + panMain.Top * 2;
        }

        private void lblDomain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.GetUrl());
        }

        private void ctxMenuDelete_Click(object sender, EventArgs e)
        {
            this.OnDelete?.Invoke(this, EventArgs.Empty);
        }

        private void lblPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.Path);
        }

        private void ctxMenuEdit_Click(object sender, EventArgs e)
        {
            this.OnEdit?.Invoke(this.ctxMenuEdit, EventArgs.Empty);
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            btnAction.ContextMenuStrip.Show(
                btnAction.PointToScreen(
                    new Point(-85, btnAction.Height + 5)
                )
            );
        }
    }
}
