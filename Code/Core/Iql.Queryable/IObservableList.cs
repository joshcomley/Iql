using System.Collections;
using Iql.Queryable.Events;

namespace Iql.Queryable
{
    public interface IObservableList : IList
    {
        IEventSubscriber<IObservableListChangeEvent> Change { get; }
    }
}