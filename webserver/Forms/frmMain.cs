using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServer.Apps;
using WebServer.Libraries;
using WebServer.Libraries.Config;

namespace WebServer.Forms
{
    public partial class frmMain : Form
    {
        private event EventHandler OnWebsiteConfigChanged;
        private readonly AppApache appApache;
        private readonly AppMariaDB appMariaDB;
        private readonly AppMySQL appMySQL;
        private readonly AppMongoDB appMongoDB;
        private readonly frmPHP frmPHP = new frmPHP();
        private Dictionary<string, List<WebsiteConfig>> websites;
        private bool need_reload = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Text = Program.NAME;

            Logger.OnLog += this.LogEventHandler;

            Libraries.Forms.ChangeFont(this, Fonts.Get("Varela Round"));
            Libraries.Forms.SetGradientBackground(this);

            this.appApache = new AppApache();
            this.appMariaDB = new AppMariaDB();
            this.appMySQL = new AppMySQL();
            this.appMongoDB = new AppMongoDB();

            this.appApache.OnError += new AppEventHandler(this.AppErrorHandler);
            this.appMariaDB.OnError += new AppEventHandler(this.AppErrorHandler);
            this.appMySQL.OnError += new AppEventHandler(this.AppErrorHandler);
            this.appMongoDB.OnError += new AppEventHandler(this.AppErrorHandler);
            this.appApache.OnPortChanged += (AppBase s, AppEventArgs e) => this.need_reload = true;

            new AppBridge(this.appApache, this.srvApache);
            new AppBridge(this.appMariaDB, this.srvMariaDB);
            new AppBridge(this.appMySQL, this.srvMySQL);
            new AppBridge(this.appMongoDB, this.srvMongoDB);

            this.OnWebsiteConfigChanged += new EventHandler((object s, EventArgs e) =>
            {
                if (this.appApache.IsInstalled() && this.appApache.GetWorkingState() == EWorkingState.Started)
                {
                    this.AskToRestartApache();
                }
            });
        }

        /// <summary>
        /// Write log content to textbox
        /// </summary>
        private void LogEventHandler(Logger s, LogEventArgs e)
        {
            Libraries.Forms.InvokeControl(this.txtLog, () =>
            {
                this.txtLog.SelectionStart = this.txtLog.TextLength;
                this.txtLog.SelectionLength = 0;
                this.txtLog.SelectedRtf = e.ToRtf();
                this.txtLog.AppendText("\r\n");
                this.txtLog.SelectionStart = this.txtLog.TextLength;
                this.txtLog.ScrollToCaret();
            });
        }

        /// <summary>
        /// Handle App's errors
        /// </summary>
        private void AppErrorHandler(AppBase s, AppEventArgs e) {
            Dialog.Error(e.GetMessage());
        }

        /// <summary>
        /// Clear websites list
        /// </summary>
        private void ClearWebsites()
        {
            this.SuspendLayout();

            /*for (int i = this.panWebsites.Controls.Count - 1; i > 0; i--)
            {
                if (this.panWebsites.Controls[i] is Controls.ctlWebsite)
                {
                    Libraries.Forms.InvokeControl(this.panWebsites, () =>
                    {
                        this.panWebsites.Controls.RemoveAt(i);
                    });
                }
            }*/

            this.panWebsites.Controls.Clear();

            this.ResumeLayout();
        }

        /// <summary>
        /// Get Websites list
        /// </summary>
        private bool GetWebsites()
        {
            try
            {
                this.websites = WebsiteConfig.GetWebsites(Program.DIR_WEBSITES, this.appApache.GetHttpPort(), this.appApache.GetHttpsPort());
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.StackTrace);
                Dialog.Error(ex.Message);
                return false;
            }

            try
            {
                WebsiteConfig.ValidateConfig(this.websites, Program.DIR_WEBSITES, this.appApache.GetHttpPort(), this.appApache.GetHttpsPort());
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete a website
        /// </summary>
        private void DeleteWebsite(WebsiteConfig website, frmWebsite f = null)
        {
            if (Dialog.Confirm("Are you sure you want to delete this website?") != DialogResult.Yes)
            {
                return;
            }

            if (f != null)
            {
                f.IsWaiting = true;
            }

            Task.Factory.StartNew(() =>
            {
                string dir = Path.Combine(Program.DIR_WEBSITES, website.Name);

                if (!FileSystem.Exists(dir))
                {
                    Dialog.Error("Path " + dir + " not found");

                    if (f != null)
                    {
                        f.Close();
                    }

                    return;
                }

                Libraries.Forms.InvokeControl(this, () => {
                    this.panWebsites.Enabled = false;
                    this.btnCreateWebsite.Enabled = false;
                    this.btnPHP.Enabled = false;
                });

                HostsConfig config_hosts = HostsConfig.Get();

                if (config_hosts.Exists(website.ServerName))
                {
                    config_hosts.Remove(website.ServerName);
                    config_hosts.Write(HostsConfig.FILE_HOSTS);
                }

                bool need_restart = this.appApache.IsInstalled() && this.appApache.GetWorkingState() == EWorkingState.Started;

                if (need_restart)
                {
                    this.appApache.Stop();
                }

                try
                {
                    FileSystem.Delete(dir);
                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message);
                }

                if (need_restart)
                {
                    this.appApache.Start();
                }

                if (f != null)
                {
                    Libraries.Forms.InvokeControl(f, () =>
                    {
                        f.Close();
                    });
                }

                Libraries.Forms.InvokeControl(this, () =>
                {
                    this.need_reload = true;
                    this.panWebsites.Enabled = true;
                    this.btnCreateWebsite.Enabled = true;
                    this.btnPHP.Enabled = true;
                });

                Dialog.Info("Website deleted");
            });
        }

