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
			return null;
			// #CloneEnd
		}
    }
}
