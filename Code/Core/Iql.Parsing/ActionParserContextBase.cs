using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Conversion;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Parsing.Reduction;
using Iql.Parsing.Types;

namespace Iql.Parsing
{
    public abstract class ActionParserContextBase<TRegistry, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserContext, TConverter, TActionParserBase>
        where TRegistry : RegistryStore<IqlExpression, TActionParserBase>
        where TActionParserBase : class
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, TRegistry, TActionParserBase>
        where TParserOutput : IParserOutput
        where TParserContext : ActionParserContextBase<TRegistry, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserContext, TConverter, TActionParserBase>
        where TConverter : IExpressionConverter
    {
        public TConverter Converter { get; set; }

        public bool Nested => TypeStack.Count > 1 || Ancestors.Any(a =>
                                  a.Kind == IqlExpressionKind.Expand ||
                                  a.Kind == IqlExpressionKind.Count ||
                                  a.Kind == IqlExpressionKind.Any ||
                                  a.Kind == IqlExpressionKind.All
                                  );

        protected ActionParserContextBase(TQueryAdapter adapter, Type currentEntityType, TConverter converter, ITypeResolver typeResolver)
        {
            Adapter = adapter;
            SetEntityType(currentEntityType);
            Converter = converter;
            TypeResolver = typeResolver;
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public TQueryAdapter Adapter { get; set; }
        public Type CurrentEntityType => TypeStack.LastOrDefault();
        public bool IsTypeRoot => TypeStack.Count == 1;
        public List<Type> TypeStack { get; } = new List<Type>();
        public Type RootEntityType { get; }
        protected virtual void SetEntityType(Type type)
        {
            if (IsRoot)
            {
                if (TypeStack.Count == 1)
                {
                    TypeStack[0] = type;
                    return;
                }
            }
            TypeStack.Add(type);
        }
        public ITypeResolver TypeResolver { get; }
        private readonly Dictionary<string, string> _rootEntityNames = new Dictionary<string, string>();
        private string _rootEntityName;

        public string GetRootEntityName(IqlRootReferenceExpression rootReferenceExpression)
        {
            if (rootReferenceExpression == null)
            {
                return _rootEntityName;
            }
            var name = rootReferenceExpression.VariableName;
            return GetRootEntityParameterName(name);
        }

        public string RootEntityParameterName()
        {
            return _rootEntityName;
        }

        public bool IsParameterName(string name)
        {
            return _rootEntityNames.ContainsKey(name);
        }

        public string GetRootEntityParameterName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "entity";
            }

            if (!_rootEntityNames.ContainsKey(name))
            {
                var index = _rootEntityNames.Count == 0 ? "" : (_rootEntityNames.Count + 1).ToString();
                var mappedName = $"entity{index}";
                _rootEntityNames.Add(name, mappedName);
            }

            if (_rootEntityName == null)
            {
                _rootEntityName = _rootEntityNames[name];
            }

            return _rootEntityNames[name];
        }

        public List<IqlExpression> Ancestors { get; } = new List<IqlExpression>();

        public T[] GetAncestors<T>()
        where T : IqlExpression
        {
            return Ancestors
                .Where(a => a is T).Select(a => a as T)
                .ToArray();
        }

        public T GetNearestAncestor<T>()
            where T : IqlExpression
        {
            for (var i = Ancestors.Count - 1; i >= 0; i--)
            {
                if (Ancestors[i] is T)
                {
                    return Ancestors[i] as T;
                }
            }

            return null;
        }

        public Type ResolveParameterType(string name)
        {
            var typeName = ResolveParameterTypeName(name);
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return CurrentEntityType;
            }

            return ResolveTypeFromTypeName(typeName);
        }

        protected Type ResolveTypeFromTypeName(string typeName)
        {
            return TypeResolver.ResolveTypeFromTypeName(typeName).Type;
        }

        public string ResolveParameterTypeName(string name)
        {
            var lambda = ResolveLambdaExpressionForParameter(name);
            return lambda.ParameterExpression?.EntityTypeName;
        }

        public LambdaAndParameter ResolveLambdaExpressionForParameter(string name)
        {
            //if (!string.IsNullOrWhiteSpace(parameter.EntityTypeName))
            //{
            //    return parameter.EntityTypeName;
            //}

            var lambdaExpressions = GetAncestors<IqlParameteredLambdaExpression>();

            for (var i = lambdaExpressions.Length - 1; i >= 0; i--)
            {
                var iqlParameteredExpression = lambdaExpressions[i];
                if (iqlParameteredExpression.Parameters != null)
                {
                    var lambdaParameter = iqlParameteredExpression.Parameters
                        .SingleOrDefault(p => (p.VariableName ?? "") == name);
                    if (lambdaParameter != null)
                    {
                        return new LambdaAndParameter(iqlParameteredExpression, lambdaParameter);
                        // && !string.IsNullOrWhiteSpace(lambdaParameter.EntityTypeName)
                        //return lambdaParameter.EntityTypeName;
                    }
                }
            }

            return null;
        }

        public Dictionary<IqlExpression, List<TParserOutput>> OutputMap { get; } = new Dictionary<IqlExpression, List<TParserOutput>>();

        //protected IqlPropertyPath Path { get; } = new IqlPropertyPath();
        protected string EntityPath { get; } = "";
        protected void IncrementPath(IqlExpression expression)
        {
            //if (Path == null || Path.IsEmpty)
            //{
            //    EntityConfigurationBuilder.FindConfigurationBuilderForEntityType()
            //}
            switch (expression?.Kind)
            {
                case IqlExpressionKind.Any:
                case IqlExpressionKind.All:
                case IqlExpressionKind.Count:
                    var path = IqlPropertyPath.FromPropertyExpression(
                            TypeResolver,
                            TypeResolver.FindTypeByType(CurrentEntityType),
                            (IqlPropertyExpression)((IqlParentValueExpression)expression).Parent,
                            false)
                        ;
                    SetEntityType(path.PropertyEntityConfiguration.Type);
                    break;
                case IqlExpressionKind.Lambda:
                    break;
                case IqlExpressionKind.DataSetQuery:
                    var typeName = ((IqlDataSetQueryExpression)expression).Parameters.First().EntityTypeName;
                    SetEntityType(TypeResolver.ResolveTypeFromTypeName(typeName).Type);
                    break;
            }
        }

        public bool IsRoot => Ancestors.Count == 1;
        protected void DecrementPath(IqlExpression expression)
        {
            switch (expression?.Kind)
            {
                case IqlExpressionKind.Lambda:
                    break;
                case IqlExpressionKind.Any:
                case IqlExpressionKind.All:
                case IqlExpressionKind.Count:
                case IqlExpressionKind.DataSetQuery:
                    RemoveLastEntityType();
                    break;
            }
        }

        private void RemoveLastEntityType()
        {
            TypeStack.RemoveAt(TypeStack.Count - 1);
        }

        public IqlExpression Parent()
        {
            if (Ancestors.Count < 2)
            {
                return null;
            }
            return Ancestors[Ancestors.Count - 2];
        }
    }
}