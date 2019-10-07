using System;

namespace Iql.Entities.PropertyGroups.Files
{
    public class FilePreviewBase : MetadataBase, IFilePreview
    {
        public IFileState TryGetFileState(object entity)
        {
            return FileState.TryGetFileState(StatePropertyInternal, entity);
        }

        public bool TrySetFileState(object entity, IFileState state)
        {
            return FileState.TrySetFileState(StatePropertyInternal, entity, state);
        }

        private Guid _guid;

        IProperty IFileUrlBase.UrlProperty
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }
        protected IProperty UrlPropertyInternal { get; set; }

        IProperty IFileUrlBase.StateProperty
        {
            get => StatePropertyInternal;
            set => StatePropertyInternal = value;
        }
        protected IProperty StatePropertyInternal { get; set; }

        IMediaKey IFileUrlBase.MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }

        public Guid Guid
        {
            get { return _guid; }
            set
            {
                EntityConfiguration?.Builder.GuidManager.Assert(value, this);
                _guid = value;
            }
        }

        IFile IFilePreview.File
        {
            get => FileInternal;
            set => FileInternal = value;
        }

        protected IFile FileInternal { get; set; }

        protected IMediaKey MediaKeyInternal { get; set; }

        protected IEntityConfiguration EntityConfigurationInternal => UrlPropertyInternal?.EntityConfiguration;

        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public string Key { get; set; }
        public IqlPreviewKind Kind { get; set; }

        public FilePreviewBase(Guid guid, IFile file = null, IProperty urlProperty = null, int? maxWidth = null, int? maxHeight = null, string key = null)
        {
            FileInternal = file;
            UrlPropertyInternal = urlProperty;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Key = key;
            Guid = guid;
        }

        public IEntityConfiguration EntityConfiguration => UrlPropertyInternal?.EntityConfiguration;
        public override IUserPermission ParentPermissions => EntityConfiguration;
        public IFile RootFile => FileInternal;
    }
}