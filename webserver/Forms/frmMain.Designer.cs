
namespace WebServer.Forms
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblLine = new System.Windows.Forms.Label();
            this.panHeader = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picLogos = new System.Windows.Forms.PictureBox();
            this.lblLineLight = new System.Windows.Forms.Label();
            this.panWebsiteAction = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCreateWebsite = new System.Windows.Forms.Button();
            this.btnPHP = new System.Windows.Forms.Button();
            this.timChecker = new System.Windows.Forms.Timer(this.components);
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.panServers = new System.Windows.Forms.Panel();
            this.srvMongoDB = new WebServer.Controls.ctlServer();
            this.srvMariaDB = new WebServer.Controls.ctlServer();
            this.srvMySQL = new WebServer.Controls.ctlServer();
            this.srvApache = new WebServer.Controls.ctlServer();
            this.panWebsites = new System.Windows.Forms.Panel();
            this.panServersBorder = new System.Windows.Forms.Panel();
            this.panWebsitesBorder = new System.Windows.Forms.Panel();
            this.panLogBorder = new System.Windows.Forms.Panel();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogos)).BeginInit();
            this.panWebsiteAction.SuspendLayout();
            this.panServers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLine
            // 
            this.lblLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLine.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLine.Location = new System.Drawing.Point(0, 85);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(823, 1);
            this.lblLine.TabIndex = 4;
            this.lblLine.Text = "   ";
            // 
            // panHeader
            // 
            this.panHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panHeader.Controls.Add(this.picLogo);
            this.panHeader.Controls.Add(this.picLogos);
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(823, 85);
            this.panHeader.TabIndex = 2;
            // 
            // picLogo
            // 
            this.picLogo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::WebServer.Properties.Resources.Logo;
            this.picLogo.Location = new System.Drawing.Point(12, 10);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(264, 64);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 3;
            this.picLogo.TabStop = false;
            // 
            // picLogos
            // 
            this.picLogos.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.picLogos.BackColor = System.Drawing.Color.Transparent;
            this.picLogos.Image = global::WebServer.Properties.Resources.Logos;
            this.picLogos.Location = new System.Drawing.Point(428, 10);
            this.picLogos.Name = "picLogos";
            this.picLogos.Size = new System.Drawing.Size(384, 64);
            this.picLogos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogos.TabIndex = 2;
            this.picLogos.TabStop = false;
            // 
            // lblLineLight
            // 
            this.lblLineLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLineLight.BackColor = System.Drawing.Color.White;
            this.lblLineLight.Location = new System.Drawing.Point(1, 86);
            this.lblLineLight.Name = "lblLineLight";
            this.lblLineLight.Size = new System.Drawing.Size(823, 1);
            this.lblLineLight.TabIndex = 5;
            this.lblLineLight.Text = "   ";
            // 
            // panWebsiteAction
            // 
            this.panWebsiteAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWebsiteAction.BackColor = System.Drawing.Color.Transparent;
            this.panWebsiteAction.Controls.Add(this.btnRefresh);
            this.panWebsiteAction.Controls.Add(this.btnCreateWebsite);
            this.panWebsiteAction.Controls.Add(this.btnPHP);
            this.panWebsiteAction.Location = new System.Drawing.Point(12, 101);
            this.panWebsiteAction.Margin = new System.Windows.Forms.Padding(0);
            this.panWebsiteAction.Name = "panWebsiteAction";
            this.panWebsiteAction.Size = new System.Drawing.Size(800, 24);
            this.panWebsiteAction.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::WebServer.Properties.Resources.icons8_refresh_16;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRefresh.Location = new System.Drawing.Point(537, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(89, 24);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh...";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCreateWebsite
            // 
            this.btnCreateWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWebsite.BackgroundImage = global::WebServer.Properties.Resources.icons8_plus_16;
            this.btnCreateWebsite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCreateWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateWebsite.Location = new System.Drawing.Point(711, 0);
            this.btnCreateWebsite.Name = "btnCreateWebsite";
            this.btnCreateWebsite.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCreateWebsite.Size = new System.Drawing.Size(90, 24);
            this.btnCreateWebsite.TabIndex = 5;
            this.btnCreateWebsite.Text = "Website";
            this.btnCreateWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateWebsite.UseVisualStyleBackColor = true;
            this.btnCreateWebsite.Click += new System.EventHandler(this.btnCreateWebsite_Click);
            // 
            // btnPHP
            // 
            this.btnPHP.BackgroundImage = global::WebServer.Properties.Resources.icons8_cloud_binary_code_16;
            this.btnPHP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPHP.Location = new System.Drawing.Point(-1, 0);
            this.btnPHP.Name = "btnPHP";
            this.btnPHP.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnPHP.Size = new System.Drawing.Size(109, 24);
            this.btnPHP.TabIndex = 4;
            this.btnPHP.Text = "PHP Versions";
            this.btnPHP.UseVisualStyleBackColor = true;
            this.btnPHP.Click += new System.EventHandler(this.btnPHP_Click);
            // 
            // timChecker
            // 
            this.timChecker.Enabled = true;
            this.timChecker.Tick += new System.EventHandler(this.timChecker_Tick);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.DetectUrls = false;
            this.txtLog.Location = new System.Drawing.Point(13, 449);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLog.ShortcutsEnabled = false;
            this.txtLog.Size = new System.Drawing.Size(520, 159);
            this.txtLog.TabIndex = 8;
            this.txtLog.Text = "";
            // 
            // panServers
            // 
            this.panServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panServers.AutoScroll = true;
            this.panServers.BackColor = System.Drawing.Color.Transparent;
            this.panServers.Controls.Add(this.srvMongoDB);
            this.panServers.Controls.Add(this.srvMariaDB);
            this.panServers.Controls.Add(this.srvMySQL);
            this.panServers.Controls.Add(this.srvApache);
            this.panServers.Location = new System.Drawing.Point(13, 132);
            this.panServers.Name = "panServers";
            this.panServers.Size = new System.Drawing.Size(520, 300);
            this.panServers.TabIndex = 9;
            // 
            // srvMongoDB
            // 
            this.srvMongoDB.BackColor = System.Drawing.Color.Transparent;
            this.srvMongoDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.srvMongoDB.Downloading = false;
            this.srvMongoDB.HasOptionsButton = false;
            this.srvMongoDB.Installed = false;
            this.srvMongoDB.Label = "MongoDB";
            this.srvMongoDB.Location = new System.Drawing.Point(0, 213);
            this.srvMongoDB.Margin = new System.Windows.Forms.Padding(0);
            this.srvMongoDB.Message = "     ";
            this.srvMongoDB.Name = "srvMongoDB";
            this.srvMongoDB.Only64Bit = false;
            this.srvMongoDB.Percent = 0;
            this.srvMongoDB.Port = new int[] {
        3306};
            this.srvMongoDB.Running = false;
            this.srvMongoDB.Size = new System.Drawing.Size(520, 71);
            this.srvMongoDB.TabIndex = 3;
            this.srvMongoDB.Waiting = false;
            // 
            // srvMariaDB
            // 
            this.srvMariaDB.BackColor = System.Drawing.Color.Transparent;
            this.srvMariaDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.srvMariaDB.Downloading = false;
            this.srvMariaDB.HasOptionsButton = false;
            this.srvMariaDB.Installed = false;
            this.srvMariaDB.Label = "MariaDB";
            this.srvMariaDB.Location = new System.Drawing.Point(0, 142);
            this.srvMariaDB.Margin = new System.Windows.Forms.Padding(0);
            this.srvMariaDB.Message = "     ";
            this.srvMariaDB.Name = "srvMariaDB";
            this.srvMariaDB.Only64Bit = false;
            this.srvMariaDB.Percent = 0;
            this.srvMariaDB.Port = new int[] {
        3306};
            this.srvMariaDB.Running = false;
            this.srvMariaDB.Size = new System.Drawing.Size(520, 71);
            this.srvMariaDB.TabIndex = 3;
            this.srvMariaDB.Waiting = false;
            // 
            // srvMySQL
            // 
            this.srvMySQL.BackColor = System.Drawing.Color.Transparent;
            this.srvMySQL.Dock = System.Windows.Forms.DockStyle.Top;
            this.srvMySQL.Downloading = false;
            this.srvMySQL.HasOptionsButton = false;
            this.srvMySQL.Installed = false;
            this.srvMySQL.Label = "MySQL";
            this.srvMySQL.Location = new System.Drawing.Point(0, 71);
            this.srvMySQL.Margin = new System.Windows.Forms.Padding(0);
            this.srvMySQL.Message = "     ";
            this.srvMySQL.Name = "srvMySQL";
            this.srvMySQL.Only64Bit = false;
            this.srvMySQL.Percent = 0;
            this.srvMySQL.Port = new int[] {
        3306};
            this.srvMySQL.Running = false;
            this.srvMySQL.Size = new System.Drawing.Size(520, 71);
            this.srvMySQL.TabIndex = 2;
            this.srvMySQL.Waiting = false;
            // 
            // srvApache
            // 
            this.srvApache.BackColor = System.Drawing.Color.Transparent;
            this.srvApache.Dock = System.Windows.Forms.DockStyle.Top;
            this.srvApache.Downloading = false;
            this.srvApache.HasOptionsButton = false;
            this.srvApache.Installed = false;
            this.srvApache.Label = "Apache";
            this.srvApache.Location = new System.Drawing.Point(0, 0);
            this.srvApache.Margin = new System.Windows.Forms.Padding(0);
            this.srvApache.Message = "     ";
            this.srvApache.Name = "srvApache";
            this.srvApache.Only64Bit = false;
            this.srvApache.Percent = 0;
            this.srvApache.Port = new int[] {
        80};
            this.srvApache.Running = false;
            this.srvApache.Size = new System.Drawing.Size(520, 71);
            this.srvApache.TabIndex = 1;
            this.srvApache.Waiting = false;
            // 
            // panWebsites
            // 
            this.panWebsites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWebsites.AutoScroll = true;
            this.panWebsites.BackColor = System.Drawing.Color.Transparent;
            this.panWebsites.Location = new System.Drawing.Point(550, 132);
            this.panWebsites.Name = "panWebsites";
            this.panWebsites.Size = new System.Drawing.Size(261, 476);
            this.panWebsites.TabIndex = 10;
            // 
            // panServersBorder
            // 
            this.panServersBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panServersBorder.BackColor = System.Drawing.Color.Gainsboro;
            this.panServersBorder.Location = new System.Drawing.Point(12, 131);
            this.panServersBorder.Name = "panServersBorder";
            this.panServersBorder.Size = new System.Drawing.Size(522, 302);
            this.panServersBorder.TabIndex = 11;
            // 
            // panWebsitesBorder
            // 
            this.panWebsitesBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWebsitesBorder.BackColor = System.Drawing.Color.Gainsboro;
            this.panWebsitesBorder.Location = new System.Drawing.Point(549, 131);
            this.panWebsitesBorder.Name = "panWebsitesBorder";
            this.panWebsitesBorder.Size = new System.Drawing.Size(263, 478);
            this.panWebsitesBorder.TabIndex = 11;
            // 
            // panLogBorder
            // 
            this.panLogBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panLogBorder.BackColor = System.Drawing.Color.Gainsboro;
            this.panLogBorder.Location = new System.Drawing.Point(12, 448);
            this.panLogBorder.Name = "panLogBorder";
            this.panLogBorder.Size = new System.Drawing.Size(522, 161);
            this.panLogBorder.TabIndex = 11;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(824, 621);
            this.Controls.Add(this.panWebsites);
            this.Controls.Add(this.panServers);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panWebsiteAction);
            this.Controls.Add(this.lblLineLight);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.panLogBorder);
            this.Controls.Add(this.panWebsitesBorder);
            this.Controls.Add(this.panServersBorder);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(840, 660);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebServer / AMP Stack";
            this.panHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogos)).EndInit();
            this.panWebsiteAction.ResumeLayout(false);
            this.panServers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctlServer srvApache;
        private Controls.ctlServer srvMariaDB;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox picLogos;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblLineLight;
        private Controls.ctlServer srvMySQL;
        private System.Windows.Forms.Panel panWebsiteAction;
        private System.Windows.Forms.Button btnCreateWebsite;
        private System.Windows.Forms.Button btnPHP;
        private System.Windows.Forms.Button btnRefresh;
        private Controls.ctlServer srvMongoDB;
        private System.Windows.Forms.Timer timChecker;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Panel panServers;
        private System.Windows.Forms.Panel panWebsites;
        private System.Windows.Forms.Panel panServersBorder;
        private System.Windows.Forms.Panel panWebsitesBorder;
        private System.Windows.Forms.Panel panLogBorder;
    }
}