namespace Woofy.Flows.Main
{
    partial class MainForm
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
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvwTasks = new System.Windows.Forms.DataGridView();
            this.colStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.colComic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDownloadedStrips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentPage = new System.Windows.Forms.DataGridViewLinkColumn();
            this.txtAppLog = new System.Windows.Forms.RichTextBox();
            this.tsbOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAddComic = new System.Windows.Forms.ToolStripButton();
            this.tsbEditComic = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveComic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPauseComic = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripButton();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.dgvwTasks);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.txtAppLog);
            splitContainer1.Size = new System.Drawing.Size(642, 491);
            splitContainer1.SplitterDistance = 335;
            splitContainer1.TabIndex = 10;
            // 
            // dgvwTasks
            // 
            this.dgvwTasks.AllowUserToAddRows = false;
            this.dgvwTasks.AllowUserToDeleteRows = false;
            this.dgvwTasks.AllowUserToResizeRows = false;
            this.dgvwTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvwTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatus,
            this.colComic,
            this.colDownloadedStrips,
            this.colCurrentPage});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvwTasks.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvwTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvwTasks.Location = new System.Drawing.Point(0, 0);
            this.dgvwTasks.Margin = new System.Windows.Forms.Padding(2);
            this.dgvwTasks.MultiSelect = false;
            this.dgvwTasks.Name = "dgvwTasks";
            this.dgvwTasks.ReadOnly = true;
            this.dgvwTasks.RowHeadersVisible = false;
            this.dgvwTasks.RowTemplate.Height = 24;
            this.dgvwTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvwTasks.Size = new System.Drawing.Size(642, 335);
            this.dgvwTasks.TabIndex = 7;
            this.dgvwTasks.DoubleClick += new System.EventHandler(this.OnToggleComicState);
            this.dgvwTasks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnGridCellFormatting);
            this.dgvwTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridCellContentClick);
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.MenuText;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle1;
            this.colStatus.HeaderText = "";
            this.colStatus.MinimumWidth = 20;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colStatus.Width = 20;
            // 
            // colComic
            // 
            this.colComic.DataPropertyName = "Name";
            this.colComic.FillWeight = 30F;
            this.colComic.HeaderText = "Comic";
            this.colComic.Name = "colComic";
            this.colComic.ReadOnly = true;
            this.colComic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colComic.Width = 175;
            // 
            // colDownloadedStrips
            // 
            this.colDownloadedStrips.DataPropertyName = "DownloadedStrips";
            this.colDownloadedStrips.HeaderText = "Strips";
            this.colDownloadedStrips.Name = "colDownloadedStrips";
            this.colDownloadedStrips.ReadOnly = true;
            this.colDownloadedStrips.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDownloadedStrips.Width = 50;
            // 
            // colCurrentPage
            // 
            this.colCurrentPage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCurrentPage.DataPropertyName = "CurrentPage";
            this.colCurrentPage.FillWeight = 70F;
            this.colCurrentPage.HeaderText = "Current page";
            this.colCurrentPage.LinkColor = System.Drawing.Color.Blue;
            this.colCurrentPage.Name = "colCurrentPage";
            this.colCurrentPage.ReadOnly = true;
            this.colCurrentPage.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // txtAppLog
            // 
            this.txtAppLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAppLog.Location = new System.Drawing.Point(0, 0);
            this.txtAppLog.Name = "txtAppLog";
            this.txtAppLog.ReadOnly = true;
            this.txtAppLog.Size = new System.Drawing.Size(642, 152);
            this.txtAppLog.TabIndex = 9;
            this.txtAppLog.Text = "";
            // 
            // tsbOpenFolder
            // 
            this.tsbOpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFolder.Image")));
            this.tsbOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFolder.Name = "tsbOpenFolder";
            this.tsbOpenFolder.Size = new System.Drawing.Size(84, 22);
            this.tsbOpenFolder.Text = "Open folder";
            this.tsbOpenFolder.ToolTipText = "Open the comic\'s download folder";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddComic,
            this.tsbEditComic,
            this.tsbRemoveComic,
            this.toolStripSeparator1,
            this.tsbPauseComic,
            this.tsbOpenFolder,
            this.tsbAbout,
            this.tsbSettings,
            this.tsbDonate});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(642, 25);
            this.toolStrip.TabIndex = 8;
            this.toolStrip.Text = "toolStrip";
            // 
            // tsbAddComic
            // 
            this.tsbAddComic.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddComic.Image")));
            this.tsbAddComic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddComic.Name = "tsbAddComic";
            this.tsbAddComic.Size = new System.Drawing.Size(87, 22);
            this.tsbAddComic.Text = "Add comic...";
            this.tsbAddComic.ToolTipText = "Add a new comic (Ctrl+N)";
            // 
            // tsbEditComic
            // 
            this.tsbEditComic.Image = global::Woofy.Properties.Resources.Edit;
            this.tsbEditComic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditComic.Name = "tsbEditComic";
            this.tsbEditComic.Size = new System.Drawing.Size(57, 22);
            this.tsbEditComic.Text = "Edit...";
            this.tsbEditComic.ToolTipText = "Edit the selected comic";
            // 
            // tsbRemoveComic
            // 
            this.tsbRemoveComic.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveComic.Image")));
            this.tsbRemoveComic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveComic.Name = "tsbRemoveComic";
            this.tsbRemoveComic.Size = new System.Drawing.Size(66, 22);
            this.tsbRemoveComic.Text = "Remove";
            this.tsbRemoveComic.ToolTipText = "Remove the selected comic";
            this.tsbRemoveComic.Click += new System.EventHandler(this.OnRemoveComic);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPauseComic
            // 
            this.tsbPauseComic.Image = global::Woofy.Properties.Resources.Paused;
            this.tsbPauseComic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPauseComic.Name = "tsbPauseComic";
            this.tsbPauseComic.Size = new System.Drawing.Size(98, 22);
            this.tsbPauseComic.Text = "Pause/Resume";
            this.tsbPauseComic.ToolTipText = "Pause/resume the selected comic";
            this.tsbPauseComic.Click += new System.EventHandler(this.OnToggleComicState);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAbout.Image = global::Woofy.Properties.Resources.About;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(56, 22);
            this.tsbAbout.Text = "About";
            // 
            // tsbSettings
            // 
            this.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSettings.Image = global::Woofy.Properties.Resources.Settings;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(66, 22);
            this.tsbSettings.Text = "Settings";
            // 
            // tsbDonate
            // 
            this.tsbDonate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbDonate.Image = global::Woofy.Properties.Resources.Donate;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(62, 22);
            this.tsbDonate.Text = "Donate";
            this.tsbDonate.ToolTipText = "it\'s good for the soul";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 516);
            this.Controls.Add(splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Woofy";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvwTasks;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton tsbSettings;
		private System.Windows.Forms.RichTextBox txtAppLog;
		private System.Windows.Forms.DataGridViewImageColumn colStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn colComic;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDownloadedStrips;
		private System.Windows.Forms.DataGridViewLinkColumn colCurrentPage;
		private System.Windows.Forms.ToolStripButton tsbAddComic;
		private System.Windows.Forms.ToolStripButton tsbPauseComic;
		private System.Windows.Forms.ToolStripButton tsbRemoveComic;
		private System.Windows.Forms.ToolStripButton tsbOpenFolder;
		private System.Windows.Forms.ToolStripButton tsbDonate;
		private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripButton tsbEditComic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}