using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Web;
using Woofy.Enums;
using Woofy.Exceptions;

namespace Woofy.Core.Engine
{
	public class BotOldAndOnlyUsedForReference
	{
		private readonly bool randomPausesBetweenRequests;

		public Comic Comic { get; private set; }
		private readonly IFileDownloader comicsDownloader;
		private readonly Definition definition;
		private WebClient webClient;
		private bool isDownloadCancelled;
		private readonly Random random = new Random();

#warning appSettings.ContentGroupName should be used instead.
		public const string ContentGroup = "content";

		public BotOldAndOnlyUsedForReference(Definition definition, IFileDownloader comicsDownloader, bool randomPausesBetweenRequests)
		{
			this.definition = definition;
			this.comicsDownloader = comicsDownloader;
			this.randomPausesBetweenRequests = randomPausesBetweenRequests;

			webClient = WebConnectionFactory.GetNewWebClientInstance();
		}

		public BotOldAndOnlyUsedForReference(Comic comic)
			: this(comic.Definition, new FileDownloader(comic.DownloadFolder), comic.RandomPausesBetweenRequests)
		{
			Comic = comic;
		}

		public void DownloadComicsAsync()
		{
#warning commented out
			//DownloadComicsAsync(definition.HomePage);
		}

		public void DownloadComicsAsync(string startUrl)
		{
			ThreadPool.UnsafeQueueUserWorkItem(
				delegate
					{
						//DownloadComics(startUrl);
						//DownloadComicsEx();
					}, null
				);
		}

		public void DownloadComicsEx()
		{

			
			//var steps = new IStep[] { 
			//                            new VisitStep { Data = "http://xkcd.com" }, 
			//                            new DownloadStep { Data = @"<img\ssrc=""(?<content>http://imgs.xkcd.com/comics/[\w()-]*\.(gif|jpg|jpeg|png))" },
			//                            new NextPageStep { Data = @"<a\shref=""(?<content>/[\d]*/)""\saccesskey=""p"">" }
			//                            //new WhileStep(new IStep[] {
			//                            //        new DownloadStep(@"<img\ssrc=""(?<content>http://imgs.xkcd.com/comics/[\w()-]*\.(gif|jpg|jpeg|png))"),
			//                            //        new NextPageStep(@"<a\shref=""(?<content>/[\d]*/)""\saccesskey=""p"">")
			//                            //    }
			//                            //)
			//                        };

			//var context = new Context();
			//foreach (var step in steps)
			//{
			//    step.Run(context);
			//}
		}

//        public DownloadOutcome DownloadComics(string startUrl)
//        {
//            Logger.Debug("Downloading comic {0}.", definition.Name);

//            isDownloadCancelled = false;
//            var downloadOutcome = DownloadOutcome.Successful;
            
//            try
//            {
//                var rootUri = string.IsNullOrEmpty(definition.RootUrl) ? null : new Uri(definition.RootUrl);
//                var currentUrl = startUrl;

//                while(true)
//                {
//                    if (isDownloadCancelled)
//                    {
//                        Logger.Debug("Download stopped by user.");

//                        downloadOutcome = DownloadOutcome.Cancelled;
//                        break;
//                    }

//                    Logger.Debug("Visiting page {0}.", currentUrl);

//                    var currentUri = new Uri(currentUrl);
//                    var pageContent = webClient.DownloadString(currentUri);

//                    var comicLinks = RetrieveComicLinks(pageContent, rootUri ?? currentUri, definition);
//                    var nextPageLink = RetrieveNextPageLink(pageContent, rootUri ?? currentUri, definition);
//                    var captures = new PageParser(pageContent, currentUrl, definition).GetCaptures();

//                    if (!MatchedLinksObeyRules(comicLinks.Length, definition.AllowMissingStrips, definition.AllowMultipleStrips, ref downloadOutcome))
//                        break;

//                    var nextPageStringLink = nextPageLink == null ? null : nextPageLink.AbsoluteUri;
//                    foreach (var comicLink in comicLinks)
//                    {
//                        var fileName = GetFileName(comicLink.AbsoluteUri, definition.RenamePattern, captures);
//                        if (!string.IsNullOrEmpty(definition.RenamePattern))
//                        {
//                            Logger.Debug("Renamed strip to:");
//                            Logger.Debug("\t>>{0}", fileName);
//                        }

//                        bool fileAlreadyDownloaded;
//                        comicsDownloader.DownloadFile(comicLink.AbsoluteUri, currentUrl, fileName, out fileAlreadyDownloaded);

//                        //if (fileAlreadyDownloaded && comicsToDownload == AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
//                        //   break;

//#warning get rid of updating the downloaded strip count here
//                        OnDownloadComicCompleted(new DownloadStripCompletedEventArgs(0, nextPageStringLink));
//                    }

//                    //HACK
//                    //if (fileAlreadyDownloaded && comicsToDownload == AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
//                    //    break;

//                    if (nextPageLink == null)
//                        break;

//                    currentUrl = nextPageLink.AbsoluteUri;

//                    if (randomPausesBetweenRequests)
//                        PauseForRandomPeriod();
//                }
//            }
//            catch (UriFormatException ex)
//            {
//                Logger.Debug("Encountered an exception while downloading: {0}.", ex.Message);

//                downloadOutcome = DownloadOutcome.Error;
//            }
//            catch (WebException ex)
//            {
//                Logger.Debug("Encountered an exception while downloading: {0}.", ex.Message);

//                downloadOutcome = DownloadOutcome.Error;
//            }
//            catch (RegexException ex)
//            {
//                Logger.Debug("Encountered an exception while searching for regex matches: {0}.", ex.Message);
//                downloadOutcome = DownloadOutcome.Error;
//            }
//            catch (IOException ex)
//            {
//                //if the network goes down while downloading a strip, the FileDownloader will throw an IOException containing a SocketException
//                //yummy
//                if (!(ex.InnerException is SocketException))
//                    throw;
//                Logger.Debug("Could not access the network.");
//                downloadOutcome = DownloadOutcome.Error;
//            }

//            OnDownloadCompleted(downloadOutcome);

//            return downloadOutcome;
//        }

