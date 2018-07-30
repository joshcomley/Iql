using System;
using Iql.Entities.Dates;

namespace Iql.Entities
{
    public class MediaKey<T> : MediaKeyBase
        where T : class
    {
        public File<T> File
        {
            get => (File<T>)FileInternal;
            set => FileInternal = value;
        }

        public MediaKey(File<T> file)
        {
            File = file;
        }

        public MediaKey<T> AddGroup(Action<MediaKeyGroup<T>> configureGroup)
        {
            var groupPart = new MediaKeyGroup<T>
            {
                MediaKey = this
            };
            configureGroup(groupPart);
            Groups.Add(groupPart);
            return this;
        }

        public new MediaKey<T> Clear()
        {
            ClearGroups();
            return this;
        }

        protected override void ClearGroups()
        {
            Groups.Clear();
        }
    }
}