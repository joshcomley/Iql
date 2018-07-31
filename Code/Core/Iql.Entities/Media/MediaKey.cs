using System;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public class MediaKey<T> : MediaKeyBase, IConfigurable<MediaKey<T>>
        where T : class
    {
        public IFileUrl<T> File
        {
            get => (IFileUrl<T>)FileInternal;
            set => FileInternal = value;
        }

        public MediaKey(IFileUrl<T> file)
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

        public MediaKey<T> Configure(Action<MediaKey<T>> configure)
        {
            if (configure != null)
            {
                configure(this);
            }

            return null;
        }
    }
}