using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Entities;

namespace Iql.Data.NestedSets
{
    public interface INestedSetManager
    {
        IEntityConfiguration EntityConfig { get; set; }
        bool HasLocation(object entity);
        IqlExpression GetFilter(object entity, NestedSetQueryKind kind);
        IDbQueryable GetQuery(object entity, IDataContext dataContext, NestedSetQueryKind kind);
    }
}