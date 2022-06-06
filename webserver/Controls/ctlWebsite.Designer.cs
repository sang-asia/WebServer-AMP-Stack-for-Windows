
namespace WebServer.Controls
{
    partial class ctlWebsite
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblAlias = new System.Windows.Forms.Label();
            this.panMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAction = new System.Windows.Forms.LinkLabel();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDomain = new System.Windows.Forms.LinkLabel();
            this.picSSL = new System.Windows.Forms.PictureBox();
            this.panBottom = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.LinkLabel();
            this.lblPHPVersion = new System.Windows.Forms.Label();
            this.panMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSSL)).BeginInit();
            this.panBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAlias
            // 
            this.lblAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlias.AutoEllipsis = true;
            this.lblAlias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblAlias.Location = new System.Drawing.Point(0, 18);
            this.lblAlias.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(227, 16);
            this.lblAlias.TabIndex = 1;
            this.lblAlias.Text = "johndoe.com";
            this.lblAlias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panMain.Controls.Add(this.panel1);
            this.panMain.Controls.Add(this.lblAlias);
            this.panMain.Controls.Add(this.panBottom);
            this.panMain.Location = new System.Drawing.Point(3, 10);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(229, 135);
            this.panMain.TabIndex = 2;
            this.panMain.ClientSizeChanged += new System.EventHandler(this.panMain_ClientSizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAction);
            this.panel1.Controls.Add(this.lblDomain);
            this.panel1.Controls.Add(this.picSSL);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 18);
            this.panel1.TabIndex = 3;
            // 
            // btnAction
            // 
            this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAction.ContextMenuStrip = this.ctxMenu;
            this.btnAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction.Image = global::WebServer.Properties.Resources.icons8_settings_16;
            this.btnAction.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAction.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnAction.LinkColor = System.Drawing.Color.Crimson;
            this.btnAction.Location = new System.Drawing.Point(191, -1);
            this.btnAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(22, 16);
            this.btnAction.TabIndex = 5;
            this.btnAction.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuEdit,
            this.toolStripSeparator1,
            this.ctxMenuDelete});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(111, 54);
            // 
            // ctxMenuEdit
            // 
            this.ctxMenuEdit.Image = global::WebServer.Properties.Resources.icons8_settings_16;
            this.ctxMenuEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxMenuEdit.Name = "ctxMenuEdit";
            this.ctxMenuEdit.Size = new System.Drawing.Size(110, 22);
            this.ctxMenuEdit.Text = "Config";
            this.ctxMenuEdit.Click += new System.EventHandler(this.ctxMenuEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // ctxMenuDelete
            // 
            this.ctxMenuDelete.Image = global::WebServer.Properties.Resources.icons8_trash_16;
            this.ctxMenuDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxMenuDelete.Name = "ctxMenuDelete";
            this.ctxMenuDelete.Size = new System.Drawing.Size(110, 22);
            this.ctxMenuDelete.Text = "Delete";
            this.ctxMenuDelete.Click += new System.EventHandler(this.ctxMenuDelete_Click);
            // 
            // lblDomain
            // 
            this.lblDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDomain.AutoEllipsis = true;
            this.lblDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomain.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblDomain.LinkColor = System.Drawing.Color.Black;
            this.lblDomain.Location = new System.Drawing.Point(0, 0);
            this.lblDomain.Margin = new System.Windows.Forms.Padding(0);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(166, 16);
            this.lblDomain.TabIndex = 4;
            this.lblDomain.TabStop = true;
            this.lblDomain.Text = "johndoe.com";
            this.lblDomain.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDomain_LinkClicked);
            // 
            // picSSL
            // 
            this.picSSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSSL.Image = global::WebServer.Properties.Resources.icons8_security_ssl_16;
            this.picSSL.Location = new System.Drawing.Point(173, 0);
            this.picSSL.Name = "picSSL";
            this.picSSL.Size = new System.Drawing.Size(20, 16);
            this.picSSL.TabIndex = 6;
            this.picSSL.TabStop = false;
            this.picSSL.Visible = false;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.lblPath);
            this.panBottom.Controls.Add(this.lblPHPVersion);
            this.panBottom.Location = new System.Drawing.Point(0, 34);
            this.panBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(215, 16);
            this.panBottom.TabIndex = 4;
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblPath.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPath.Location = new System.Drawing.Point(0, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(166, 16);
            this.lblPath.TabIndex = 6;
            this.lblPath.TabStop = true;
            this.lblPath.Text = "Open";
            this.lblPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblPath_LinkClicked);
            // 
            // lblPHPVersion
            // 
            this.lblPHPVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPHPVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPHPVersion.Location = new System.Drawing.Point(169, 0);
            this.lblPHPVersion.Name = "lblPHPVersion";
            this.lblPHPVersion.Size = new System.Drawing.Size(46, 18);
            this.lblPHPVersion.TabIndex = 5;
            this.lblPHPVersion.Text = "7.4.15";
            this.lblPHPVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctlWebsite
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ctlWebsite";
            this.Size = new System.Drawing.Size(235, 155);
            this.panMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ctxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSSL)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.FlowLayoutPanel panMain;
        private System.Windows.Forms.LinkLabel lblDomain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPHPVersion;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.LinkLabel btnAction;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDelete;
        private System.Windows.Forms.LinkLabel lblPath;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox picSSL;
    }
}
