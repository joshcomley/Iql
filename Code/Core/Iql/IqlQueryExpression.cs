using System.Collections.Generic;

namespace Iql
{
    public class IqlQueryExpression : IqlExpression
    {
        public IqlQueryExpression(IqlExpression parent = null)
            : base(IqlExpressionKind.Query, IqlType.Collection, parent) { }

        public IqlQueryExpression() : this(null)
        {

        }

        public IqlDataSetReference DataSet { get; set; }
        public List<IqlOrderByExpression> OrderBys { get; set; }
        public List<IqlExpandExpression> Expands { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public IqlExpression Filter { get; set; }
    }
}