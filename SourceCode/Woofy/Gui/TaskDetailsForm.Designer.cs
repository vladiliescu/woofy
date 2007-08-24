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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.cbComics = new System.Windows.Forms.ComboBox();
            this.numComicsToDownload = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbDownloadOnlyNew = new System.Windows.Forms.RadioButton();
            this.rbDownloadLast = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkOverrideStartUrl = new System.Windows.Forms.CheckBox();
            this.txtOverrideStartUrl = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numComicsToDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(9, 10);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(36, 13);
            label1.TabIndex = 0;
            label1.Text = "Comic";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(365, 100);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(40, 13);
            label2.TabIndex = 5;
            label2.Text = "comics";
            // 
            // cbComics
            // 
            this.cbComics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComics.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbComics.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbComics.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.cbComics, 3);
            this.cbComics.Location = new System.Drawing.Point(9, 26);
            this.cbComics.Margin = new System.Windows.Forms.Padding(2);
            this.cbComics.Name = "cbComics";
            this.cbComics.Size = new System.Drawing.Size(396, 21);
            this.cbComics.TabIndex = 1;
            this.cbComics.SelectedIndexChanged += new System.EventHandler(this.cbComics_SelectedIndexChanged);
            // 
            // numComicsToDownload
            // 
            this.numComicsToDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numComicsToDownload.Location = new System.Drawing.Point(315, 98);
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
            this.numComicsToDownload.TabIndex = 4;
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
            this.btnOk.Location = new System.Drawing.Point(251, 167);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(330, 167);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbDownloadOnlyNew
            // 
            this.rbDownloadOnlyNew.AutoSize = true;
            this.rbDownloadOnlyNew.Checked = true;
            this.rbDownloadOnlyNew.Location = new System.Drawing.Point(9, 98);
            this.rbDownloadOnlyNew.Margin = new System.Windows.Forms.Padding(2);
            this.rbDownloadOnlyNew.Name = "rbDownloadOnlyNew";
            this.rbDownloadOnlyNew.Size = new System.Drawing.Size(137, 17);
            this.rbDownloadOnlyNew.TabIndex = 2;
            this.rbDownloadOnlyNew.TabStop = true;
            this.rbDownloadOnlyNew.Text = "Download latest comics";
            this.rbDownloadOnlyNew.UseVisualStyleBackColor = true;
            // 
            // rbDownloadLast
            // 
            this.rbDownloadLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDownloadLast.AutoSize = true;
            this.rbDownloadLast.Location = new System.Drawing.Point(219, 98);
            this.rbDownloadLast.Margin = new System.Windows.Forms.Padding(2);
            this.rbDownloadLast.Name = "rbDownloadLast";
            this.rbDownloadLast.Size = new System.Drawing.Size(92, 17);
            this.rbDownloadLast.TabIndex = 3;
            this.rbDownloadLast.Text = "Download last";
            this.rbDownloadLast.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 118);
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
            this.txtDownloadFolder.Location = new System.Drawing.Point(6, 134);
            this.txtDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.Size = new System.Drawing.Size(320, 20);
            this.txtDownloadFolder.TabIndex = 9;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(330, 132);
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
            this.chkOverrideStartUrl.Location = new System.Drawing.Point(9, 50);
            this.chkOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
            this.chkOverrideStartUrl.Name = "chkOverrideStartUrl";
            this.chkOverrideStartUrl.Size = new System.Drawing.Size(103, 17);
            this.chkOverrideStartUrl.TabIndex = 11;
            this.chkOverrideStartUrl.Text = "Override start url";
            this.chkOverrideStartUrl.UseVisualStyleBackColor = true;
            this.chkOverrideStartUrl.CheckedChanged += new System.EventHandler(this.chkOverrideStartUrl_CheckedChanged);
            // 
            // txtOverrideStartUrl
            // 
            this.txtOverrideStartUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOverrideStartUrl.Enabled = false;
            this.txtOverrideStartUrl.Location = new System.Drawing.Point(9, 72);
            this.txtOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtOverrideStartUrl.Name = "txtOverrideStartUrl";
            this.txtOverrideStartUrl.Size = new System.Drawing.Size(396, 20);
            this.txtOverrideStartUrl.TabIndex = 12;
            // 
            // TaskDetailsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(416, 199);
            this.Controls.Add(this.txtOverrideStartUrl);
            this.Controls.Add(this.chkOverrideStartUrl);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDownloadFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbDownloadLast);
            this.Controls.Add(this.rbDownloadOnlyNew);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.numComicsToDownload);
            this.Controls.Add(this.cbComics);
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComics;
        private System.Windows.Forms.NumericUpDown numComicsToDownload;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbDownloadOnlyNew;
        private System.Windows.Forms.RadioButton rbDownloadLast;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOverrideStartUrl;
        private System.Windows.Forms.CheckBox chkOverrideStartUrl;
    }
}