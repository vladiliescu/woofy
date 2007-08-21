using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using log4net.Core;

using Woofy.Core;

namespace Woofy.Gui
{
    public partial class DefinitionsDebugForm : Form
    {
        public DefinitionsDebugForm()
        {
            InitializeComponent();            
        }        

        private void DefinitionsDebugForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            foreach (ComicInfo comicDefinition in ComicInfo.GetAvailableComicInfos())
            {
                comicDefinitionsSelector.Items.Add(comicDefinition);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startDebugButton_Click(object sender, EventArgs e)
        {
            eventsRichTextBox.Focus();

            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    MonitorDebugMessages();
                }
            , null);

            RunComicTest((ComicInfo)comicDefinitionsSelector.Items[4]);            
        }

        private void MonitorDebugMessages()
        {
            while (true)
            {
                LoggingEvent[] latestEvents =  Logger.GetLatestDebugMessages();
                this.Invoke(new MethodInvoker(
                    delegate
                    {
                        foreach (LoggingEvent loggingEvent in latestEvents)
                            eventsRichTextBox.AppendText(string.Format("[{0:T}] {1}\n", loggingEvent.TimeStamp, loggingEvent.MessageObject));
                    }
                ));
                

                Thread.Sleep(500);
            }
        }

        private void RunComicTest(ComicInfo comicDefinition)
        {
            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    CountingFileDownloader countingFileDownloader = new CountingFileDownloader();
                    ComicsProvider comicsProvider = new ComicsProvider(comicDefinition, countingFileDownloader);

                    comicsProvider.DownloadComics(ComicsProvider.AllAvailableComics);

                    string[] comics = countingFileDownloader.ComicLinks;

                    if (comics[comics.Length - 1] == comicDefinition.FirstIssue)
                        Logger.Debug("The first strip has been reached, so the comic definition works as expected.");
                    else
                        Logger.Debug("The first strip has not been reached, the definition needs some more work.");
                }
            , null);            
        }
    }
}