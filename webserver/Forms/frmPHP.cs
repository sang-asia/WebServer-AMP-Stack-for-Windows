using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServer.Libraries;
using WebServer.Libraries.PhpDotNet;

namespace WebServer.Forms
{
    public partial class frmPHP : Form
    {
        private readonly Manager _php = new Manager();
        private readonly Tasks _tasks = new Tasks();
        private Release _selected_release = null;
        private Install _selected_install = null;

        public frmPHP()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.lstAvailable.FormattingEnabled = true;
            this.lstAvailable.DisplayMember = "Version";
            this.lstAvailable.ValueMember = "Url";
            this.lstInstalled.FormattingEnabled = true;
            this.lstInstalled.DisplayMember = "Version";
            this.lstInstalled.ValueMember = "Path";
            this.rdbArchive.Checked = false;
            this.rdbRelease.Checked = true;
            this.rdbArchive.CheckedChanged += new EventHandler(CheckedChanged);
            this.rdbRelease.CheckedChanged += new EventHandler(CheckedChanged);

            Libraries.Forms.ChangeFont(this, Fonts.Get("Varela Round"));
            Libraries.Forms.SetGradientBackground(this);
        }

        /// <summary>
        /// Change php archives list
        /// </summary>
        private void CheckedChanged(object sender, EventArgs e)
        {
            _php.Type = rdbArchive.Checked ? EVersionType.Archive : EVersionType.Release;
        }

        /// <summary>
        /// Install a selected version
        /// </summary>
        private void Install()
        {
            if (this._selected_release == null)
            {
                Dialog.Info("Please select a version to Install");
            }
            else
            {
                string version = this._selected_release.Version.ToString(3);
                Log("Installing PHP " + version + " from " + this._selected_release.Url + "... ");

                if (_php.Install(this._selected_release))
                {
                    Log("Success");
                    Dialog.Info("PHP " + version + " installed successfully");
                    RefreshInstalls();
                }
                else
                {
                    Log("Failed");
                    Dialog.Error("Can not install PHP " + version);
                }
            }
        }

        /// <summary>
        /// Refresh installed versions
        /// </summary>
        private void RefreshInstalls()
        {
            Log("Refreshing installed versions... ");

            if (_php.RefreshInstalls())
            {
                Libraries.Forms.InvokeControl(this.lstInstalled, () => this.lstInstalled.Items.Clear());
                this._selected_install = null;

                foreach (KeyValuePair<Version, Install> i in _php.Installs)
                {
                    Libraries.Forms.InvokeControl(this.lstInstalled, () => this.lstInstalled.Items.Insert(0, i.Value));
                }

                Log("Success");
            }   
            else
            {
                Log("Failed");
                Dialog.Error("Can not get installed versions. Please try again!");
            }
        }

        /// <summary>
        /// Refresh Available list
        /// </summary>
        private void RefreshReleases()
        {
            Log("Reading versions list from " + this._php.Url + "... ");

            if (_php.RefreshReleases())
            {
                Libraries.Forms.InvokeControl(this.lstAvailable, () => this.lstAvailable.Items.Clear());
                this._selected_release = null;

                foreach (KeyValuePair<Version, Release> r in _php.Releases)
                {
                    Libraries.Forms.InvokeControl(this.lstAvailable, () => this.lstAvailable.Items.Insert(0, r.Value));
                }

                Log("Success");
            }
            else
            {
                Log("Failed");
                Dialog.Error("Can not fetch version list. Please try again!");
                return;
            }
        }

        /// <summary>
        /// Remove an installed version
        /// </summary>
        private void Remove()
        {
            if (this._selected_install == null)
            {
                Libraries.Forms.InvokeControl(this, () => Dialog.Info("Please select a version to Remove"));
            }
            else
            {
                string version = this._selected_install.Version.ToString(3);

                if (Dialog.Confirm("Are you sure you want to remove PHP " + version) == DialogResult.Yes)
                {
                    Log("Removing PHP " + version + "... ");

                    if (_php.Remove(this._selected_install))
                    {
                        Libraries.Forms.InvokeControl(this.lstInstalled, () => this.lstInstalled.Items.Remove(this._selected_install));
                        Log("Success");
                        Dialog.Info("PHP " + version + " removed successfully");
                    }
                    else
                    {
                        Log("Failed");
                        Dialog.Error("Can not remove PHP " + version);
                    }
                }
            }
        }

        /// <summary>
        /// Set waiting status
        /// </summary>
        private void SetWaiting(bool is_waiting)
        {
            Libraries.Forms.InvokeControl(this.panControls, () => this.panControls.Enabled = !is_waiting);
            Libraries.Forms.InvokeControl(this, () => this.Cursor = is_waiting ? Cursors.WaitCursor : Cursors.Default);
        }

        /// <summary>
        /// Log message to textbox
        /// </summary>
        /// <param name="message"></param>
        private void Log(string message, bool new_line = true, bool time = true)
        {
            message = (time ? "[" + DateTime.Now.ToString("HH:mm:ss") + "] " : "") + message;
            message += new_line ? "\r\n" : "";
            Libraries.Forms.InvokeControl(txtLog, () => txtLog.AppendText(message));
        }

        /// <summary>
        /// Click Refresh button
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetWaiting(true);

            this._tasks.CreateTask(() =>
            {
                RefreshReleases();
                SetWaiting(false);
            });
        }

        /// <summary>
        /// Click Install button
        /// </summary>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            SetWaiting(true);

            this._tasks.CreateTask(() =>
            {
                Install();
                SetWaiting(false);
            });
        }

        /// <summary>
        /// Click Remove button
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            SetWaiting(true);

            this._tasks.CreateTask(() =>
            {
                Remove();
                SetWaiting(false);
            });
        }

        /// <summary>
        /// Form closing
        /// </summary>
        private void frmPHP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Cursor == Cursors.WaitCursor)
            {
                if (Dialog.Confirm("Do you want to abort current action?") == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    this._tasks.CancelTasks();
                }
            }
        }

        /// <summary>
        /// Form shown
        /// </summary>
        private void frmPHP_Shown(object sender, EventArgs e)
        {
            SetWaiting(true);

            this._tasks.CreateTask(() =>
            {
                RefreshReleases();
                RefreshInstalls();
                SetWaiting(false);
            });
        }

        /// <summary>
        /// Select a release
        /// </summary>
        private void lstAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstAvailable.SelectedIndex == -1)
            {
                this._selected_release = null;
            }
            else
            {
                this._selected_release = (Release)this.lstAvailable.SelectedItem;
            }
        }

        /// <summary>
        /// Select an install
        /// </summary>
        private void lstInstalled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstInstalled.SelectedIndex == -1)
            {
                this._selected_install = null;
            }
            else
            {
                this._selected_install = (Install)this.lstInstalled.SelectedItem;
            }
        }
    }
}
