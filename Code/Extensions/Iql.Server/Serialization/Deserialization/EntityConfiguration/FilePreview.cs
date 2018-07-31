using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Serialization
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
        public IProperty Property
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

    }
}