using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlPolygonExpression : IqlExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlPolygonExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points?.ToList();
        }

        public bool Intersects(IqlPointExpression point)
        {
            return IqlPointExpression.IntersectsPolygon(point.X, point.Y, this);
        }
    }
}