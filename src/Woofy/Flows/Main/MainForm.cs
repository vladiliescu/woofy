using System;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Properties;

namespace Woofy.Flows.Main
{
	public partial class MainForm : Form
	{
		public IMainPresenter Presenter { get; set; }
        
		public MainForm()
		{
			InitializeComponent();

			RegisterCommands();
		}

		private void RegisterCommands()
		{
			tsbAddComic.Click += (o, e) => Presenter.AddComic();
            tsbEditComic.Click += (o, e) =>
            {
                var comic = GetSelectedComic();
                if (comic == null)
                    return;

                Presenter.EditComic(comic.Id);
            };
			tsbSettings.Click += (o, e) =>
			{
				using (var settingsForm = new SettingsForm())
					settingsForm.ShowDialog();
			};
			txtAppLog.LinkClicked += (o, e) => Presenter.Open(e.LinkText);
			txtAppLog.TextChanged += (o, e) =>
			{
                txtAppLog.SuspendLayout();
				txtAppLog.SelectionStart = txtAppLog.Text.Length;
				txtAppLog.ScrollToCaret();
                txtAppLog.ResumeLayout();
			};

            tsbOpenFolder.Click += (o, e) => {
                var comic = GetSelectedComic();
                if (comic == null)
                    return;
                
                Presenter.OpenFolder(comic.Id);
            };

			tsbDonate.Click += (o, e) => Presenter.Donate();
			tsbAbout.Click += (o, e) => Presenter.DisplayAboutScreen();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			Icon = Resources.ApplicationIcon;
			dgvwTasks.AutoGenerateColumns = false;
			txtAppLog.DataBindings.Add(new Binding("Text", Presenter, "AppLog"));

			Presenter.Initialize(this);
			dgvwTasks.DataSource = Presenter.Comics;
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.N)
				Presenter.AddComic();
		}

		private void OnResize(object sender, EventArgs e)
		{
			if (!Presenter.MinimizeToTray)
				return;

			if (WindowState == FormWindowState.Minimized)
				Hide();
		}

		public void HideOrShow()
		{
			if (Visible)
			{
				WindowState = FormWindowState.Minimized;
				Visible = false;
			}
			else
			{
				Visible = true;
				WindowState = FormWindowState.Normal;
			}
		}

		private void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
#warning I should extract these indexes and place them in some instance members
			if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colStatus) || e.Value == null)
				return;

			var status = (ComicViewModel.ComicStatus)e.Value;
			switch (status)
			{
				case ComicViewModel.ComicStatus.Paused:
					e.Value = Resources.Paused;
					break;
				case ComicViewModel.ComicStatus.Running:
					e.Value = Resources.Running;
					break;
				case ComicViewModel.ComicStatus.Finished:
					e.Value = Resources.Finished;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGridCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colCurrentPage))
				return;

			var value = dgvwTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
			if (value == null)
				return;

			Presenter.Open(value);
		}

		private void OnToggleComicState(object sender, EventArgs e)
		{
			var comic = GetSelectedComic();
			if (comic == null)
				return;

			Presenter.ToggleComicState(comic.Id);
		}

		private void OnRemoveComic(object sender, EventArgs e)
		{
			var comic = GetSelectedComic();
			if (comic == null)
				return;

			var remove = MessageBox.Show("Are you sure you want to remove {0}?".FormatTo(comic.Name), "Woofy", MessageBoxButtons.YesNo);
			if (remove != DialogResult.Yes)
				return;

			Presenter.Remove(comic.Id);
		}

		private ComicViewModel GetSelectedComic()
		{
			if (dgvwTasks.SelectedRows.Count == 0)
				return null;
			return (ComicViewModel)dgvwTasks.SelectedRows[0].DataBoundItem;
		}
	}
}