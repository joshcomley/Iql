using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlLineExpression : IqlExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlLineExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind) : base(kind, IqlType.Class)
        {
            Points = points.ToList();
        }
    }
}