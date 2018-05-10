using System.Collections.Generic;

namespace Iql
{
    //public interface IIqlPropertyNavigationExpression
    //{
    //    IqlPropertyExpression NavigationProperty { get; set; }
    //}
    //public class IqlPropertyNavigationExpression : IqlNavigationExpression, IIqlPropertyNavigationExpression
    //{
    //    public IqlPropertyNavigationExpression(IqlExpression parent = null) : base(IqlExpressionKind.PropertyNavigation, IqlType.Class, parent) { }

    //    public IqlPropertyNavigationExpression():this(null)
    //    {
            
    //    }

    //    public IqlPropertyExpression NavigationProperty { get; set; }
    //}

    //public class IqlCollectionPropertyNavigationExpression : IqlCollectitonQueryExpression, IIqlPropertyNavigationExpression
    //{
    //    public IqlCollectionPropertyNavigationExpression(IqlExpression parent = null)
    //        : base(IqlExpressionKind.CollectionPropertyNavigation, parent) { }

    //    public IqlCollectionPropertyNavigationExpression() : this(null)
    //    {

    //    }

    //    public IqlPropertyExpression NavigationProperty { get; set; }
    //}

    public class IqlDataSetQueryExpression : IqlCollectitonQueryExpression
    {
        public IqlDataSetQueryExpression(IqlExpression parent = null)
            : base(IqlExpressionKind.DataSetQuery, parent) { }

        public IqlDataSetQueryExpression() : this(null)
        {

        }

        public IqlDataSetReferenceExpression DataSet { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlDataSetQueryExpression();
			expression.DataSet = (IqlDataSetReferenceExpression)DataSet?.Clone();
			if(OrderBys == null)
			{
				expression.OrderBys = null;
			}
			else
			{
				var listCopy = new List<IqlOrderByExpression>();
				for(var i = 0; i < OrderBys.Count; i++)
				{
					listCopy.Add((IqlOrderByExpression)OrderBys[i]?.Clone());
				}
				expression.OrderBys = listCopy;
			}
			expression.IncludeCount = IncludeCount;
			expression.Skip = Skip;
			expression.Take = Take;
			if(Expands == null)
			{
				expression.Expands = null;
			}
			else
			{
				var listCopy = new List<IqlExpandExpression>();
				for(var i = 0; i < Expands.Count; i++)
				{
					listCopy.Add((IqlExpandExpression)Expands[i]?.Clone());
				}
				expression.Expands = listCopy;
			}
			expression.Filter = Filter?.Clone();
			expression.WithKey = (IqlWithKeyExpression)WithKey?.Clone();
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
