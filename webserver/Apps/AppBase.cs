using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServer.Libraries;

namespace WebServer.Apps
{
    delegate void AppEventHandler(AppBase s, AppEventArgs e);

    /// <summary>
    /// Application's Installation States
    /// </summary>
    enum EInstallationState
    {
        Unknown,
        NotInstalled,   // Invoked by App
        // Starting,       // Invoked by App
        // Downloading,    // Invoked by App
        // Downloaded,     // Invoked by App
        Installing,     // Invoked by App
        Installed       // Invoked by Watcher
    }

    /// <summary>
    /// Application's Working States
    /// </summary>
    enum EWorkingState
    {
        Unknown,
        Starting,       // Invoked by App
        Started,        // Invoked by Watcher
        Stopping,       // Invoked by App
        Stopped,        // Invoked by Watcher
        Uninstalling,   // Invoked by App
        // Uninstalled     // Invoked by App
    }

    /// <summary>
    /// Application's Service Actions
    /// </summary>
    enum EServiceAction
    {
        Start,
        Stop,
        ModeManual,
        ModeAuto
    }

    /// <summary>
    /// Used for App's events
    /// </summary>
    class AppEventArgs: EventArgs
    {
        private readonly string message;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppEventArgs(string message = null)
        {
            this.message = message;
        }

        /// <summary>
        /// Get event message
        /// </summary>
        public string GetMessage()
        {
            return this.message;
        }
    }

    /// <summary>
    /// Base class for Applications
    /// </summary>
    abstract class AppBase
    {
        // EVENTS
        public event AppEventHandler OnInstallationStateChanged;
        public event AppEventHandler OnWorkingStateChanged;
        public event AppEventHandler OnError;
        public event AppEventHandler OnPercentageChanged;
        public event AppEventHandler OnPortChanged;
        public event AppEventHandler OnDownloadStateChanged;

        // VARIABLES
        private EInstallationState installation_state = EInstallationState.Unknown;
        private EWorkingState working_state = EWorkingState.Unknown;
        private ServiceController service;
        private ServiceControllerStatus service_status;
        private List<int> ports = new List<int> { };
        private int percentage = 0;
        private readonly ToolStripItemCollection menu_items = new ToolStripItemCollection(new ToolStrip(), new ToolStripItem[] { });
        private ToolStripMenuItem ctxMenuStartAuto;
        private bool is_downloading = false;

        // ABSTRACT METHODS

        /// <summary>
        /// TRUE if this app can only run on x64 OS
        /// </summary>
        abstract public bool Only64Bit();

        /// <summary>
        /// Path to the EXE file that used to register service
        /// </summary>
        abstract protected string GetExeFile();

        /// <summary>
        /// App name
        /// </summary>
        abstract public string GetName();

        /// <summary>
        /// List of menu items. Null value if there are no menu item
        /// </summary>
        abstract protected ToolStripItemCollection GetMenuItems();

        /// <summary>
        /// Load App config
        /// </summary>
        abstract protected void LoadConfig();

        /// <summary>
        /// Main method to do the install steps
        /// </summary>
        abstract protected void DoInstall();

        /// <summary>
        /// Main method to do the uninstall steps
        /// </summary>
        abstract protected void DoUninstall();

        /// <summary>
        /// TRUE if this app has been installed
        /// </summary>
        /// <returns></returns>
        abstract public bool IsInstalled();

        /// <summary>
        /// Start this app
        /// </summary>
        abstract public void Start();

        /// <summary>
        /// Stop this app
        /// </summary>
        abstract public void Stop();

        /// <summary>
        /// TRUE if this app has Options button
        /// </summary>
        abstract public bool HasOptionsButton();

        /// <summary>
        /// TRUE if this app has service
        /// </summary>
        /// <returns></returns>
        abstract public bool HasService();

        /// <summary>
        /// Validate binary, service, config, auto create missing stuffs and return TRUE.
        /// In case the issues can't be fixed, return FALSE
        /// </summary>
        abstract public bool CanRun();

        /// <summary>
        /// Constructor
        /// </summary>
        public AppBase()
        {
            if (this.IsInstalled())
            {
                this.SetInstallationState(EInstallationState.Installed);
            }
            else
            {
                this.SetInstallationState(EInstallationState.NotInstalled);
            }

            this.InitMenu();

            AppWatcher.Watch(this);
        }

