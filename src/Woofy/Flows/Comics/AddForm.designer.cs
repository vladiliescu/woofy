namespace Woofy.Flows.Comics
{
	partial class AddForm
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
            System.Windows.Forms.GroupBox groupBox1;
            this.cbComics = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkPrependIndex = new System.Windows.Forms.CheckBox();
            this.chkEmbedMetadata = new System.Windows.Forms.CheckBox();
            this.ttInfo = new System.Windows.Forms.ToolTip(this.components);
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.cbComics);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(403, 51);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Comic";
            // 
            // cbComics
            // 
            this.cbComics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComics.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbComics.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbComics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComics.FormattingEnabled = true;
            this.cbComics.Location = new System.Drawing.Point(5, 18);
            this.cbComics.Margin = new System.Windows.Forms.Padding(2);
            this.cbComics.MaxDropDownItems = 15;
            this.cbComics.Name = "cbComics";
            this.cbComics.Size = new System.Drawing.Size(393, 21);
            this.cbComics.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(262, 117);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOK);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(341, 117);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkPrependIndex
            // 
            this.chkPrependIndex.AutoSize = true;
            this.chkPrependIndex.Location = new System.Drawing.Point(17, 69);
            this.chkPrependIndex.Name = "chkPrependIndex";
            this.chkPrependIndex.Size = new System.Drawing.Size(234, 17);
            this.chkPrependIndex.TabIndex = 4;
            this.chkPrependIndex.Text = "Prepend the index to each downloaded strip";
            this.chkPrependIndex.UseVisualStyleBackColor = true;
            // 
            // chkEmbedMetadata
            // 
            this.chkEmbedMetadata.AutoSize = true;
            this.chkEmbedMetadata.Checked = true;
            this.chkEmbedMetadata.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmbedMetadata.Location = new System.Drawing.Point(17, 93);
            this.chkEmbedMetadata.Name = "chkEmbedMetadata";
            this.chkEmbedMetadata.Size = new System.Drawing.Size(132, 17);
            this.chkEmbedMetadata.TabIndex = 5;
            this.chkEmbedMetadata.Text = "Embed XMP metadata";
            this.chkEmbedMetadata.UseVisualStyleBackColor = true;
            // 
            // ttInfo
            // 
            this.ttInfo.AutoPopDelay = 10000;
            this.ttInfo.InitialDelay = 500;
            this.ttInfo.ReshowDelay = 100;
            // 
            // AddForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(427, 151);
            this.Controls.Add(this.chkEmbedMetadata);
            this.Controls.Add(this.chkPrependIndex);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Comic";
            this.Load += new System.EventHandler(this.OnLoad);
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbComics;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkPrependIndex;
        private System.Windows.Forms.CheckBox chkEmbedMetadata;
        private System.Windows.Forms.ToolTip ttInfo;
	}
}