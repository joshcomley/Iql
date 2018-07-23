namespace Iql.Entities.NestedSets
{
    public class NestedSetProperty
    {
        public IProperty Property { get; }
        public INestedSet NestedSet { get; }
        public NestedSetPropertyKind Kind { get; } = NestedSetPropertyKind.None;

        public NestedSetProperty(IProperty property)
        {
            Property = property;
            if (property.EntityConfiguration != null && property.EntityConfiguration.NestedSets != null)
            {
                foreach (var nestedSet in property.EntityConfiguration.NestedSets)
                {
                    if (nestedSet.LeftProperty == Property) { Kind = NestedSetPropertyKind.Left; }
                    if (nestedSet.RightProperty == Property) { Kind = NestedSetPropertyKind.Right; }
                    if (nestedSet.LeftOfProperty == Property) { Kind = NestedSetPropertyKind.LeftOf; }
                    if (nestedSet.RightOfProperty == Property) { Kind = NestedSetPropertyKind.RightOf; }
                    if (nestedSet.KeyProperty == Property) { Kind = NestedSetPropertyKind.Key; }
                    if (nestedSet.LevelProperty == Property) { Kind = NestedSetPropertyKind.Level; }
                    if (nestedSet.ParentIdProperty == Property) { Kind = NestedSetPropertyKind.ParentId; }
                    if (nestedSet.ParentProperty == Property) { Kind = NestedSetPropertyKind.Parent; }
                    if (nestedSet.IdProperty == Property) { Kind = NestedSetPropertyKind.Id; }

                    if (Kind != NestedSetPropertyKind.None)
                    {
                        NestedSet = nestedSet;
                        break;
                    }
                }
            }
        }
    }
}