        /// <summary>
        /// Do the installation
        /// </summary>
        public void Install()
        {
            if (!this.SupportedPlatform())
            {
                this.Error("This application is not supported by your platform");
            }
            else
            {
                if (this.IsInstalled())
                {
                    this.OnError?.Invoke(this, new AppEventArgs("Application already been installed"));
                }
                else
                {
                    this.DoInstall();
                }
            }
        }

        /// <summary>
        /// Do the uninstall
        /// </summary>
        public void Uninstall()
        {
            if (this.IsInstalled())
            {
                this.SetWorkingState(EWorkingState.Uninstalling);
                this.DoUninstall();
            }
            else
            {
                this.OnError?.Invoke(this, new AppEventArgs("Application has not been installed"));
            }
        }

        /// <summary>
        /// Start generic app by service
        /// </summary>
        protected bool StartService()
        {
            if (!this.HasService())
            {
                return false;
            }

            this.Log("Starting service...");
            this.SetWorkingState(EWorkingState.Starting);

            if (!this.DoServiceAction(EServiceAction.Start))
            {
                this.SetWorkingState(EWorkingState.Stopped);
                this.Log("Service can not be started. Please try again!", LogType.Error);
                return false;
            }

            this.Log("Service started");

            return true;
        }

        /// <summary>
        /// Stop generic app by service
        /// </summary>
        protected bool StopService()
        {
            if (!this.HasService())
            {
                return false;
            }

            this.Log("Stopping service...");
            this.SetWorkingState(EWorkingState.Stopping);

            if (!this.DoServiceAction(EServiceAction.Stop))
            {
                this.SetWorkingState(EWorkingState.Unknown);
                this.Log("Service can not be stopped. Please try again!", LogType.Error);
                return false;
            }

            this.Log("Service stopped");

            return true;
        }

        /// <summary>
        /// Get first service by image path
        /// </summary>
        protected ServiceController GetFirstService()
        {
            if (!this.HasService())
            {
                return null;
            }

            string file = this.GetExeFile();

            if (file == null)
            {
                return null;
            }

            ServiceController[] services = Services.FindByImage(file);

            if (services.Length == 0)
            {
                return null;
            }

            return services[0];
        }

        /// <summary>
        /// Start/Stop a service
        /// </summary>
        private bool DoServiceAction(EServiceAction action, ServiceController service = null)
        {
            if (!this.HasService())
            {
                return false;
            }

            if (service == null)
            {
                this.service = this.GetFirstService();
                service = this.service;
            }

            if (service == null)
            {
                this.Error("Service not found");
                this.Log("Service not found", LogType.Error);
                return false;
            }

            // Change StartupType
            if (action == EServiceAction.ModeAuto)
            {
                Services.SetStartAutomatic(service);
                this.Log("Service start mode: Run when Windows startup");
                return true;
            }
            else if (action == EServiceAction.ModeManual)
            {
                Services.SetStartManual(service);
                this.Log("Service start mode: Manual");
                return true;
            }

            // Control service: Start/Stop
            ServiceControllerStatus waiting_status;

            try
            {
                if (action == EServiceAction.Start)
                {
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        return true;
                    }

                    waiting_status = ServiceControllerStatus.Running;
                    service.Start();
                }
                else if (action == EServiceAction.Stop)
                {
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        return true;
                    }

                    waiting_status = ServiceControllerStatus.Stopped;
                    service.Stop();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                this.Error(e.Message);
                return false;
            }

            try
            {
                service.WaitForStatus(waiting_status, TimeSpan.FromSeconds(60));
            }
            catch (Exception)
            {
                this.Error("Service's action timeout");
                this.Log("Service's action timeout", LogType.Error);
                return false;
            }

            service.Refresh();

