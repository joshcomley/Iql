using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public abstract class IqlLineExpression : IqlReferenceExpression, IPointsExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        protected IqlLineExpression(IEnumerable<IqlPointExpression> points, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            Points = points?.ToList();
        }

        public double Length()
        {
            var current = Points[0];
            double total = 0;
            for (var i = 1; i < Points.Count; i++)
            {
                var point = Points[i];
                total += IqlPointExpression.DistanceBetween(current.X, current.Y, point.X, point.Y);
                current = point;
            }
            return total;
        }
    }
}