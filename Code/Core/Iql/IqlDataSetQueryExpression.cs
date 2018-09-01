using System;
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
        public IqlDataSetQueryExpression(string entityTypeName = null, IqlExpression parent = null)
            : base(entityTypeName, IqlExpressionKind.DataSetQuery, parent)
        {
        }

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
			if(Parameters == null)
			{
				expression.Parameters = null;
			}
			else
			{
				var listCopy = new List<IqlRootReferenceExpression>();
				for(var i = 0; i < Parameters.Count; i++)
				{
					listCopy.Add((IqlRootReferenceExpression)Parameters[i]?.Clone());
				}
				expression.Parameters = listCopy;
			}
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				DataSet?.FlattenInternal(expressions, checker);
				if(OrderBys != null)
				{
					for(var i = 0; i < OrderBys.Count; i++)
					{
						OrderBys[i]?.FlattenInternal(expressions, checker);
					}
				}
				if(Expands != null)
				{
					for(var i = 0; i < Expands.Count; i++)
					{
						Expands[i]?.FlattenInternal(expressions, checker);
					}
				}
				Filter?.FlattenInternal(expressions, checker);
				WithKey?.FlattenInternal(expressions, checker);
				if(Parameters != null)
				{
					for(var i = 0; i < Parameters.Count; i++)
					{
						Parameters[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			DataSet = (IqlDataSetReferenceExpression)context.Replace(this, nameof(DataSet), null, DataSet);
			if(OrderBys != null)
			{
				for(var i = 0; i < OrderBys.Count; i++)
				{
					OrderBys[i] = (IqlOrderByExpression)context.Replace(this, nameof(OrderBys), i, OrderBys[i]);
				}
			}
			if(Expands != null)
			{
				for(var i = 0; i < Expands.Count; i++)
				{
					Expands[i] = (IqlExpandExpression)context.Replace(this, nameof(Expands), i, Expands[i]);
				}
			}
			Filter = context.Replace(this, nameof(Filter), null, Filter);
			WithKey = (IqlWithKeyExpression)context.Replace(this, nameof(WithKey), null, WithKey);
			if(Parameters != null)
			{
				for(var i = 0; i < Parameters.Count; i++)
				{
					Parameters[i] = (IqlRootReferenceExpression)context.Replace(this, nameof(Parameters), i, Parameters[i]);
				}
			}
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}
    }
}
