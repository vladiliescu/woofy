using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Woofy.Gui
{
    public partial class DownloadProgressForm : Form
    {
        #region Instance Members
        private int _fileSize; 
        #endregion

        #region .ctor
        /// <summary>
        /// Creates a new instance of the <see cref="DownloadProgressForm"/> and sets the file to be downloaded size.
        /// </summary>
        /// <param name="fileSize">The size (in kiloBytes) of the file to be downloaded.</param>
        public DownloadProgressForm(int fileSize)
        {
            InitializeComponent();
            _fileSize = fileSize;
            pbDownloadProgress.Maximum = fileSize;
        } 
        #endregion

        #region Events - Form
        private void DownloadProgressForm_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(typeof(Program), "Woofy.ico");
            lblDownloadDetails.Text = string.Format("{0} kB/ {1} kB", pbDownloadProgress.Value, _fileSize);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Increments the download progress with a specified number of bytes.
        /// </summary>
        /// <param name="bytesDownloaded">Number of bytes by which to increment the progress.</param>
        public void IncrementProgress(int bytesDownloaded)
        {
            pbDownloadProgress.Invoke(new MethodInvoker(
                delegate
                {
                    int kiloBytesDownloaded = bytesDownloaded / 1024;
                    pbDownloadProgress.Increment(kiloBytesDownloaded);
                    lblDownloadDetails.Text = string.Format("{0} kB/ {1} kB", pbDownloadProgress.Value, _fileSize);
                }
            ));
        } 
        #endregion        
    }
}