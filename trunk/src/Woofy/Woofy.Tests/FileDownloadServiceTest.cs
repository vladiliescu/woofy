using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using MbUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Woofy.Core;
using Woofy.Other;
using Woofy.Exceptions;
using Woofy.Entities;
using System.Net;
using System.IO;

namespace Woofy.Tests
{
    [TestFixture]
    public class FileDownloadServiceTest
    {
        private MockRepository _mocks;
        private FileWrapper _fileWrapper;
        private FileWrapper _fileWrapperDynamic;
        private PathWrapper _pathWrapper;
        private WebClientWrapper _webClient;
        private WebClientWrapper _webClientDynamic;
        private FileDownloadService _fileDownloadService;
        private FileDownloadService _fileDownloadServiceDynamic;

        #region SetUp/TearDown
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();

            _fileWrapper = _mocks.PartialMock<FileWrapper>();
            _pathWrapper = _mocks.PartialMock<PathWrapper>();
            _webClient = _mocks.PartialMock<WebClientWrapper>();
            _fileDownloadService = new FileDownloadService(_webClient, _fileWrapper, _pathWrapper);

            _webClientDynamic = _mocks.DynamicMock<WebClientWrapper>();
            _fileWrapperDynamic = _mocks.DynamicMock<FileWrapper>();
            _fileDownloadServiceDynamic = new FileDownloadService(_webClientDynamic, _fileWrapperDynamic, _pathWrapper);
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion

        #region DownloadFile
        //[Test]
        //public void DownloadFile_ShouldDownloadFileWithATemporaryExtensionAndRenameItAfterDownloading()
        //{
        //    string fileName = "myfile.zip";
        //    Uri fileAddress = new Uri("http://mysite.com/" + fileName);
        //    using (_mocks.Record())
        //    {
        //        Expect
        //            .Call(delegate { _webClientDynamic.DownloadFile(null, null); })
        //            .IgnoreArguments()
        //            .Constraints(Property.Value("AbsoluteUri", "http://mysite.com/myfile.zip"), Text.EndsWith(fileName + ".!wf"));

        //        Expect
        //            .Call(delegate { _fileWrapperDynamic.Move(null, null); })
        //            .IgnoreArguments()
        //            .Constraints(Text.EndsWith(fileName + ".!wf"), Text.EndsWith(fileName));
        //    }

        //    using (_mocks.Playback())
        //    {
        //        _fileDownloadServiceDynamic.DownloadFile(fileAddress);
        //    }
        //}

        //[Test]
        //public void DownloadFile_ShouldReturnThePathToTheDownloadedFile()
        //{
        //    string fileName = "myfile.zip";
        //    Uri fileAddress = new Uri("http://mysite.com/" + fileName);
        //    using (_mocks.Record())
        //    {
        //        Expect
        //            .Call(delegate { _webClientDynamic.DownloadFile(null, null); })
        //            .IgnoreArguments();

        //        Expect
        //            .Call(delegate { _fileWrapperDynamic.Move(null, null); })
        //            .IgnoreArguments();
        //    }

        //    using (_mocks.Playback())
        //    {
        //        string downloadedFilePath = _fileDownloadServiceDynamic.DownloadFile(fileAddress);
        //        Assert.AreEqual(Path.Combine(Constants.DefaultDownloadFolder, fileName), downloadedFilePath);
        //    }
        //}

        //[Test]
        //[ExpectedException(typeof(WebException))]
        //public void DownloadFile_ShouldHandleDownloadExceptionsByDeletingThePartialFileAndRethrowing()
        //{
        //    string fileName = "myfile.zip";
        //    Uri fileAddress = new Uri("http://mysite.com/" + fileName);
        //    using (_mocks.Record())
        //    {
        //        Expect
        //            .Call(delegate { _webClientDynamic.DownloadFile(null, null); })
        //            .IgnoreArguments()
        //            .Throw(new WebException());

        //        Expect
        //            .Call(delegate { _fileWrapperDynamic.Delete(null); })
        //            .IgnoreArguments()
        //            .Constraints(Text.EndsWith(fileName + ".!wf"));
        //    }

        //    using (_mocks.Playback())
        //    {
        //        _fileDownloadServiceDynamic.DownloadFile(fileAddress);
        //    }
        //}

