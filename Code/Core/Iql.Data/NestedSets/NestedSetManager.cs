using System.Linq;
using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Data.Queryable;
using Iql.Entities;

namespace Iql.Data.NestedSets
{
    public class NestedSetManager<T> : INestedSetManager
        where T : class
    {
        private IEntityConfiguration _entityConfig;

        public IEntityConfiguration EntityConfig
        {
            get => _entityConfig;
            set
            {
                _entityConfig = value;
                if (_entityConfig != null)
                {
                    IdProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetId).FirstOrDefault();
                    ParentIdProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetParentId).FirstOrDefault();
                    ParentProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetParent).FirstOrDefault();
                    LeftProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetLeft).FirstOrDefault();
                    RightProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetRight).FirstOrDefault();
                    LeftOfProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetLeftOf).FirstOrDefault();
                    RightOfProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetRightOf).FirstOrDefault();
                    KeyProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetKey).FirstOrDefault();
                    LevelProperty = EntityConfig.FindPropertiesByHint(KnownHints.NestedSetLevel).FirstOrDefault();
                }
            }
        }

        public IProperty RightOfProperty { get; set; }

        public IProperty LeftOfProperty { get; set; }

        public IProperty LevelProperty { get; set; }

        public IProperty IdProperty { get; set; }

        public IProperty ParentProperty { get; set; }

        public IProperty ParentIdProperty { get; set; }

        public IProperty KeyProperty { get; set; }

        public IProperty RightProperty { get; set; }

        public IProperty LeftProperty { get; set; }

        public NestedSetManager(IEntityConfiguration entityConfig = null)
        {
            EntityConfig = entityConfig ?? EntityConfigurationBuilder.FindConfigurationForEntityType(typeof(T));
        }

        public bool HasLocation(T entity)
        {
            var left = LeftProperty.PropertyGetter(entity);
            var right = RightProperty.PropertyGetter(entity);
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
            if (EntityConfig.HasHint(KnownHints.NestedSet))
            {
                switch (kind)
                {
                    case NestedSetQueryKind.Descendents:
                        {
                            var gt = new IqlIsGreaterThanExpression(
                                IqlExpression.GetPropertyExpression(LeftProperty.Name),
                                new IqlLiteralExpression(LeftProperty.PropertyGetter(entity), IqlType.Integer)
                            );
                            var lt = new IqlIsLessThanExpression(
                                IqlExpression.GetPropertyExpression(RightProperty.Name),
                                new IqlLiteralExpression(RightProperty.PropertyGetter(entity), IqlType.Integer)
                            );
                            var key = new IqlIsEqualToExpression(
                                IqlExpression.GetPropertyExpression(KeyProperty.Name),
                                new IqlLiteralExpression(KeyProperty.PropertyGetter(entity), IqlType.String)
                            );
                            filter = new IqlExpression[] { gt, lt, key }.And();
                        }
                        break;
                    case NestedSetQueryKind.Children:
                        {
                            var keyCheck = new IqlIsEqualToExpression(
                                IqlExpression.GetPropertyExpression(KeyProperty.Name),
                                new IqlLiteralExpression(KeyProperty.PropertyGetter(entity), IqlType.String)
                            );
                            var parentIdCheck = new IqlIsEqualToExpression(
                                IqlExpression.GetPropertyExpression(ParentIdProperty.Name),
                                new IqlLiteralExpression(IdProperty.PropertyGetter(entity), IqlType.Integer)
                            );
                            filter = new IqlExpression[] { keyCheck, parentIdCheck }.And();
                        }
                        break;
                    case NestedSetQueryKind.Parent:
                        {
                            var keyCheck = new IqlIsEqualToExpression(
                                IqlExpression.GetPropertyExpression(KeyProperty.Name),
                                new IqlLiteralExpression(KeyProperty.PropertyGetter(entity), IqlType.String)
                            );
                            var parentIdCheck = new IqlIsEqualToExpression(
                                IqlExpression.GetPropertyExpression(IdProperty.Name),
                                new IqlLiteralExpression(ParentIdProperty.PropertyGetter(entity), IqlType.Integer)
                            );
                            filter = new IqlExpression[] { keyCheck, parentIdCheck }.And();
                        }
                        break;
                }
            }
            return filter;
        }

        IqlExpression INestedSetManager.GetFilter(object entity, NestedSetQueryKind kind)
        {
            return GetFilter((T)entity, kind);
        }
    }
}