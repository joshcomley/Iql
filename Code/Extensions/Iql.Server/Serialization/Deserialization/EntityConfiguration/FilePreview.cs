using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class FilePreview : FilePreviewBase
    {
        public IMediaKey MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }
        public IFile File
        {
            get => FileInternal;
            set => FileInternal = value;
        }
        public IProperty UrlProperty
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

    }
}