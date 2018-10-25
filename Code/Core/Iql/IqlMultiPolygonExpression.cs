using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlMultiPolygonExpression : IqlExpression, IGeographic
    {
        public List<IqlPolygonExpression> Points { get; set; }
        protected IqlMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points, IqlExpressionKind kind) : base(kind, IqlType.Class)
        {
            Points = points.ToList();
        }

        public int Srid { get; set; }
    }
}