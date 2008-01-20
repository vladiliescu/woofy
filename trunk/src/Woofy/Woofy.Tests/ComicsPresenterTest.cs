using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;
using Woofy.Services;

namespace Woofy.Tests
{
    [TestFixture]
    public class ComicsPresenterTest
    {
        private MockRepository _mocks;

        #region SetUp/TearDown
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion

        #region RefreshComicFavicons
        [Test]
        public void RefreshComicFavicons_Should()
        {
        }
        #endregion
    }
}
