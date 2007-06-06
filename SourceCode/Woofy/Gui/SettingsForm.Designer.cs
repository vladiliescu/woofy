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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.txtDefaultDownloadFolder = new System.Windows.Forms.TextBox();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(271, 128);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 128);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default download folder";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(350, 31);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "B&rowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Location = new System.Drawing.Point(13, 68);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(114, 17);
            this.chkUseProxy.TabIndex = 3;
            this.chkUseProxy.Text = "Use a proxy server";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(35, 94);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(290, 94);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "Port:";
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Enabled = false;
            this.txtProxyAddress.Location = new System.Drawing.Point(88, 91);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(183, 20);
            this.txtProxyAddress.TabIndex = 5;
            // 
            // txtDefaultDownloadFolder
            // 
            this.txtDefaultDownloadFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Woofy.Properties.Settings.Default, "DefaultDownloadFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDefaultDownloadFolder.Location = new System.Drawing.Point(11, 32);
            this.txtDefaultDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtDefaultDownloadFolder.Name = "txtDefaultDownloadFolder";
            this.txtDefaultDownloadFolder.Size = new System.Drawing.Size(335, 20);
            this.txtDefaultDownloadFolder.TabIndex = 1;
            this.txtDefaultDownloadFolder.Text = global::Woofy.Properties.Settings.Default.DefaultDownloadFolder;
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.Enabled = false;
            this.errorProvider.SetIconPadding(this.txtProxyPort, 2);
            this.txtProxyPort.Location = new System.Drawing.Point(326, 91);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.Size = new System.Drawing.Size(99, 20);
            this.txtProxyPort.TabIndex = 10;
            this.txtProxyPort.Validating += new System.ComponentModel.CancelEventHandler(this.txtProxyPort_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(445, 163);
            this.Controls.Add(this.txtProxyPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtProxyAddress);
            this.Controls.Add(this.chkUseProxy);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDefaultDownloadFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}