namespace Woofy.Gui.Main
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.dgvwTasks = new System.Windows.Forms.DataGridView();
			this.TaskStatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.TaskNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ComicsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpenTaskFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemNewTask = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPauseTask = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonSelectComics = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPauseTask = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonOpenFolder = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.hideShowWoofyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.stopAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.trayMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvwTasks
			// 
			this.dgvwTasks.AllowUserToAddRows = false;
			this.dgvwTasks.AllowUserToDeleteRows = false;
			this.dgvwTasks.AllowUserToResizeRows = false;
			this.dgvwTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dgvwTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskStatusColumn,
            this.TaskNameColumn,
            this.ComicsColumn});
			this.dgvwTasks.ContextMenuStrip = this.contextMenuStrip;
			this.dgvwTasks.Location = new System.Drawing.Point(9, 23);
			this.dgvwTasks.Margin = new System.Windows.Forms.Padding(2);
			this.dgvwTasks.Name = "dgvwTasks";
			this.dgvwTasks.ReadOnly = true;
			this.dgvwTasks.RowHeadersVisible = false;
			this.dgvwTasks.RowTemplate.Height = 24;
			this.dgvwTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwTasks.Size = new System.Drawing.Size(614, 410);
			this.dgvwTasks.TabIndex = 7;
			this.dgvwTasks.DoubleClick += new System.EventHandler(this.dgvwTasks_DoubleClick);
			this.dgvwTasks.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvwTasks_DataBindingComplete);
			this.dgvwTasks.SelectionChanged += new System.EventHandler(this.dgvwTasks_SelectionChanged);
			// 
			// TaskStatusColumn
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = null;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.MenuText;
			this.TaskStatusColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.TaskStatusColumn.HeaderText = "";
			this.TaskStatusColumn.MinimumWidth = 20;
			this.TaskStatusColumn.Name = "TaskStatusColumn";
			this.TaskStatusColumn.ReadOnly = true;
			this.TaskStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.TaskStatusColumn.Width = 20;
			// 
			// TaskNameColumn
			// 
			this.TaskNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TaskNameColumn.DataPropertyName = "Name";
			this.TaskNameColumn.HeaderText = "Task";
			this.TaskNameColumn.Name = "TaskNameColumn";
			this.TaskNameColumn.ReadOnly = true;
			this.TaskNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ComicsColumn
			// 
			this.ComicsColumn.HeaderText = "Comics";
			this.ComicsColumn.Name = "ComicsColumn";
			this.ComicsColumn.ReadOnly = true;
			this.ComicsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenTaskFolder,
            this.toolStripSeparator1,
            this.toolStripMenuItemNewTask,
            this.toolStripMenuItemPauseTask});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip.Size = new System.Drawing.Size(183, 98);
			// 
			// toolStripMenuItemOpenTaskFolder
			// 
			this.toolStripMenuItemOpenTaskFolder.Image = global::Woofy.Properties.Resources.OpenFolder;
			this.toolStripMenuItemOpenTaskFolder.Name = "toolStripMenuItemOpenTaskFolder";
			this.toolStripMenuItemOpenTaskFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.toolStripMenuItemOpenTaskFolder.Size = new System.Drawing.Size(182, 22);
			this.toolStripMenuItemOpenTaskFolder.Text = "Open folder";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
			// 
			// toolStripMenuItemNewTask
			// 
			this.toolStripMenuItemNewTask.Image = global::Woofy.Properties.Resources.New;
			this.toolStripMenuItemNewTask.Name = "toolStripMenuItemNewTask";
			this.toolStripMenuItemNewTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.toolStripMenuItemNewTask.Size = new System.Drawing.Size(182, 22);
			this.toolStripMenuItemNewTask.Text = "New";
			// 
			// toolStripMenuItemPauseTask
			// 
			this.toolStripMenuItemPauseTask.Image = global::Woofy.Properties.Resources.Paused;
			this.toolStripMenuItemPauseTask.Name = "toolStripMenuItemPauseTask";
			this.toolStripMenuItemPauseTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.toolStripMenuItemPauseTask.Size = new System.Drawing.Size(182, 22);
			this.toolStripMenuItemPauseTask.Text = "Pause";
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSelectComics,
            this.toolStripButtonPauseTask,
            this.toolStripSeparator2,
            this.toolStripButtonOpenFolder,
            this.toolStripButtonSettings});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(632, 25);
			this.toolStrip.TabIndex = 8;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripButtonSelectComics
			// 
			this.toolStripButtonSelectComics.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectComics.Image")));
			this.toolStripButtonSelectComics.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSelectComics.Name = "toolStripButtonSelectComics";
			this.toolStripButtonSelectComics.Size = new System.Drawing.Size(102, 22);
			this.toolStripButtonSelectComics.Text = "Select comics...";
			this.toolStripButtonSelectComics.ToolTipText = "Creates a new task (Ctrl+S)";
			// 
			// toolStripButtonPauseTask
			// 
			this.toolStripButtonPauseTask.Enabled = false;
			this.toolStripButtonPauseTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPauseTask.Image")));
			this.toolStripButtonPauseTask.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPauseTask.Name = "toolStripButtonPauseTask";
			this.toolStripButtonPauseTask.Size = new System.Drawing.Size(56, 22);
			this.toolStripButtonPauseTask.Text = "Pause";
			this.toolStripButtonPauseTask.ToolTipText = "Pauses/resumes the selected tasks (Ctrl+P)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonOpenFolder
			// 
			this.toolStripButtonOpenFolder.Enabled = false;
			this.toolStripButtonOpenFolder.Image = global::Woofy.Properties.Resources.OpenFolder;
			this.toolStripButtonOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpenFolder.Name = "toolStripButtonOpenFolder";
			this.toolStripButtonOpenFolder.Size = new System.Drawing.Size(84, 22);
			this.toolStripButtonOpenFolder.Text = "Open folder";
			this.toolStripButtonOpenFolder.ToolTipText = "Opens the folder where the comics of the selected task have been downloaded (Ctrl" +
				"+O)";
			// 
			// toolStripButtonSettings
			// 
			this.toolStripButtonSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButtonSettings.Image = global::Woofy.Properties.Resources.Settings;
			this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSettings.Name = "toolStripButtonSettings";
			this.toolStripButtonSettings.Size = new System.Drawing.Size(66, 22);
			this.toolStripButtonSettings.Text = "Settings";
			this.toolStripButtonSettings.ToolTipText = "Displays the settings form";
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.trayMenuStrip;
			this.notifyIcon.Visible = true;
			// 
			// trayMenuStrip
			// 
			this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideShowWoofyToolStripMenuItem,
            this.toolStripSeparator3,
            this.stopAllTasksToolStripMenuItem,
            this.startAllTasksToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
			this.trayMenuStrip.Name = "trayMenuStrip";
			this.trayMenuStrip.ShowImageMargin = false;
			this.trayMenuStrip.Size = new System.Drawing.Size(147, 104);
			// 
			// hideShowWoofyToolStripMenuItem
			// 
			this.hideShowWoofyToolStripMenuItem.Name = "hideShowWoofyToolStripMenuItem";
			this.hideShowWoofyToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.hideShowWoofyToolStripMenuItem.Text = "&Hide/Show Woofy";
			this.hideShowWoofyToolStripMenuItem.Click += new System.EventHandler(this.hideShowWoofyToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
			// 
			// stopAllTasksToolStripMenuItem
			// 
			this.stopAllTasksToolStripMenuItem.Name = "stopAllTasksToolStripMenuItem";
			this.stopAllTasksToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.stopAllTasksToolStripMenuItem.Text = "&Pause all tasks";
			// 
			// startAllTasksToolStripMenuItem
			// 
			this.startAllTasksToolStripMenuItem.Name = "startAllTasksToolStripMenuItem";
			this.startAllTasksToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.startAllTasksToolStripMenuItem.Text = "&Resume all tasks";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 442);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.dgvwTasks);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "Woofy";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.trayMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvwTasks;
        private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonSelectComics;
        private System.Windows.Forms.ToolStripButton toolStripButtonPauseTask;
        private System.Windows.Forms.DataGridViewImageColumn TaskStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComicsColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewTask;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPauseTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenTaskFolder;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hideShowWoofyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem stopAllTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}