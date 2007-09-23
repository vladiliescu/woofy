using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using Woofy.Core;

namespace UnitTests
{
    [TestFixture]
    public class WebPathTest
    {
        #region GetDirectory
        [Test]
        [ExpectedArgumentException]
        public void TestThrowsIfDirectoryDoesntStartWithHttp()
        {
            WebPath.GetDirectory("woofy.sourceforge.net");
        }

        [Test]
        public void TestWorksOnSimpleDirectories()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net");
            Assert.AreEqual("http://woofy.sourceforge.net", directory);
        }

        [Test]
        public void TestWorksOnSimpleDirectoriesThatEndWithSlash()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net/");
            Assert.AreEqual("http://woofy.sourceforge.net", directory);
        }

        [Test]
        public void TestWorksOnFiles()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net/favicon.ico");
            Assert.AreEqual("http://woofy.sourceforge.net", directory);
        }

        [Test]
        public void TestWorksOnHttps()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net");
            Assert.AreEqual("https://woofy.sourceforge.net", directory);
        }

        [Test]
        public void TestWorksOnComplexDirectories()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2");
            Assert.AreEqual("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        [Test]
        public void TestWorksOnComplexDirectoriesEndingWithSlash()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2/");
            Assert.AreEqual("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        [Test]
        public void TestWorksOnComplexFiles()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2/favicon.ico");
            Assert.AreEqual("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        #endregion

        #region Combine
        [Test]
        [ExpectedArgumentException]
        public void TestThrowsIfFirstPathDoesntStartWithHttp()
        {
            WebPath.Combine("woofy.sourceforge.net", "favicon.ico");
        }

        [Test]
        public void TestCombinesPathsWithNoSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net", "favicon.ico");
            Assert.AreEqual("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Test]
        public void TestCombinesPathsWithAlternatingSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net/", "favicon.ico");
            Assert.AreEqual("http://woofy.sourceforge.net/favicon.ico", path);

            path = WebPath.Combine("http://woofy.sourceforge.net", "/favicon.ico");
            Assert.AreEqual("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Test]
        public void TestCombinesPathsWithBothSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net/", "/favicon.ico");
            Assert.AreEqual("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Test]
        public void TestCombinesComplexPaths()
        {
            string path = WebPath.Combine("https://woofy.sourceforge.net/dir1/dir2", "/dir3/dir4/favicon.ico");
            Assert.AreEqual("https://woofy.sourceforge.net/dir1/dir2/dir3/dir4/favicon.ico", path);
        }

        [Test]
        [ExpectedArgumentException]
        public void TestThrowsIfFirstPathIsNotADirectory()
        {
            WebPath.Combine("http://woofy.sourceforge.net/favicon.ico", "favicon.ico");
        } 
        #endregion

        #region IsAbsolute

        [Test]
        public void TestWorksOnAbsolutePath()
        {
            bool isAbsolute = WebPath.IsAbsolute("http://woofy.sourceforge.net");
            Assert.AreEqual(true, isAbsolute);
        }

        [Test]
        public void TestWorksOnAbsoluteHttpsPath()
        {
            bool isAbsolute = WebPath.IsAbsolute("https://woofy.sourceforge.net");
            Assert.AreEqual(true, isAbsolute);
        }

        [Test]
        public void TestWorksOnRelativePath()
        {
            bool isAbsolute = WebPath.IsAbsolute("/comics/mycomic.png");
            Assert.AreEqual(false, isAbsolute);
        }

        #endregion

        #region GetRootPath
        [Test]
        public void TestWorksOnRootPath()
        {
            string rootPath = WebPath.GetRootPath("http://woofy.sourceforge.net");
            Assert.AreEqual("http://woofy.sourceforge.net", rootPath);
        }

        [Test]
        public void TestWorksOnComplexPath()
        {
            string rootPath = WebPath.GetRootPath("http://woofy.sourceforge.net/comics/mycomic");
            Assert.AreEqual("http://woofy.sourceforge.net", rootPath);
        }
        #endregion
    }
}
