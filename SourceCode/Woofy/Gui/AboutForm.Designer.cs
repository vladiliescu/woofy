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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkFamFamFam = new System.Windows.Forms.LinkLabel();
            this.lnkMailto = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 28);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(229, 17);
            label1.TabIndex = 1;
            label1.Text = "Mark James for his wonderful icons";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(21, 74);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(272, 17);
            label2.TabIndex = 2;
            label2.Text = "My girlfriend Mihaela for some great ideas\n";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(222, 192);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 25);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&Close";
            // 
            // lnkWebAddress
            // 
            this.lnkWebAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkWebAddress.AutoSize = true;
            this.lnkWebAddress.Location = new System.Drawing.Point(3, 192);
            this.lnkWebAddress.Name = "lnkWebAddress";
            this.lnkWebAddress.Size = new System.Drawing.Size(184, 17);
            this.lnkWebAddress.TabIndex = 2;
            this.lnkWebAddress.TabStop = true;
            this.lnkWebAddress.Text = "http://woofy.sourceforge.net";
            this.lnkWebAddress.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkWebAddress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebAddress_LinkClicked);
            // 
            // lblFullProductName
            // 
            this.lblFullProductName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFullProductName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblFullProductName, 2);
            this.lblFullProductName.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullProductName.ForeColor = System.Drawing.Color.Teal;
            this.lblFullProductName.Location = new System.Drawing.Point(71, 0);
            this.lblFullProductName.Name = "lblFullProductName";
            this.lblFullProductName.Size = new System.Drawing.Size(183, 24);
            this.lblFullProductName.TabIndex = 26;
            this.lblFullProductName.Text = "Full Product Name";
            // 
            // lblAuthorInfo
            // 
            this.lblAuthorInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAuthorInfo.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAuthorInfo, 2);
            this.lblAuthorInfo.Location = new System.Drawing.Point(124, 30);
            this.lblAuthorInfo.Name = "lblAuthorInfo";
            this.lblAuthorInfo.Size = new System.Drawing.Size(77, 17);
            this.lblAuthorInfo.TabIndex = 27;
            this.lblAuthorInfo.Text = "Author Info";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.62992F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.37008F));
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblAuthorInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lnkWebAddress, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblFullProductName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lnkMailto, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 221);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.lnkFamFamFam);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 105);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thanks to";
            // 
            // lnkFamFamFam
            // 
            this.lnkFamFamFam.AutoSize = true;
            this.lnkFamFamFam.Location = new System.Drawing.Point(41, 45);
            this.lnkFamFamFam.Name = "lnkFamFamFam";
            this.lnkFamFamFam.Size = new System.Drawing.Size(262, 17);
            this.lnkFamFamFam.TabIndex = 0;
            this.lnkFamFamFam.TabStop = true;
            this.lnkFamFamFam.Text = "http://www.famfamfam.com/lab/icons/silk/";
            this.lnkFamFamFam.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkFamFamFam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFamFamFam_LinkClicked);
            // 
            // lnkMailto
            // 
            this.lnkMailto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnkMailto.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lnkMailto, 2);
            this.lnkMailto.Location = new System.Drawing.Point(86, 50);
            this.lnkMailto.Name = "lnkMailto";
            this.lnkMailto.Size = new System.Drawing.Size(154, 17);
            this.lnkMailto.TabIndex = 29;
            this.lnkMailto.TabStop = true;
            this.lnkMailto.Text = "vlad.iliescu@gmail.com";
            this.lnkMailto.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkMailto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMailto_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(350, 243);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkWebAddress;
        private System.Windows.Forms.Label lblFullProductName;
        private System.Windows.Forms.Label lblAuthorInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkFamFamFam;
        private System.Windows.Forms.LinkLabel lnkMailto;
    }
}
