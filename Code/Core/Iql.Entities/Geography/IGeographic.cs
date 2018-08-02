namespace Iql.Entities.Geography
{
    /*
     *         public const string NestedSet = Prefix + nameof(NestedSet);
        public const string NestedSetLeft = NestedSet + ":Left";
        public const string NestedSetRight = NestedSet + ":Right";
        public const string NestedSetLeftOf = NestedSet + ":LeftOf";
        public const string NestedSetRightOf = NestedSet + ":RightOf";
        public const string NestedSetKey = NestedSet + ":Key";
        public const string NestedSetLevel = NestedSet + ":Level";
        public const string NestedSetParentId = NestedSet + ":ParentId";
        public const string NestedSetParent = NestedSet + ":Parent";
        public const string NestedSetId = NestedSet + ":Id";

     */

    public interface IGeographic : ISimpleProperty, IConfigurable<IGeographic>
    {
        IProperty LongitudeProperty { get; set; }
        IProperty LatitudeProperty { get; set; }
    }
}