        //[Test]
        //public void DownloadFile_ShouldOverwriteExistingFileIfSpecified()
        //{
        //    string fileName = "myfile.zip";
        //    using (_mocks.Record())
        //    {
        //        Expect
        //            .Call(delegate { _webClientDynamic.DownloadFile(null, null); })
        //            .IgnoreArguments();

        //        Expect
        //            .Call(_fileWrapperDynamic.Exists(null))
        //            .IgnoreArguments()
        //            .Return(true);

        //        Expect
        //            .Call(delegate { _fileWrapperDynamic.Delete(null); })
        //            .IgnoreArguments()
        //            .Constraints(Text.EndsWith(fileName));

        //        Expect
        //            .Call(delegate { _fileWrapperDynamic.Move(null, null); })
        //            .IgnoreArguments()
        //            .Constraints(Text.EndsWith(fileName + ".!wf"), Text.EndsWith(fileName));
        //    }

        //    using (_mocks.Playback())
        //    {
        //        _fileDownloadServiceDynamic.DownloadFile(new Uri("http://mysite.com/" + fileName), null, true);
        //    }
        //}

        //[Test]
        //public void DownloadFile_ShouldNotOverwriteExistingFileAndReturnNullIfNotSpecified()
        //{

        //    string fileName = "myfile.zip";
        //    using (_mocks.Record())
        //    {
        //        Expect
        //            .Call(delegate { _webClientDynamic.DownloadFile(null, null); })
        //            .IgnoreArguments();

        //        Expect
        //            .Call(_fileWrapperDynamic.Exists(null))
        //            .IgnoreArguments()
        //            .Return(true);
        //    }

        //    using (_mocks.Playback())
        //    {
        //        Assert.IsNull(_fileDownloadServiceDynamic.DownloadFile(new Uri("http://mysite.com/" + fileName), null, false));
        //    }
        //}
        #endregion

        #region DownloadFileAsync
        [Test]
        public void DownloadFileAsync_ShouldReturnAnExceptionWhenFileAlreadyExists()
        {
            IEventSubscriber subscriber = _mocks.CreateMock<IEventSubscriber>();

            using (_mocks.Record())
            {
                Expect
                    .Call(_fileWrapper.Exists(null))
                    .IgnoreArguments()
                    .Return(true);

                _fileDownloadService.DownloadFileCompleted += subscriber.Handler;

                Expect
                    .Call(delegate { subscriber.Handler(_fileDownloadService, new AsyncCompletedEventArgs(null, false, null)); })
                    .Constraints(Is.Equal(_fileDownloadService),
                                    Property.ValueConstraint("Error", Is.TypeOf<FileAlreadyPresentException>()) && Property.Value("Cancelled", false))
                    .Message("If the file already exists, then an exception must be returned.");

                Expect
                    .Call(delegate { _webClient.DownloadFileAsync(null, null); })
                    .IgnoreArguments()
                    .Repeat.Never()
                    .Message("If the file already exists, then the method should no longer try to download the file.");
            }

            using (_mocks.Playback())
            {
                _fileDownloadService.DownloadFileAsync(new Uri("http://mysite.com/file"));
            }

            _fileDownloadService.DownloadFileCompleted -= subscriber.Handler;
        }
        #endregion

        #region GetPathToDownloadFileTo
        [Test]
        public void GetPathToDownloadFileTo_ShouldCombineTheFileNameWithTheDefaultDownloadFolderWhenDownloadFolderIsEmpty()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_pathWrapper.Combine(ApplicationSettings.DefaultDownloadFolder, "myfile.zip"))
                    .Return(null);
            }

            using (_mocks.Playback())
            {
                _fileDownloadService.GetPathToDownloadFileTo(new Uri("http://www.mysite.com/subdir/myfile.zip"), null);
            }
        }

        [Test]
        public void GetPathToDownloadFileTo_ShouldCombineTheFileNameWithTheDownloadFolderWhenDownloadFolderIsNotEmpty()
        {
            string downloadFolder = "c:\\";
            using (_mocks.Record())
            {
                Expect
                    .Call(_pathWrapper.Combine(downloadFolder, "myfile.zip"))
                    .Return(null);
            }

            using (_mocks.Playback())
            {
                _fileDownloadService.GetPathToDownloadFileTo(new Uri("http://www.mysite.com/subdir/myfile.zip"), downloadFolder);
            }
        }
        #endregion
    }
}
