using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlPolygonExpression : IqlExpression, IGeographicExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlPolygonExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points.ToList();
        }

        public int Srid { get; set; }
    }
}