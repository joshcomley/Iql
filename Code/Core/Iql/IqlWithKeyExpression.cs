using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlWithKeyExpression : IqlExpression
    {
        public List<IqlIsEqualToExpression> KeyEqualToExpressions { get; set; }

        public IqlWithKeyExpression(IEnumerable<IqlIsEqualToExpression> keyEqualToExpressions, 
            IqlExpression parent = null)
            : base(IqlExpressionKind.WithKey, IqlType.Class, parent)
        {
            KeyEqualToExpressions = keyEqualToExpressions?.ToList();
        }

        public IqlWithKeyExpression() : this(null)
        {

        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlWithKeyExpression(null);
			if(KeyEqualToExpressions == null)
			{
				expression.KeyEqualToExpressions = null;
			}
			else
			{
				var listCopy = new List<IqlIsEqualToExpression>();
				for(var i = 0; i < KeyEqualToExpressions.Count; i++)
				{
					listCopy.Add((IqlIsEqualToExpression)KeyEqualToExpressions[i]?.Clone());
				}
				expression.KeyEqualToExpressions = listCopy;
			}
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