		/// <summary>
		/// Stops the current download.
		/// </summary>
		public void StopDownload()
		{
			isDownloadCancelled = true;
		}

		private void PauseForRandomPeriod()
		{
			Thread.Sleep(500 + random.Next(1500));
		}

		private string GetFileName(string fileLink, string renamePattern, IDictionary<string, string> captures)
		{
			var baseFileName = fileLink.Substring(fileLink.LastIndexOf('/') + 1);
			var fileName = baseFileName;
			if (!string.IsNullOrEmpty(renamePattern))
			{
				fileName = Regex.Replace(renamePattern, @"\$\{(?<name>[\w\d]*)\}",
				                         delegate(Match match)
				                         	{
				                         		var name = match.Groups["name"].Value;
				                         		if (name == "fileName")
				                         			return Path.GetFileNameWithoutExtension(baseFileName);

				                         		return captures[name];
				                         	}
					);

				if (!fileName.Contains("."))
					fileName += Path.GetExtension(baseFileName);
			}

			return fileName;
		}

		public static Uri[] RetrieveLinksFromPage(string pageContent, Uri currentUri, string regex)
		{
			var links = new List<Uri>();
			MatchCollection matches;

			try
			{
				matches = Regex.Matches(pageContent, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			}
			catch (ArgumentException ex)
			{
				throw new RegexException(ex.Message, ex);
			}

			foreach (Match match in matches)
			{
				string capturedContent = match.Groups[ContentGroup].Success ? match.Groups[ContentGroup].Value : match.Value;

				//just in case someone html-encoded the link; happened with Gone With The Blastwave;
				capturedContent = HttpUtility.HtmlDecode(capturedContent);

				if (WebPath.IsAbsolute(capturedContent))
					links.Add(new Uri(capturedContent));
				else
					links.Add(new Uri(currentUri, capturedContent));
			}

			return links.ToArray();
		}

		/// <summary>
		/// Checks if the matched comic links obey the comic's download rules (i.e. no multiple strip matches).
		/// </summary>
		/// <param name="downloadOutcome">If a rule is not obeyed, then this parameter will contain the respective outcome.</param>
		public static bool MatchedLinksObeyRules(int linksLength, bool allowMissingStrips, bool allowMultipleStrips, ref DownloadOutcome downloadOutcome)
		{
			if (linksLength == 0 && !allowMissingStrips)
			{
				downloadOutcome = DownloadOutcome.NoStripMatchesRuleBroken;
				return false;
			}

			if (linksLength > 1 && !allowMultipleStrips)
			{
				downloadOutcome = DownloadOutcome.MultipleStripMatchesRuleBroken;
				return false;
			}

			return true;
		}

		public static string GetProperStartUrlFromPage(string pageContent, Uri pageUri, string latestPageRegex)
		{
			if (string.IsNullOrEmpty(latestPageRegex))
				return pageUri.AbsoluteUri;

			Uri[] links = RetrieveLinksFromPage(pageContent, pageUri, latestPageRegex);

			if (links.Length == 0)
			{
				Logger.Debug("No links match the latestPageRegex element. Using the startUrl element ({0}).", pageUri);
				return pageUri.AbsoluteUri;
			}

			bool isFirst = true;
			Logger.Debug("Found {0} link(s) that match the latestPageRegex element:", links.Length);
			foreach (Uri link in links)
			{
				if (isFirst)
				{
					Logger.Debug("\t>>{0}.", link);
					isFirst = false;
				}
				else
				{
					Logger.Debug("\t{0}.", link);
				}
			}

			return links[0].AbsoluteUri;
		}

		/// <summary>
		/// Returns the back button link from the specified page, or, if there are several back button links in the page, it returns null.
		/// </summary>
		private Uri RetrieveNextPageLink(string pageContent, Uri currentUri, ComicDefinition definition)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			var links = RetrieveLinksFromPage(pageContent, currentUri, definition.NextPageRegex);

			if (links.Length > 0)
			{
				bool isFirst = true;
				Logger.Debug("Found {0} link(s):", links.Length);
				foreach (Uri link in links)
				{
					if (isFirst)
					{
						Logger.Debug("\t>>{0}.", link);
						isFirst = false;
					}
					else
					{
						Logger.Debug("\t{0}.", link);
					}
				}

				return links[0];
			}
            
#warning does this still apply?
			Logger.Debug("No links match the nextPageRegex element. The comic has completed.");

			return null;
		}

		private Uri[] RetrieveComicLinks(string pageContent, Uri pageUri, ComicDefinition comicInfo)
		{
			Uri[] comicLinks = RetrieveLinksFromPage(pageContent, pageUri, comicInfo.ComicRegex);
			Logger.Debug("Found {0} strip(s):", comicLinks.Length);
			foreach (Uri comicLink in comicLinks)
			{
				Logger.Debug("\t{0}.", comicLink);
			}

			return comicLinks;
		}

		/// <summary>
		/// Occurs when a single comic is downloaded.
		/// </summary>
		public event EventHandler<DownloadStripCompletedEventArgs> DownloadComicCompleted;
		protected virtual void OnDownloadComicCompleted(DownloadStripCompletedEventArgs e)
		{
			var eventReference = DownloadComicCompleted;
			if (eventReference != null)
				eventReference(this, e);
		}

		/// <summary>
		/// Occurs when the entire download is completed.
		/// </summary>
		public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;
		protected virtual void OnDownloadCompleted(DownloadOutcome downloadOutcome)
		{
			var eventReference = DownloadCompleted;
			if (eventReference != null)
				eventReference(this, new DownloadCompletedEventArgs(downloadOutcome));
		}
	}
}