using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlPolygonExpression : IqlExpression, IGeographic
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlPolygonExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind) : base(kind, IqlType.Class)
        {
            Points = points.ToList();
        }

        public int Srid { get; set; }
    }
}