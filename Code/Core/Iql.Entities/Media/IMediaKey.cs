using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities
{
    public abstract class MediaKeyBase : IMediaKey
    {
        protected IProperty PropertyInternal { get; set; }
        IProperty IMediaKey.Property => PropertyInternal;
        public List<MediaKeyPart> Parts { get; set; } = new List<MediaKeyPart>();
        public string[] Evaluate(object entity)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var parts = new List<string>();

            foreach (var keyPart in Parts)
            {
                if (keyPart.IsPropertyPath)
                {
                    var propertyPath = IqlPropertyPath.FromString(keyPart.Key, PropertyInternal.EntityConfiguration);
                    parts.Add((propertyPath.Evaluate(entity) ?? "").ToString());
                }
                else
                {
                    parts.Add(keyPart.Key);
                }
            }

            return parts.ToArray();
        }
    }
    public class MediaKey<T> : MediaKeyBase
        where T : class
    {
        public IEntityProperty<T> Property
        {
            get => (IEntityProperty<T>) PropertyInternal;
            set => PropertyInternal = value;
        }

        public MediaKey(IEntityProperty<T> property)
        {
            Property = property;
        }

        public MediaKey<T> AddPropertyPath(Expression<Func<T, object>> property)
        {
            var path = IqlPropertyPath.FromLambda(property, (EntityConfiguration<T>)Property.EntityConfiguration);
            Parts.Add(new MediaKeyPart
            {
                IsPropertyPath = true,
                Key = path.PathToHere
            });
            return this;
        }

        public MediaKey<T> Clear()
        {
            Parts.Clear();
            return this;
        }

        public MediaKey<T> AddString(string key)
        {
            Parts.Add(new MediaKeyPart
            {
                IsPropertyPath = false,
                Key = key
            });
            return this;
        }
    }

    public class MediaKeyPart
    {
        public bool IsPropertyPath { get; set; }
        public string Key { get; set; }
    }

    public interface IMediaKey
    {
        IProperty Property { get; }
        List<MediaKeyPart> Parts { get; set; }
        string[] Evaluate(object entity);
    }
}