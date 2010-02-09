namespace Woofy.Gui
{
    partial class SettingsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtDefaultDownloadFolder = new System.Windows.Forms.TextBox();
			this.chkAutomaticallyCheckForUpdates = new System.Windows.Forms.CheckBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
			this.chkUseProxy = new System.Windows.Forms.CheckBox();
			this.txtProxyAddress = new System.Windows.Forms.TextBox();
			this.txtProxyPort = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.lblPort = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.chkUseCredentials = new System.Windows.Forms.CheckBox();
			this.chkCloseWhenAllComicsHaveFinished = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Default download folder";
			// 
			// txtDefaultDownloadFolder
			// 
			this.txtDefaultDownloadFolder.Location = new System.Drawing.Point(7, 20);
			this.txtDefaultDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
			this.txtDefaultDownloadFolder.Name = "txtDefaultDownloadFolder";
			this.txtDefaultDownloadFolder.Size = new System.Drawing.Size(332, 20);
			this.txtDefaultDownloadFolder.TabIndex = 0;
			// 
			// chkAutomaticallyCheckForUpdates
			// 
			this.chkAutomaticallyCheckForUpdates.AutoSize = true;
			this.chkAutomaticallyCheckForUpdates.Checked = true;
			this.chkAutomaticallyCheckForUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutomaticallyCheckForUpdates.Location = new System.Drawing.Point(7, 68);
			this.chkAutomaticallyCheckForUpdates.Name = "chkAutomaticallyCheckForUpdates";
			this.chkAutomaticallyCheckForUpdates.Size = new System.Drawing.Size(177, 17);
			this.chkAutomaticallyCheckForUpdates.TabIndex = 3;
			this.chkAutomaticallyCheckForUpdates.Text = "&Automatically check for updates";
			this.chkAutomaticallyCheckForUpdates.UseVisualStyleBackColor = true;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(343, 19);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "B&rowse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// chkMinimizeToTray
			// 
			this.chkMinimizeToTray.AutoSize = true;
			this.chkMinimizeToTray.Checked = true;
			this.chkMinimizeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMinimizeToTray.Location = new System.Drawing.Point(7, 45);
			this.chkMinimizeToTray.Name = "chkMinimizeToTray";
			this.chkMinimizeToTray.Size = new System.Drawing.Size(98, 17);
			this.chkMinimizeToTray.TabIndex = 2;
			this.chkMinimizeToTray.Text = "&Minimize to tray";
			this.chkMinimizeToTray.UseVisualStyleBackColor = true;
			// 
			// chkUseProxy
			// 
			this.chkUseProxy.AutoSize = true;
			this.chkUseProxy.Location = new System.Drawing.Point(6, 6);
			this.chkUseProxy.Name = "chkUseProxy";
			this.chkUseProxy.Size = new System.Drawing.Size(114, 17);
			this.chkUseProxy.TabIndex = 3;
			this.chkUseProxy.Text = "Use a proxy server";
			this.chkUseProxy.UseVisualStyleBackColor = true;
			this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
			// 
			// txtProxyAddress
			// 
			this.txtProxyAddress.Enabled = false;
			this.txtProxyAddress.Location = new System.Drawing.Point(81, 29);
			this.txtProxyAddress.Name = "txtProxyAddress";
			this.txtProxyAddress.Size = new System.Drawing.Size(196, 20);
			this.txtProxyAddress.TabIndex = 5;
			// 
			// txtProxyPort
			// 
			this.txtProxyPort.Enabled = false;
			this.errorProvider.SetIconPadding(this.txtProxyPort, 2);
			this.txtProxyPort.Location = new System.Drawing.Point(319, 29);
			this.txtProxyPort.Name = "txtProxyPort";
			this.txtProxyPort.Size = new System.Drawing.Size(99, 20);
			this.txtProxyPort.TabIndex = 10;
			this.txtProxyPort.Validating += new System.ComponentModel.CancelEventHandler(this.txtProxyPort_Validating);
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(28, 32);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(48, 13);
			this.lblAddress.TabIndex = 4;
			this.lblAddress.Text = "Address:";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(283, 32);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(29, 13);
			this.lblPort.TabIndex = 6;
			this.lblPort.Text = "Port:";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(292, 169);
			this.btnOK.Margin = new System.Windows.Forms.Padding(2);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(371, 169);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// txtPassword
			// 
			this.txtPassword.Enabled = false;
			this.errorProvider.SetIconPadding(this.txtPassword, 2);
			this.txtPassword.Location = new System.Drawing.Point(293, 80);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(125, 20);
			this.txtPassword.TabIndex = 15;
			this.txtPassword.UseSystemPasswordChar = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(434, 152);
			this.tabControl1.TabIndex = 15;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.chkCloseWhenAllComicsHaveFinished);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.txtDefaultDownloadFolder);
			this.tabPage1.Controls.Add(this.chkAutomaticallyCheckForUpdates);
			this.tabPage1.Controls.Add(this.chkMinimizeToTray);
			this.tabPage1.Controls.Add(this.btnBrowse);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(426, 126);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtPassword);
			this.tabPage2.Controls.Add(this.txtUsername);
			this.tabPage2.Controls.Add(this.lblPassword);
			this.tabPage2.Controls.Add(this.lblUsername);
			this.tabPage2.Controls.Add(this.chkUseCredentials);
			this.tabPage2.Controls.Add(this.chkUseProxy);
			this.tabPage2.Controls.Add(this.txtProxyPort);
			this.tabPage2.Controls.Add(this.txtProxyAddress);
			this.tabPage2.Controls.Add(this.lblPort);
			this.tabPage2.Controls.Add(this.lblAddress);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(426, 126);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Advanced";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtUsername
			// 
			this.txtUsername.Enabled = false;
			this.txtUsername.Location = new System.Drawing.Point(92, 80);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(133, 20);
			this.txtUsername.TabIndex = 13;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(231, 83);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 14;
			this.lblPassword.Text = "Password:";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(28, 83);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(58, 13);
			this.lblUsername.TabIndex = 12;
			this.lblUsername.Text = "Username:";
			// 
			// chkUseCredentials
			// 
			this.chkUseCredentials.AutoSize = true;
			this.chkUseCredentials.Location = new System.Drawing.Point(4, 57);
			this.chkUseCredentials.Name = "chkUseCredentials";
			this.chkUseCredentials.Size = new System.Drawing.Size(99, 17);
			this.chkUseCredentials.TabIndex = 11;
			this.chkUseCredentials.Text = "Use credentials";
			this.chkUseCredentials.UseVisualStyleBackColor = true;
			this.chkUseCredentials.CheckedChanged += new System.EventHandler(this.chkUseCredentials_CheckedChanged);
			// 
			// chkCloseWhenAllComicsHaveFinished
			// 
			this.chkCloseWhenAllComicsHaveFinished.AutoSize = true;
			this.chkCloseWhenAllComicsHaveFinished.Location = new System.Drawing.Point(7, 91);
			this.chkCloseWhenAllComicsHaveFinished.Name = "chkCloseWhenAllComicsHaveFinished";
			this.chkCloseWhenAllComicsHaveFinished.Size = new System.Drawing.Size(293, 17);
			this.chkCloseWhenAllComicsHaveFinished.TabIndex = 4;
			this.chkCloseWhenAllComicsHaveFinished.Text = "&Close Woofy when all comics have finished downloading";
			this.chkCloseWhenAllComicsHaveFinished.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(455, 204);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDefaultDownloadFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtProxyPort;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.CheckBox chkAutomaticallyCheckForUpdates;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.CheckBox chkUseCredentials;
		private System.Windows.Forms.CheckBox chkCloseWhenAllComicsHaveFinished;
    }
}