            if (service.Status == waiting_status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Restart Service
        /// </summary>
        /// <returns></returns>
        public bool RestartService()
        {
            if (this.StopService())
            {
                return this.StartService();
            }

            return false;
        }

        /// <summary>
        /// Delete all services that image is this app's exe file
        /// </summary>
        public void DeleteServices()
        {
            if (!this.HasService())
            {
                return;
            }

            string file = this.GetExeFile();

            if (file == null)
            {
                return;
            }

            this.Log("Deleting service...");
            ServiceController[] services = Services.FindByImage(file);

            foreach (ServiceController s in services)
            {
                if (s.Status != ServiceControllerStatus.Stopped)
                {
                    this.DoServiceAction(EServiceAction.Stop, s);
                }

                s.Refresh();

                if (s.Status == ServiceControllerStatus.Stopped)
                {
                    Services.Delete(s);
                    this.Log("Service deleted");
                }
            }
        }

        /// <summary>
        /// Make service start manually
        /// </summary>
        public bool ServiceStartManual()
        {
            return this.DoServiceAction(EServiceAction.ModeManual);
        }

        /// <summary>
        /// Make service start automatically
        /// </summary>
        public bool ServiceStartAuto()
        {
            return this.DoServiceAction(EServiceAction.ModeAuto);
        }

        /// <summary>
        /// Check service status
        /// </summary>
        public void CheckStatus()
        {
            // It not installed, check installation state
            if (this.installation_state == EInstallationState.Installed)
            {
                if (!this.CanRun())
                {
                    this.SetInstallationState(EInstallationState.NotInstalled);
                    this.Error("Your " + this.GetName() + " binary has been corrupted. Please install again!");
                    return;
                }
            }
            else
            {
                if (this.IsInstalled())
                {
                    this.SetInstallationState(EInstallationState.Installed);
                }

                return;
            }

            // Try to find service if this app has service
            if (this.HasService() && this.service == null)
            {
                this.service = this.GetFirstService();
            }

            // If app don't have service or uninstalling, bypass this check
            if (this.service == null || this.working_state == EWorkingState.Uninstalling)
            {
                return;
            }

            // Refresh service info
            this.service.Refresh();

            try
            {
                this.service.Status.ToString();
            }
            catch (Exception)
            {
                return;
            }

            // Check service status
            if (this.service_status != this.service.Status)
            {
                this.service_status = this.service.Status;

                if (this.service_status == ServiceControllerStatus.Stopped && this.working_state != EWorkingState.Stopped)
                {
                    this.SetWorkingState(EWorkingState.Stopped);
                }
                else if (this.service_status == ServiceControllerStatus.Running && this.working_state != EWorkingState.Started)
                {
                    this.SetWorkingState(EWorkingState.Started);
                }
            }
        }

        /// <summary>
        /// Get version info of app exe file
        /// </summary>
        /// <returns></returns>
        public FileVersionInfo GetFileInfo()
        {
            if (!File.Exists(this.GetExeFile()))
            {
                return null;
            }

            return FileVersionInfo.GetVersionInfo(this.GetExeFile());
        }

        /// <summary>
        /// Get exe version
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            FileVersionInfo info = this.GetFileInfo();

            if (info == null)
            {
                return null;
            }

            return info.FileVersion;
        }

        /// <summary>
        /// Check whether an app can run on this platform or not
        /// </summary>
        /// <returns></returns>
        public bool SupportedPlatform()
        {
            return !this.Only64Bit() || Program.IS_X64;
        }

        /// <summary>
        /// Init Options context menu
        /// </summary>
        private void InitMenu()
        {
            // App Menu Items
            ToolStripItemCollection app_menu_items = this.GetMenuItems();

            if (app_menu_items != null && app_menu_items.Count > 0)
            {
                this.menu_items.AddRange(app_menu_items);
                this.menu_items.Add(new ToolStripSeparator
                {
                    Name = "ctxMenuSep2",
                    Size = new System.Drawing.Size(150, 6)
                });
            }

            // Service Actions
            if (this.HasService())
            {
                // Set Service auto start
                this.ctxMenuStartAuto = new ToolStripMenuItem
                {
                    Name = "ctxMenuStartAuto",
                    Size = new System.Drawing.Size(150, 22),
                    Text = "Run at Startup"
                };

                this.ctxMenuStartAuto.Click += new EventHandler((object s, EventArgs e) =>
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)s;
                    ServiceStartMode waiting_mode;

                    if (item.Checked)
                    {
                        waiting_mode = ServiceStartMode.Manual;
                        this.ServiceStartManual();
                    }
                    else
                    {
                        waiting_mode = ServiceStartMode.Automatic;
                        this.ServiceStartAuto();
                    }

                    this.CheckStatus();

                    if (this.service != null && this.service.StartType == waiting_mode)
                    {
                        item.Checked = this.service.StartType == ServiceStartMode.Automatic;
                        Dialog.Info("Application's Startup Mode has been changed successfully");
                    }
                    else
                    {
                        this.Error("Can not change Application's Startup Mode");
                    }
                });

                PaintEventHandler paint_handler = null;

