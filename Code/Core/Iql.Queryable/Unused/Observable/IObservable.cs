using System;

namespace Iql.Queryable.Unused.Observable
{
    public interface IObservable
    {
        void Subscribe(Action<object> s = null, Action<object> error = null);
    }
}