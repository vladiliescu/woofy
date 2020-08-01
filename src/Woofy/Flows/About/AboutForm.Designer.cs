namespace Woofy.Flows.About
{
	partial class AboutForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            System.Windows.Forms.Label label2;
            this.btnOK = new System.Windows.Forms.Button();
            this.lblProductInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lnkIconCredits = new System.Windows.Forms.LinkLabel();
            this.lblIconCredit = new System.Windows.Forms.Label();
            this.lnkFamFamFam = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lnkHomePage = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 55);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(190, 13);
            label1.TabIndex = 1;
            label1.Text = "Mark James for his wonderful silk icons";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 96);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(350, 13);
            label2.TabIndex = 6;
            label2.Text = "Phil Harvey for writing ExifTool (used by Woofy to embed XMP metadata)";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(303, 293);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&Close";
            // 
            // lblProductInfo
            // 
            this.lblProductInfo.AutoSize = true;
            this.lblProductInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProductInfo.Location = new System.Drawing.Point(9, 119);
            this.lblProductInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProductInfo.Name = "lblProductInfo";
            this.lblProductInfo.Size = new System.Drawing.Size(72, 15);
            this.lblProductInfo.TabIndex = 26;
            this.lblProductInfo.Text = "Product Info";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.lnkIconCredits);
            this.groupBox1.Controls.Add(this.lblIconCredit);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.lnkFamFamFam);
            this.groupBox1.Location = new System.Drawing.Point(11, 142);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(367, 135);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thanks To";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(28, 110);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(204, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://owl.phy.queensu.ca/~phil/exiftool/";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkLabelClicked);
            // 
            // lnkIconCredits
            // 
            this.lnkIconCredits.AutoSize = true;
            this.lnkIconCredits.Location = new System.Drawing.Point(28, 28);
            this.lnkIconCredits.Name = "lnkIconCredits";
            this.lnkIconCredits.Size = new System.Drawing.Size(169, 13);
            this.lnkIconCredits.TabIndex = 4;
            this.lnkIconCredits.TabStop = true;
            this.lnkIconCredits.Text = "http://hobbit1978.deviantart.com/";
            this.lnkIconCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkLabelClicked);
            // 
            // lblIconCredit
            // 
            this.lblIconCredit.AutoSize = true;
            this.lblIconCredit.Location = new System.Drawing.Point(13, 15);
            this.lblIconCredit.Name = "lblIconCredit";
            this.lblIconCredit.Size = new System.Drawing.Size(310, 13);
            this.lblIconCredit.TabIndex = 3;
            this.lblIconCredit.Text = "Jeremy James for allowing me to use his artwork as Woofy\'s icon";
            // 
            // lnkFamFamFam
            // 
            this.lnkFamFamFam.AutoSize = true;
            this.lnkFamFamFam.Location = new System.Drawing.Point(28, 69);
            this.lnkFamFamFam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkFamFamFam.Name = "lnkFamFamFam";
            this.lnkFamFamFam.Size = new System.Drawing.Size(213, 13);
            this.lnkFamFamFam.TabIndex = 0;
            this.lnkFamFamFam.TabStop = true;
            this.lnkFamFamFam.Text = "http://www.famfamfam.com/lab/icons/silk/";
            this.lnkFamFamFam.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkFamFamFam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkLabelClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Woofy.Properties.Resources.WoofyLogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(390, 120);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.OnWoofyHomePageClicked);
            // 
            // lnkHomePage
            // 
            this.lnkHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkHomePage.AutoSize = true;
            this.lnkHomePage.Location = new System.Drawing.Point(12, 298);
            this.lnkHomePage.Name = "lnkHomePage";
            this.lnkHomePage.Size = new System.Drawing.Size(99, 13);
            this.lnkHomePage.TabIndex = 34;
            this.lnkHomePage.TabStop = true;
            this.lnkHomePage.Text = "https://vladiliescu.net";
            this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnHomePageClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(390, 328);
            this.Controls.Add(this.lnkHomePage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProductInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblProductInfo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel lnkFamFamFam;
		private System.Windows.Forms.Label lblIconCredit;
		private System.Windows.Forms.LinkLabel lnkIconCredits;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel lnkHomePage;
        private System.Windows.Forms.LinkLabel linkLabel1;
	}
}