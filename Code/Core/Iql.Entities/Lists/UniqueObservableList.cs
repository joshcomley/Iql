namespace Iql.Entities.Lists
{
    public sealed class UniqueObservableList<T> : ObservableList<T>
    {
        public override bool EnsureUnique
        {
            get => true;
            set { }
        }
    }
}