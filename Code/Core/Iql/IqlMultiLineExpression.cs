using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlMultiLineExpression : IqlSridExpression
    {
        public List<IqlLineExpression> Lines { get; set; }
        public IqlMultiLineExpression(IEnumerable<IqlLineExpression> points = null, IqlType type = IqlType.GeographyMultiLine, int? srid = null) : base(srid, type)
        {
            Lines = points?.ToList();
        }

        public IqlMultiLineExpression() : base(null, IqlType.GeographyMultiLine)
        {

        }

        public double Length(IqlDistanceKind unit = IqlDistanceKind.Meters)
        {
            if (Lines == null)
            {
                return 0;
            }

            return Lines.Sum(_ => _.Length());
        }


        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				if(Lines != null)
				{
					for(var i = 0; i < Lines.Count; i++)
					{
						context.Flatten(Lines[i]);
					}
				}
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			if(Lines != null)
			{
				for(var i = 0; i < Lines.Count; i++)
				{
					Lines[i] = (IqlLineExpression)context.Replace(this, nameof(Lines), i, Lines[i]);
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

		public static IqlMultiLineExpression Clone(IqlMultiLineExpression source)
		{
			// #CloneStart

			var expression = new IqlMultiLineExpression();
			if(source.Lines == null)
			{
				expression.Lines = null;
			}
			else
			{
				var listCopy = new List<IqlLineExpression>();
				for(var i = 0; i < source.Lines.Count; i++)
				{
					listCopy.Add((IqlLineExpression)source.Lines[i]?.Clone());
				}
				expression.Lines = listCopy;
			}
			expression.Srid = source.Srid;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
