using System.Windows.Forms;
using Woofy.External;

namespace Woofy.Gui
{
    partial class DefinitionsDebugForm
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            this.comicDefinitionsList = new System.Windows.Forms.ListView();
            this.txtOverrideStartUrl = new System.Windows.Forms.TextBox();
            this.chkOverrideStartUrl = new System.Windows.Forms.CheckBox();
            this.eventsRichTextBox = new ExRichTextBox();
            this.outputContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.abortButton = new System.Windows.Forms.Button();
            this.lblFoundStrips = new System.Windows.Forms.Label();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.outputContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Comic";
            columnHeader1.Width = 390;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Definition Author";
            columnHeader2.Width = 180;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.comicDefinitionsList);
            groupBox1.Controls.Add(this.txtOverrideStartUrl);
            groupBox1.Controls.Add(this.chkOverrideStartUrl);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(608, 334);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Test Selection";
            // 
            // comicDefinitionsList
            // 
            this.comicDefinitionsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comicDefinitionsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.comicDefinitionsList.Cursor = System.Windows.Forms.Cursors.Default;
            this.comicDefinitionsList.FullRowSelect = true;
            this.comicDefinitionsList.GridLines = true;
            this.comicDefinitionsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.comicDefinitionsList.HideSelection = false;
            this.comicDefinitionsList.Location = new System.Drawing.Point(6, 19);
            this.comicDefinitionsList.MultiSelect = false;
            this.comicDefinitionsList.Name = "comicDefinitionsList";
            this.comicDefinitionsList.Size = new System.Drawing.Size(596, 258);
            this.comicDefinitionsList.TabIndex = 7;
            this.comicDefinitionsList.UseCompatibleStateImageBehavior = false;
            this.comicDefinitionsList.View = System.Windows.Forms.View.Details;
            this.comicDefinitionsList.DoubleClick += new System.EventHandler(this.comicDefinitionsList_DoubleClick);
            // 
            // txtOverrideStartUrl
            // 
            this.txtOverrideStartUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOverrideStartUrl.Enabled = false;
            this.txtOverrideStartUrl.Location = new System.Drawing.Point(5, 303);
            this.txtOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtOverrideStartUrl.Name = "txtOverrideStartUrl";
            this.txtOverrideStartUrl.Size = new System.Drawing.Size(597, 20);
            this.txtOverrideStartUrl.TabIndex = 14;
            // 
            // chkOverrideStartUrl
            // 
            this.chkOverrideStartUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOverrideStartUrl.AutoSize = true;
            this.chkOverrideStartUrl.Location = new System.Drawing.Point(5, 282);
            this.chkOverrideStartUrl.Margin = new System.Windows.Forms.Padding(2);
            this.chkOverrideStartUrl.Name = "chkOverrideStartUrl";
            this.chkOverrideStartUrl.Size = new System.Drawing.Size(103, 17);
            this.chkOverrideStartUrl.TabIndex = 13;
            this.chkOverrideStartUrl.Text = "Override start url";
            this.chkOverrideStartUrl.UseVisualStyleBackColor = true;
            this.chkOverrideStartUrl.CheckedChanged += new System.EventHandler(this.chkOverrideStartUrl_CheckedChanged);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.eventsRichTextBox);
            groupBox2.Location = new System.Drawing.Point(12, 352);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(608, 305);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Output";
            // 
            // eventsRichTextBox
            // 
            this.eventsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventsRichTextBox.ContextMenuStrip = this.outputContextMenu;
            this.eventsRichTextBox.Location = new System.Drawing.Point(6, 19);
            this.eventsRichTextBox.Name = "eventsRichTextBox";
            this.eventsRichTextBox.ReadOnly = true;
            this.eventsRichTextBox.Size = new System.Drawing.Size(596, 280);
            this.eventsRichTextBox.TabIndex = 6;
            this.eventsRichTextBox.Text = "";
            this.eventsRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.eventsRichTextBox_LinkClicked);
            // 
            // outputContextMenu
            // 
            this.outputContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.outputContextMenu.Name = "outputContextMenu";
            this.outputContextMenu.Size = new System.Drawing.Size(149, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Woofy.Properties.Resources.Copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(464, 663);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startDebugButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(545, 663);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseButton.Location = new System.Drawing.Point(464, 663);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 17;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Visible = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // abortButton
            // 
            this.abortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.abortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.abortButton.Location = new System.Drawing.Point(545, 663);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.TabIndex = 18;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Visible = false;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // lblFoundStrips
            // 
            this.lblFoundStrips.AutoSize = true;
            this.lblFoundStrips.Location = new System.Drawing.Point(12, 663);
            this.lblFoundStrips.Name = "lblFoundStrips";
            this.lblFoundStrips.Size = new System.Drawing.Size(117, 13);
            this.lblFoundStrips.TabIndex = 7;
            this.lblFoundStrips.Text = "I\'ve found <009> strips.";
            this.lblFoundStrips.Visible = false;
            // 
            // DefinitionsDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 698);
            this.Controls.Add(this.lblFoundStrips);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startButton);
            this.MinimizeBox = false;
            this.Name = "DefinitionsDebugForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debug Comic Definitions";
            this.Load += new System.EventHandler(this.DefinitionsDebugForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefinitionsDebugForm_FormClosing);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            this.outputContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button closeButton;
        private RichTextBox eventsRichTextBox;
        private System.Windows.Forms.ListView comicDefinitionsList;
        private System.Windows.Forms.TextBox txtOverrideStartUrl;
        private System.Windows.Forms.CheckBox chkOverrideStartUrl;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.Label lblFoundStrips;
        private System.Windows.Forms.ContextMenuStrip outputContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}