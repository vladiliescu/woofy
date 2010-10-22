using System;
using System.Windows.Forms;
using System.Diagnostics;

using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Gui
{
    partial class AboutForm : Form
    {
		readonly IApplicationInfo applicationInfo = ContainerAccessor.Resolve<IApplicationInfo>();
		readonly IDefinitionStore definitionStore = ContainerAccessor.Resolve<IDefinitionStore>();

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
            var definition = new ListViewItem(comicDefinition.Name, authorGroup);
            definition.Tag = comicDefinition.HomePage;
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

            var authorGroup = new ListViewGroup(trimmedAuthor, trimmedAuthor);
            definitionAuthors.Groups.Add(authorGroup);

            return authorGroup;
        }

    	private void InitComicDefinitionsList()
    	{
#warning commented out
			var comicDefinitions = definitionStore.Definitions;
			//foreach (var comicDefinition in comicDefinitions)
			//{
			//    var author = comicDefinition.Author.IsNotNullOrEmpty() ? comicDefinition.Author : "unknown";
			//    var authorGroup = ObtainAuthorGroup(author);
			//    AddComicDefinitionToAuthor(comicDefinition, authorGroup);                    
			//}


    		var groups = new ListViewGroup[definitionAuthors.Groups.Count];
    		definitionAuthors.Groups.CopyTo(groups, 0);
    		definitionAuthors.Groups.Clear();
    		Array.Sort(groups, (a, b) => a.Name.CompareTo(b.Name));
    		definitionAuthors.Groups.AddRange(groups);
    	}

		private void OnHomePageClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(AppSettingsOld.AuthorHomePage);
		}

		private void OnWoofyHomePageClicked(object sender, EventArgs e)
		{
			Process.Start(AppSettingsOld.HomePage);
		}
    }
}
