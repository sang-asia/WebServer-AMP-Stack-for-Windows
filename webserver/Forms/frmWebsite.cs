using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using WebServer.Libraries;
using WebServer.Libraries.Config;
using WebServer.Libraries.PhpDotNet;

namespace WebServer.Forms
{
    public partial class frmWebsite : Form
    {
        public event EventHandler OnCreated;
        public event EventHandler OnDelete;
        public event EventHandler OnSaved;

        private WebsiteConfig _config = new WebsiteConfig();
        private HostsConfig _config_hosts = HostsConfig.Get();
        private bool _is_new = true;
        private readonly Manager _php = new Manager();
        private int _port = 80;
        private int _ssl_port = 443;
        private Dictionary<Version, Install> _versions;

        public WebsiteConfig Config
        {
            get => this._config;
            set
            {
                this._config = value;

                if (value != null)
                {
                    this._is_new = false;
                    this.txtName.Text = value.Name;
                    this.txtPublicPath.Text = value.PublicPath;
                    this.txtDomain.Text = value.ServerName;
                    this.btnDelete.Visible = true;
                    this.chkUseHosts.Checked = this._config_hosts.Exists(value.ServerName);
                    this.chkSSL.Checked = value.HasSsl();

                    if (value.External)
                    {
                        this.txtFolder.Text = value.BaseDirectory;
                    }

                    this.SelectPHPComboBox();
                }
            }
        }

        public bool IsWaiting
        {
            get => !this.panMain.Enabled;
            set => this.panMain.Enabled = !value;
        }

        public int Port
        {
            get => this._port;
            set => this._port = value;
        }

        public int SslPort
        {
            get => this._ssl_port;
            set => this._ssl_port = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public frmWebsite()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Libraries.Forms.ChangeFont(this, Fonts.Get("Varela Round"));
            Libraries.Forms.SetGradientBackground(this);

            // Get PHP versions
            this._php.RefreshInstalls();
            this._versions = this._php.Installs;

            this._config_hosts = HostsConfig.Get();
            cboPHP.Items.Add("");

            foreach (KeyValuePair<Version, Install> v in this._versions)
            {
                cboPHP.Items.Add(v.Key.ToString(3));
            }

            this.cboPHP_SelectedIndexChanged(this.cboPHP, EventArgs.Empty);
            this.GetPHPConfigs();
        }

        /// <summary>
        /// Select combo PHP versions
        /// </summary>
        private void SelectPHPComboBox()
        {
            for (int i = 0; i < this.cboPHP.Items.Count; i++)
            {
                if (this.cboPHP.Items[i].ToString() == this._config.PHPVersion)
                {
                    this.cboPHP.SelectedItem = this.cboPHP.Items[i];
                    break;
                }
            }
        }

        /// <summary>
        /// Check a PHP config
        /// </summary>
        private void GetPHPConfig(TextBox ctl, string directive)
        {
            if (this._config.PHPConfig.Exists(directive))
            {
                ctl.Text = this._config.PHPConfig.Get(directive)[0].Value;
            }
        }

        /// <summary>
        /// Check PHP Configs
        /// </summary>
        private void GetPHPConfigs()
        {
            this.GetPHPConfig(this.txtMaxUpload, "upload_max_filesize");
            this.GetPHPConfig(this.txtPostMaxSize, "post_max_size");
            this.GetPHPConfig(this.txtTimeout, "max_execution_time");
            this.GetPHPConfig(this.txtMaxInputVars, "max_input_vars");
        }

