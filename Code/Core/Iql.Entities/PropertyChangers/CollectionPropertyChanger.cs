using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Entities.PropertyChangers
{
    public class CollectionPropertyChanger : ComplexPropertyChanger<IList>
    {
        public CollectionPropertyChanger()
        {
            CanSilentlyChange = true;
        }
        private static CollectionPropertyChanger _instance;

        public static CollectionPropertyChanger Instance => _instance = _instance ?? new CollectionPropertyChanger();
        protected override bool CheckEquivalence(IList remoteValue, IList localValue)
        {
            throw new NotImplementedException();
//            return !AreDifferent(left, right);
        }

        public static bool AreDifferent(IList left, IList right)
        {
            if (left == null)
            {
                return right == null || right.Count > 0;
            }

            if (right == null)
            {
                return left.Count > 0;
            }

            if (left.Count != right.Count)
            {
                return true;
            }

            for (var i = 0; i < left.Count; i++)
            {
                var item = left[i];
                //var itemState = DataTracker.GetEntityState(item);
                //if (itemState != null && itemState.HasChangedSinceSnapshot)
                //{
                //    return true;
                //}
                if (!right.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        protected override IList CloneValueInternal(IList value)
        {
            if (value == null)
            {
                return null;
            }

            var list = new List<object>();
            foreach (var item in value)
            {
                list.Add(item);
            }
            return list;
        }

        //protected override void ApplyToInternal(IqlPointExpression value, IqlPointExpression applyTo)
        //{
        //    applyTo.Y = value.Y;
        //    applyTo.X = value.X;
        //    applyTo.Srid = value.Srid;
        //    applyTo.ReturnType = value.ReturnType;
        //}
    }
}