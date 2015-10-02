namespace UmbracoTestFramework.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using log4net;
    using Models.DocumentTypes;
    using Umbraco.Core.Models;
    using Zone.UmbracoMapper;

    public class NodeService : INodeService
    {
        private readonly IUmbracoHelperService _umbracoHelperService;
        private readonly IUmbracoMapper _umbracoMapper;
        private readonly IPublishedContentExtensionService _publishedContentExtensionService;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public NodeService(
            IUmbracoHelperService umbracoHelperService,
            IUmbracoMapper umbracoMapper,
            IPublishedContentExtensionService publishedContentExtensionService)
        {
            _umbracoHelperService = umbracoHelperService;
            _umbracoMapper = umbracoMapper;
            _publishedContentExtensionService = publishedContentExtensionService;
        }

        public T GetPage<T>(int nodeId) where T : BaseDocumentType, new()
        {            
            IPublishedContent publishedContent = _umbracoHelperService.TypedContent(nodeId);
            
            return publishedContent == null ? default(T) : MapPublishedContentToType<T>(publishedContent);           
        }

        public IEnumerable<T> GetChildren<T>(int rootNodeId = -1) where T : BaseDocumentType, new()
        {
            IPublishedContent root = rootNodeId == -1
                ? _umbracoHelperService.ContentAtRoot()
                : _umbracoHelperService.TypedContent(rootNodeId);

            if (root == null)
            {
                return new List<T>();
            }

            IEnumerable<IPublishedContent> children = _publishedContentExtensionService.Children(root);

            var childrenList = children as IList<IPublishedContent> ?? children.ToList();

            return !childrenList.Any()
                ? new List<T>()
                : childrenList.Select(MapPublishedContentToType<T>).Where(x => x != null);
        }

        private T MapPublishedContentToType<T>(IPublishedContent publishedContent) where T : BaseDocumentType, new()
        {
            if (publishedContent == null)
            {
                return null;
            }

            try
            {                
                var t = new T();
                _umbracoMapper.Map(publishedContent, t);
                return t;
            }
            catch (Exception exception)
            {
                if (publishedContent.DocumentTypeAlias == typeof (T).Name)
                {
                    Log.Error(
                        string.Format("Could not map {0} with node ID {1}", publishedContent.DocumentTypeAlias,
                            publishedContent.Id), exception);
                }

                return null;
            }
        }
    }
}