using System.Collections;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Lists
{
    public interface IObservableList : IList
    {
        IEventSubscriber<IObservableListChangeEvent> Change { get; }
    }
}