using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.PropertyGroups.Files
{
    public class FileBase : SimplePropertyGroupBase<IFile>, IFile
    {
        protected override string ResolveName()
        {
            return UrlPropertyInternal?.Name ?? "File";
        }

        public FileBase(
            IProperty urlProperty = null,
            IProperty nameProperty = null,
            IProperty versionProperty = null,
            IProperty kindProperty = null,
            string key = null) : base(null, key)
        {
            UrlProperty = urlProperty;
            NameProperty = nameProperty;
            VersionProperty = versionProperty;
            KindProperty = kindProperty;
        }

        IMediaKey IFileUrlBase.MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }
        protected IMediaKey MediaKeyInternal { get; set; }
        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.File;

        public override IEntityConfiguration EntityConfiguration =>
            EntityConfigurationInternal;

        protected IEntityConfiguration EntityConfigurationInternal =>
            (UrlProperty ?? NameProperty ?? VersionProperty ?? KindProperty)?.EntityConfiguration;

        public override PropertyKind Kind { get; set; } = PropertyKind.SimpleCollection;
        public IList<IFilePreview> Previews { get; set; } = new List<IFilePreview>();
        public IFile RootFile => RootFileInternal;
        protected IFile RootFileInternal => this;
        protected IProperty UrlPropertyInternal { get; set; }
        public IProperty UrlProperty
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

        public IProperty NameProperty { get; set; }
        public IProperty VersionProperty { get; set; }
        public IProperty KindProperty { get; set; }

        public override IPropertyGroup[] GetGroupProperties()
        {
            var list = new List<IPropertyGroup>();
            list.AddRange(new[] { UrlProperty, NameProperty, VersionProperty, KindProperty });
            if (Previews != null)
            {
                for (var i = 0; i < Previews.Count; i++)
                {
                    var preview = Previews[i];
                    list.Add(preview.UrlProperty);
                }
            }

            return list.Where(_ => _ != null).ToArray();
        }

        public FilePropertyKind GetPropertyKind(IProperty property)
        {
            if (property == UrlProperty)
            {
                return FilePropertyKind.FileUrl;
            }

            if (Previews?.Any(p => p.UrlProperty == property) == true)
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