using System;

namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFileUrlBase : IEntityConfigurationItem, IMetadata
    {
        IFile RootFile { get; }
        IProperty UrlProperty { get; set; }
        Guid Guid { get; set; }
        IMediaKey MediaKey { get; set; }
    }
}