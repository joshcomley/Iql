using System.Linq;

namespace Iql.Entities.NestedSets
{
    public class NestedSet : PropertyGroupBase<INestedSet>, INestedSet
    {
        public override PropertyKind Kind
        {
            get => PropertyKind.SimpleCollection;
            set { }
        }
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