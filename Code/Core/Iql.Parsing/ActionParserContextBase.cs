using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Iql.Parsing.Reduction;
using Iql.Parsing.Types;
using Newtonsoft.Json.Linq;

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
        class RootReferenceTypeMap
        {
            public string Name { get; }
            public Type Type { get; }

            public RootReferenceTypeMap(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }
        public TConverter Converter { get; set; }

        public bool Nested => Ancestors.Any(a =>
                                  a.Kind == IqlExpressionKind.Expand ||
                                  a.Kind == IqlExpressionKind.Count ||
                                  a.Kind == IqlExpressionKind.Any ||
                                  a.Kind == IqlExpressionKind.All
                                  );

        protected ActionParserContextBase(TQueryAdapter adapter, Type currentEntityType, TConverter converter, ITypeResolver typeResolver)
        {
            Adapter = adapter;
            SetEntityType(currentEntityType, Expression);
            Converter = converter;
            TypeResolver = typeResolver;// ?? new TypeResolver();
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public TQueryAdapter Adapter { get; set; }
        public Type CurrentEntityType => TypeStack.LastOrDefault();
        public bool IsTypeRoot => TypeStack.Count == 1;
        private List<Type> _typeStack = null;
        public List<Type> TypeStack => _typeStack = _typeStack ?? new List<Type>();
        public Type RootEntityType { get; }
        private Dictionary<IqlExpression, Type> _foundTypes = new Dictionary<IqlExpression, Type>();
        private List<RootReferenceTypeMap> _rootReferenceResolvedTypes = new List<RootReferenceTypeMap>();
        protected virtual void SetEntityType(Type type, IqlExpression expression)
        {
#if !TypeScript
            if (type == typeof(JObject))
            {
                type = typeof(JToken);
            }
#endif
            if (IsRoot)
            {
                if (TypeStack.Count == 1)
                {
                    TypeStack[0] = type;
                    return;
                }
            }
            TypeStack.Add(type);
            if (expression != null && !_foundTypes.ContainsKey(expression))
            {
                _foundTypes.Add(expression, type);
                var rootVariableName = TryResolveRootVariableName(expression);
                if (rootVariableName != null)
                {
                    _rootReferenceResolvedTypes.Add(new RootReferenceTypeMap(rootVariableName, type));
                }
            }
        }

        private static string TryResolveRootVariableName(IqlExpression expression)
        {
            string rootVariableName = null;
            switch (expression.Kind)
            {
                case IqlExpressionKind.RootReference:
                    rootVariableName = ((IqlRootReferenceExpression)expression).VariableName;
                    break;
                case IqlExpressionKind.Any:
                case IqlExpressionKind.All:
                case IqlExpressionKind.Count:
                    var iqlLambdaExpression = ((IqlParentValueLambdaExpression)expression).Value;
                    if (iqlLambdaExpression != null)
                    {
                        rootVariableName = iqlLambdaExpression.Parameters[0].VariableName;
                    }
                    break;
            }

            return rootVariableName;
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
        private List<IqlExpression> _ancestors = null;

        public List<IqlExpression> Ancestors => _ancestors = _ancestors ?? new List<IqlExpression>();

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
            var match = _rootReferenceResolvedTypes.FirstOrDefault(_ => _.Name == name);
            if (match != null)
            {
                return match.Type;
            }
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
            var typeName = lambda.ParameterExpression?.EntityTypeName;
            if (string.IsNullOrWhiteSpace(typeName))
            {
            }

            return typeName;
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
        private Dictionary<IqlExpression, List<TParserOutput>> _outputMap = null;

        public Dictionary<IqlExpression, List<TParserOutput>> OutputMap => _outputMap = _outputMap ?? new Dictionary<IqlExpression, List<TParserOutput>>();

        //protected IqlPropertyPath Path { get; } = new IqlPropertyPath();
        protected string EntityPath { get; } = "";
        protected void IncrementPath(IqlExpression expression, Action action)
        {
            var resolved = IncrementPathInternal(expression);
            action();
            if (resolved)
            {
                DecrementPath(expression);
            }
        }
        protected async Task IncrementPathAsync(IqlExpression expression, Func<Task> action)
        {
            var resolved = IncrementPathInternal(expression);
            await action();
            if (resolved)
            {
                DecrementPath(expression);
            }
        }

        private bool IncrementPathInternal(IqlExpression expression)
        {
            var resolved = false;
            if (TypeResolver != null)
            {
                switch (expression?.Kind)
                {
                    case IqlExpressionKind.Any:
                    case IqlExpressionKind.All:
                    case IqlExpressionKind.Count:
                    case IqlExpressionKind.RootReference:
                        var parent = expression.Parent;
                        if (parent != null && parent.Kind == IqlExpressionKind.Property && CurrentEntityType != null)
                        {
                            var path = IqlPropertyPath.FromPropertyExpression(
                                    TypeResolver,
                                    TypeResolver.FindTypeByType(CurrentEntityType),
                                    (IqlPropertyExpression)parent,
                                    false)
                                ;
                            if (path != null)
                            {
                                var type = path.PropertyEntityConfiguration?.Type ?? path.Property?.Type;
                                if (type != null)
                                {
                                    SetEntityType(type, expression);
                                    resolved = true;
                                }
                            }
                        }

                        if (!resolved && parent != null)
                        {
                            // Check to see if we have a literal value to trace the type from
                            resolved = TrySetRuntimeType(expression);
                        }
                        break;
                    case IqlExpressionKind.Lambda:
                        break;
                    case IqlExpressionKind.DataSetQuery:
                        var typeName = ((IqlDataSetQueryExpression)expression).Parameters.First().EntityTypeName;
                        var typeMetadata = TypeResolver.ResolveTypeFromTypeName(typeName);
                        if (typeMetadata?.Type != null)
                        {
                            resolved = true;
                            SetEntityType(typeMetadata.Type, expression);
                        }

                        break;
                }
            }

            return resolved;
        }

        private bool TrySetRuntimeType(IqlExpression expression)
        {
            var parent = expression.Parent;
            var resolved = false;
            var value = parent.TryResolveRuntimeValue();
            if (value?.Success == true)
            {
                if (value.Value is IEnumerable)
                {
                    // TODO: Resolve base enumerable type for types with multiple
                    // generic parameters that inherit from IEnumerable
                    var genericArguments = value.Value.GetType().GenericTypeArguments;
                    if (genericArguments.Length == 0)
                    {
                        object firstValue = null;
                        foreach (var val in (IEnumerable)value.Value)
                        {
                            if (val != null)
                            {
                                firstValue = val;
                                break;
                            }
                        }

                        if (firstValue != null)
                        {
                            resolved = true;
                            SetEntityType(firstValue.GetType(), expression);
                        }
                    }
                    else
                    {
                        resolved = true;
                        SetEntityType(genericArguments[0], expression);
                    }
                }
            }
            else
            {
                var path = new List<IqlPropertyExpression>();
                while (parent != null)
                {
                    if (parent.Kind == IqlExpressionKind.RootReference)
                    {
                        var variableName = ((IqlRootReferenceExpression)parent).VariableName;
                        var match = _rootReferenceResolvedTypes.FirstOrDefault(_ => _.Name == variableName);
                        if (match != null)
                        {
                            var type = match.Type;
                            for (var i = path.Count - 1; i >= 0; i--)
                            {
                                var propertyInfo = type.GetProperty(path[i].PropertyName);
                                if (propertyInfo == null)
                                {
                                    return false;
                                }
                                type = propertyInfo.PropertyType;
                                if (typeof(IEnumerable).IsAssignableFrom(type))
                                {
                                    type = type.GenericTypeArguments[0];
                                }
                            }

                            resolved = true;
                            SetEntityType(type, expression);
                            break;
                        }
                    }
                    else if (parent.Kind == IqlExpressionKind.Property)
                    {
                        path.Add((IqlPropertyExpression)parent);
                    }
                    parent = parent.Parent;
                }
            }

            return resolved;
        }

        public bool IsRoot => Ancestors.Count == 1;
        private void DecrementPath(IqlExpression expression)
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

            if (expression != null)
            {
                if (_foundTypes.ContainsKey(expression))
                {
                    _foundTypes.Remove(expression);
                }
                var rootVariableName = TryResolveRootVariableName(expression);
                if (rootVariableName != null)
                {
                    _rootReferenceResolvedTypes.RemoveAt(_rootReferenceResolvedTypes.Count - 1);
                }
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