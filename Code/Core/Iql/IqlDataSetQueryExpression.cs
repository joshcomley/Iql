using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlNavigationExpression : IqlExpression
    {
        protected IqlNavigationExpression(IqlExpressionKind kind, IqlType type, IqlExpression parent = null) : base(kind, IqlType.Class, parent) { }
        public List<IqlExpandExpression> Expands { get; set; }
        public IqlExpression Filter { get; set; }
        public IqlWithKeyExpression WithKey { get; set; }
    }

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
    public abstract class IqlCollectitonQueryExpression : IqlNavigationExpression
    {
        protected IqlCollectitonQueryExpression(IqlExpressionKind kind, IqlExpression parent = null) : base(kind, IqlType.Class, parent) { }
        public List<IqlOrderByExpression> OrderBys { get; set; }
        public bool? IncludeCount { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }

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

        public IqlDataSetReference DataSet { get; set; }
    }
}