        /// <summary>
        /// Click save button
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {   
            if (this.txtDomain.Text.Length == 0)
            {
                Dialog.Error("Please enter domain for this website");
                return;
            }
            
            if (this.txtName.Text.Length == 0)
            {
                Dialog.Error("Please enter name (directory name) for this website");
                return;
            }

            string new_dir = Path.Combine(Program.DIR_WEBSITES, this.txtName.Text);

            this.Config.FcgidInitialEnv.Clear();
            this.Config.FcgidWrapper.Clear();

            if (this.Config.Name.Length > 0)
            {
                if (this.txtName.Text != this.Config.Name)
                {
                    if (FileSystem.Exists(new_dir))
                    {
                        Dialog.Error("Path " + new_dir + " has been existed");
                        return;
                    }
                    else
                    {
                        string old_dir = Path.Combine(Program.DIR_WEBSITES, this.Config.Name);
                        Directory.Move(old_dir, new_dir);
                    }
                }
            }
            else
            {
                if (!FileSystem.IsFolderNorFile(new_dir))
                {
                    Dialog.Error("Can not create folder " + new_dir);
                    return;
                }
            }

            // Modify hosts file
            if (this.txtDomain.Text != "localhost" && this.txtDomain.Text != "127.0.0.1")
            {
                bool is_hosts_changed = false;

                if (this.chkUseHosts.Checked)
                {
                    if (!this._config_hosts.Exists(this.txtDomain.Text))
                    {
                        this._config_hosts.Append(this.txtDomain.Text, "127.0.0.1");
                        is_hosts_changed = true;
                    }
                }
                else
                {
                    if (this._config_hosts.Exists(this.Config.ServerName))
                    {
                        this._config_hosts.Remove(this.Config.ServerName);
                        is_hosts_changed = true;
                    }
                }

                if (is_hosts_changed)
                {
                    try
                    {
                        this._config_hosts.Write(HostsConfig.FILE_HOSTS);
                    }
                    catch (Exception ex)
                    {
                        Dialog.Error(ex.Message);
                    }
                }
            }

            this.Config.Name = this.txtName.Text;
            this.Config.Port = this._port;
            this.Config.PublicPath = this.txtPublicPath.Text;
            this.Config.PHPVersion = this.cboPHP.Text;
            this.Config.ServerName = this.txtDomain.Text;

            if (this.txtFolder.Text != "")
            {
                this.Config.External = true;
                this.Config.BaseDirectory = this.txtFolder.Text;
                this.Config.DocumentRoot = Path.GetFullPath(Path.Combine(this.txtFolder.Text, this.Config.PublicPath));

                if (!this.Config.DocumentRoot.StartsWith(this.Config.BaseDirectory))
                {
                    this.Config.PublicPath = "";
                    this.Config.DocumentRoot = Path.GetFullPath(Path.Combine(this.txtFolder.Text, this.Config.PublicPath));
                }
            }
            else
            {
                this.Config.External = false;
            }

            if (this.cboPHP.SelectedIndex > 0)
            {
                string php_dir = Path.Combine(Program.DIR_PHP, this.cboPHP.Text);

                this.Config.FcgidInitialEnv.Add(new KeyValuePair<string, string>("PHPRC", new_dir));
                this.Config.FcgidWrapper.Add(new KeyValuePair<string, string>(Path.Combine(php_dir, "php-cgi.exe"), ".php"));

                this.Config.PHPConfig.SetDirective("upload_max_filesize", this.txtMaxUpload.Text);
                this.Config.PHPConfig.SetDirective("post_max_size", this.txtPostMaxSize.Text);
                this.Config.PHPConfig.SetDirective("max_execution_time", this.txtTimeout.Text);
                this.Config.PHPConfig.SetDirective("max_input_vars", this.txtMaxInputVars.Text);
                this.Config.PHPConfig.SetDirective("extension_dir", Path.Combine(php_dir, "ext"));

                this.Config.PHPConfig.ClearExtensions();

                foreach (var item in this.chkExtensions.CheckedItems)
                {
                    this.Config.PHPConfig.Add(item.ToString() == "opcache" ? "zend_extension" : "extension", item.ToString());
                }
            }

            if (this.chkSSL.Checked)
            {
                this.Config.AddSslSupport(this._ssl_port);
            }
            else
            {
                this.Config.RemoveSslSupport();
            }

            try
            {
                this.Config.Save(new_dir);
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.Message);
                return;
            }

            if (this._is_new)
            {
                this.OnCreated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.OnSaved?.Invoke(this, EventArgs.Empty);
            }

            this.Close();
        }

        /// <summary>
        /// Auto trim name
        /// </summary>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.txtName.Text = this.txtName.Text.Trim();
        }

        /// <summary>
        /// Auto trim domain
        /// </summary>
        private void txtDomain_TextChanged(object sender, EventArgs e)
        {
            this.txtDomain.Text = this.txtDomain.Text.Trim();
        }

        /// <summary>
        /// Trigger delete website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.OnDelete?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Add extensions list
        /// </summary>
        private void cboPHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkExtensions.Items.Clear();
            this.grpPHP.Enabled = this.cboPHP.Text != "";

            string dir_extensions = Path.Combine(Program.DIR_PHP, this.cboPHP.Text, "ext");
            List<KeyValuePair<string, string>> current_extensions = this._config.PHPConfig.Get("extension");
            current_extensions.AddRange(this._config.PHPConfig.Get("zend_extension"));

            if (this.cboPHP.Text != "")
            {
                if (Directory.Exists(dir_extensions))
                {
                    string[] files = Directory.GetFiles(dir_extensions, "*.dll");

                    foreach (string f in files)
                    {
                        string extension_name = new FileInfo(f).Name.Replace(".dll", "");

                        if (extension_name.StartsWith("php_"))
                        {
                            extension_name = extension_name.Substring(4);
                        }

                        this.chkExtensions.Items.Add(extension_name);

                        if (current_extensions.Contains(new KeyValuePair<string, string>("extension", extension_name))
                            || current_extensions.Contains(new KeyValuePair<string, string>("zend_extension", extension_name)))
                        {
                            this.chkExtensions.SetItemChecked(this.chkExtensions.Items.Count - 1, true);
                        }
                    }
                }
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (this.fbdFolder.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = this.fbdFolder.SelectedPath;
            }
        }

        private void lblClearFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtFolder.Text = "";
        }
    }
}
