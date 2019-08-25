using System;

namespace Iql.Entities.PropertyGroups.Files
{
    public class FilePreview<T> : FilePreviewBase, IFileUrl<T>
        where T : class
    {
        public new EntityConfiguration<T> EntityConfiguration => EntityConfigurationInternal as EntityConfiguration<T>;
        public new File<T> RootFile => (File<T>) FileInternal;

        public IEntityProperty<T> UrlProperty
        {
            get => (IEntityProperty<T>) UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

        public MediaKey<T> MediaKey
        {
            get => (MediaKey<T>) MediaKeyInternal;
            set => MediaKeyInternal = value;
        }

        public FilePreview(Guid guid, IqlPreviewKind kind = IqlPreviewKind.Image, IFile file = null, IProperty urlProperty = null, int? maxWidth = null, int? maxHeight = null, string key = null)
            : base(guid, file, urlProperty, maxWidth, maxHeight, key)
        {
            MediaKey = new MediaKey<T>(this);
            Kind = kind;
        }
    }
}