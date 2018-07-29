namespace Iql.Entities.NestedSets
{
    public interface INestedSet : IPropertyGroup, IConfigurable<INestedSet>
    {
        string SetKey { get; set; }
        IProperty LeftProperty { get; set; }
        IProperty RightProperty { get; set; }
        IProperty LeftOfProperty { get; set; }
        IProperty RightOfProperty { get; set; }
        IProperty KeyProperty { get; set; }
        IProperty LevelProperty { get; set; }
        IProperty ParentIdProperty { get; set; }
        IProperty ParentProperty { get; set; }
        IProperty IdProperty { get; set; }
    }
}