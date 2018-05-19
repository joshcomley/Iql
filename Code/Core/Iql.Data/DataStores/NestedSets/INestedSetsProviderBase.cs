using System.Threading.Tasks;

namespace Iql.Data.DataStores.NestedSets
{
    public interface INestedSetsProviderBase
    {
        Task SlideDeleteAsync(object node);

        Task DeleteAsync(object node);

        Task MoveToAsync(object node, object newParent, NestedSetsNodeMoveKind kind);

        Task InsertAsChildOfAsync(object node, object parent = null);

        Task InsertToLeftOfAsync(object node, object sibling);

        Task InsertToRightOfAsync(object node, object sibling);
    }
}