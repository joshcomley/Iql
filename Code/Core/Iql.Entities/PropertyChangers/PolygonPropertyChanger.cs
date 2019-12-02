using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.PropertyChangers
{
    public class PolygonPropertyChanger : ComplexPropertyChanger<IqlPolygonExpression>
    {
        private static PolygonPropertyChanger _instance = null;
        public static PolygonPropertyChanger Instance => _instance = _instance ?? new PolygonPropertyChanger();
        protected override bool CheckEquivalence(IqlPolygonExpression newValue, IqlPolygonExpression oldValue)
        {
            if (oldValue.Srid != newValue.Srid)
            {
                return false;
            }

            if (!IsRingEquivalent(oldValue.OuterRing, newValue.OuterRing))
            {
                return false;
            }

            if (oldValue.InnerRings == null && (newValue.InnerRings == null || newValue.InnerRings.Count == 0))
            {
                return true;
            }

            if (newValue.InnerRings == null && (oldValue.InnerRings == null || oldValue.InnerRings.Count == 0))
            {
                return true;
            }

            var oldInnerRings = oldValue.InnerRings ?? new List<IqlRingExpression>();
            var newInnerRings = newValue.InnerRings ?? new List<IqlRingExpression>();

            if (oldInnerRings.Count != newInnerRings.Count)
            {
                return false;
            }

            for (var i = 0; i < oldInnerRings.Count; i++)
            {
                var oldRing = oldInnerRings[i];
                var newRing = newInnerRings[i];
                if (!IsRingEquivalent(oldRing, newRing))
                {
                    return false;
                }
            }

            return true;
        }

        protected override IqlPolygonExpression CloneValueInternal(IqlPolygonExpression value)
        {
            return new IqlPolygonExpression(
                CloneRing(value.OuterRing),
                CloneRings(value.InnerRings),
                value.ReturnType,
                value.Srid);
        }

        //protected override void ApplyToInternal(IqlPolygonExpression value, IqlPolygonExpression applyTo)
        //{
        //    throw new System.NotImplementedException();
        //}

        private IEnumerable<IqlRingExpression> CloneRings(List<IqlRingExpression> rings)
        {
            if (rings == null)
            {
                return null;
            }

            return rings.Select(r => CloneRing(r));
        }

        private IqlRingExpression CloneRing(IqlRingExpression ring)
        {
            if (ring == null)
            {
                return null;
            }
            return new IqlRingExpression(
                ClonePoints(ring.Points),
                ring.Srid);
        }

        private IEnumerable<IqlPointExpression> ClonePoints(List<IqlPointExpression> ringPoints)
        {
            if (ringPoints == null)
            {
                return null;
            }

            return ringPoints.Select(_ => PointPropertyChanger.Instance.CloneValue(_));
        }

        private bool IsRingEquivalent(IqlRingExpression oldValue, IqlRingExpression newValue)
        {
            if (oldValue == null && newValue == null)
            {
                return true;
            }

            if (oldValue == null && (newValue.Points == null || newValue.Points.Count == 0))
            {
                return true;
            }

            if (newValue == null && (oldValue.Points == null || oldValue.Points.Count == 0))
            {
                return true;
            }

            var oldPoints = oldValue?.Points.ToList() ?? new List<IqlPointExpression>();
            var newPoints = newValue?.Points.ToList() ?? new List<IqlPointExpression>();

            NormalizePoints(oldPoints);
            NormalizePoints(newPoints);

            if (oldPoints.Count != newPoints.Count)
            {
                return false;
            }

            for (var i = 0; i < oldPoints.Count; i++)
            {
                if (!IsPointEquivalent(oldPoints[i], newPoints[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private void NormalizePoints(List<IqlPointExpression> oldPoints)
        {
            if (IsPointEquivalent(oldPoints[0], oldPoints[oldPoints.Count - 1]))
            {
                oldPoints.RemoveAt(oldPoints.Count - 1);
            }
        }

        private bool IsPointEquivalent(IqlPointExpression left, IqlPointExpression right)
        {
            if (left == null && right != null)
            {
                return false;
            }

            if (right == null && left != null)
            {
                return false;
            }

            return left.X == right.X && left.Y == right.Y;
        }
    }
}