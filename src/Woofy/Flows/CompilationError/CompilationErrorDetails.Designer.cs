using System.Windows.Forms;

namespace Woofy.Flows.CompilationError
{
	partial class CompilationErrorDetails
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
			System.Windows.Forms.Label label1;
			this.lnkErrorSource = new System.Windows.Forms.LinkLabel();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnRetry = new System.Windows.Forms.Button();
			this.txtErrorDetails = new System.Windows.Forms.RichTextBox();
			label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(225, 13);
			label1.TabIndex = 2;
			label1.Text = "The following definition could not be compiled:";
			// 
			// lnkErrorSource
			// 
			this.lnkErrorSource.AutoSize = true;
			this.lnkErrorSource.Location = new System.Drawing.Point(12, 22);
			this.lnkErrorSource.Name = "lnkErrorSource";
			this.lnkErrorSource.Size = new System.Drawing.Size(68, 13);
			this.lnkErrorSource.TabIndex = 3;
			this.lnkErrorSource.TabStop = true;
			this.lnkErrorSource.Text = "c:\\my.file.def";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(630, 181);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// btnRetry
			// 
			this.btnRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnRetry.Location = new System.Drawing.Point(549, 181);
			this.btnRetry.Name = "btnRetry";
			this.btnRetry.Size = new System.Drawing.Size(75, 23);
			this.btnRetry.TabIndex = 0;
			this.btnRetry.Text = "&Retry";
			this.btnRetry.UseVisualStyleBackColor = true;
			// 
			// txtErrorDetails
			// 
			this.txtErrorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtErrorDetails.Location = new System.Drawing.Point(12, 38);
			this.txtErrorDetails.Name = "txtErrorDetails";
			this.txtErrorDetails.ReadOnly = true;
			this.txtErrorDetails.Size = new System.Drawing.Size(693, 137);
			this.txtErrorDetails.TabIndex = 4;
			this.txtErrorDetails.Text = "";
			// 
			// CompilationErrorDetails
			// 
			this.AcceptButton = this.btnRetry;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(717, 216);
			this.Controls.Add(this.btnRetry);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtErrorDetails);
			this.Controls.Add(this.lnkErrorSource);
			this.Controls.Add(label1);
			this.MinimumSize = new System.Drawing.Size(550, 150);
			this.Name = "CompilationErrorDetails";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel lnkErrorSource;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRetry;
		private RichTextBox txtErrorDetails;
	}
}