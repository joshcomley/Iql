using System.Threading.Tasks;

namespace Iql.Queryable.Data.DataStores.NestedSets
{
    public interface INestedSetsProvider<in TEntity> : INestedSetsProviderBase
    {
        Task SlideDeleteAsync(TEntity node);

        Task DeleteAsync(TEntity node);

        Task MoveToAsync(TEntity node, TEntity newParent, NestedSetsNodeMoveKind kind);

        Task InsertAsChildOfAsync(TEntity node, TEntity parent = default(TEntity));

        Task InsertToLeftOfAsync(TEntity node, TEntity sibling);

        Task InsertToRightOfAsync(TEntity node, TEntity sibling);
    }
}