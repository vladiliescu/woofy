namespace Woofy.Gui
{
    partial class DownloadProgressForm
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
            this.pbDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.lblDownloadDetails = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbDownloadProgress
            // 
            this.pbDownloadProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownloadProgress.Location = new System.Drawing.Point(12, 25);
            this.pbDownloadProgress.Name = "pbDownloadProgress";
            this.pbDownloadProgress.Size = new System.Drawing.Size(304, 25);
            this.pbDownloadProgress.TabIndex = 0;
            // 
            // lblDownloadDetails
            // 
            this.lblDownloadDetails.AutoSize = true;
            this.lblDownloadDetails.Location = new System.Drawing.Point(12, 9);
            this.lblDownloadDetails.Name = "lblDownloadDetails";
            this.lblDownloadDetails.Size = new System.Drawing.Size(57, 13);
            this.lblDownloadDetails.TabIndex = 1;
            this.lblDownloadDetails.Text = "x kB/ y kB";
            // 
            // DownloadProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 62);
            this.Controls.Add(this.lblDownloadDetails);
            this.Controls.Add(this.pbDownloadProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DownloadProgressForm";
            this.Text = "Downloading...";
            this.Load += new System.EventHandler(this.DownloadProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbDownloadProgress;
        private System.Windows.Forms.Label lblDownloadDetails;
    }
}