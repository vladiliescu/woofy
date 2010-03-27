namespace Woofy.Gui
{
    partial class TaskDetailsForm
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
			System.Windows.Forms.Label label2;
			this.cbComics = new System.Windows.Forms.ComboBox();
			this.numComicsToDownload = new System.Windows.Forms.NumericUpDown();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rbDownloadOnlyNew = new System.Windows.Forms.RadioButton();
			this.rbDownloadLast = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDownloadFolder = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.chkOverrideStartUrl = new System.Windows.Forms.CheckBox();
			this.txtOverrideStartUrl = new System.Windows.Forms.TextBox();
			this.gbAdvanced = new System.Windows.Forms.GroupBox();
			this.chkRandomPauses = new System.Windows.Forms.CheckBox();
			this.chkAdvancedOptions = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.notificationToolTip = new System.Windows.Forms.ToolTip(this.components);
			label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numComicsToDownload)).BeginInit();
			this.gbAdvanced.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(335, 65);
			label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 7;
			label2.Text = "comics";
			// 
			// cbComics
			// 
			this.cbComics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbComics.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cbComics.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbComics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComics.FormattingEnabled = true;
			this.cbComics.Location = new System.Drawing.Point(8, 18);
			this.cbComics.Margin = new System.Windows.Forms.Padding(2);
			this.cbComics.MaxDropDownItems = 15;
			this.cbComics.Name = "cbComics";
			this.cbComics.Size = new System.Drawing.Size(367, 21);
			this.cbComics.TabIndex = 1;
			this.cbComics.SelectedIndexChanged += new System.EventHandler(this.cbComics_SelectedIndexChanged);
			// 
			// numComicsToDownload
			// 
			this.numComicsToDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numComicsToDownload.Location = new System.Drawing.Point(285, 63);
			this.numComicsToDownload.Margin = new System.Windows.Forms.Padding(2);
			this.numComicsToDownload.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.numComicsToDownload.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numComicsToDownload.Name = "numComicsToDownload";
			this.numComicsToDownload.Size = new System.Drawing.Size(46, 20);
			this.numComicsToDownload.TabIndex = 6;
			this.numComicsToDownload.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numComicsToDownload.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numComicsToDownload.Click += new System.EventHandler(this.numComicsToDownload_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(238, 259);
			this.btnOk.Margin = new System.Windows.Forms.Padding(2);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(317, 259);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// rbDownloadOnlyNew
			// 
			this.rbDownloadOnlyNew.AutoSize = true;
			this.rbDownloadOnlyNew.Checked = true;
			this.rbDownloadOnlyNew.Location = new System.Drawing.Point(8, 63);
			this.rbDownloadOnlyNew.Margin = new System.Windows.Forms.Padding(2);
			this.rbDownloadOnlyNew.Name = "rbDownloadOnlyNew";
			this.rbDownloadOnlyNew.Size = new System.Drawing.Size(137, 17);
			this.rbDownloadOnlyNew.TabIndex = 4;
			this.rbDownloadOnlyNew.TabStop = true;
			this.rbDownloadOnlyNew.Text = "Download latest comics";
			this.rbDownloadOnlyNew.UseVisualStyleBackColor = true;
			// 
			// rbDownloadLast
			// 
			this.rbDownloadLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbDownloadLast.AutoSize = true;
			this.rbDownloadLast.Location = new System.Drawing.Point(189, 63);
			this.rbDownloadLast.Margin = new System.Windows.Forms.Padding(2);
			this.rbDownloadLast.Name = "rbDownloadLast";
			this.rbDownloadLast.Size = new System.Drawing.Size(92, 17);
			this.rbDownloadLast.TabIndex = 5;
			this.rbDownloadLast.Text = "Download last";
			this.rbDownloadLast.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 44);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Download folder";
			// 
			// txtDownloadFolder
			// 
			this.txtDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDownloadFolder.Location = new System.Drawing.Point(8, 60);
			this.txtDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
			this.txtDownloadFolder.Name = "txtDownloadFolder";
			this.txtDownloadFolder.Size = new System.Drawing.Size(289, 20);
			this.txtDownloadFolder.TabIndex = 9;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(301, 58);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 10;
			this.btnBrowse.Text = "B&rowse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// chkOverrideStartUrl
			// 
			this.chkOverrideStartUrl.AutoSize = true;
			this.chkOverrideStartUrl.Location = new System.Drawing.Point(8, 18);
			this.chkOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
			this.chkOverrideStartUrl.Name = "chkOverrideStartUrl";
			this.chkOverrideStartUrl.Size = new System.Drawing.Size(103, 17);
			this.chkOverrideStartUrl.TabIndex = 2;
			this.chkOverrideStartUrl.Text = "Override start url";
			this.chkOverrideStartUrl.UseVisualStyleBackColor = true;
			this.chkOverrideStartUrl.CheckedChanged += new System.EventHandler(this.chkOverrideStartUrl_CheckedChanged);
			// 
			// txtOverrideStartUrl
			// 
			this.txtOverrideStartUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOverrideStartUrl.Enabled = false;
			this.txtOverrideStartUrl.Location = new System.Drawing.Point(8, 39);
			this.txtOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
			this.txtOverrideStartUrl.Name = "txtOverrideStartUrl";
			this.txtOverrideStartUrl.Size = new System.Drawing.Size(366, 20);
			this.txtOverrideStartUrl.TabIndex = 3;
			// 
			// gbAdvanced
			// 
			this.gbAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbAdvanced.Controls.Add(this.chkRandomPauses);
			this.gbAdvanced.Controls.Add(this.txtOverrideStartUrl);
			this.gbAdvanced.Controls.Add(this.numComicsToDownload);
			this.gbAdvanced.Controls.Add(this.chkOverrideStartUrl);
			this.gbAdvanced.Controls.Add(label2);
			this.gbAdvanced.Controls.Add(this.rbDownloadOnlyNew);
			this.gbAdvanced.Controls.Add(this.rbDownloadLast);
			this.gbAdvanced.Location = new System.Drawing.Point(12, 135);
			this.gbAdvanced.Name = "gbAdvanced";
			this.gbAdvanced.Size = new System.Drawing.Size(379, 110);
			this.gbAdvanced.TabIndex = 13;
			this.gbAdvanced.TabStop = false;
			this.gbAdvanced.Text = "Advanced";
			// 
			// chkRandomPauses
			// 
			this.chkRandomPauses.AutoSize = true;
			this.chkRandomPauses.Location = new System.Drawing.Point(8, 85);
			this.chkRandomPauses.Name = "chkRandomPauses";
			this.chkRandomPauses.Size = new System.Drawing.Size(190, 17);
			this.chkRandomPauses.TabIndex = 11;
			this.chkRandomPauses.Text = "Random pauses between requests";
			this.chkRandomPauses.UseVisualStyleBackColor = true;
			// 
			// chkAdvancedOptions
			// 
			this.chkAdvancedOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAdvancedOptions.AutoSize = true;
			this.chkAdvancedOptions.Location = new System.Drawing.Point(235, 94);
			this.chkAdvancedOptions.Name = "chkAdvancedOptions";
			this.chkAdvancedOptions.Size = new System.Drawing.Size(141, 17);
			this.chkAdvancedOptions.TabIndex = 14;
			this.chkAdvancedOptions.Text = "Show &advanced options";
			this.chkAdvancedOptions.UseVisualStyleBackColor = true;
			this.chkAdvancedOptions.CheckedChanged += new System.EventHandler(this.OnAdvancedOptionsCheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.cbComics);
			this.groupBox1.Controls.Add(this.chkAdvancedOptions);
			this.groupBox1.Controls.Add(this.txtDownloadFolder);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(380, 117);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Comic";
			// 
			// notificationToolTip
			// 
			this.notificationToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
			this.notificationToolTip.ToolTipTitle = "Warning";
			// 
			// TaskDetailsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(403, 293);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gbAdvanced);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskDetailsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Task Details";
			this.Load += new System.EventHandler(this.TaskDetails_Load);
			((System.ComponentModel.ISupportInitialize)(this.numComicsToDownload)).EndInit();
			this.gbAdvanced.ResumeLayout(false);
			this.gbAdvanced.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComics;
        private System.Windows.Forms.NumericUpDown numComicsToDownload;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbDownloadOnlyNew;
		private System.Windows.Forms.RadioButton rbDownloadLast;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOverrideStartUrl;
        private System.Windows.Forms.CheckBox chkOverrideStartUrl;
		private System.Windows.Forms.GroupBox gbAdvanced;
		private System.Windows.Forms.CheckBox chkAdvancedOptions;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolTip notificationToolTip;
		private System.Windows.Forms.CheckBox chkRandomPauses;
    }
}