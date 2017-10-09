namespace Iql.Queryable.Data.EntityConfiguration
{
    public class KeyProperty<TProperty> : KeyPropertyBase
    {
        public KeyProperty(string name) : base(name, typeof(TProperty))
        {
        }
    }
}