namespace UmbracoTestFramework.Tests.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Umbraco.Core.Models;
    using Mocks;
    using Models.DocumentTypes;
    using Services;
    using Services.Contracts;
    using Zone.UmbracoMapper;

    [TestClass]
    public class NodeServiceTests
    {
        private Mock<IUmbracoHelperService> _umbracoHelperService;
        private Mock<IPublishedContentExtensionService> _publishedContentExtensionService;
        private INodeService _nodeService;

        [TestMethod]
        public void TestGetPageExists()
        {
            const int id = 1;

            CreateNewNodeService();
            _umbracoHelperService.Setup(x => x.TypedContent(id)).Returns(new MockPublishedContent(id));

            var baseDocumentType = _nodeService.GetPage<BaseDocumentType>(id);

            Assert.AreEqual(id, baseDocumentType.Id);
        }

        [TestMethod]
        public void TestGetPageDoesntExists()
        {
            const int id = 1;

            CreateNewNodeService();
            _umbracoHelperService.Setup(x => x.TypedContent(id)).Returns((IPublishedContent) null);

            var baseDocumentType = _nodeService.GetPage<BaseDocumentType>(id);

            Assert.IsNull(baseDocumentType);
        }

        [TestMethod]
        public void TestGetChildrenRootIdIsEmpty()
        {
            const int id = 1;

            CreateNewNodeService();
            _umbracoHelperService.Setup(x => x.TypedContent(id)).Returns((IPublishedContent)null);

            var baseDocumentType = _nodeService.GetPage<BaseDocumentType>(id);

            Assert.IsNull(baseDocumentType);
        } 

        private void CreateNewNodeService()
        {
            _umbracoHelperService = new Mock<IUmbracoHelperService>();
            _publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            _nodeService = new NodeService(
                _umbracoHelperService.Object,
                new UmbracoMapper(),
                _publishedContentExtensionService.Object);
        }
    }
}
