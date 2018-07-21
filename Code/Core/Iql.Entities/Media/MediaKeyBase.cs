using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public abstract class MediaKeyBase : IMediaKey
    {
        protected IProperty PropertyInternal { get; set; }
        public string Separator { get; set; } = "/";
        IProperty IMediaKey.Property => PropertyInternal;
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