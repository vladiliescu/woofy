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
			this.lvwActiveComics = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvwAvailableComics
			// 
			this.lvwAvailableComics.Location = new System.Drawing.Point(12, 12);
			this.lvwAvailableComics.Name = "lvwAvailableComics";
			this.lvwAvailableComics.Size = new System.Drawing.Size(241, 220);
			this.lvwAvailableComics.TabIndex = 1;
			this.lvwAvailableComics.UseCompatibleStateImageBehavior = false;
			// 
			// lvwActiveComics
			// 
			this.lvwActiveComics.Location = new System.Drawing.Point(306, 12);
			this.lvwActiveComics.Name = "lvwActiveComics";
			this.lvwActiveComics.Size = new System.Drawing.Size(252, 220);
			this.lvwActiveComics.TabIndex = 2;
			this.lvwActiveComics.UseCompatibleStateImageBehavior = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(259, 106);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(41, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = ">";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(259, 135);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(41, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "<";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(402, 275);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "OK";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(483, 275);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 6;
			this.button4.Text = "Cancel";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// ComicsSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 310);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lvwActiveComics);
			this.Controls.Add(this.lvwAvailableComics);
			this.Name = "ComicSelectionForm";
			this.Text = "ComicsSelectionForm";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvwAvailableComics;
		private System.Windows.Forms.ListView lvwActiveComics;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}