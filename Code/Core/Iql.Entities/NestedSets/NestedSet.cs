using System.Linq;

namespace Iql.Entities.NestedSets
{
    public class NestedSet : SimplePropertyGroupBase<INestedSet>, INestedSet
    {
        public override IProperty PrimaryProperty
        {
            get { return IdProperty; }
        }

        protected override string ResolveName()
        {
            return "Hierarchy";
        }

        public override IqlPropertyKind Kind
        {
            get => IqlPropertyKind.SimpleCollection;
            set { }
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.NestedSet;

        public override IEntityConfiguration EntityConfiguration =>
            (LeftProperty ?? RightProperty ?? LeftOfProperty ?? RightOfProperty ?? KeyProperty ?? LevelProperty ?? ParentIdProperty ?? ParentProperty ?? IdProperty)?.EntityConfiguration;
        public string SetKey { get; set; }
        public IProperty LeftProperty { get; set; }
        public IProperty RightProperty { get; set; }
        public IProperty LeftOfProperty { get; set; }
        public IProperty RightOfProperty { get; set; }
        public IProperty KeyProperty { get; set; }
        public IProperty LevelProperty { get; set; }
        public IProperty ParentIdProperty { get; set; }
        public IProperty ParentProperty { get; set; }
        public IProperty IdProperty { get; set; }

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(IdProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(LeftProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(RightProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(LeftOfProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(RightOfProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(KeyProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(LevelProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(ParentIdProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(ParentProperty, IqlPropertySearchKind.None),
            };
        }

        public NestedSetPropertyKind GetPropertyKind(IProperty property)
        {
            if (LeftProperty == property) { return NestedSetPropertyKind.Left; }
            if (RightProperty == property) { return NestedSetPropertyKind.Right; }
            if (LeftOfProperty == property) { return NestedSetPropertyKind.LeftOf; }
            if (RightOfProperty == property) { return NestedSetPropertyKind.RightOf; }
            if (KeyProperty == property) { return NestedSetPropertyKind.Key; }
            if (LevelProperty == property) { return NestedSetPropertyKind.Level; }
            if (ParentIdProperty == property) { return NestedSetPropertyKind.ParentId; }
            if (ParentProperty == property) { return NestedSetPropertyKind.Parent; }
            if (IdProperty == property) { return NestedSetPropertyKind.Id; }

            return NestedSetPropertyKind.None;
        }

        public NestedSet(IProperty leftProperty = null,
            IProperty rightProperty = null,
            IProperty leftOfProperty = null,
            IProperty rightOfProperty = null,
            IProperty keyProperty = null,
            IProperty levelProperty = null,
            IProperty parentIdProperty = null,
            IProperty parentProperty = null,
            IProperty idProperty = null,
            string setKey = null,
            string key = null) : base(null, key)
        {
            SetKey = setKey;
            LeftProperty = leftProperty;
            RightProperty = rightProperty;
            LeftOfProperty = leftOfProperty;
            RightOfProperty = rightOfProperty;
            KeyProperty = keyProperty;
            LevelProperty = levelProperty;
            ParentIdProperty = parentIdProperty;
            ParentProperty = parentProperty;
            IdProperty = idProperty;
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[]
                {
                    LeftProperty,
                    RightProperty,
                    LeftOfProperty,
                    RightOfProperty,
                    KeyProperty,
                    LevelProperty,
                    ParentIdProperty,
                    ParentProperty,
                    IdProperty
                }.Where(p => p != null)
                .ToArray();
        }
    }
}