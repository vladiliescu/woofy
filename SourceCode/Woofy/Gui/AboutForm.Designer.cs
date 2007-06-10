namespace Woofy.Gui
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
            this.lnkWebAddress = new System.Windows.Forms.LinkLabel();
            this.lblFullProductName = new System.Windows.Forms.Label();
            this.lblAuthorInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkIconCredits = new System.Windows.Forms.LinkLabel();
            this.lblIconCredit = new System.Windows.Forms.Label();
            this.lnkFamFamFam = new System.Windows.Forms.LinkLabel();
            this.lnkMailto = new System.Windows.Forms.LinkLabel();
            this.pbWoofyLogo = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWoofyLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 69);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(190, 13);
            label1.TabIndex = 1;
            label1.Text = "Mark James for his wonderful silk icons";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 108);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(201, 13);
            label2.TabIndex = 2;
            label2.Text = "My girlfriend Mihaela for some great ideas\n";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(223, 235);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&Close";
            // 
            // lnkWebAddress
            // 
            this.lnkWebAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkWebAddress.AutoSize = true;
            this.lnkWebAddress.Location = new System.Drawing.Point(9, 240);
            this.lnkWebAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkWebAddress.Name = "lnkWebAddress";
            this.lnkWebAddress.Size = new System.Drawing.Size(143, 13);
            this.lnkWebAddress.TabIndex = 2;
            this.lnkWebAddress.TabStop = true;
            this.lnkWebAddress.Text = "http://woofy.sourceforge.net";
            this.lnkWebAddress.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkWebAddress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebAddress_LinkClicked);
            // 
            // lblFullProductName
            // 
            this.lblFullProductName.AutoSize = true;
            this.lblFullProductName.Font = new System.Drawing.Font("Book Antiqua", 16F, System.Drawing.FontStyle.Bold);
            this.lblFullProductName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFullProductName.Location = new System.Drawing.Point(172, 12);
            this.lblFullProductName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFullProductName.Name = "lblFullProductName";
            this.lblFullProductName.Size = new System.Drawing.Size(200, 27);
            this.lblFullProductName.TabIndex = 26;
            this.lblFullProductName.Text = "Full Product Name";
            // 
            // lblAuthorInfo
            // 
            this.lblAuthorInfo.AutoSize = true;
            this.lblAuthorInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblAuthorInfo.Location = new System.Drawing.Point(174, 49);
            this.lblAuthorInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAuthorInfo.Name = "lblAuthorInfo";
            this.lblAuthorInfo.Size = new System.Drawing.Size(58, 15);
            this.lblAuthorInfo.TabIndex = 27;
            this.lblAuthorInfo.Text = "Copyright";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lnkIconCredits);
            this.groupBox1.Controls.Add(this.lblIconCredit);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.lnkFamFamFam);
            this.groupBox1.Location = new System.Drawing.Point(12, 95);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(286, 131);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thanks to";
            // 
            // lnkIconCredits
            // 
            this.lnkIconCredits.AutoSize = true;
            this.lnkIconCredits.Location = new System.Drawing.Point(34, 45);
            this.lnkIconCredits.Name = "lnkIconCredits";
            this.lnkIconCredits.Size = new System.Drawing.Size(169, 13);
            this.lnkIconCredits.TabIndex = 4;
            this.lnkIconCredits.TabStop = true;
            this.lnkIconCredits.Text = "http://hobbit1978.deviantart.com/";
            this.lnkIconCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIconCredits_LinkClicked);
            // 
            // lblIconCredit
            // 
            this.lblIconCredit.AutoSize = true;
            this.lblIconCredit.Location = new System.Drawing.Point(19, 19);
            this.lblIconCredit.Name = "lblIconCredit";
            this.lblIconCredit.Size = new System.Drawing.Size(261, 26);
            this.lblIconCredit.TabIndex = 3;
            this.lblIconCredit.Text = "Jeremy James for the permission to use his creation as\nWoofy\'s icon";
            // 
            // lnkFamFamFam
            // 
            this.lnkFamFamFam.AutoSize = true;
            this.lnkFamFamFam.Location = new System.Drawing.Point(28, 83);
            this.lnkFamFamFam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkFamFamFam.Name = "lnkFamFamFam";
            this.lnkFamFamFam.Size = new System.Drawing.Size(213, 13);
            this.lnkFamFamFam.TabIndex = 0;
            this.lnkFamFamFam.TabStop = true;
            this.lnkFamFamFam.Text = "http://www.famfamfam.com/lab/icons/silk/";
            this.lnkFamFamFam.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkFamFamFam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFamFamFam_LinkClicked);
            // 
            // lnkMailto
            // 
            this.lnkMailto.AutoSize = true;
            this.lnkMailto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lnkMailto.Location = new System.Drawing.Point(174, 64);
            this.lnkMailto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkMailto.Name = "lnkMailto";
            this.lnkMailto.Size = new System.Drawing.Size(79, 15);
            this.lnkMailto.TabIndex = 29;
            this.lnkMailto.TabStop = true;
            this.lnkMailto.Text = "Author Name";
            this.lnkMailto.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkMailto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMailto_LinkClicked);
            // 
            // pbWoofyLogo
            // 
            this.pbWoofyLogo.Image = global::Woofy.Properties.Resources.Woofy128x128;
            this.pbWoofyLogo.Location = new System.Drawing.Point(12, 12);
            this.pbWoofyLogo.Name = "pbWoofyLogo";
            this.pbWoofyLogo.Size = new System.Drawing.Size(141, 67);
            this.pbWoofyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbWoofyLogo.TabIndex = 30;
            this.pbWoofyLogo.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 262);
            this.Controls.Add(this.lnkWebAddress);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblAuthorInfo);
            this.Controls.Add(this.pbWoofyLogo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFullProductName);
            this.Controls.Add(this.lnkMailto);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbWoofyLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkWebAddress;
        private System.Windows.Forms.Label lblFullProductName;
        private System.Windows.Forms.Label lblAuthorInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkFamFamFam;
        private System.Windows.Forms.LinkLabel lnkMailto;
        private System.Windows.Forms.Label lblIconCredit;
        private System.Windows.Forms.LinkLabel lnkIconCredits;
        private System.Windows.Forms.PictureBox pbWoofyLogo;
    }
}
