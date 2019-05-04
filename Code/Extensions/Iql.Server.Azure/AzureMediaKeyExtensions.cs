using System;
using System.Linq.Expressions;
using Iql.Entities;

namespace Iql.Server.Azure
{
    public static class AzureMediaKeyExtensions
    {
        public static MediaKey<T> ConfigureAzureMediaKey<T>(this MediaKey<T> mediaKey,
            Expression<Func<T, object>> containerExpression,
            Expression<Func<T, object>> blobExpression,
            string key = null)
            where T : class
        {
            return mediaKey.ConfigureAzureMediaKeyCustom(
                g => g.AddPropertyPath(containerExpression),
                    g => g.AddPropertyPath(blobExpression),
                key);
        }

        public static MediaKey<T> ConfigureAzureMediaKeyCustom<T>(this MediaKey<T> mediaKey,
            Action<MediaKeyGroup<T>> configureContainer,
            Action<MediaKeyGroup<T>> configureBlob,
            string key = null,
            bool appendUrlPropertyName = true)
            where T : class
        {
            return mediaKey
                .AddGroup(configureContainer)
                .AddGroup(g =>
                {
                    configureBlob(g);
                    if (appendUrlPropertyName)
                    {
                        g.AddString(key ?? mediaKey.File.UrlProperty.Name);
                    }
                });
        }
    }
}