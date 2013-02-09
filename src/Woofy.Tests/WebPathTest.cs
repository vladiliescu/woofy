using System;
using Woofy.Core;
using Woofy.Core.Engine;
using Xunit;

namespace Woofy.Tests
{
    public class WebPathTest
    {
        #region GetDirectory
        [Fact]
        public void TestThrowsIfDirectoryDoesntStartWithHttp()
        {
			Assert.Throws<ArgumentException>(() => WebPath.GetDirectory("woofy.sourceforge.net"));
        }

        [Fact]
        public void TestWorksOnSimpleDirectories()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net");
            Assert.Equal("http://woofy.sourceforge.net", directory);
        }

        [Fact]
        public void TestWorksOnSimpleDirectoriesThatEndWithSlash()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net/");
            Assert.Equal("http://woofy.sourceforge.net", directory);
        }

        [Fact]
        public void TestWorksOnFiles()
        {
            string directory = WebPath.GetDirectory("http://woofy.sourceforge.net/favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net", directory);
        }

        [Fact]
        public void TestWorksOnHttps()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net");
            Assert.Equal("https://woofy.sourceforge.net", directory);
        }

        [Fact]
        public void TestWorksOnComplexDirectories()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2");
            Assert.Equal("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        [Fact]
        public void TestWorksOnComplexDirectoriesEndingWithSlash()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2/");
            Assert.Equal("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        [Fact]
        public void TestWorksOnComplexFiles()
        {
            string directory = WebPath.GetDirectory("https://woofy.sourceforge.net/dir1/dir2/favicon.ico");
            Assert.Equal("https://woofy.sourceforge.net/dir1/dir2", directory);
        }

        #endregion

        #region Combine
        [Fact]
        public void TestThrowsIfFirstPathDoesntStartWithHttp()
        {
            Assert.Throws<ArgumentException>(() => WebPath.Combine("woofy.sourceforge.net", "favicon.ico"));
        }

        [Fact]
        public void TestCombinesPathsWithNoSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net", "favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Fact]
        public void TestCombinesPathsWithAlternatingSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net/", "favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net/favicon.ico", path);

            path = WebPath.Combine("http://woofy.sourceforge.net", "/favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Fact]
        public void TestCombinesPathsWithBothSlashes()
        {
            string path = WebPath.Combine("http://woofy.sourceforge.net/", "/favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net/favicon.ico", path);
        }

        [Fact]
        public void TestCombinesComplexPaths()
        {
            string path = WebPath.Combine("https://woofy.sourceforge.net/dir1/dir2", "/dir3/dir4/favicon.ico");
            Assert.Equal("https://woofy.sourceforge.net/dir1/dir2/dir3/dir4/favicon.ico", path);
        }

        [Fact]
        public void TestCombinesIfFirstPathIsNotADirectory()
        {
            string resultedPath = WebPath.Combine("http://woofy.sourceforge.net/favicon.ico", "favicon4.ico");
            Assert.Equal("http://woofy.sourceforge.net/favicon4.ico", resultedPath);
        }

        [Fact]
        public void ShouldCombineRelativePaths()
        {
            string resultedPath = WebPath.Combine("http://woofy.sourceforge.net/comics/mycomic", "../favicon.ico");
            Assert.Equal("http://woofy.sourceforge.net/comics/favicon.ico", resultedPath);
        }

        #endregion

        #region IsAbsolute

        [Fact]
        public void TestWorksOnAbsolutePath()
        {
            bool isAbsolute = WebPath.IsAbsolute("http://woofy.sourceforge.net");
            Assert.Equal(true, isAbsolute);
        }

        [Fact]
        public void TestWorksOnAbsoluteHttpsPath()
        {
            bool isAbsolute = WebPath.IsAbsolute("https://woofy.sourceforge.net");
            Assert.Equal(true, isAbsolute);
        }

        [Fact]
        public void TestWorksOnRelativePath()
        {
            bool isAbsolute = WebPath.IsAbsolute("/comics/mycomic.png");
            Assert.Equal(false, isAbsolute);
        }

        #endregion

        #region GetRootPath
        [Fact]
        public void TestWorksOnRootPath()
        {
            string rootPath = WebPath.GetRootPath("http://woofy.sourceforge.net");
            Assert.Equal("http://woofy.sourceforge.net", rootPath);
        }

        [Fact]
        public void TestWorksOnComplexPath()
        {
            string rootPath = WebPath.GetRootPath("http://woofy.sourceforge.net/comics/mycomic");
            Assert.Equal("http://woofy.sourceforge.net", rootPath);
        }
        #endregion
    }
}
