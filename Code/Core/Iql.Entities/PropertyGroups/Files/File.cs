namespace Iql.Entities.Dates
{
    public class File<T> : FileBase
        where T : class
    {
        public MediaKey<T> MediaKey
        {
            get => (MediaKey<T>)MediaKeyInternal;
            set => MediaKeyInternal = value;
        }

        public File(
            IProperty fileUrlProperty = null,
            IProperty previewUrlProperty = null,
            IProperty nameProperty = null,
            IProperty versionProperty = null,
            IProperty kindProperty = null,
            string key = null) : base(
            fileUrlProperty,
            previewUrlProperty,
            nameProperty,
            versionProperty,
            kindProperty,
            key)
        {
            FileUrlProperty = fileUrlProperty;
            PreviewUrlProperty = previewUrlProperty;
            NameProperty = nameProperty;
            VersionProperty = versionProperty;
            KindProperty = kindProperty;
        }
    }
}