using System;
using System.Windows.Forms;
using System.Diagnostics;

using Woofy.Core;

namespace Woofy.Gui
{
    partial class AboutForm : Form
    {
		private readonly IApplicationInfo applicationInfo = new ApplicationInfo();

        public AboutForm()
        {
            InitializeComponent();

            Text = string.Format("About {0}", applicationInfo.Name);
            lblProductInfo.Text = string.Format("{0} {1} {2} {3}", applicationInfo.Name, applicationInfo.Version, applicationInfo.Copyright, applicationInfo.Company);

            btnOK.Focus();

            InitComicDefinitionsList();
        }

    	private void OnLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			Process.Start(((LinkLabel)sender).Text);
        }

    	private void definitionAuthors_DoubleClick(object sender, EventArgs e)
    	{
    		if (definitionAuthors.SelectedItems.Count == 0)
    			return;

    		Process.Start((string)definitionAuthors.SelectedItems[0].Tag);
    	}

    	private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

    	private void AddComicDefinitionToAuthor(ComicDefinition comicDefinition, ListViewGroup authorGroup)
        {
            ListViewItem definition = new ListViewItem(comicDefinition.FriendlyName, authorGroup);
            definition.Tag = comicDefinition.StartUrl;
            definitionAuthors.Items.Add(definition);
        }

    	private ListViewGroup ObtainAuthorGroup(string author)
        {
            string trimmedAuthor = author.Trim();
            foreach (ListViewGroup group in definitionAuthors.Groups)
            {
                if (group.Name.Equals(trimmedAuthor, StringComparison.OrdinalIgnoreCase))
                    return group;
            }

            ListViewGroup authorGroup = new ListViewGroup(trimmedAuthor, trimmedAuthor);
            definitionAuthors.Groups.Add(authorGroup);

            return authorGroup;
        }

    	private void InitComicDefinitionsList()
    	{
    		ComicDefinition[] comicDefinitions = ComicDefinition.GetAvailableComicDefinitions();
    		foreach (ComicDefinition comicDefinition in comicDefinitions)
    		{
    			if (string.IsNullOrEmpty(comicDefinition.Author))
    				continue;
    			ListViewGroup authorGroup = ObtainAuthorGroup(comicDefinition.Author);
    			AddComicDefinitionToAuthor(comicDefinition, authorGroup);                    
    		}


    		ListViewGroup[] groups = new ListViewGroup[definitionAuthors.Groups.Count];
    		definitionAuthors.Groups.CopyTo(groups, 0);
    		definitionAuthors.Groups.Clear();
    		Array.Sort(groups, (a, b) => a.Name.CompareTo(b.Name));
    		definitionAuthors.Groups.AddRange(groups);
    	}
    }
}