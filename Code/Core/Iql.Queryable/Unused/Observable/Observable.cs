using System;

namespace Iql.Queryable.Unused.Observable
{
    public class Observable<T> : IObservable
    {
        public Observable(Action<Subscriber<T>> resolver)
        {
        }

        void IObservable.Subscribe(Action<object> s = null, Action<object> error = null)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Action<T> s = null, Action<object> error = null)
        {
        }

        public Observable<TResult> Map<TResult>(Func<T, TResult> action)
        {
            return null;
        }
    }
}