using System;
using System.Collections.Generic;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.Parsing;
using Iql.Parsing.Reduction;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public class JavaScriptExpressionNodeParseContext<TEntity, TJavaScriptExpressionNode>
        : IExpressionParserInstance
        where TEntity : class
        where TJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        private readonly List<object> _objectStack = new List<object>();
        public Func<string, object> Evaluate;
        public TJavaScriptExpressionNode Expression;

        public JavaScriptExpressionNodeParseContext(
            JavaScriptExpressionToIqlConverter converter,
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            TEntity rootEntity,
            string rootEntityVariableName
        )
        {
            Converter = converter;
            Adapter = new JavaScriptQueryExpressionAdapterIql<TEntity>();
            Data = Adapter.NewData();
#if TypeScript
            EvaluateContext = evaluateContext;
#endif
            RootEntity = rootEntity;
            RootEntityVariableName = rootEntityVariableName;
            Reducer = new IqlReducer(
#if TypeScript
                evaluateContext
#endif
                );
            var ctx = this;
            Evaluate = key =>
            {
#if TypeScript
                if (key == "_this" || key == "this")
                {
                    return ctx.EvaluateContext.Context;
                }
                var result = evaluateContext.Evaluate(key);
                return result;
#else
                return null;
#endif
            };
        }

        public IqlReducer Reducer { get; set; }

        public JavaScriptExpressionToIqlConverter Converter { get; set; }
        public JavaScriptQueryExpressionAdapterIql<TEntity> Adapter { get; set; }
        public JavaScriptToIqlExpressionData Data { get; set; }
        public EvaluateContext EvaluateContext { get; set; }
        public TEntity RootEntity { get; set; }

        public string RootEntityVariableName { get; set; }
        //public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        JavaScriptExpressionNode IExpressionParserInstance.Expression
        {
            get => Expression;
            set => Expression = (TJavaScriptExpressionNode) value;
        }

        IJavaScriptExpressionAdapterBase IExpressionParserInstance.Adapter
        {
            get => Adapter;
            set => Adapter = (JavaScriptQueryExpressionAdapterIql<TEntity>) value;
        }

        public List<object> ObjectStack()
        {
            return _objectStack;
        }

        //public object BaseObject()
        //{
        //    return _objectStack.Count < 1 ? null : _objectStack[0];
        //}

        public object Parent()
        {
            if (_objectStack.Count < 1)
            {
                return null;
            }
            return _objectStack[_objectStack.Count - 1];
        }

        private void PushCurrentObject(object obj)
        {
            _objectStack.Add(obj);
        }

        private object PopCurrentObject()
        {
            var obj = Parent();
            if (obj == null)
            {
                return null;
            }
            _objectStack.RemoveAt(_objectStack.Count - 1);
            return obj;
        }

        public IqlExpression ParseSubTree(WhereQueryExpression where)
        {
            return Converter.ConvertExpressionToIql<IqlExpression>(where).Expression;
        }

        //public T ExpressionAs<T>()
        //    where T : TJavaScriptExpressionNode
        //{
        //    return Expression as TJavaScriptExpressionNode as T;
        //}

        public IqlParseResult Parse(JavaScriptExpressionNode expression)
        {
            return Converter.ParseJavaScriptExpressionTree(expression, this) as IqlParseResult;
        }

        public IqlParseResult ParseWith(JavaScriptExpressionNode expression, object entity)
        {
            PushCurrentObject(entity);
            var val = Converter.ParseJavaScriptExpressionTree(expression, this);
            PopCurrentObject();
            return val as IqlParseResult;
        }

        public IqlParseResult ParseLeft()
        {
            return Converter.ParseJavaScriptExpressionTree(
                    (Expression as JavaScriptExpressionNode as BinaryJavaScriptExpressionNode).Left, this)
                as IqlParseResult;
        }

        public IqlParseResult ParseRight()
        {
            return Converter.ParseJavaScriptExpressionTree(
                    (Expression as JavaScriptExpressionNode as BinaryJavaScriptExpressionNode).Right, this)
                as IqlParseResult;
        }
    }
}