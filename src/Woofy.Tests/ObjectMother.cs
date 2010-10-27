using System;
using Moq;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.SystemProxies;

namespace Woofy.Tests
{
    public class ObjectMother
    {
        public readonly Mock<IFileProxy> File = new Mock<IFileProxy>();
        public readonly Mock<IAppSettings> AppSettings = new Mock<IAppSettings>();
        public readonly Mock<IDefinitionStore> DefinitionStore = new Mock<IDefinitionStore>();
        public readonly Mock<IUserSettings> UserSettings = new Mock<IUserSettings>();
        public readonly Mock<IPageParser> PageParser = new Mock<IPageParser>();
        public readonly Mock<IWebClientProxy> WebClient = new Mock<IWebClientProxy>();

        public ComicStore CreateComicStore()
        {
            return new ComicStore(AppSettings.Object, DefinitionStore.Object, File.Object, UserSettings.Object);
        }

        public VisitExpression CreateVisitExpression()
        {
            return new VisitExpression(PageParser.Object, WebClient.Object);
        }
    }
}