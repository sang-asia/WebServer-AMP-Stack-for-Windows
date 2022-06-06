using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServer.Libraries;
using WebServer.Libraries.Config;
using WebServer.Libraries.Downloader;

namespace WebServer.Apps
{
    class AppApache: AppBase
    {
        // EVENTS

        // VARIABLES
        public static readonly string DIR_BIN = Path.Combine(Program.DIR_BIN, "Apache");
        public static readonly string DIR_TEMP = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());
        public static readonly string FILE_TEMP = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());
        public static readonly string FILE_CONFIG = Path.Combine(DIR_BIN, "conf", "httpd.conf");
        public static readonly string FILE_FCGI_NAME = "mod_fcgid.so";
        public static readonly string FILE_FCGI = Path.Combine(DIR_BIN, "modules", FILE_FCGI_NAME);
        public static readonly string FILE_EXE = Path.Combine(DIR_BIN, "bin", "httpd.exe");
        public static readonly string FILE_WEB_CONFIG_NAME = "websites.conf";
        public static readonly string FILE_SSL_CONFIG_NAME = "websites_ssl.conf";
        public static readonly string FILE_WEB_CONFIG = Path.Combine(Program.DIR_CONFIG, FILE_WEB_CONFIG_NAME);
        public static readonly string FILE_SSL_CONFIG = Path.Combine(Program.DIR_CONFIG, FILE_SSL_CONFIG_NAME);

        public static readonly string URL_APACHE = Program.IS_X64
            ? "https://cdn.wamp.asia/httpd-2.4.48-win64-VS16.zip"
            : "https://cdn.wamp.asia/httpd-2.4.48-win32-VS16.zip";

        public static readonly string URL_FCGI = Program.IS_X64
            ? "https://cdn.wamp.asia/mod_fcgid-2.3.10-win64-VS16.zip"
            : "https://cdn.wamp.asia/mod_fcgid-2.3.10-win32-VS16.zip";

        private readonly ApacheConfig config = new ApacheConfig();
        private readonly ApacheConfig config_ssl = new ApacheConfig();

        /// <summary>
        /// Check supported platform
        /// </summary>
        /// <returns></returns>
        public override bool Only64Bit()
        {
            return false;
        }

        /// <summary>
        /// Get main EXE file
        /// </summary>
        protected override string GetExeFile()
        {
            return FILE_EXE;
        }

        /// <summary>
        /// Application Name
        /// </summary>
        public override string GetName()
        {
            string version = this.GetVersion();
            return "Apache" + (version != null ? " " + version : "");
        }

        /// <summary>
        /// Menu items
        /// </summary>
        /// <returns></returns>
        protected override ToolStripItemCollection GetMenuItems()
        {
            return new ToolStripItemCollection(new ToolStrip(), new ToolStripItem[]
            {
                this.GetMenuChangeHttpPort(),
                this.GetMenuChangeHttpsPort(),
            });
        }

        /// <summary>
        /// Menu Item to change HTTP Port
        /// </summary>
        private ToolStripMenuItem GetMenuChangeHttpPort()
        {
            ToolStripMenuItem ctxMenuChangeHttpPort = new ToolStripMenuItem
            {
                Name = "ctxMenuChangePort",
                Image = Properties.Resources.icons8_internet_hub_16,
                Text = "Change Port",
            };

            this.HandleMenuChangePort(ctxMenuChangeHttpPort, this.config, FILE_CONFIG);

            return ctxMenuChangeHttpPort;
        }

        /// <summary>
        /// Menu Item to change HTTPS Port
        /// </summary>
        private ToolStripMenuItem GetMenuChangeHttpsPort()
        {
            ToolStripMenuItem ctxMenuChangeHttpsPort = new ToolStripMenuItem
            {
                Name = "ctxMenuChangeHttpsPort",
                Image = Properties.Resources.icons8_internet_hub_16,
                Text = "Change SSL Port",
            };

            this.HandleMenuChangePort(ctxMenuChangeHttpsPort, this.config_ssl, FILE_SSL_CONFIG);

            return ctxMenuChangeHttpsPort;
        }

        /// <summary>
        /// Handle change port functions
        /// </summary>
        private void HandleMenuChangePort(ToolStripMenuItem menu, ApacheConfig config, string file_config)
        {
            menu.Click += new EventHandler((object menu_item, EventArgs menu_event) => {
                Forms.frmChangePort f = new Forms.frmChangePort
                {
                    Port = config.GetPort()
                };

                f.OnSaved += (int new_port) => {
                    config.ChangePort(new_port);
                    config.Write(file_config);
                    this.LoadPorts();
                    f.Close();

                    if (this.IsInstalled() && this.GetWorkingState() == EWorkingState.Started)
                    {
                        if (Dialog.Confirm("Do you want to restart server to apply new configuration?") == DialogResult.Yes)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                this.RestartService();
                            });
                        }
                    }
                };

                f.ShowDialog();
            });
        }

        /// <summary>
        /// Load config
        /// </summary>
        protected override void LoadConfig()
        {
            this.config.Open(FILE_CONFIG);
            this.config.Parse();
            this.CheckConfig();
            this.CheckWebsitesConfig();
            this.CheckSSLConfig();
            this.LoadPorts();
        }

        /// <summary>
        /// Check Apache Config
        /// </summary>
        private void CheckConfig()
        {
            bool is_changed = false;

            // Check root
            if (DIR_BIN != this.config.GetRoot())
            {
                this.config.ChangeRoot(DIR_BIN);
                is_changed = true;
            }

            // Check server name
            this.config.Search(@"^(\s*)ServerName(\s+)(.*)$",
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("ServerName", "localhost");
                    is_changed = true;
                }
            );

            // Check mod_rewrite
            this.config.Search(@"^(\s*)LoadModule(\s+)rewrite_module(\s+)modules/mod_rewrite.so(\s*)$",
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("LoadModule", "rewrite_module modules/mod_rewrite.so");
                    is_changed = true;
                }
            );

            // Check mod_ssl
            this.config.Search(@"^(\s*)LoadModule(\s+)ssl_module(\s+)modules/mod_ssl.so(\s*)$",
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("LoadModule", "ssl_module modules/mod_ssl.so");
                    is_changed = true;
                }
            );

            // Check mod_ssl
            this.config.Search(@"^(\s*)LoadModule(\s+)socache_shmcb_module(\s+)modules/mod_socache_shmcb.so(\s*)$",
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("LoadModule", "socache_shmcb_module modules/mod_socache_shmcb.so");
                    is_changed = true;
                }
            );

            // Check websites include
            string include_config_path = "\"" + FILE_WEB_CONFIG + "\"";

            this.config.Search(@"^(\s)*IncludeOptional(\s)*("")*(.)+" + FILE_WEB_CONFIG_NAME + @"("")*(\s*)$",
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(include_config_path))
                    {
                        lines[index] = "IncludeOptional " + include_config_path;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("IncludeOptional", include_config_path);
                    is_changed = true;
                }
            );

            // Check changes
            if (is_changed)
            {
                this.config.Write(FILE_CONFIG);
                this.Log("Modify config \"" + FILE_CONFIG + "\"", LogType.Warning);
            }
        }

        /// <summary>
        /// Check websites.conf in config folder
        /// </summary>
        private void CheckWebsitesConfig()
        {
            ApacheConfig cfg = new ApacheConfig();
            string module_path = "\"" + FILE_FCGI + "\"";
            string include_website_path = "\"" + Path.Combine(Program.DIR_WEBSITES, "*", WebsiteConfig.FILE_CONFIG) + "\"";
            string include_ssl_path = "\"" + FILE_SSL_CONFIG + "\"";
            string mime_type = "application/x-httpd-php .php";
            bool is_changed = false;

            // Load web config if existed
            if (File.Exists(FILE_WEB_CONFIG))
            {
                cfg.Open(FILE_WEB_CONFIG);
                cfg.Parse();
            }

            // Check mod_fcgid module
            cfg.Search(@"^LoadModule(\s)+fcgid_module(\s)+("")*(.)*" + FILE_FCGI_NAME + @"("")*$", 
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(module_path))
                    {
                        lines[index] = "LoadModule fcgid_module " + module_path;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("LoadModule", "fcgid_module " + module_path);
                    is_changed = true;
                }
            );

            // Check PHP mime type
            cfg.Search(@"^AddType(\s)+(.)+(\s)+\.php$",
                action_not_found: (BaseConfig c) =>
                {
                    cfg.Append("AddType", mime_type);
                    is_changed = true;
                }
            );

            // Check includes for websites
            cfg.Search(@"^IncludeOptional(\s)+("")*(.)+\\\*\\" + WebsiteConfig.FILE_CONFIG + @"("")*$", 
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(include_website_path))
                    {
                        lines[index] = "IncludeOptional " + include_website_path;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("IncludeOptional", include_website_path);
                    is_changed = true;
                }
            );

            // Check includes for websites
            cfg.Search(@"^IncludeOptional(\s)+("")*(.)+\\\" + FILE_SSL_CONFIG_NAME + @"("")*$", 
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(include_ssl_path))
                    {
                        lines[index] = "IncludeOptional " + include_ssl_path;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("IncludeOptional", include_ssl_path);
                    is_changed = true;
                }
            );

            // Write all changes
            if (is_changed)
            {
                cfg.Write(FILE_WEB_CONFIG);
                this.Log("Modify config \"" + FILE_WEB_CONFIG + "\"", LogType.Warning);
            }
        }

        /// <summary>
        /// Check websites_ssl.conf in config folder
        /// </summary>
        private void CheckSSLConfig()
        {
            bool is_changed = false;

            if (File.Exists(FILE_SSL_CONFIG))
            {
                this.config_ssl.Open(FILE_SSL_CONFIG);
                this.config_ssl.Parse();
            }

            if (!this.config_ssl.Exists("Listen"))
            {
                this.config_ssl.Append("Listen", "443");
                is_changed = true;
            }

            is_changed = is_changed || this.config_ssl.SetDirective("SSLCipherSuite", "HIGH:MEDIUM:!MD5:!RC4:!3DES");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLProxyCipherSuite", "HIGH:MEDIUM:!MD5:!RC4:!3DES");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLHonorCipherOrder", "on");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLProtocol", "all -SSLv3");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLProxyProtocol", "all -SSLv3");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLPassPhraseDialog", "builtin");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLSessionCache", @"""shmcb:${SRVROOT}/logs/ssl_scache(512000)""");
            is_changed = is_changed || this.config_ssl.SetDirective("SSLSessionCacheTimeout", "300");

            // Check includes for websites
            string include_dir = "\"" + Path.Combine(Program.DIR_WEBSITES, "*", WebsiteConfig.FILE_CONFIG_SSL) + "\"";

            this.config_ssl.Search(@"^IncludeOptional(\s)+("")*(.)+\\\*\\" + WebsiteConfig.FILE_CONFIG_SSL + @"("")*$",
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(include_dir))
                    {
                        lines[index] = "IncludeOptional " + include_dir;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("IncludeOptional", include_dir);
                    is_changed = true;
                }
            );

            if (is_changed)
            {
                this.config_ssl.Write(FILE_SSL_CONFIG);
                this.Log("Modify config \"" + FILE_SSL_CONFIG + "\"", LogType.Warning);
            }
        }

        /// <summary>
        /// Load ports list
        /// </summary>
        private void LoadPorts()
        {
            this.ClearPorts();

            int port_http = this.config.GetPort();
            int port_https = this.config_ssl.GetPort();
            string log = "";

            if (port_http > 0)
            {
                this.AddPort(port_http);
                log = "HTTP Port: " + port_http.ToString();
            }

            if (port_https > 0)
            {
                this.AddPort(port_https);
                log += (log.Length > 0 ? " / " : "") + "HTTPS Port: " + port_https.ToString();
            }

            this.Log(log);
        }

        /// <summary>
        /// Install Apache
        /// </summary>
        protected override void DoInstall()
        {
            Logger log = Logger.Setup(this.GetName());

            this.IsDownloading(true);
            this.SetPercentage(0);

            // Remove old config file
            if (File.Exists(FILE_WEB_CONFIG))
            {
                log.Log("Delete old config file \"" + FILE_WEB_CONFIG + "\"", LogType.Warning);
                File.Delete(FILE_WEB_CONFIG);
            }

            log.Log("Getting Apache URL...");
            string real_url = URL_APACHE;
            // string real_url = SourceForge.GetDownloadUrl(URL_APACHE);
            // log.Log("Apache URL: " + real_url);

            if (real_url == null)
            {
                Dialog.Error("Could not get Apache file URL. Please try again!");
                log.Log("Could not get Apache file URL", LogType.Error);
                return;
            }

            Job job = Manager.Add(real_url, FILE_TEMP);
            
            job.OnPercentageChanged += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                this.SetPercentage(e.GetJob().GetPercent());
            });

            job.OnCancelled += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                this.IsDownloading(false);
                this.Error("Could not download Apache. Please try again!");
                log.Log("Could not download Apache", LogType.Error);
            });

            job.OnCompleted += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                log.Log("Start install");

                this.IsDownloading(false);
                this.SetInstallationState(EInstallationState.Installing);

                // Delete old service
                this.DeleteServices();

                // Install Apache
                try
                {
                    this.DoExtractApache();
                    log.Log("Apache extracted");
                }
                catch (Exception)
                {
                    this.DoCleanup();
                    this.SetInstallationState(EInstallationState.NotInstalled);
                    this.Error("Could not extract downloaded archive. Please try again!");
                    log.Log("Could not extract Apache", LogType.Error);
                    return;
                }

                // Download mod_fcgid
                log.Log("Getting mod_fcgid URL...");
                real_url = URL_FCGI;
                // real_url = SourceForge.GetDownloadUrl(URL_FCGI);
                // log.Log("mod_fcgid URL: " + real_url);

                if (real_url == null)
                {
                    Dialog.Error("Could not get mod_fcgid file URL. Please try again!");
                    log.Log("Could not get mod_fcgid file URL", LogType.Error);
                    return;
                }

                try
                {
                    log.Log("Downloading mod_fcgid...");
                    Manager.Add(real_url, FILE_TEMP).Start(false);
                }
                catch (Exception)
                {
                    this.DoCleanup();
                    this.SetInstallationState(EInstallationState.NotInstalled);
                    this.Error("Could not download mod_fcgid. Please try again!");
                    log.Log("Could not download mod_fcgid", LogType.Error);
                    return;
                }

                // Install mod_fcgid
                try
                {
                    this.DoExtractFcgi();
                    log.Log("mod_fcgid extracted");
                }
                catch (Exception)
                {
                    this.DoCleanup();
                    this.SetInstallationState(EInstallationState.NotInstalled);
                    this.Error("Could not extract mod_fcgid. Please try again!");
                    log.Log("Could not extract mod_fcgid", LogType.Error);
                    return;
                }

                // Install service
                this.DoInstallService();

                this.SetInstallationState(EInstallationState.Installed);
                log.Log("Application installed successfully", LogType.Success);
            });

            job.Start();
            log.Log("Downloading Apache...");
        }

        /// <summary>
        /// Extract downloaded binary
        /// </summary>
        private void DoExtractApache()
        {
            // Extract binaries
            ZipFile.ExtractToDirectory(FILE_TEMP, DIR_TEMP);

            // Remove existed folder
            if (Directory.Exists(DIR_BIN))
            {
                FileSystem.Delete(DIR_BIN);
            }

            // Move extracted folder
            Directory.Move(Path.Combine(DIR_TEMP, "Apache24"), DIR_BIN);

            // Remove temporary files
            this.DoCleanup();
        }

        /// <summary>
        /// Extract FCGI module
        /// </summary>
        /// <returns></returns>
        private void DoExtractFcgi()
        {
            // Extract binaries
            ZipFile.ExtractToDirectory(FILE_TEMP, DIR_TEMP);

            // Move extracted folder
            if (File.Exists(FILE_FCGI))
            {
                FileSystem.Delete(FILE_FCGI);
            }

            File.Move(Path.Combine(DIR_TEMP, "mod_fcgid-2.3.10", "mod_fcgid.so"), FILE_FCGI);

            // Remove temporary files
            this.DoCleanup();
        }

        /// <summary>
        /// Install service
        /// </summary>
        private void DoInstallService()
        {
            if (File.Exists(FILE_EXE) && this.GetFirstService() == null)
            {
                this.Log("Installing service");

                // Install service
                string service_name = "WebServer Apache " + Guid.NewGuid().ToString();
                Processes.Exec(FILE_EXE, "-k install -n \"" + service_name + "\"");

                // Waiting for Windows init service properly and set service run manually
                for (int i = 0; i < 15; i++)
                {
                    if (this.GetFirstService() != null)
                    {
                        break;
                    }

                    Thread.Sleep(2000);
                }

                this.ServiceStartManual();
                this.Log("Service installed", LogType.Success);
            }
        }

        /// <summary>
        /// Cleanup temp file/folder
        /// </summary>
        protected void DoCleanup()
        {
            if (FileSystem.Exists(FILE_TEMP))
            {
                FileSystem.Delete(FILE_TEMP);
            }

            if (FileSystem.Exists(DIR_TEMP))
            {
                FileSystem.Delete(DIR_TEMP);
            }
        }

        /// <summary>
        /// Uninstall Apache
        /// </summary>
        protected override void DoUninstall()
        {
            this.Log("Uninstalling...");
            this.SetWorkingState(EWorkingState.Uninstalling);

            Task.Factory.StartNew(() =>
            {
                this.DeleteServices();
                FileSystem.Delete(DIR_BIN);
                this.SetInstallationState(EInstallationState.NotInstalled);
                this.Log("Uninstalled", LogType.Error);
            });
        }

        /// <summary>
        /// Check Installation Status
        /// </summary>
        public override bool IsInstalled()
        {
            return File.Exists(FILE_EXE)
                && File.Exists(FILE_CONFIG)
                && File.Exists(FILE_FCGI)
                && Directory.Exists(DIR_BIN);
        }

        /// <summary>
        /// Start Apache
        /// </summary>
        public override void Start()
        {
            this.DoInstallService();

            int[] ports = { this.config.GetPort(), this.config_ssl.GetPort() };

            foreach (int port in ports)
            {
                if (port == 0)
                {
                    continue;
                }
                
                if (!Network.PortAvailable(port))
                {
                    this.Error("Port " + port.ToString() + " has been used by another application.");
                    this.Log("Port \"" + port.ToString() + "\" has been used by another application.", LogType.Error);
                    return;
                }
            }

            this.StartService();
        }

        /// <summary>
        /// Stop Apache
        /// </summary>
        public override void Stop()
        {
            this.StopService();
        }

        /// <summary>
        /// Options button visible
        /// </summary>
        public override bool HasOptionsButton()
        {
            return true;
        }

        /// <summary>
        /// This app has service
        /// </summary>
        public override bool HasService()
        {
            return true;
        }

        /// <summary>
        /// Validate app
        /// </summary>
        public override bool CanRun()
        {
            return FileSystem.Exists(DIR_BIN, new string[][] {
                new string[] { "bin", "httpd.exe"},
                new string[] { "conf", "httpd.conf"},
                new string[] { "modules", "mod_fcgid.so"},
            });
        }

        /// <summary>
        /// Get HTTP port
        /// </summary>
        public int GetHttpPort()
        {
            return this.config.GetPort();
        }

        /// <summary>
        /// Get HTTPS port
        /// </summary>
        public int GetHttpsPort()
        {
            return this.config_ssl.GetPort();
        }
    }
}
