
namespace WebServer.Forms
{
    partial class frmWebsite
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
            this.panMain = new System.Windows.Forms.Panel();
            this.grpPHP = new System.Windows.Forms.GroupBox();
            this.chkExtensions = new System.Windows.Forms.CheckedListBox();
            this.lblPostMaxSize = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPostMaxSize = new System.Windows.Forms.TextBox();
            this.txtMaxInputVars = new System.Windows.Forms.TextBox();
            this.txtMaxUpload = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.chkUseHosts = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboPHP = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPublicPath = new System.Windows.Forms.TextBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lblClearFolder = new System.Windows.Forms.LinkLabel();
            this.panMain.SuspendLayout();
            this.grpPHP.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panMain.BackColor = System.Drawing.Color.Transparent;
            this.panMain.Controls.Add(this.lblClearFolder);
            this.panMain.Controls.Add(this.btnFolder);
            this.panMain.Controls.Add(this.grpPHP);
            this.panMain.Controls.Add(this.chkSSL);
            this.panMain.Controls.Add(this.chkUseHosts);
            this.panMain.Controls.Add(this.btnDelete);
            this.panMain.Controls.Add(this.btnSave);
            this.panMain.Controls.Add(this.cboPHP);
            this.panMain.Controls.Add(this.txtName);
            this.panMain.Controls.Add(this.txtFolder);
            this.panMain.Controls.Add(this.txtPublicPath);
            this.panMain.Controls.Add(this.txtDomain);
            this.panMain.Controls.Add(this.label3);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.label8);
            this.panMain.Controls.Add(this.label4);
            this.panMain.Controls.Add(this.label1);
            this.panMain.Location = new System.Drawing.Point(12, 12);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(405, 519);
            this.panMain.TabIndex = 0;
            // 
            // grpPHP
            // 
            this.grpPHP.Controls.Add(this.chkExtensions);
            this.grpPHP.Controls.Add(this.lblPostMaxSize);
            this.grpPHP.Controls.Add(this.label6);
            this.grpPHP.Controls.Add(this.label5);
            this.grpPHP.Controls.Add(this.label7);
            this.grpPHP.Controls.Add(this.txtPostMaxSize);
            this.grpPHP.Controls.Add(this.txtMaxInputVars);
            this.grpPHP.Controls.Add(this.txtMaxUpload);
            this.grpPHP.Controls.Add(this.txtTimeout);
            this.grpPHP.Location = new System.Drawing.Point(6, 206);
            this.grpPHP.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.grpPHP.Name = "grpPHP";
            this.grpPHP.Size = new System.Drawing.Size(394, 244);
            this.grpPHP.TabIndex = 31;
            this.grpPHP.TabStop = false;
            this.grpPHP.Text = "PHP Config";
            // 
            // chkExtensions
            // 
            this.chkExtensions.CheckOnClick = true;
            this.chkExtensions.FormattingEnabled = true;
            this.chkExtensions.IntegralHeight = false;
            this.chkExtensions.Location = new System.Drawing.Point(9, 19);
            this.chkExtensions.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.chkExtensions.Name = "chkExtensions";
            this.chkExtensions.Size = new System.Drawing.Size(204, 212);
            this.chkExtensions.TabIndex = 40;
            // 
            // lblPostMaxSize
            // 
            this.lblPostMaxSize.AutoSize = true;
            this.lblPostMaxSize.Location = new System.Drawing.Point(219, 68);
            this.lblPostMaxSize.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblPostMaxSize.Name = "lblPostMaxSize";
            this.lblPostMaxSize.Size = new System.Drawing.Size(76, 13);
            this.lblPostMaxSize.TabIndex = 0;
            this.lblPostMaxSize.Text = "post_max_size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 19);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "upload_max_filesize";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "max_input_vars";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(219, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "max_execution_time (seconds)";
            // 
            // txtPostMaxSize
            // 
            this.txtPostMaxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPostMaxSize.Location = new System.Drawing.Point(222, 84);
            this.txtPostMaxSize.Name = "txtPostMaxSize";
            this.txtPostMaxSize.Size = new System.Drawing.Size(166, 20);
            this.txtPostMaxSize.TabIndex = 50;
            this.txtPostMaxSize.Text = "2M";
            this.txtPostMaxSize.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // txtMaxInputVars
            // 
            this.txtMaxInputVars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxInputVars.Location = new System.Drawing.Point(222, 182);
            this.txtMaxInputVars.Name = "txtMaxInputVars";
            this.txtMaxInputVars.Size = new System.Drawing.Size(166, 20);
            this.txtMaxInputVars.TabIndex = 70;
            this.txtMaxInputVars.Text = "10000";
            this.txtMaxInputVars.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // txtMaxUpload
            // 
            this.txtMaxUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxUpload.Location = new System.Drawing.Point(222, 35);
            this.txtMaxUpload.Name = "txtMaxUpload";
            this.txtMaxUpload.Size = new System.Drawing.Size(166, 20);
            this.txtMaxUpload.TabIndex = 50;
            this.txtMaxUpload.Text = "2M";
            this.txtMaxUpload.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // txtTimeout
            // 
            this.txtTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeout.Location = new System.Drawing.Point(222, 133);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(166, 20);
            this.txtTimeout.TabIndex = 60;
            this.txtTimeout.Text = "300";
            this.txtTimeout.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(275, 28);
            this.chkSSL.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(86, 17);
            this.chkSSL.TabIndex = 3;
            this.chkSSL.Text = "SSL Support";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // chkUseHosts
            // 
            this.chkUseHosts.AutoSize = true;
            this.chkUseHosts.Location = new System.Drawing.Point(275, 175);
            this.chkUseHosts.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chkUseHosts.Name = "chkUseHosts";
            this.chkUseHosts.Size = new System.Drawing.Size(99, 17);
            this.chkUseHosts.TabIndex = 35;
            this.chkUseHosts.Text = "Add hosts entry";
            this.chkUseHosts.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackgroundImage = global::WebServer.Properties.Resources.icons8_trash_16;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDelete.Location = new System.Drawing.Point(6, 490);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImage = global::WebServer.Properties.Resources.icons8_save_16__1_;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(325, 490);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1000;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboPHP
            // 
            this.cboPHP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPHP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPHP.FormattingEnabled = true;
            this.cboPHP.Location = new System.Drawing.Point(275, 124);
            this.cboPHP.Name = "cboPHP";
            this.cboPHP.Size = new System.Drawing.Size(125, 21);
            this.cboPHP.TabIndex = 8;
            this.cboPHP.SelectedIndexChanged += new System.EventHandler(this.cboPHP_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(6, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(263, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtPublicPath
            // 
            this.txtPublicPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPublicPath.Location = new System.Drawing.Point(6, 124);
            this.txtPublicPath.Name = "txtPublicPath";
            this.txtPublicPath.Size = new System.Drawing.Size(263, 20);
            this.txtPublicPath.TabIndex = 5;
            this.txtPublicPath.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // txtDomain
            // 
            this.txtDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDomain.Location = new System.Drawing.Point(6, 173);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(263, 20);
            this.txtDomain.TabIndex = 30;
            this.txtDomain.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "PHP Version";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Public path (Eg: public, www)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 157);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domain";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "External folder:";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(6, 75);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(263, 20);
            this.txtFolder.TabIndex = 3;
            this.txtFolder.TextChanged += new System.EventHandler(this.txtDomain_TextChanged);
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(275, 75);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(38, 20);
            this.btnFolder.TabIndex = 4;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lblClearFolder
            // 
            this.lblClearFolder.AutoSize = true;
            this.lblClearFolder.Location = new System.Drawing.Point(86, 59);
            this.lblClearFolder.Name = "lblClearFolder";
            this.lblClearFolder.Size = new System.Drawing.Size(92, 13);
            this.lblClearFolder.TabIndex = 1001;
            this.lblClearFolder.TabStop = true;
            this.lblClearFolder.Text = "Use internal folder";
            this.lblClearFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClearFolder_LinkClicked);
            // 
            // frmWebsite
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(432, 543);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWebsite";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Website Config";
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.grpPHP.ResumeLayout(false);
            this.grpPHP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.ComboBox cboPHP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPublicPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkUseHosts;
        private System.Windows.Forms.CheckedListBox chkExtensions;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.TextBox txtMaxUpload;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpPHP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaxInputVars;
        private System.Windows.Forms.Label lblPostMaxSize;
        private System.Windows.Forms.TextBox txtPostMaxSize;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.LinkLabel lblClearFolder;
    }
}