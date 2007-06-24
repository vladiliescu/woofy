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
            this.lblProductInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkIconCredits = new System.Windows.Forms.LinkLabel();
            this.lblIconCredit = new System.Windows.Forms.Label();
            this.lnkFamFamFam = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 58);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(190, 13);
            label1.TabIndex = 1;
            label1.Text = "Mark James for his wonderful silk icons";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 97);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(142, 13);
            label2.TabIndex = 2;
            label2.Text = "Mihaela for some great ideas\n";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(283, 275);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&Close";
            // 
            // lnkWebAddress
            // 
            this.lnkWebAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkWebAddress.AutoSize = true;
            this.lnkWebAddress.Location = new System.Drawing.Point(9, 280);
            this.lnkWebAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkWebAddress.Name = "lnkWebAddress";
            this.lnkWebAddress.Size = new System.Drawing.Size(143, 13);
            this.lnkWebAddress.TabIndex = 2;
            this.lnkWebAddress.TabStop = true;
            this.lnkWebAddress.Text = "http://woofy.sourceforge.net";
            this.lnkWebAddress.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkWebAddress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebAddress_LinkClicked);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lnkIconCredits);
            this.groupBox1.Controls.Add(this.lblIconCredit);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.lnkFamFamFam);
            this.groupBox1.Location = new System.Drawing.Point(12, 148);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(346, 122);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thanks to";
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
            this.lnkIconCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIconCredits_LinkClicked);
            // 
            // lblIconCredit
            // 
            this.lblIconCredit.AutoSize = true;
            this.lblIconCredit.Location = new System.Drawing.Point(13, 15);
            this.lblIconCredit.Name = "lblIconCredit";
            this.lblIconCredit.Size = new System.Drawing.Size(325, 13);
            this.lblIconCredit.TabIndex = 3;
            this.lblIconCredit.Text = "Jeremy James for the permission to use his creation as Woofy\'s icon";
            // 
            // lnkFamFamFam
            // 
            this.lnkFamFamFam.AutoSize = true;
            this.lnkFamFamFam.Location = new System.Drawing.Point(28, 72);
            this.lnkFamFamFam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkFamFamFam.Name = "lnkFamFamFam";
            this.lnkFamFamFam.Size = new System.Drawing.Size(213, 13);
            this.lnkFamFamFam.TabIndex = 0;
            this.lnkFamFamFam.TabStop = true;
            this.lnkFamFamFam.Text = "http://www.famfamfam.com/lab/icons/silk/";
            this.lnkFamFamFam.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkFamFamFam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFamFamFam_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Woofy.Properties.Resources.WoofyLogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(369, 120);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 302);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lnkWebAddress);
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
        private System.Windows.Forms.LinkLabel lnkWebAddress;
        private System.Windows.Forms.Label lblProductInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkFamFamFam;
        private System.Windows.Forms.Label lblIconCredit;
        private System.Windows.Forms.LinkLabel lnkIconCredits;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