        /// <summary>
        /// Reload websites list from the config files
        /// </summary>
        private void ReloadWebsites()
        {
            Libraries.Forms.InvokeControl(this.btnRefresh, () => {
                this.btnRefresh.Enabled = false;
            });

            this.SuspendLayout();
            this.ClearWebsites();

            if (!this.GetWebsites())
            {
                return;
            }

            foreach (KeyValuePair<string, List<WebsiteConfig>> file in this.websites)
            {
                foreach (WebsiteConfig website in file.Value)
                {
                    Controls.ctlWebsite ctl = new Controls.ctlWebsite
                    {
                        Alias = website.ServerAliases.ToArray(),
                        Domain = website.ServerName,
                        Path = website.DocumentRoot,
                        PHPVersion = website.PHPVersion,
                        Port = website.Port,
                        SslPort = website.HasSsl() ? website.SslPort : 0,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        Width = this.panWebsites.ClientSize.Width
                    };

                    ctl.OnEdit += new EventHandler((object s, EventArgs e) => {
                        if (Directory.Exists(Path.Combine(Program.DIR_WEBSITES, website.Name)))
                        {
                            frmWebsite f = new frmWebsite();
                            f.Config = website;
                            f.Port = this.appApache.GetHttpPort();
                            f.SslPort = this.appApache.GetHttpsPort();

                            f.OnSaved += new EventHandler((object s1, EventArgs e1) =>
                            {
                                ctl.Alias = website.ServerAliases.ToArray();
                                ctl.Domain = website.ServerName;
                                ctl.Path = website.DocumentRoot;
                                ctl.PHPVersion = website.PHPVersion;
                                ctl.Port = website.Port;
                                ctl.SslPort = website.HasSsl() ? website.SslPort : 0;
                                this.OnWebsiteConfigChanged?.Invoke(this, EventArgs.Empty);
                            });

                            f.OnDelete += new EventHandler((object s1, EventArgs e1) =>
                            {
                                this.DeleteWebsite(website, f);
                            });

                            f.ShowDialog(this);
                        }
                        else
                        {
                            Libraries.Forms.InvokeControl(this.panWebsites, () =>
                            {
                                this.need_reload = true;
                            });

                            Dialog.Error("This website folder does not exist any more");
                        }
                    });

                    ctl.OnDelete += new EventHandler((object s1, EventArgs e1) => {
                        this.DeleteWebsite(website);
                    });

                    Libraries.Forms.InvokeControl(this.panWebsites, () =>
                    {
                        this.panWebsites.Controls.Add(ctl);
                    });
                }
            }

            Libraries.Forms.InvokeControl(this.panWebsites, () =>
            {
                for (int i = this.panWebsites.Controls.Count - 1; i >= 0; i--)
                {
                    this.panWebsites.Controls[i].Dock = DockStyle.Top;
                }
            });

            this.ResumeLayout();

            Libraries.Forms.InvokeControl(this.btnRefresh, () =>
            {
                this.btnRefresh.Enabled = true;
            });
        }

        /// <summary>
        /// Ask user to restart Apache
        /// </summary>
        private void AskToRestartApache()
        {
            if (Dialog.Confirm("Do you want to restart Apache to apply new configuration?") == DialogResult.Yes)
            {
                Task.Factory.StartNew(() =>
                {
                    this.appApache.RestartService();
                });
            }
        }

        /// <summary>
        /// Trigger form to manage PHP versions
        /// </summary>
        private void btnPHP_Click(object sender, System.EventArgs e)
        {
            this.frmPHP.ShowDialog();
        }

        /// <summary>
        /// Trigger form to create new website
        /// </summary>
        private void btnCreateWebsite_Click(object sender, System.EventArgs e)
        {
            frmWebsite f = new frmWebsite();

            f.OnCreated += new EventHandler((object s1, EventArgs e1) =>
            {
                this.need_reload = true;
                this.OnWebsiteConfigChanged?.Invoke(this, EventArgs.Empty);
            });

            f.Port = this.appApache.GetHttpPort();
            f.SslPort = this.appApache.GetHttpsPort();
            f.ShowDialog();
        }

        /// <summary>
        /// Trigger refresh websites list
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.need_reload = true;
        }

        /// <summary>
        /// Checker
        /// </summary>
        private void timChecker_Tick(object sender, EventArgs e)
        {
            timChecker.Enabled = false;

            if (this.need_reload)
            {
                this.need_reload = false;
                this.ReloadWebsites();
            }

            timChecker.Enabled = true;
        }
    }
}
