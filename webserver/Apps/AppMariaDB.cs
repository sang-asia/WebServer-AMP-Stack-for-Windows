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
    class AppMariaDB: AppBase
    {
        // EVENTS

        // VARIABLES
        public static readonly string DIR_BIN = Path.Combine(Program.DIR_BIN, "MariaDB");
        public static readonly string DIR_PLUGIN = Path.Combine(DIR_BIN, "lib", "plugin");
        public static readonly string DIR_TEMP = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());
        public static readonly string FILE_TEMP = Path.Combine(Program.DIR_TEMP, Guid.NewGuid().ToString());
        public static readonly string FILE_CONFIG = Path.Combine(Program.DIR_MARIADB_DATA, "my.ini");
        public static readonly string FILE_EXE = Path.Combine(DIR_BIN, "bin", "mariadbd.exe");

        // SRC:
        // - http://sgp1.mirrors.digitalocean.com/mariadb/mariadb-10.5.8/winx64-packages/mariadb-10.5.8-winx64.zip
        // - http://sgp1.mirrors.digitalocean.com/mariadb/mariadb-10.5.8/win32-packages/mariadb-10.5.8-win32.zip
        // SF:
        // - https://master.dl.sourceforge.net/project/webserver-amp-stack/mariadb/mariadb-10.5.8-winx64.zip?viasf=1
        // - https://master.dl.sourceforge.net/project/webserver-amp-stack/mariadb/mariadb-10.5.8-win32.zip?viasf=1

        public static readonly string URL = Program.IS_X64
            ? "https://cdn.wamp.asia/mariadb-10.6.3-winx64.zip"
            : "https://cdn.wamp.asia/mariadb-10.6.3-win32.zip";

        private readonly MySQLConfig config = new MySQLConfig();

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
            return "MariaDB" + (version != null ? " " + version : "");
        }

        /// <summary>
        /// Menu items
        /// </summary>
        /// <returns></returns>
        protected override ToolStripItemCollection GetMenuItems()
        {
            return new ToolStripItemCollection(new ToolStrip(), new ToolStripItem[]
            {
                this.GetMenuChangePort(),
            });
        }

        /// <summary>
        /// Menu Item to change Port
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem GetMenuChangePort()
        {
            ToolStripMenuItem ctxMenuChangePort = new ToolStripMenuItem
            {
                Name = "ctxMenuChangePort",
                Image = Properties.Resources.icons8_internet_hub_16,
                Text = "Change Port",
            };

            ctxMenuChangePort.Click += new EventHandler((object menu_item, EventArgs menu_event) => {
                Forms.frmChangePort f = new Forms.frmChangePort
                {
                    Port = this.config.GetPort()
                };

                f.OnSaved += (int new_port) => {
                    this.config.ChangePort(new_port);
                    this.config.Write(FILE_CONFIG);
                    this.ClearPorts();
                    this.AddPort(new_port);
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

            return ctxMenuChangePort;
        }

        /// <summary>
        /// Load config
        /// </summary>
        protected override void LoadConfig()
        {
            bool is_changed = false;
            this.config.Open(FILE_CONFIG);

            // Check datadir
            string datadir = Program.DIR_MARIADB_DATA.Replace(@"\", "/");

            this.config.Search(@"^datadir(\s)*=(\s)*(.)*$",
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(datadir))
                    {
                        lines[index] = "datadir=" + datadir;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("datadir", datadir);
                    is_changed = true;
                }
            );

            // Check plugin dir
            string plugin_dir = DIR_PLUGIN.Replace(@"\", "/");

            this.config.Search(@"^plugin-dir(\s)*=(\s)*(.)*$",
                action_for_match: (ref List<string> lines, ref int index) =>
                {
                    if (!lines[index].Trim().EndsWith(plugin_dir))
                    {
                        lines[index] = "plugin-dir=" + plugin_dir;
                        is_changed = true;
                    }
                },
                action_not_found: (BaseConfig c) =>
                {
                    c.Append("plugin-dir", plugin_dir);
                    is_changed = true;
                }
            );

            // Check config change
            if (is_changed)
            {
                this.config.Write(FILE_CONFIG);
                this.Log("Modify config \"" + FILE_CONFIG + "\"", LogType.Warning);
            }

            this.ClearPorts();

            int port = this.config.GetPort();
            this.AddPort(port);
            this.Log("Port: " + port.ToString());
        }

        /// <summary>
        /// Install
        /// </summary>
        protected override void DoInstall()
        {
            Logger log = Logger.Setup(this.GetName());

            this.IsDownloading(true);
            this.SetPercentage(0);

            log.Log("Getting MariaDB URL...");
            string real_url = URL;
            // string real_url = SourceForge.GetDownloadUrl(URL);

            if (real_url == null)
            {
                Dialog.Error("Could not get file URL. Please try again!");
                log.Log("Could not get file URL", LogType.Error);
                return;
            }

            Job job = Manager.Add(real_url, FILE_TEMP);

            job.OnStart += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                this.IsDownloading(true);
            });
            
            job.OnPercentageChanged += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                this.SetPercentage(e.GetJob().GetPercent());
            });

            job.OnCancelled += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                this.IsDownloading(false);
                this.Error("Could not download MariaDB. Please try again!");
                log.Log("Could not download MariaDB", LogType.Error);
            });

            job.OnCompleted += new DownloaderEventHandler((object s, DownloaderEventArgs e) => {
                log.Log("Start install");

                this.IsDownloading(false);
                this.SetInstallationState(EInstallationState.Installing);

                // Delete old service
                this.DeleteServices();

                // Install MariaDB
                try {
                    this.DoExtract();
                    log.Log("MariaDB extracted");
                }
                catch (Exception)
                {
                    this.DoCleanup();
                    this.SetInstallationState(EInstallationState.NotInstalled);
                    this.Error("Could not extract downloaded archive. Please try again!");
                    log.Log("Could not extract MariaDB", LogType.Error);
                    return;
                }

                // Init data folder
                string datadir = Program.DIR_MYSQL_DATA.Replace("\\", "/");

                if (!File.Exists(FILE_CONFIG))
                {
                    string file_install = Path.Combine(DIR_BIN, "bin", "mariadb-install-db.exe");
                    Processes.Exec(file_install, "--datadir=\"" + Program.DIR_MARIADB_DATA + "\" --password=123456 --port=3306");
                    this.Log("Initialize data folder \"" + Program.DIR_MARIADB_DATA + "\"");
                }

                // Create config
                File.WriteAllText(FILE_CONFIG, "[mysqld]\n"
                    + "datadir=" + datadir + "\n"
                    + "port=3306\n"
                    + "[client]\n"
                    + "port=3306\n"
                    + "plugin-dir=" + DIR_PLUGIN.Replace("\\", "/") + "\n"
                );

                this.Log("Create config file \"" + FILE_CONFIG + "\"");

                // Install service
                this.DoInstallService();
                this.SetInstallationState(EInstallationState.Installed);

                log.Log("Application installed successfully", LogType.Success);
                log.Log("Default root password is: 123456", LogType.Warning);

                Dialog.Info("Default root password is: 123456");
            });

            job.Start();
            log.Log("Downloading MariaDB...");
        }

        /// <summary>
        /// Extract downloaded binary
        /// </summary>
        private void DoExtract()
        {
            // Extract binaries
            ZipFile.ExtractToDirectory(FILE_TEMP, DIR_TEMP);

            // Move extracted folder
            if (Directory.Exists(DIR_BIN))
            {
                this.Uninstall();
            }

            Directory.Move(Path.Combine(DIR_TEMP, Program.IS_X64 ? "mariadb-10.6.3-winx64" : "mariadb-10.6.3-win32"), DIR_BIN);

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
                string service_name = "WebServer MariaDB " + Guid.NewGuid().ToString();
                Processes.Exec(FILE_EXE, "--install-manual \"" + service_name + "\" --defaults-file=\"" + FILE_CONFIG + "\"");

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
        /// Uninstall
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
                && Directory.Exists(DIR_BIN);
        }

        /// <summary>
        /// Start
        /// </summary>
        public override void Start()
        {
            this.DoInstallService();

            int port = this.config.GetPort();

            if (port == 0 || Network.PortAvailable(port))
            {
                this.StartService();
            }
            else
            {
                this.Error("Port " + port.ToString() + " has been used by another application.");
                this.Log("Port \"" + port.ToString() + "\" has been used by another application.", LogType.Error);
            }
        }

        /// <summary>
        /// Stop
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
            return 
                FileSystem.Exists(DIR_BIN, new string[][] {
                    new string[] { "bin", "mariadbd.exe"},
                })
                && FileSystem.Exists(Program.DIR_MARIADB_DATA, new string[][] {
                    new string[] { "my.ini"},
                });
        }
    }
}
