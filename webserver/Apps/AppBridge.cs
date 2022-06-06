using System;
using System.Linq;
using System.Windows.Forms;
using WebServer.Controls;
using WebServer.Libraries;

namespace WebServer.Apps
{
    class AppBridge
    {
        private readonly ctlServer ctl;
        private readonly Tasks tasks = new Tasks();
        private readonly Enum[] waiting_states = new Enum[] {
            // EInstallationState.Starting,
            // EInstallationState.Downloading,
            // EInstallationState.Downloaded,
            EInstallationState.Installing,
            EWorkingState.Starting,
            EWorkingState.Stopping,
            EWorkingState.Uninstalling
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public AppBridge(AppBase app, ctlServer ctl)
        {
            this.ctl = ctl;
            this.ctl.HasOptionsButton = app.HasOptionsButton();
            this.ctl.Only64Bit = app.Only64Bit();
            this.ctl.MenuItems = app.MenuItems();
            this.ctl.Label = app.GetName();
            this.ctl.Port = app.GetPorts();

            app.OnInstallationStateChanged += new AppEventHandler(this.StateChanged);
            app.OnWorkingStateChanged += new AppEventHandler(this.StateChanged);
            app.OnPortChanged += new AppEventHandler(this.PortChanged);
            app.OnPercentageChanged += new AppEventHandler(this.PercentageChanged);
            app.OnDownloadStateChanged += new AppEventHandler(this.DownloadStateChanged);

            this.ctl.OnInstallClick += new EventHandler((object s, EventArgs e) => this.tasks.CreateTask(() => app.Install()));
            this.ctl.OnUninstallClick += new EventHandler((object s, EventArgs e) => this.tasks.CreateTask(() => app.Uninstall()));
            this.ctl.OnStartClick += new EventHandler((object s, EventArgs e) => this.tasks.CreateTask(() => app.Start()));
            this.ctl.OnStopClick += new EventHandler((object s, EventArgs e) => this.tasks.CreateTask(() => app.Stop()));

            this.StateChanged(app, new AppEventArgs());
        }

        /// <summary>
        /// Invoke control
        /// </summary>
        private void InvokeControl(Action f)
        {
            if (this.ctl.InvokeRequired)
            {
                this.ctl.Invoke(new MethodInvoker(f));
            }
            else
            {
                f();
            }
        }

        /// <summary>
        /// Check installed state from app's state
        /// </summary>
        /// <param name="app"></param>
        private void CheckInstalledState(AppBase app)
        {
            this.InvokeControl(() =>
            {
                switch (app.GetInstallationState())
                {
                    case EInstallationState.NotInstalled:
                        this.ctl.Installed = false;
                        this.ctl.Message = "";
                        break;
                    case EInstallationState.Installing:
                        this.ctl.Message = "Installing...";
                        break;
                    case EInstallationState.Installed:
                        this.ctl.Installed = app.IsInstalled();
                        this.ctl.Message = "";
                        break;
                    default:
                        this.ctl.Message = "";
                        break;
                }

                this.ctl.Label = app.GetName();
            });
        }

        /// <summary>
        /// Check running state from app's state
        /// </summary>
        /// <param name="app"></param>
        private void CheckRunningState(AppBase app)
        {
            this.InvokeControl(() =>
            {
                if (app.IsInstalled())
                {
                    this.ctl.Running = app.GetWorkingState() == EWorkingState.Started || app.GetWorkingState() == EWorkingState.Stopping;

                    switch (app.GetWorkingState())
                    {
                        case EWorkingState.Starting:
                            this.ctl.Message = "Starting...";
                            break;
                        case EWorkingState.Stopping:
                            this.ctl.Message = "Stopping...";
                            break;
                        case EWorkingState.Uninstalling:
                            this.ctl.Message = "Uninstalling...";
                            break;
                        default:
                            this.ctl.Message = "";
                            break;
                    }
                }
                else
                {
                    this.ctl.Running = false;
                }
            });
        }

        /// <summary>
        /// Check waiting state for control from app's states
        /// </summary>
        private void CheckWaitingState(AppBase app)
        {
            this.InvokeControl(() =>
            {
                this.ctl.Waiting = app.IsDownloading()
                    || this.waiting_states.Contains(app.GetInstallationState())
                    || this.waiting_states.Contains(app.GetWorkingState());
            });
        }

        /// <summary>
        /// Handle Installation status changed event
        /// </summary>
        private void StateChanged(AppBase app, AppEventArgs e)
        {
            this.CheckInstalledState(app);
            this.CheckRunningState(app);
            this.CheckWaitingState(app);
        }

        /// <summary>
        /// Handle Percentage changed event
        /// </summary>
        private void PercentageChanged(AppBase app, AppEventArgs e)
        {
            this.InvokeControl(() =>
            {
                this.ctl.Percent = app.GetPercentage();
            });
        }

        /// <summary>
        /// Handle Port changed event
        /// </summary>
        private void PortChanged(AppBase app, AppEventArgs e)
        {
            this.InvokeControl(() =>
            {
                this.ctl.Port = app.GetPorts();
            });
        }

        /// <summary>
        /// Handle Download State changed event
        /// </summary>
        private void DownloadStateChanged(AppBase app, AppEventArgs e)
        {
            this.CheckWaitingState(app);

            this.InvokeControl(() =>
            {
                this.ctl.Downloading = app.IsDownloading();
                this.ctl.Message = app.IsDownloading() ? "Downloading..." : "";
            });
        }
    }
}
