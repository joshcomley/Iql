using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlMultiPolygonExpression : IqlExpression
    {
        public List<IqlPolygonExpression> Polygons { get; set; }
        protected IqlMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Polygons = points?.ToList();
        }

        public bool Intersects(IqlPointExpression point)
        {
            return Polygons != null && Polygons.Any(_ => _.Intersects(point));
        }
    }
}