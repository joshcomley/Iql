using System;
using System.Linq.Expressions;
using Iql.Entities;

namespace Iql.Server.Azure
{
    public static class AzureMediaKeyExtensions
    {
        public static MediaKey<T> ConfigureAzureMediaKey<T>(this MediaKey<T> mediaKey,
            Expression<Func<T, object>> parentExpression,
            Expression<Func<T, object>> blobExpression,
            string containerName = "files")
            where T : class
        {
            return mediaKey.ConfigureAzureMediaKeyCustom(
                g => g.AddPropertyPath(parentExpression),
                g => g.AddPropertyPath(blobExpression),
                g => g.AddString(containerName)
                );
        }

        public static MediaKey<T> ConfigureAzureMediaKey<T>(this MediaKey<T> mediaKey,
            Expression<Func<T, object>> parentExpression,
            Expression<Func<T, object>> blobExpression,
            Expression<Func<T, object>> containerExpression)
            where T : class
        {
            return mediaKey.ConfigureAzureMediaKeyCustom(
                g => g.AddPropertyPath(parentExpression),
                g => g.AddPropertyPath(blobExpression),
                g => g.AddPropertyPath(containerExpression)
                );
        }

        public static MediaKey<T> ConfigureAzureMediaKeyCustom<T>(this MediaKey<T> mediaKey,
            Action<MediaKeyGroup<T>> configureParent,
            Action<MediaKeyGroup<T>> configureBlob,
            string containerName = "files")
            where T : class
        {
            return mediaKey.ConfigureAzureMediaKeyCustom(
                configureParent,
                configureBlob,
                _ => _.AddString(containerName)
            );
        }

        public static MediaKey<T> ConfigureAzureMediaKeyCustom<T>(
            this MediaKey<T> mediaKey,
            Action<MediaKeyGroup<T>> configureParent,
            Action<MediaKeyGroup<T>> configureBlob,
            Action<MediaKeyGroup<T>> configureContainer
            )
            where T : class
        {
            return mediaKey
                .AddGroup(configureContainer)
                .AddGroup(g =>
                {
                    configureParent(g);
                    g.AddString("___ID___");
                    configureBlob(g);
                    g.AddString($"___FILE___");
                    g.AddString(mediaKey.File.Guid.ToString());
                });
        }
    }
}