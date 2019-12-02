using System.Collections.Generic;
using System.Linq;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public abstract class MediaKeyBase : IMediaKey
    {
        protected IFileUrlBase FileInternal { get; set; }
        public string Separator { get; set; } = "/";
        IFileUrlBase IMediaKey.File => FileInternal;
        private IList<IMediaKeyGroup> _groups;
        public IList<IMediaKeyGroup> Groups => _groups = _groups ?? new List<IMediaKeyGroup>();
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