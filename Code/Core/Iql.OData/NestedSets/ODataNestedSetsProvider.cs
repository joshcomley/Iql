using System;
using System.Threading.Tasks;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Http;

namespace Iql.OData.NestedSets
{
    public class ODataNestedSetsProvider<T> : NestedSetsProvider<T>
    {
        public IHttpProvider HttpProvider { get; }

        public ODataNestedSetsProvider(IHttpProvider httpProvider)
        {
            HttpProvider = httpProvider;
        }

        public override Task SlideDeleteAsync(T node)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(T node)
        {
            throw new NotImplementedException();
        }

        public override Task MoveToAsync(T node, T newParent, NestedSetsNodeMoveKind kind)
        {
            throw new NotImplementedException();
        }

        public override Task InsertAsChildOfAsync(T node, T parent = default(T))
        {
            throw new NotImplementedException();
        }

        public override Task InsertToLeftOfAsync(T node, T sibling)
        {
            throw new NotImplementedException();
        }

        public override Task InsertToRightOfAsync(T node, T sibling)
        {
            throw new NotImplementedException();
        }
    }
}