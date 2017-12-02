using System.Collections.Generic;

namespace Iql.Queryable
{
    public class RelatedList<TSource, T> : List<T>, IRelatedList
        where T : class
    {
        public TSource Owner { get; }

        object IRelatedList.Owner => Owner;

        public RelatedList(TSource owner, IEnumerable<T> source = null)
        {
            Owner = owner;
            this.Initialize(source);
        }
    }

    public interface IRelatedList
    {
        object Owner { get; }
    }
}