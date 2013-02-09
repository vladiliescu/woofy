namespace Woofy.Flows.Comics
{
    partial class EditForm
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
            this.chkPrependIndex = new System.Windows.Forms.CheckBox();
            this.gbComic = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkEmbedMetadata = new System.Windows.Forms.CheckBox();
            this.ttInfo = new System.Windows.Forms.ToolTip(this.components);
            this.gbComic.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPrependIndex
            // 
            this.chkPrependIndex.AutoSize = true;
            this.chkPrependIndex.Location = new System.Drawing.Point(6, 19);
            this.chkPrependIndex.Name = "chkPrependIndex";
            this.chkPrependIndex.Size = new System.Drawing.Size(234, 17);
            this.chkPrependIndex.TabIndex = 0;
            this.chkPrependIndex.Text = "Prepend the index to each downloaded strip";
            this.chkPrependIndex.UseVisualStyleBackColor = true;
            // 
            // gbComic
            // 
            this.gbComic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbComic.Controls.Add(this.chkEmbedMetadata);
            this.gbComic.Controls.Add(this.chkPrependIndex);
            this.gbComic.Location = new System.Drawing.Point(12, 12);
            this.gbComic.Name = "gbComic";
            this.gbComic.Size = new System.Drawing.Size(264, 69);
            this.gbComic.TabIndex = 0;
            this.gbComic.TabStop = false;
            this.gbComic.Text = "Settings";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(120, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOk);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkEmbedMetadata
            // 
            this.chkEmbedMetadata.AutoSize = true;
            this.chkEmbedMetadata.Location = new System.Drawing.Point(6, 42);
            this.chkEmbedMetadata.Name = "chkEmbedMetadata";
            this.chkEmbedMetadata.Size = new System.Drawing.Size(132, 17);
            this.chkEmbedMetadata.TabIndex = 6;
            this.chkEmbedMetadata.Text = "Embed XMP metadata";
            this.chkEmbedMetadata.UseVisualStyleBackColor = true;
            // 
            // ttInfo
            // 
            this.ttInfo.AutoPopDelay = 10000;
            this.ttInfo.InitialDelay = 500;
            this.ttInfo.ReshowDelay = 100;
            // 
            // EditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 122);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbComic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit";
            this.gbComic.ResumeLayout(false);
            this.gbComic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComic;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkPrependIndex;
        private System.Windows.Forms.CheckBox chkEmbedMetadata;
        private System.Windows.Forms.ToolTip ttInfo;
    }
}