using System;
using System.Linq.Expressions;

namespace Iql.Entities.PropertyGroups.Files
{
    public class File<T> : FileBase, IFileUrl<T>
        where T : class
    {
        public new EntityConfiguration<T> EntityConfiguration => EntityConfigurationInternal as EntityConfiguration<T>;
        public MediaKey<T> MediaKey
        {
            get => (MediaKey<T>)MediaKeyInternal;
            set => MediaKeyInternal = value;
        }

        public File(
            Guid guid,
            IProperty urlProperty = null,
            IProperty nameProperty = null,
            IProperty versionProperty = null,
            IProperty kindProperty = null,
            string key = null) : base(
            guid,
            urlProperty,
            nameProperty,
            versionProperty,
            kindProperty,
            key)
        {
            UrlPropertyInternal = urlProperty;
            NameProperty = nameProperty;
            VersionProperty = versionProperty;
            KindProperty = kindProperty;
            MediaKey = new MediaKey<T>(this);
        }

        public File<T> SetUrlProperty(Expression<Func<T, object>> property)
        {
            UrlPropertyInternal = EntityConfiguration.FindNestedPropertyByExpression(property);
            return this;
        }

        public File<T> SetNameProperty(Expression<Func<T, object>> property)
        {
            NameProperty = EntityConfiguration.FindNestedPropertyByExpression(property);
            return this;
        }

        public File<T> SetVersionProperty(Expression<Func<T, object>> property)
        {
            VersionProperty = EntityConfiguration.FindNestedPropertyByExpression(property);
            return this;
        }

        public File<T> SetKindProperty(Expression<Func<T, object>> property)
        {
            KindProperty = EntityConfiguration.FindNestedPropertyByExpression(property);
            return this;
        }

        public File<T> AddPreview(
            Expression<Func<T, object>> property, 
            Guid guid,
            IqlPreviewKind kind = IqlPreviewKind.Image,
            int? maxWidth = null, 
            int? maxHeight = null, 
            string key = null,
            Action<FilePreview<T>> configure = null)
        {
            var propertyResolved = EntityConfiguration.FindPropertyByExpression(property);
            var filePreview = new FilePreview<T>(guid, kind, this, propertyResolved, maxWidth, maxHeight, key);
            Previews.Add(filePreview);
            if (configure != null)
            {
                configure(filePreview);
            }
            return this;
        }

        public new IEntityProperty<T> UrlProperty
        {
            get => (IEntityProperty<T>) UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

        public new File<T> RootFile => (File<T>) RootFileInternal;

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(UrlProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(KindProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(NameProperty, PropertySearchKind.Primary),
                new PropertyGroupMetadata(VersionProperty, PropertySearchKind.None),
            };
        }
    }
}