﻿namespace Iql.Entities.PropertyGroups.Files
{
    public class FilePreviewBase : MetadataBase, IFilePreview
    {
        IProperty IFileUrlBase.UrlProperty
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }
        protected IProperty UrlPropertyInternal { get; set; }

        IMediaKey IFileUrlBase.MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
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

        public FilePreviewBase(IFile file = null, IProperty urlProperty = null, int? maxWidth = null, int? maxHeight = null, string key = null)
        {
            FileInternal = file;
            UrlPropertyInternal = urlProperty;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Key = key;
        }

        public IEntityConfiguration EntityConfiguration => UrlPropertyInternal?.EntityConfiguration;
        public IFile RootFile => FileInternal;
    }
}