using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class CustomReportsDefinition : SpecialTypeDefinition
    {
        public override Type InternalType => typeof(IqlCustomReport);

        public IProperty UserIdProperty { get; set; }
        public IProperty NameProperty { get; set; }
        public IProperty EntityTypeProperty { get; set; }
        public IProperty IqlProperty { get; set; }
        public IProperty FieldsProperty { get; set; }
        public IProperty SortProperty { get; set; }
        public IProperty SortDescendingProperty { get; set; }
        public IProperty SearchProperty { get; set; }

        public CustomReportsDefinition(IProperty idProperty, IProperty userIdProperty, IProperty nameProperty, IProperty entityTypeProperty, IProperty iqlProperty, IProperty fieldsProperty, IProperty sortProperty, IProperty sortDescendingProperty, IProperty searchProperty) : base(idProperty)
        {
            UserIdProperty = userIdProperty;
            NameProperty = nameProperty;
            EntityTypeProperty = entityTypeProperty;
            IqlProperty = iqlProperty;
            FieldsProperty = fieldsProperty;
            SortProperty = sortProperty;
            SortDescendingProperty = sortDescendingProperty;
            SearchProperty = searchProperty;
        }

        public static CustomReportsDefinition Define<T>(
            EntityConfiguration<T> entityConfiguration,
            Expression<Func<T, object>> idProperty,
            Expression<Func<T, object>> userIdProperty,
            Expression<Func<T, object>> nameProperty,
            Expression<Func<T, object>> entityTypeProperty,
            Expression<Func<T, object>> iqlProperty,
            Expression<Func<T, object>> fieldsProperty,
            Expression<Func<T, object>> sortProperty,
            Expression<Func<T, object>> sortDescendingProperty,
            Expression<Func<T, object>> searchProperty)
            where T : class
        {
            var definition = new CustomReportsDefinition(
                entityConfiguration.FindNestedPropertyByLambdaExpression(idProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(userIdProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(nameProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(entityTypeProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(iqlProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(fieldsProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(sortProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(sortDescendingProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(searchProperty));
            entityConfiguration.SpecialTypeDefinition = definition;
            var entityType = entityConfiguration.Builder.EntityType<IqlCustomReport>();
            entityType.ManageKind = EntityManageKind.None;
            entityType.SpecialTypeDefinition = definition;
            return definition;
        }

        public override IProperty ResolvePropertyMap(string internalPropertyName)
        {
            switch (internalPropertyName)
            {
                case nameof(IqlCustomReport.Id):
                    return IdProperty;
                case nameof(IqlCustomReport.UserId):
                    return UserIdProperty;
                case nameof(IqlCustomReport.Name):
                    return NameProperty;
                case nameof(IqlCustomReport.EntityType):
                    return EntityTypeProperty;
                case nameof(IqlCustomReport.Iql):
                    return IqlProperty;
                case nameof(IqlCustomReport.Fields):
                    return FieldsProperty;
                case nameof(IqlCustomReport.Sort):
                    return SortProperty;
                case nameof(IqlCustomReport.SortDescending):
                    return SortDescendingProperty;
                case nameof(IqlCustomReport.Search):
                    return SearchProperty;
            }
            return null;
        }

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { NameProperty, EntityTypeProperty, IqlProperty, FieldsProperty };
        }
    }
}