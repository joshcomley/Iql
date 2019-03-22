using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class CustomReportsDefinition : SpecialTypeDefinition
    {
        private PropertyMap[] _propertyMaps;
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

        public override PropertyMap[] PropertyMaps =>
            _propertyMaps =
                _propertyMaps ??
                new[]
                {
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Id), IdProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.UserId), UserIdProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Name), NameProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.EntityType), EntityTypeProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Iql), IqlProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Fields), FieldsProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Sort), SortProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.SortDescending),
                        SortDescendingProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlCustomReport.Search), SearchProperty),
                };

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(IdProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(UserIdProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(NameProperty, PropertySearchKind.Primary),
                new PropertyGroupMetadata(EntityTypeProperty, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(IqlProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(FieldsProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(SortProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(SortDescendingProperty, PropertySearchKind.None),
            };
        }

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { NameProperty, EntityTypeProperty, IqlProperty, FieldsProperty };
        }
    }
}