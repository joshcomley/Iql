using System.Threading.Tasks;

namespace Iql.Data.DataStores.NestedSets
{
    public abstract class NestedSetsProvider<T> : INestedSetsProvider<T>
    {
        public abstract Task SlideDeleteAsync(T node);
        public abstract Task DeleteAsync(T node);
        public abstract Task MoveToAsync(T node, T newParent, NestedSetsNodeMoveKind kind);
        public abstract Task InsertAsChildOfAsync(T node, T parent = default(T));
        public abstract Task InsertToLeftOfAsync(T node, T sibling);
        public abstract Task InsertToRightOfAsync(T node, T sibling);

        Task INestedSetsProviderBase.SlideDeleteAsync(object node)
        {
            return SlideDeleteAsync((T)node);
        }

        Task INestedSetsProviderBase.DeleteAsync(object node)
        {
            return DeleteAsync((T)node);
        }

        Task INestedSetsProviderBase.MoveToAsync(object node, object newParent, NestedSetsNodeMoveKind kind)
        {
            return MoveToAsync((T)node, (T)newParent, kind);
        }

        Task INestedSetsProviderBase.InsertAsChildOfAsync(object node, object parent = null)
        {
            return InsertAsChildOfAsync((T)node, (T)parent);
        }

        Task INestedSetsProviderBase.InsertToLeftOfAsync(object node, object sibling)
        {
            return InsertToLeftOfAsync((T)node, (T)sibling);
        }

        Task INestedSetsProviderBase.InsertToRightOfAsync(object node, object sibling)
        {
            return InsertToRightOfAsync((T)node, (T)sibling);
        }
    }
}