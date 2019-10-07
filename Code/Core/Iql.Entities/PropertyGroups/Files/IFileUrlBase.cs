using System;

namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFileUrlBase : IEntityConfigurationItem, IMetadata
    {
        IFileState TryGetFileState(object entity);
        bool TrySetFileState(object entity, IFileState state);
        IFile RootFile { get; }
        IProperty UrlProperty { get; set; }
        IProperty StateProperty { get; set; }
        Guid Guid { get; set; }
        IMediaKey MediaKey { get; set; }
    }
}