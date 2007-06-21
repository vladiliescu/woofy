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
        private int _fileSize;

        /// <summary>
        /// Creates a new instance of the <see cref="DownloadProgressForm"/> and sets the file to be downloaded size.
        /// </summary>
        /// <param name="fileSize">The size of the file to be downloaded.</param>
        public DownloadProgressForm(int fileSize)
        {
            InitializeComponent();
            _fileSize = fileSize;
        }

        /// <summary>
        /// Increments the download progress with a specified number of bytes.
        /// </summary>
        /// <param name="bytesDownloaded">Number of bytes by which to increment the progress.</param>
        public void IncrementProgress(int bytesDownloaded)
        {
            pbDownloadProgress.Invoke(new MethodInvoker(
                delegate
                {
                    pbDownloadProgress.Increment(bytesDownloaded / 1024);
                    lblDownloadDetails.Text = string.Format("{0} kB/ {1} kB", bytesDownloaded, _fileSize);
                }
            ));
        }

        private void DownloadProgressForm_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(typeof(Program), "Woofy.ico");
        }
    }
}