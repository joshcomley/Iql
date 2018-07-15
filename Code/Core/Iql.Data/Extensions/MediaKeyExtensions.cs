using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Extensions
{
    public static class MediaKeyExtensions
    {
        public static async Task<string[]> EvaluateAsync(this IMediaKey mediaKey, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var parts = new List<string>();

            foreach (var keyPart in mediaKey.Parts)
            {
                if (keyPart.IsPropertyPath)
                {
                    var propertyPath = IqlPropertyPath.FromString(keyPart.Key, mediaKey.Property.EntityConfiguration);
                    parts.Add((await propertyPath.EvaluateAsync(entity, dataContext) ?? "").ToString());
                }
                else
                {
                    parts.Add(keyPart.Key);
                }
            }

            return parts.ToArray();
        }
    }
}