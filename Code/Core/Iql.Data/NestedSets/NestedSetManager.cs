using Iql.Data.Context;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.NestedSets;

namespace Iql.Data.NestedSets
{
    public class NestedSetManager<T> : INestedSetManager
        where T : class
    {
        public INestedSet NestedSet { get; }

        public IEntityConfiguration EntityConfig
        {
            get => NestedSet.EntityConfiguration;
        }

        public NestedSetManager(INestedSet nestedSet)
        {
            NestedSet = nestedSet;
        }

        public bool HasLocation(T entity)
        {
            var left = NestedSet.LeftProperty.GetValue(entity);
            var right = NestedSet.RightProperty.GetValue(entity);
            return !Equals(left, 0) && !Equals(left, null) && !Equals(right, 0) && !Equals(right, null);
        }

        bool INestedSetManager.HasLocation(object entity)
        {
            return HasLocation((T)entity);
        }

        public DbQueryable<T> GetQuery(T entity, IDataContext dataContext, NestedSetQueryKind kind)
        {
            return ToQuery(dataContext, GetFilter(entity, kind));
        }

        private DbQueryable<T> ToQuery(IDataContext dataContext, IqlExpression filter)
        {
            var set = dataContext.GetDbSetByEntityType(EntityConfig.Type);
            var query = set.WhereEquals(filter
#if TypeScript
                    , null
#endif
            );
            return (DbQueryable<T>)query;
        }

        IDbQueryable INestedSetManager.GetQuery(object entity, IDataContext dataContext, NestedSetQueryKind kind)
        {
            return GetQuery((T)entity, dataContext, kind);
        }

        public IqlExpression GetFilter(T entity, NestedSetQueryKind kind)
        {
            IqlExpression filter = null;
            var keyCheck = new IqlIsEqualToExpression(
                IqlExpression.GetPropertyExpression(NestedSet.KeyProperty.Name),
                new IqlLiteralExpression(NestedSet.KeyProperty.GetValue(entity), NestedSet.KeyProperty.TypeDefinition.ToIqlType())
            );
            switch (kind)
            {
                case NestedSetQueryKind.Descendents:
                    {
                        var gt = new IqlIsGreaterThanExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.LeftProperty.Name),
                            new IqlLiteralExpression(NestedSet.LeftProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        var lt = new IqlIsLessThanExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.RightProperty.Name),
                            new IqlLiteralExpression(NestedSet.RightProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        filter = new IqlExpression[] { gt, lt, keyCheck }.And();
                    }
                    break;
                case NestedSetQueryKind.Children:
                    {
                        var parentIdCheck = new IqlIsEqualToExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.ParentIdProperty.Name),
                            new IqlLiteralExpression(NestedSet.IdProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        filter = new IqlExpression[] { keyCheck, parentIdCheck }.And();
                    }
                    break;
                case NestedSetQueryKind.Parent:
                    {
                        var parentIdCheck = new IqlIsEqualToExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.IdProperty.Name),
                            new IqlLiteralExpression(NestedSet.ParentIdProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        filter = new IqlExpression[] { keyCheck, parentIdCheck }.And();
                    }
                    break;
                case NestedSetQueryKind.Ancestors:
                    {
                        var leftCheck = new IqlIsLessThanExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.LeftProperty.Name),
                            new IqlLiteralExpression(NestedSet.LeftProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        var rightCheck = new IqlIsGreaterThanExpression(
                            IqlExpression.GetPropertyExpression(NestedSet.RightProperty.Name),
                            new IqlLiteralExpression(NestedSet.RightProperty.GetValue(entity), NestedSet.IdProperty.TypeDefinition.ToIqlType())
                        );
                        filter = new IqlExpression[] { leftCheck, rightCheck, keyCheck }.And();
                    }
                    break;
            }
            return filter;
        }

        IqlExpression INestedSetManager.GetFilter(object entity, NestedSetQueryKind kind)
        {
            return GetFilter((T)entity, kind);
        }
    }
}