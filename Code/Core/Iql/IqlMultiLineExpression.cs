using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlMultiLineExpression : IqlExpression
    {
        public List<IqlLineExpression> Lines { get; set; }
        protected IqlMultiLineExpression(IEnumerable<IqlLineExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Lines = points.ToList();
        }
    }
}