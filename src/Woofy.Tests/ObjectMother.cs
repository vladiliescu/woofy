using Moq;
using Woofy.Core;
using Woofy.Core.SystemProxies;

namespace Woofy.Tests
{
    public class ObjectMother
    {
        public readonly Mock<IFileProxy> File = new Mock<IFileProxy>();
        public readonly Mock<IAppSettings> AppSettings = new Mock<IAppSettings>();
        public readonly Mock<IDefinitionStore> DefinitionStore = new Mock<IDefinitionStore>();

        public ComicStore CreateComicStore()
        {
            return new ComicStore(AppSettings.Object, DefinitionStore.Object, File.Object);
        }
    }
}