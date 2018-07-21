using System;
using System.Collections.Generic;

namespace Iql.Entities
{
    public class MediaKey<T> : MediaKeyBase
        where T : class
    {
        public IEntityProperty<T> Property
        {
            get => (IEntityProperty<T>)PropertyInternal;
            set => PropertyInternal = value;
        }

        public MediaKey(IEntityProperty<T> property)
        {
            Property = property;
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