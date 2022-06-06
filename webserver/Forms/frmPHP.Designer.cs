
namespace WebServer.Forms
{
    partial class frmPHP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstAvailable = new System.Windows.Forms.ListBox();
            this.lstInstalled = new System.Windows.Forms.ListBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbRelease = new System.Windows.Forms.RadioButton();
            this.rdbArchive = new System.Windows.Forms.RadioButton();
            this.panControls = new System.Windows.Forms.Panel();
            this.grpVersionsList = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panControls.SuspendLayout();
            this.grpVersionsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.lstAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAvailable.FormattingEnabled = true;
            this.lstAvailable.IntegralHeight = false;
            this.lstAvailable.Location = new System.Drawing.Point(0, 21);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(117, 209);
            this.lstAvailable.TabIndex = 0;
            this.lstAvailable.SelectedIndexChanged += new System.EventHandler(this.lstAvailable_SelectedIndexChanged);
            // 
            // lstInstalled
            // 
            this.lstInstalled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstInstalled.FormattingEnabled = true;
            this.lstInstalled.IntegralHeight = false;
            this.lstInstalled.Location = new System.Drawing.Point(238, 21);
            this.lstInstalled.Name = "lstInstalled";
            this.lstInstalled.Size = new System.Drawing.Size(117, 209);
            this.lstInstalled.TabIndex = 200;
            this.lstInstalled.SelectedIndexChanged += new System.EventHandler(this.lstInstalled_SelectedIndexChanged);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(123, 21);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(109, 23);
            this.btnInstall.TabIndex = 100;
            this.btnInstall.Text = "Install >>";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(123, 50);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(109, 23);
            this.btnRemove.TabIndex = 101;
            this.btnRemove.Text = "<< Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(123, 207);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 23);
            this.btnRefresh.TabIndex = 104;
            this.btnRefresh.Text = "Refresh...";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 101;
            this.label1.Text = "Available";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(235, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 101;
            this.label2.Text = "Installed";
            // 
            // rdbRelease
            // 
            this.rdbRelease.AutoSize = true;
            this.rdbRelease.BackColor = System.Drawing.Color.Transparent;
            this.rdbRelease.Checked = true;
            this.rdbRelease.Location = new System.Drawing.Point(10, 22);
            this.rdbRelease.Name = "rdbRelease";
            this.rdbRelease.Size = new System.Drawing.Size(64, 17);
            this.rdbRelease.TabIndex = 102;
            this.rdbRelease.TabStop = true;
            this.rdbRelease.Text = "Release";
            this.rdbRelease.UseVisualStyleBackColor = false;
            // 
            // rdbArchive
            // 
            this.rdbArchive.AutoSize = true;
            this.rdbArchive.BackColor = System.Drawing.Color.Transparent;
            this.rdbArchive.Location = new System.Drawing.Point(10, 45);
            this.rdbArchive.Name = "rdbArchive";
            this.rdbArchive.Size = new System.Drawing.Size(61, 17);
            this.rdbArchive.TabIndex = 103;
            this.rdbArchive.Text = "Archive";
            this.rdbArchive.UseVisualStyleBackColor = false;
            // 
            // panControls
            // 
            this.panControls.BackColor = System.Drawing.Color.Transparent;
            this.panControls.Controls.Add(this.grpVersionsList);
            this.panControls.Controls.Add(this.label1);
            this.panControls.Controls.Add(this.lstAvailable);
            this.panControls.Controls.Add(this.label2);
            this.panControls.Controls.Add(this.lstInstalled);
            this.panControls.Controls.Add(this.btnInstall);
            this.panControls.Controls.Add(this.btnRemove);
            this.panControls.Controls.Add(this.btnRefresh);
            this.panControls.Location = new System.Drawing.Point(12, 12);
            this.panControls.Name = "panControls";
            this.panControls.Size = new System.Drawing.Size(357, 230);
            this.panControls.TabIndex = 103;
            // 
            // grpVersionsList
            // 
            this.grpVersionsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpVersionsList.Controls.Add(this.rdbRelease);
            this.grpVersionsList.Controls.Add(this.rdbArchive);
            this.grpVersionsList.Location = new System.Drawing.Point(123, 128);
            this.grpVersionsList.Name = "grpVersionsList";
            this.grpVersionsList.Size = new System.Drawing.Size(109, 73);
            this.grpVersionsList.TabIndex = 102;
            this.grpVersionsList.TabStop = false;
            this.grpVersionsList.Text = "Versions List";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(12, 248);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(357, 162);
            this.txtLog.TabIndex = 300;
            // 
            // frmPHP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 422);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPHP";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage PHP Versions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPHP_FormClosing);
            this.Shown += new System.EventHandler(this.frmPHP_Shown);
            this.panControls.ResumeLayout(false);
            this.grpVersionsList.ResumeLayout(false);
            this.grpVersionsList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstAvailable;
        private System.Windows.Forms.ListBox lstInstalled;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbRelease;
        private System.Windows.Forms.RadioButton rdbArchive;
        private System.Windows.Forms.Panel panControls;
        private System.Windows.Forms.GroupBox grpVersionsList;
        private System.Windows.Forms.TextBox txtLog;
    }
}