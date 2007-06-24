namespace Woofy.Gui
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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "All ({0})"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Active ({0})"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Finished ({0})"}, -1);
            this.dgvwTasks = new System.Windows.Forms.DataGridView();
            this.TaskStatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.TaskNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComicsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemNewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPauseTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemOpenTaskFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPauseTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.lvwCategories = new System.Windows.Forms.ListView();
            this.icon = new System.Windows.Forms.ColumnHeader();
            this.text = new System.Windows.Forms.ColumnHeader();
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
            this.dgvwTasks.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvwTasks_DataBindingComplete);
            this.dgvwTasks.DoubleClick += new System.EventHandler(this.dgvwTasks_DoubleClick);
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
            this.toolStripMenuItemNewTask,
            this.toolStripMenuItemPauseTask,
            this.toolStripMenuItemDeleteTask,
            this.toolStripSeparator1,
            this.toolStripMenuItemOpenTaskFolder});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.Size = new System.Drawing.Size(189, 98);
            // 
            // toolStripMenuItemNewTask
            // 
            this.toolStripMenuItemNewTask.Image = global::Woofy.Properties.Resources.New;
            this.toolStripMenuItemNewTask.Name = "toolStripMenuItemNewTask";
            this.toolStripMenuItemNewTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItemNewTask.Size = new System.Drawing.Size(188, 22);
            this.toolStripMenuItemNewTask.Text = "New";
            this.toolStripMenuItemNewTask.Click += new System.EventHandler(this.toolStripMenuItemNewTask_Click);
            // 
            // toolStripMenuItemPauseTask
            // 
            this.toolStripMenuItemPauseTask.Image = global::Woofy.Properties.Resources.Paused;
            this.toolStripMenuItemPauseTask.Name = "toolStripMenuItemPauseTask";
            this.toolStripMenuItemPauseTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripMenuItemPauseTask.Size = new System.Drawing.Size(188, 22);
            this.toolStripMenuItemPauseTask.Text = "Pause";
            this.toolStripMenuItemPauseTask.Click += new System.EventHandler(this.toolStripMenuItemPauseTask_Click);
            // 
            // toolStripMenuItemDeleteTask
            // 
            this.toolStripMenuItemDeleteTask.Image = global::Woofy.Properties.Resources.Delete;
            this.toolStripMenuItemDeleteTask.Name = "toolStripMenuItemDeleteTask";
            this.toolStripMenuItemDeleteTask.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemDeleteTask.Size = new System.Drawing.Size(188, 22);
            this.toolStripMenuItemDeleteTask.Text = "Delete";
            this.toolStripMenuItemDeleteTask.Click += new System.EventHandler(this.toolStripMenuItemDeleteTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // toolStripMenuItemOpenTaskFolder
            // 
            this.toolStripMenuItemOpenTaskFolder.Image = global::Woofy.Properties.Resources.OpenFolder;
            this.toolStripMenuItemOpenTaskFolder.Name = "toolStripMenuItemOpenTaskFolder";
            this.toolStripMenuItemOpenTaskFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOpenTaskFolder.Size = new System.Drawing.Size(188, 22);
            this.toolStripMenuItemOpenTaskFolder.Text = "Open folder";
            this.toolStripMenuItemOpenTaskFolder.Click += new System.EventHandler(this.toolStripMenuItemOpenTaskFolder_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddTask,
            this.toolStripButtonPauseTask,
            this.toolStripButtonDeleteTask,
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
            // toolStripButtonAddTask
            // 
            this.toolStripButtonAddTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddTask.Image")));
            this.toolStripButtonAddTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddTask.Name = "toolStripButtonAddTask";
            this.toolStripButtonAddTask.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonAddTask.Text = "New";
            this.toolStripButtonAddTask.ToolTipText = "Creates a new task (Ctrl+N)";
            this.toolStripButtonAddTask.Click += new System.EventHandler(this.toolStripButtonAddTask_Click);
            // 
            // toolStripButtonPauseTask
            // 
            this.toolStripButtonPauseTask.Enabled = false;
            this.toolStripButtonPauseTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPauseTask.Image")));
            this.toolStripButtonPauseTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPauseTask.Name = "toolStripButtonPauseTask";
            this.toolStripButtonPauseTask.Size = new System.Drawing.Size(62, 22);
            this.toolStripButtonPauseTask.Text = "Pause";
            this.toolStripButtonPauseTask.ToolTipText = "Pauses/unpauses the selected tasks (Ctrl+P)";
            this.toolStripButtonPauseTask.Click += new System.EventHandler(this.toolStripButtonPauseTask_Click);
            // 
            // toolStripButtonDeleteTask
            // 
            this.toolStripButtonDeleteTask.Enabled = false;
            this.toolStripButtonDeleteTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteTask.Image")));
            this.toolStripButtonDeleteTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteTask.Name = "toolStripButtonDeleteTask";
            this.toolStripButtonDeleteTask.Size = new System.Drawing.Size(64, 22);
            this.toolStripButtonDeleteTask.Text = "Delete";
            this.toolStripButtonDeleteTask.ToolTipText = "Deletes the currently selected tasks (Del)";
            this.toolStripButtonDeleteTask.Click += new System.EventHandler(this.toolStripButtonDeleteTask_Click);
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
            this.toolStripButtonOpenFolder.Size = new System.Drawing.Size(95, 22);
            this.toolStripButtonOpenFolder.Text = "Open folder";
            this.toolStripButtonOpenFolder.ToolTipText = "Opens the folder where the comics of the selected task have been downloaded (Ctrl" +
                "+O)";
            this.toolStripButtonOpenFolder.Click += new System.EventHandler(this.toolStripButtonOpenFolder_Click);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSettings.Image = global::Woofy.Properties.Resources.Settings;
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(74, 22);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.ToolTipText = "Displays the settings form";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // lvwCategories
            // 
            this.lvwCategories.BackColor = System.Drawing.Color.Red;
            this.lvwCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.icon,
            this.text});
            this.lvwCategories.FullRowSelect = true;
            this.lvwCategories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            this.lvwCategories.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lvwCategories.Location = new System.Drawing.Point(9, 152);
            this.lvwCategories.Margin = new System.Windows.Forms.Padding(2);
            this.lvwCategories.Name = "lvwCategories";
            this.lvwCategories.Size = new System.Drawing.Size(101, 366);
            this.lvwCategories.TabIndex = 9;
            this.lvwCategories.UseCompatibleStateImageBehavior = false;
            this.lvwCategories.View = System.Windows.Forms.View.Details;
            this.lvwCategories.Visible = false;
            // 
            // icon
            // 
            this.icon.Width = 30;
            // 
            // text
            // 
            this.text.Width = 99;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.trayMenuStrip;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
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
            this.trayMenuStrip.Size = new System.Drawing.Size(154, 104);
            // 
            // hideShowWoofyToolStripMenuItem
            // 
            this.hideShowWoofyToolStripMenuItem.Name = "hideShowWoofyToolStripMenuItem";
            this.hideShowWoofyToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.hideShowWoofyToolStripMenuItem.Text = "&Hide/Show Woofy";
            this.hideShowWoofyToolStripMenuItem.Click += new System.EventHandler(this.hideShowWoofyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
            // 
            // stopAllTasksToolStripMenuItem
            // 
            this.stopAllTasksToolStripMenuItem.Name = "stopAllTasksToolStripMenuItem";
            this.stopAllTasksToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.stopAllTasksToolStripMenuItem.Text = "&Pause all tasks";
            this.stopAllTasksToolStripMenuItem.Click += new System.EventHandler(this.stopAllTasksToolStripMenuItem_Click);
            // 
            // startAllTasksToolStripMenuItem
            // 
            this.startAllTasksToolStripMenuItem.Name = "startAllTasksToolStripMenuItem";
            this.startAllTasksToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.startAllTasksToolStripMenuItem.Text = "&Resume all tasks";
            this.startAllTasksToolStripMenuItem.Click += new System.EventHandler(this.startAllTasksToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 442);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dgvwTasks);
            this.Controls.Add(this.lvwCategories);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Woofy";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripButton toolStripButtonAddTask;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteTask;
        private System.Windows.Forms.ListView lvwCategories;
        private System.Windows.Forms.ColumnHeader icon;
        private System.Windows.Forms.ColumnHeader text;
        private System.Windows.Forms.ToolStripButton toolStripButtonPauseTask;
        private System.Windows.Forms.DataGridViewImageColumn TaskStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComicsColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPauseTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteTask;
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