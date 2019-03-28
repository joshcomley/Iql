using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities
{
    public class MediaKeyGroup<T> : IMediaKeyGroup
        where T : class
    {
        public MediaKey<T> MediaKey { get; internal set; }

        IMediaKey IMediaKeyGroup.MediaKey => MediaKey;

        public List<IMediaKeyPart> Parts { get; set; } = new List<IMediaKeyPart>();

        public string[] Evaluate(object entity)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var parts = new List<string>();

            foreach (var keyPart in Parts)
            {
                if (keyPart.IsPropertyPath)
                {
                    var propertyPath = IqlPropertyPath.FromString(keyPart.Key, MediaKey.File.EntityConfiguration.TypeMetadata);
                    parts.Add((propertyPath.Evaluate(entity) ?? "").ToString());
                }
                else
                {
                    parts.Add(keyPart.Key);
                }
            }

            return parts.ToArray();
        }

        public string Separator { get; set; } = "-";

        public string EvaluateToString(object entity)
        {
            return string.Join(Separator, Evaluate(entity));
        }

        public MediaKeyGroup<T> AddPropertyPath(Expression<Func<T, object>> property)
        {
            var path = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(property, MediaKey.File.EntityConfiguration.Builder, typeof(T));
            //IqlPropertyPath.FromLambda(property, (EntityConfiguration<T>)Property.EntityConfiguration);
            var propertyPath = new List<string>();
            var lambda = path.Expression as IqlLambdaExpression;
            var iql = lambda.Body;
            while (iql.Parent != null && iql.Kind == IqlExpressionKind.Property)
            {
                propertyPath.Add((iql as IqlPropertyExpression).PropertyName);
                iql = iql.Parent;
            }

            var propertyPathReversed = new List<string>();
            for (var i = propertyPath.Count - 1; i >= 0; i--)
            {
                propertyPathReversed.Add(propertyPath[i]);
            }

            Parts.Add(new MediaKeyPart
            {
                MediaKey = MediaKey,
                IsPropertyPath = true,
                Key = string.Join("/", propertyPathReversed)
            });
            return this;
        }

        public MediaKeyGroup<T> AddString(string key)
        {
            Parts.Add(new MediaKeyPart
            {
                MediaKey = MediaKey,
                IsPropertyPath = false,
                Key = key
            });
            return this;
        }

        public MediaKeyGroup<T> Clear()
        {
            Parts.Clear();
            return this;
        }
    }
}