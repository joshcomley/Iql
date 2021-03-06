﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;

namespace Iql.Data.Extensions
{
    public class MediaKeyEvaluationSession : IEvaluationSessionContainer
    {
        public MediaKeyEvaluationSession(IEvaluationSessionContainer evaluationSession = null)
        {
            Session = evaluationSession?.Session ?? new EvaluationSession();
        }

        public IEvaluationSession Session { get; set; }

        public async Task<string[]> EvaluateGroupAsync(IMediaKeyGroup mediaGroup, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath

            var parts = new List<string>();

            foreach (var keyPart in mediaGroup.Parts)
            {
                if (keyPart.IsPropertyPath)
                {
                    var propertyPath = IqlPropertyPath.FromString(
                        mediaGroup.MediaKey.File.EntityConfiguration.Builder,
                        keyPart.Key,
                        mediaGroup.MediaKey.File.EntityConfiguration.TypeMetadata
                    );
                    var iqlPropertyPathEvaluationResult = await Session.EvaluateAsync(
                        propertyPath,
                        entity,
                        dataContext,
                        false);
                    parts.Add((iqlPropertyPathEvaluationResult.Value ?? "").ToString());
                }
                else
                {
                    parts.Add(keyPart.Key);
                }
            }

            return parts.ToArray();
        }

        public async Task<string> EvaluateGroupToStringAsync(IMediaKeyGroup mediaGroup, object entity, IDataContext dataContext)
        {
            var parts = await EvaluateGroupAsync(mediaGroup, entity, dataContext);
            return string.Join(mediaGroup.Separator, parts);
        }

        public async Task<string[][]> EvaluateAsync(IMediaKey mediaKey, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var groups = new List<string[]>();

            foreach (var keyGroup in mediaKey.Groups)
            {
                var parts = await EvaluateGroupAsync(keyGroup, entity, dataContext);
                groups.Add(parts);
            }

            return groups.ToArray();
        }

        public async Task<string> EvaluateToStringAsync(IMediaKey mediaKey, object entity, IDataContext dataContext)
        {
            // TODO: Add evaluation with data context to IqlPropertyPath
            var groups = new List<string>();

            foreach (var keyGroup in mediaKey.Groups)
            {
                var groupString = await EvaluateGroupToStringAsync(keyGroup, entity, dataContext);
                groups.Add(groupString);
            }

            return string.Join(mediaKey.Separator, groups);
        }
    }
}