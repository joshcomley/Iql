﻿using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Dates;

namespace Iql.Entities
{
    public abstract class MediaKeyBase : IMediaKey
    {
        protected IFile FileInternal { get; set; }
        public string Separator { get; set; } = "/";
        IFile IMediaKey.File => FileInternal;
        public IList<IMediaKeyGroup> Groups { get; } = new List<IMediaKeyGroup>();
        public string[][] Evaluate(object entity)
        {
            var groups = new List<string[]>();
            foreach (var group in Groups)
            {
                groups.Add(group.Evaluate(entity));
            }
            return groups.ToArray();
        }

        public string EvaluateToString(object entity)
        {
            return string.Join(Separator, Groups.Select(g => g.EvaluateToString(entity)));
        }

        public IMediaKey Clear()
        {
            ClearGroups();
            return this;
        }

        protected abstract void ClearGroups();
    }
}