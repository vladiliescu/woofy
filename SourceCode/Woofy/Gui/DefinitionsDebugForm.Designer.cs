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
            this.comicDefinitionsSelector = new System.Windows.Forms.CheckedListBox();
            this.startDebugButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.eventsRichTextBox = new Woofy.Core.ExRichTextBox();
            this.SuspendLayout();
            // 
            // comicDefinitionsSelector
            // 
            this.comicDefinitionsSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comicDefinitionsSelector.CheckOnClick = true;
            this.comicDefinitionsSelector.FormattingEnabled = true;
            this.comicDefinitionsSelector.Location = new System.Drawing.Point(12, 12);
            this.comicDefinitionsSelector.Name = "comicDefinitionsSelector";
            this.comicDefinitionsSelector.Size = new System.Drawing.Size(634, 274);
            this.comicDefinitionsSelector.TabIndex = 0;
            // 
            // startDebugButton
            // 
            this.startDebugButton.Location = new System.Drawing.Point(304, 515);
            this.startDebugButton.Name = "startDebugButton";
            this.startDebugButton.Size = new System.Drawing.Size(75, 23);
            this.startDebugButton.TabIndex = 2;
            this.startDebugButton.Text = "Start";
            this.startDebugButton.UseVisualStyleBackColor = true;
            this.startDebugButton.Click += new System.EventHandler(this.startDebugButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(385, 515);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // eventsRichTextBox
            // 
            this.eventsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventsRichTextBox.HiglightColor = Woofy.Core.RtfColor.White;
            this.eventsRichTextBox.Location = new System.Drawing.Point(12, 292);
            this.eventsRichTextBox.Name = "eventsRichTextBox";
            this.eventsRichTextBox.Size = new System.Drawing.Size(634, 191);
            this.eventsRichTextBox.TabIndex = 6;
            this.eventsRichTextBox.Text = "";
            this.eventsRichTextBox.TextColor = Woofy.Core.RtfColor.Black;
            // 
            // DefinitionsDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 577);
            this.Controls.Add(this.eventsRichTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startDebugButton);
            this.Controls.Add(this.comicDefinitionsSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DefinitionsDebugForm";
            this.Text = "ComicDefinitionsDebug";
            this.Load += new System.EventHandler(this.DefinitionsDebugForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox comicDefinitionsSelector;
        private System.Windows.Forms.Button startDebugButton;
        private System.Windows.Forms.Button closeButton;
        private Woofy.Core.ExRichTextBox eventsRichTextBox;
    }
}