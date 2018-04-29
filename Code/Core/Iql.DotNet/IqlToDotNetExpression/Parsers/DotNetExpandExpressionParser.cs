using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetExpandExpressionParser : DotNetActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpandExpression action, DotNetIqlParserInstance parser)
        {
            return new IqlFinalExpression<Expression>(
                parser.Chain<TEntity>(null, e => e.Expand(action)));
        }

        //public override IqlExpression ToQueryString(
        //    IqlExpandExpression action,
        //    DotNetIqlParserInstance parser)
        //{
        //    //parser.WithContext(ctx => ctx.Expand())
        //    //var entityConfiguration = parser.Converter.ResolvEntityConfigurationBuilder(parser.RootEntityType);
        //    //var path = IqlPropertyPath.FromPropertyExpression(
        //    //    entityConfiguration.GetEntityByType(parser.RootEntityType),
        //    //    action.NavigationProperty
        //    //);
        //    //var relationshipExpander = new RelationshipExpander();
        //    //var dataContext = parser.Converter.DataContext;
        //    //var targetDbSet = dataContext.GetDbSetByEntityType(path.Property.Relationship.OtherEnd.Configuration.Type);
        //    //Expression<Func<T, T>> expression 
        //    return new IqlFinalExpression<Expression>();
        //}
    }
}