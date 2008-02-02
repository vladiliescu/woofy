using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;
using Woofy.Services;
using Woofy.Controllers;
using Woofy.Other;
using Woofy.Entities;

namespace Woofy.Tests
{
    [TestFixture]
    public class ComicsPresenterTest
    {
        private MockRepository _mocks;

        private PageParseService _pageParseService;
        private FileWrapper _file;
        private PathWrapper _path;
        private WebClientWrapper _webClient;
        private ComicsPresenter _comicsPresenter;

        private IEventSubscriber _eventSubscriber;

        #region SetUp/TearDown
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();            

            _pageParseService = _mocks.PartialMock<PageParseService>();
            _file = _mocks.PartialMock<FileWrapper>();
            _path = _mocks.PartialMock<PathWrapper>();
            _webClient = _mocks.PartialMock<WebClientWrapper>();

            _comicsPresenter = _mocks.PartialMock<ComicsPresenter>(_pageParseService, _webClient, _path, _file);

            _eventSubscriber = _mocks.CreateMock<IEventSubscriber>();
            _comicsPresenter.RefreshViewsRequired += _eventSubscriber.Handler;
        }

        [TearDown]
        public void TearDown()
        {
            _comicsPresenter.RefreshViewsRequired -= _eventSubscriber.Handler;
        }
        #endregion

        #region RefreshComicFavicons
        [Test]
        public void RefreshComicFavicons_ShouldReturnIfNoFaviconFound()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_pageParseService.RetrieveFaviconAddressFromPage(null))
                    .IgnoreArguments()
                    .Return(null);

                Expect
                    .Call(delegate { _webClient.DownloadFile(null, null); })
                    .IgnoreArguments()
                    .Repeat.Never();
            }

            using (_mocks.Playback())
            {
                _comicsPresenter.RefreshComicFavicon(new Comic());
            }
        }

        [Test]
        public void RefreshComicFavicons_ShouldDownloadFaviconAndRefreshView()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_pageParseService.RetrieveFaviconAddressFromPage(null))
                    .IgnoreArguments()
                    .Return(new Uri("http://www.mysite.com"));

                Expect
                    .Call(delegate { _webClient.DownloadFile(null, null); })
                    .IgnoreArguments()
                    .Repeat.Once();                

                Expect
                    .Call(delegate { _eventSubscriber.Handler(_comicsPresenter, EventArgs.Empty); });
            }

            using (_mocks.Playback())
            {
                _comicsPresenter.RefreshComicFavicon(new Comic());
            }
        }
        #endregion
    }
}
