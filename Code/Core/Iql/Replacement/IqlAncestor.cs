using System.Diagnostics;

namespace Iql
{
    [DebuggerDisplay("{Owner}.{PropertyName}{IndexString}")]
    public class IqlAncestor
    {
        public IqlAncestor Parent { get; }
        public IqlExpression Owner { get; }
        public IqlExpression Value { get; }
        public string PropertyName { get; }
        public int? Index { get; }
#if DEBUG
        private string IndexString => Index == null ? null : $"[{Index}]";
#endif

        public IqlAncestor(IqlExpression owner, string propertyName, int? index, IqlExpression value, IqlAncestor parent)
        {
            Owner = owner;
            PropertyName = propertyName;
            Index = index;
            Value = value;
            Parent = parent;
        }
    }
}