                Action<object, PaintEventArgs> paint_action = (object s, PaintEventArgs e) =>
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)s;
                    item.Paint -= paint_handler;
                    item.Checked = this.service != null && this.service.StartType == ServiceStartMode.Automatic;
                };

                paint_handler = new PaintEventHandler(paint_action);
                this.ctxMenuStartAuto.Paint += paint_handler;

                ToolStripMenuItem ctxMenuRestart = new ToolStripMenuItem
                {
                    Name = "ctxMenuRestart",
                    Image = Properties.Resources.icons8_restart_16,
                    Size = new System.Drawing.Size(150, 22),
                    Text = "Restart"
                };

                ctxMenuRestart.Click += new EventHandler((object s, EventArgs e) => {
                    Task.Factory.StartNew(() => this.RestartService());
                });

                this.menu_items.AddRange(new ToolStripItem[]
                {
                    ctxMenuRestart,
                    this.ctxMenuStartAuto,
                    new ToolStripSeparator
                    {
                        Name = "ctxMenuSep",
                        Size = new System.Drawing.Size(150, 6)
                    }
                });
            }

            // Uninstall
            ToolStripMenuItem ctxMenuUninstall = new ToolStripMenuItem
            {
                Name = "ctxMenuUninstall",
                Image = Properties.Resources.icons8_trash_16,
                Size = new System.Drawing.Size(150, 22),
                Text = "Uninstall"
            };

            ctxMenuUninstall.Click += new EventHandler((object s, EventArgs e) => {
                if (Dialog.Confirm("Are you sure you want to uninstall " + this.GetName()) == DialogResult.Yes)
                {
                    this.Uninstall();
                }
            });

            this.menu_items.Add(ctxMenuUninstall);
        }

        /// <summary>
        /// Raise error event
        /// </summary>
        protected void Error(string message)
        {
            this.OnError?.Invoke(this, new AppEventArgs(message));
        }

        /// <summary>
        /// Set application's installation state
        /// </summary>
        protected void SetInstallationState(EInstallationState state)
        {
            if (this.installation_state == state)
            {
                return;
            }

            this.installation_state = state;

            if (state == EInstallationState.Installed)
            {
                this.LoadConfig();
                this.service = this.GetFirstService();
            }
            else if (state == EInstallationState.NotInstalled)
            {
                if (this.service != null)
                {
                    this.service.Dispose();
                    this.service = null;
                }

                this.SetWorkingState(EWorkingState.Unknown);
            }

            OnInstallationStateChanged?.Invoke(this, new AppEventArgs());
        }

        /// <summary>
        /// Get application's installation state
        /// </summary>
        public EInstallationState GetInstallationState()
        {
            return this.installation_state;
        }

        /// <summary>
        /// Set application's working state
        /// </summary>
        protected void SetWorkingState(EWorkingState state)
        {
            this.working_state = state;
            OnWorkingStateChanged?.Invoke(this, new AppEventArgs());
        }

        /// <summary>
        /// Get application's working state
        /// </summary>
        public EWorkingState GetWorkingState()
        {
            return this.working_state;
        }

        /// <summary>
        /// Set Percentage value
        /// </summary>
        protected void SetPercentage(int value)
        {
            this.percentage = value;
            this.OnPercentageChanged?.Invoke(this, new AppEventArgs());
        }

        /// <summary>
        /// Get action percentage
        /// </summary>
        public int GetPercentage()
        {
            return this.percentage;
        }

        /// <summary>
        /// Set Port value
        /// </summary>
        protected void AddPort(int port)
        {
            this.ports.Add(port);
            this.OnPortChanged?.Invoke(this, new AppEventArgs());
        }

        /// <summary>
        /// Clear all ports
        /// </summary>
        protected void ClearPorts()
        {
            bool is_changed = this.ports.Count > 0;
            this.ports = new List<int> { };

            if (is_changed)
            {
                this.OnPortChanged?.Invoke(this, new AppEventArgs());
            }
        }

        /// <summary>
        /// Get application port
        /// </summary>
        public int[] GetPorts()
        {
            return this.ports.ToArray();
        }

        /// <summary>
        /// Get menu items
        /// </summary>
        public ToolStripItemCollection MenuItems()
        {
            return this.menu_items;
        }

        /// <summary>
        /// Set and trigger downloading state event
        /// </summary>
        protected void IsDownloading(bool value)
        {
            this.is_downloading = value;
            this.OnDownloadStateChanged?.Invoke(this, new AppEventArgs());
        }

        /// <summary>
        /// Get current downloading state
        /// </summary>
        public bool IsDownloading()
        {
            return this.is_downloading;
        }
        
        /// <summary>
        /// Write log
        /// </summary>
        public void Log(string content, LogType type = LogType.Info)
        {
            Logger.App(this.GetName()).Log(content, type);
        }
    }
}
