using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Extensions
{
    public static class MediaKeyExtensions
    {
        public static async Task<string[][]> EvaluateAsync(this IMediaKey mediaKey, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var groups = new List<string[]>();

            foreach (var keyGroup in mediaKey.Groups)
            {
                var parts = await keyGroup.EvaluateAsync(entity, dataContext);
                groups.Add(parts);
            }

            return groups.ToArray();
        }

        public static async Task<string> EvaluateToStringAsync(this IMediaKey mediaKey, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var groups = new List<string>();

            foreach (var keyGroup in mediaKey.Groups)
            {
                var groupString = await keyGroup.EvaluateToStringAsync(entity, dataContext);
                groups.Add(groupString);
            }

            return string.Join(mediaKey.Separator, groups);
        }
    }
}