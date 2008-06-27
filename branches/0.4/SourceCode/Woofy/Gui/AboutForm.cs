using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

using Woofy.Core;

namespace Woofy.Gui
{
    partial class AboutForm : Form
    {
        #region .ctor
        public AboutForm()
        {
            InitializeComponent();

            this.Text = string.Format("About {0}", AssemblyTitle);
            this.lblProductInfo.Text = string.Format("{0} {1} Copyright {2} {3}", AssemblyTitle, AssemblyShortVersion, AssemblyCopyright, AssemblyCompany);

            this.btnOK.Focus();

            InitComicDefinitionsList();
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
        }
        #endregion

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyShortVersion
        {
            get
            {
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                return assemblyName.Version.ToString();
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        #region Events - clicks
        private void lnkFamFamFam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.famfamfam.com/lab/icons/silk/");
        }

        private void lnkMailto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:vlad.iliescu@gmail.com");
        }

        private void lnkWebAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://woofy.sourceforge.net");
        }

        private void lnkIconCredits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://hobbit1978.deviantart.com/");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Helper Methods
        private void AddComicDefinitionToAuthor(ComicDefinition comicDefinition, ListViewGroup authorGroup)
        {
            ListViewItem definition = new ListViewItem(comicDefinition.FriendlyName, authorGroup);
            definition.Tag = comicDefinition.StartUrl;
            this.definitionAuthors.Items.Add(definition);
        }

        private ListViewGroup ObtainAuthorGroup(string author)
        {
            string trimmedAuthor = author.Trim();
            foreach (ListViewGroup group in this.definitionAuthors.Groups)
            {
                if (group.Name.Equals(trimmedAuthor, StringComparison.OrdinalIgnoreCase))
                    return group;
            }

            ListViewGroup authorGroup = new ListViewGroup(trimmedAuthor, trimmedAuthor);
            this.definitionAuthors.Groups.Add(authorGroup);

            return authorGroup;
        } 
        #endregion

        private void definitionAuthors_DoubleClick(object sender, EventArgs e)
        {
            if (this.definitionAuthors.SelectedItems.Count == 0)
                return;


            System.Diagnostics.Process.Start((string)this.definitionAuthors.SelectedItems[0].Tag);
        }
    }
}
