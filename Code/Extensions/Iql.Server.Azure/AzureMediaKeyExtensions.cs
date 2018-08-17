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
            return mediaKey
                .AddGroup(g => g.AddPropertyPath(containerExpression))
                .AddGroup(g => g.AddPropertyPath(blobExpression).AddString(key ?? mediaKey.File.UrlProperty.Name));
        }
    }
}