
namespace WebServer.Controls
{
    partial class ctlServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlServer));
            this.lblPortText = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panMain = new System.Windows.Forms.FlowLayoutPanel();
            this.pic64bit = new System.Windows.Forms.PictureBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.panSub = new System.Windows.Forms.FlowLayoutPanel();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblLine = new System.Windows.Forms.Label();
            this.prbPercent = new System.Windows.Forms.ProgressBar();
            this.lblLineLight = new System.Windows.Forms.Label();
            this.btnControl = new System.Windows.Forms.Button();
            this.btnActions = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic64bit)).BeginInit();
            this.panSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPortText
            // 
            this.lblPortText.AutoSize = true;
            this.lblPortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortText.Location = new System.Drawing.Point(127, 0);
            this.lblPortText.Margin = new System.Windows.Forms.Padding(0);
            this.lblPortText.Name = "lblPortText";
            this.lblPortText.Size = new System.Drawing.Size(34, 13);
            this.lblPortText.TabIndex = 5;
            this.lblPortText.Text = "Port:";
            this.lblPortText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(15, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 24);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Apache";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(161, 0);
            this.lblPort.Margin = new System.Windows.Forms.Padding(0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(13, 13);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "0";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusText.Location = new System.Drawing.Point(15, 0);
            this.lblStatusText.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(47, 13);
            this.lblStatusText.TabIndex = 7;
            this.lblStatusText.Text = "Status:";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblStatus.Location = new System.Drawing.Point(62, 0);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(65, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "RUNNING";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(121, 0);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(22, 20);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "     ";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panMain.Controls.Add(this.lblName);
            this.panMain.Controls.Add(this.pic64bit);
            this.panMain.Controls.Add(this.lblMessage);
            this.panMain.Controls.Add(this.lblPercent);
            this.panMain.Location = new System.Drawing.Point(-5, 10);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.panMain.Size = new System.Drawing.Size(356, 32);
            this.panMain.TabIndex = 12;
            this.panMain.WrapContents = false;
            // 
            // pic64bit
            // 
            this.pic64bit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic64bit.Image = global::WebServer.Properties.Resources.icons8_64_bit_48;
            this.pic64bit.Location = new System.Drawing.Point(97, 3);
            this.pic64bit.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.pic64bit.Name = "pic64bit";
            this.pic64bit.Size = new System.Drawing.Size(21, 21);
            this.pic64bit.TabIndex = 13;
            this.pic64bit.TabStop = false;
            this.pic64bit.Visible = false;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoEllipsis = true;
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(143, 0);
            this.lblPercent.Margin = new System.Windows.Forms.Padding(0);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblPercent.Size = new System.Drawing.Size(22, 20);
            this.lblPercent.TabIndex = 12;
            this.lblPercent.Text = "     ";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panSub
            // 
            this.panSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panSub.Controls.Add(this.lblStatusText);
            this.panSub.Controls.Add(this.lblStatus);
            this.panSub.Controls.Add(this.lblPortText);
            this.panSub.Controls.Add(this.lblPort);
            this.panSub.Location = new System.Drawing.Point(-5, 45);
            this.panSub.Name = "panSub";
            this.panSub.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.panSub.Size = new System.Drawing.Size(356, 16);
            this.panSub.TabIndex = 13;
            // 
            // ctxMenu
            // 
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(61, 4);
            this.ctxMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.ctxMenu_Closed);
            // 
            // lblLine
            // 
            this.lblLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLine.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLine.Location = new System.Drawing.Point(0, 69);
            this.lblLine.Margin = new System.Windows.Forms.Padding(0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(442, 1);
            this.lblLine.TabIndex = 14;
            // 
            // prbPercent
            // 
            this.prbPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbPercent.Location = new System.Drawing.Point(15, 45);
            this.prbPercent.Name = "prbPercent";
            this.prbPercent.Size = new System.Drawing.Size(417, 16);
            this.prbPercent.TabIndex = 15;
            this.prbPercent.Visible = false;
            // 
            // lblLineLight
            // 
            this.lblLineLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLineLight.BackColor = System.Drawing.Color.White;
            this.lblLineLight.Location = new System.Drawing.Point(0, 70);
            this.lblLineLight.Margin = new System.Windows.Forms.Padding(0);
            this.lblLineLight.Name = "lblLineLight";
            this.lblLineLight.Size = new System.Drawing.Size(442, 1);
            this.lblLineLight.TabIndex = 17;
            // 
            // btnControl
            // 
            this.btnControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnControl.Location = new System.Drawing.Point(357, 10);
            this.btnControl.Name = "btnControl";
            this.btnControl.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnControl.Size = new System.Drawing.Size(75, 24);
            this.btnControl.TabIndex = 2;
            this.btnControl.Text = "...";
            this.btnControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnControl.UseVisualStyleBackColor = false;
            this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnActions
            // 
            this.btnActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActions.BackgroundImage = global::WebServer.Properties.Resources.icons8_settings_16;
            this.btnActions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnActions.ContextMenuStrip = this.ctxMenu;
            this.btnActions.FlatAppearance.BorderSize = 0;
            this.btnActions.Location = new System.Drawing.Point(357, 37);
            this.btnActions.Name = "btnActions";
            this.btnActions.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnActions.Size = new System.Drawing.Size(75, 24);
            this.btnActions.TabIndex = 18;
            this.btnActions.Text = "Options";
            this.btnActions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActions.UseVisualStyleBackColor = false;
            this.btnActions.Click += new System.EventHandler(this.btnActions_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInstall.BackgroundImage")));
            this.btnInstall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInstall.FlatAppearance.BorderSize = 0;
            this.btnInstall.Location = new System.Drawing.Point(357, 10);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnInstall.Size = new System.Drawing.Size(75, 24);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Install";
            this.btnInstall.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // ctlServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnActions);
            this.Controls.Add(this.lblLineLight);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.panSub);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.prbPercent);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnControl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ctlServer";
            this.Size = new System.Drawing.Size(442, 71);
            this.SizeChanged += new System.EventHandler(this.ctlServer_SizeChanged);
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic64bit)).EndInit();
            this.panSub.ResumeLayout(false);
            this.panSub.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPortText;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.FlowLayoutPanel panMain;
        private System.Windows.Forms.FlowLayoutPanel panSub;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.ProgressBar prbPercent;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.Label lblLineLight;
        private System.Windows.Forms.Button btnActions;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.PictureBox pic64bit;
    }
}
