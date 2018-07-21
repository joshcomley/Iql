using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Extensions
{
    public static class MediaKeyGroupExtensions
    {
        public static async Task<string[]> EvaluateAsync(this IMediaKeyGroup mediaGroup, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath

            var parts = new List<string>();

            foreach (var keyPart in mediaGroup.Parts)
            {
                if (keyPart.IsPropertyPath)
                {
                    var propertyPath = IqlPropertyPath.FromString(keyPart.Key, mediaGroup.MediaKey.Property.EntityConfiguration);
                    parts.Add((await propertyPath.EvaluateAsync(entity, dataContext) ?? "").ToString());
                }
                else
                {
                    parts.Add(keyPart.Key);
                }
            }

            return parts.ToArray();
        }

        public static async Task<string> EvaluateToStringAsync(this IMediaKeyGroup mediaGroup, object entity, IDataContext dataContext)
        {
            var parts = await mediaGroup.EvaluateAsync(entity, dataContext);
            return string.Join(mediaGroup.Separator, parts);
        }
    }
}