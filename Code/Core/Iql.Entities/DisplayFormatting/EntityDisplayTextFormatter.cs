using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.DisplayFormatting
{
    public class EntityDisplayTextFormatter<TEntity> : IEntityDisplayTextFormatter
    {
        private Func<TEntity, string> _formatFunction;
        private Func<FormatterContext<TEntity>, string> _formatAndInterceptFunction;
        private Dictionary<string, IqlExpression> _expressionLookup;
        public Expression<Func<TEntity, string>> FormatterExpression { get; }
        public string Key { get; }

        public Func<TEntity, string> Format => _formatFunction ?? (_formatFunction = FormatterExpression.Compile());

        LambdaExpression IEntityDisplayTextFormatter.FormatterExpression => FormatterExpression;
        Func<object, string> IEntityDisplayTextFormatter.Format => obj => Format((TEntity)obj);

        public string FormatAndIntercept(TEntity entity, Func<FormatterContext<TEntity>, IqlExpression, object, string> expression)
        {
            return FormatAndInterceptWith(new InlineFormatterContext<TEntity>(entity, expression));
        }

        string IEntityDisplayTextFormatter.FormatAndIntercept(object entity,
            Func<IFormatterContext, IqlExpression, object, string> expression)
        {
            return FormatAndIntercept((TEntity) entity,
                expression);
        }

        public string FormatAndInterceptWith(FormatterContext<TEntity> context)
        {
            if (_formatAndInterceptFunction == null)
            {
                var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(FormatterExpression,
                    typeof(TEntity)).Expression as IqlLambdaExpression;
                var body = iql.Body;
                var rootReferenceRoots = new List<IqlExpression>();
                var propertiesToIgnore = new List<IqlPropertyExpression>();
                var propertiesToReplace = new List<IqlPropertyExpression>();
                var expressionLookup = new Dictionary<string, IqlExpression>();
                body.ReplaceWith((replaceContext, expression) =>
                {
                    if (expression.Kind == IqlExpressionKind.RootReference)
                    {
                        var path = new List<IqlPropertyExpression>();
                        for (var i = replaceContext.Ancestors.Count - 1; i >= 0; i--)
                        {
                            if (replaceContext.Ancestors[i].Owner != null && replaceContext.Ancestors[i].Owner.Kind == IqlExpressionKind.Property)
                            {
                                path.Add(replaceContext.Ancestors[i].Owner as IqlPropertyExpression);
                            }
                            else
                            {
                                break;
                            }
                        }

                        var last = path.LastOrDefault();
                        if (last != null)
                        {
                            path.RemoveAt(path.Count - 1);
                            propertiesToIgnore.AddRange(path);
                            propertiesToReplace.Add(last);
                        }
                        rootReferenceRoots.Add(replaceContext.Parent == null ? expression : replaceContext.Parent.Value);
                    }
                    return expression;
                });
                body.ReplaceWith((replaceContext, expression) =>
                {
                    if (expression.Kind == IqlExpressionKind.Property)
                    {
                        var property = expression as IqlPropertyExpression;
                        if (propertiesToReplace.Contains(property) && !propertiesToIgnore.Contains(property))
                        {
                            var key = Guid.NewGuid().ToString();
                            expressionLookup.Add(key, property);
                            return new IqlInvocationExpression(nameof(FormatterContext<object>.FormatInternal),
                                IqlType.String,
                                    new IqlRootReferenceExpression())
                                .AddParameter(new IqlLiteralExpression(key))
                                .AddParameter(property);
                        }
                    }
                    if (rootReferenceRoots.Contains(expression))
                    {
                        return new IqlPropertyExpression(nameof(FormatterContext<object>.Entity),
                            expression as IqlReferenceExpression);
                    }
                    return expression;
                });
                iql = new IqlLambdaExpression(IqlType.String, body).AddParameter();
                var lambda = IqlConverter.Instance.ConvertIqlToExpression<FormatterContext<TEntity>>(iql);
                var finalExpression = (Func<FormatterContext<TEntity>, string>)lambda
                    .Compile();
                _formatAndInterceptFunction = finalExpression;
                _expressionLookup = expressionLookup;
            }
            context.ExpressionLookup = _expressionLookup;
            var result = _formatAndInterceptFunction(context);
            return result;
        }

        string IEntityDisplayTextFormatter.FormatAndInterceptWith(IFormatterContext context)
        {
            return FormatAndInterceptWith((FormatterContext<TEntity>) context);
        }

        public EntityDisplayTextFormatter(Expression<Func<TEntity, string>> formatterExpression, string key)
        {
            FormatterExpression = formatterExpression;
            Key = key;
        }
    }
}