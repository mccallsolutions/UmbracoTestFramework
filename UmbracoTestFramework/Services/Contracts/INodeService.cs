namespace UmbracoTestFramework.Services.Contracts
{
    using System.Collections.Generic;
    using Models.DocumentTypes;

    public interface INodeService
    {
        T GetPage<T>(int nodeId) where T : BaseDocumentType, new();
        IEnumerable<T> GetChildren<T>(int rootNodeId) where T : BaseDocumentType, new();
    }
}
