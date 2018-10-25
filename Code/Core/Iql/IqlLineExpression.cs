using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlLineExpression : IqlExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlLineExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points.ToList();
        }
    }
}