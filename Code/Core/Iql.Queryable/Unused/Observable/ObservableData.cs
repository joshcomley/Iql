namespace Iql.Queryable.Unused.Observable
{
    public class ObservableData<T>
    {
        private readonly Subscriber<T> _resolve;
        private T _data;
        private bool _dataHasBeenSet;

        public ObservableData(Observable<T> observable, Subscriber<T> resolve)
        {
            Observable = observable;
            _resolve = resolve;
        }

        public Observable<T> Observable { get; set; }

        public T GetData()
        {
            return _data;
        }

        public void SetData(T obj)
        {
            _dataHasBeenSet = true;
            _data = obj;
            _resolve.Next(obj);
        }

        public bool DataHasBeenSet()
        {
            return _dataHasBeenSet;
        }
    }
}