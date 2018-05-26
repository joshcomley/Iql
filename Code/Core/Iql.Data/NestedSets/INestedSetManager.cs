using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Queryable;

namespace Iql.Data.NestedSets
{
    public enum NestedSetQueryKind
    {
        Descendents = 1,
        Children = 2,
        Parent = 3
    };
    public interface INestedSetManager
    {
        IEntityConfiguration EntityConfig { get; set; }
        bool HasLocation(object entity);
        IqlExpression GetFilter(object entity, NestedSetQueryKind kind);
        IDbQueryable GetQuery(object entity, IDataContext dataContext, NestedSetQueryKind kind);
    }
}