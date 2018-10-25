using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlMultiPointExpression : IqlExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlMultiPointExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points.ToList();
        }
    }
}