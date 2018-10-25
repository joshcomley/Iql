using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlMultiPolygonExpression : IqlExpression
    {
        public List<IqlPolygonExpression> Points { get; set; }
        protected IqlMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points.ToList();
        }
    }
}