using System.Linq;

namespace Iql.Entities.Dates
{
    public class FileBase : PropertyGroupBase<IFile>, IFile
    {
        public FileBase(
            IProperty fileUrlProperty = null,
            IProperty previewUrlProperty = null,
            IProperty nameProperty = null,
            IProperty versionProperty = null,
            IProperty kindProperty = null,
            string key = null) : base(null, key)
        {
            FileUrlProperty = fileUrlProperty;
            PreviewUrlProperty = previewUrlProperty;
            NameProperty = nameProperty;
            VersionProperty = versionProperty;
            KindProperty = kindProperty;
        }

        IMediaKey IFile.MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }
        protected IMediaKey MediaKeyInternal { get; set; }
        public override IEntityConfiguration EntityConfiguration =>
            (FileUrlProperty ?? PreviewUrlProperty ?? NameProperty ?? VersionProperty ?? KindProperty)?.EntityConfiguration;
        public override PropertyKind Kind { get; set; }
        public IProperty FileUrlProperty { get; set; }
        public IProperty PreviewUrlProperty { get; set; }
        public IProperty NameProperty { get; set; }
        public IProperty VersionProperty { get; set; }
        public IProperty KindProperty { get; set; }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { FileUrlProperty, PreviewUrlProperty, NameProperty, VersionProperty, KindProperty }
                .Where(p => !Equals(null, p)).ToArray();
        }

        public FilePropertyKind GetPropertyKind(IProperty property)
        {
            if (property == FileUrlProperty)
            {
                return FilePropertyKind.FileUrl;
            }

            if (property == PreviewUrlProperty)
            {
                return FilePropertyKind.PreviewUrl;
            }

            if (property == VersionProperty)
            {
                return FilePropertyKind.Version;
            }

            if (property == KindProperty)
            {
                return FilePropertyKind.Kind;
            }

            return FilePropertyKind.None;
        }
    }
}