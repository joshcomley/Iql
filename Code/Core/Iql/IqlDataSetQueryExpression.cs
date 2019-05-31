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


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(DataSet);
				if(OrderBys != null)
				{
					for(var i = 0; i < OrderBys.Count; i++)
					{
						context.Flatten(OrderBys[i]);
					}
				}
				if(Expands != null)
				{
					for(var i = 0; i < Expands.Count; i++)
					{
						context.Flatten(Expands[i]);
					}
				}
				context.Flatten(Filter);
				context.Flatten(WithKey);
				if(Parameters != null)
				{
					for(var i = 0; i < Parameters.Count; i++)
					{
						context.Flatten(Parameters[i]);
					}
				}
				context.Flatten(Parent);

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

		public static IqlDataSetQueryExpression Clone(IqlDataSetQueryExpression source)
		{
			// #CloneStart

			var expression = new IqlDataSetQueryExpression();
			expression.DataSet = (IqlDataSetReferenceExpression)source.DataSet?.Clone();
			if(source.OrderBys == null)
			{
				expression.OrderBys = null;
			}
			else
			{
				var listCopy = new List<IqlOrderByExpression>();
				for(var i = 0; i < source.OrderBys.Count; i++)
				{
					listCopy.Add((IqlOrderByExpression)source.OrderBys[i]?.Clone());
				}
				expression.OrderBys = listCopy;
			}
			expression.IncludeCount = source.IncludeCount;
			expression.Skip = source.Skip;
			expression.Take = source.Take;
			expression.EntityTypeName = source.EntityTypeName;
			if(source.Expands == null)
			{
				expression.Expands = null;
			}
			else
			{
				var listCopy = new List<IqlExpandExpression>();
				for(var i = 0; i < source.Expands.Count; i++)
				{
					listCopy.Add((IqlExpandExpression)source.Expands[i]?.Clone());
				}
				expression.Expands = listCopy;
			}
			expression.Filter = source.Filter?.Clone();
			expression.WithKey = (IqlWithKeyExpression)source.WithKey?.Clone();
			if(source.Parameters == null)
			{
				expression.Parameters = null;
			}
			else
			{
				var listCopy = new List<IqlRootReferenceExpression>();
				for(var i = 0; i < source.Parameters.Count; i++)
				{
					listCopy.Add((IqlRootReferenceExpression)source.Parameters[i]?.Clone());
				}
				expression.Parameters = listCopy;
			}
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
