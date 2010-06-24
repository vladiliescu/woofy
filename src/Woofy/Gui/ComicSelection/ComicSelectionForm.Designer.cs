namespace Woofy.Gui.ComicSelection
{
	partial class ComicSelectionForm
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
			this.lvwAvailableComics = new System.Windows.Forms.ListView();
			this.chAvailableComics = new System.Windows.Forms.ColumnHeader();
			this.lvwActiveComics = new System.Windows.Forms.ListView();
			this.chActiveComics = new System.Windows.Forms.ColumnHeader();
			this.btnActivateComic = new System.Windows.Forms.Button();
			this.btnDeactivateComic = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvwAvailableComics
			// 
			this.lvwAvailableComics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwAvailableComics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAvailableComics});
			this.lvwAvailableComics.FullRowSelect = true;
			this.lvwAvailableComics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwAvailableComics.HideSelection = false;
			this.lvwAvailableComics.Location = new System.Drawing.Point(12, 12);
			this.lvwAvailableComics.Name = "lvwAvailableComics";
			this.lvwAvailableComics.ShowItemToolTips = true;
			this.lvwAvailableComics.Size = new System.Drawing.Size(278, 262);
			this.lvwAvailableComics.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwAvailableComics.TabIndex = 1;
			this.lvwAvailableComics.UseCompatibleStateImageBehavior = false;
			this.lvwAvailableComics.View = System.Windows.Forms.View.Details;
			// 
			// chAvailableComics
			// 
			this.chAvailableComics.Text = "Available Comics";
			// 
			// lvwActiveComics
			// 
			this.lvwActiveComics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwActiveComics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActiveComics});
			this.lvwActiveComics.FullRowSelect = true;
			this.lvwActiveComics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwActiveComics.HideSelection = false;
			this.lvwActiveComics.Location = new System.Drawing.Point(343, 12);
			this.lvwActiveComics.Name = "lvwActiveComics";
			this.lvwActiveComics.ShowItemToolTips = true;
			this.lvwActiveComics.Size = new System.Drawing.Size(277, 262);
			this.lvwActiveComics.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwActiveComics.TabIndex = 2;
			this.lvwActiveComics.UseCompatibleStateImageBehavior = false;
			this.lvwActiveComics.View = System.Windows.Forms.View.Details;
			// 
			// chActiveComics
			// 
			this.chActiveComics.Text = "Active Comics";
			// 
			// btnActivateComic
			// 
			this.btnActivateComic.Location = new System.Drawing.Point(296, 123);
			this.btnActivateComic.Name = "btnActivateComic";
			this.btnActivateComic.Size = new System.Drawing.Size(41, 23);
			this.btnActivateComic.TabIndex = 3;
			this.btnActivateComic.Text = ">";
			this.btnActivateComic.UseVisualStyleBackColor = true;
			this.btnActivateComic.Click += new System.EventHandler(this.OnActivateComic);
			// 
			// btnDeactivateComic
			// 
			this.btnDeactivateComic.Location = new System.Drawing.Point(296, 152);
			this.btnDeactivateComic.Name = "btnDeactivateComic";
			this.btnDeactivateComic.Size = new System.Drawing.Size(41, 23);
			this.btnDeactivateComic.TabIndex = 4;
			this.btnDeactivateComic.Text = "<";
			this.btnDeactivateComic.UseVisualStyleBackColor = true;
			this.btnDeactivateComic.Click += new System.EventHandler(this.OnDeactivateComic);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(463, 280);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.OnOK);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(545, 280);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ComicSelectionForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(632, 315);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnDeactivateComic);
			this.Controls.Add(this.btnActivateComic);
			this.Controls.Add(this.lvwActiveComics);
			this.Controls.Add(this.lvwAvailableComics);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ComicSelectionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select the active comics";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvwAvailableComics;
		private System.Windows.Forms.ListView lvwActiveComics;
		private System.Windows.Forms.Button btnActivateComic;
		private System.Windows.Forms.Button btnDeactivateComic;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader chAvailableComics;
		private System.Windows.Forms.ColumnHeader chActiveComics;
	}
}