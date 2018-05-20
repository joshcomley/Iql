using System.Collections.Generic;

namespace Iql.Tests.Utility
{
    public class RelationshipFilterContextExpressions
    {
        public static IqlExpression Get()
        {
            return new IqlLambdaExpression
            {
                Body = new IqlLambdaExpression
                {
                    Body = new IqlIsEqualToExpression
                    {
                        Left = new IqlPropertyExpression
                        {
                            PropertyName = "ClientId",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                EntityTypeName = "Site",
                                VariableName = "site",
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        },
                        Right = new IqlPropertyExpression
                        {
                            PropertyName = "ClientId",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlPropertyExpression
                            {
                                PropertyName = "Owner",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlVariableExpression
                                {
                                    EntityTypeName = "RelationshipFilterContext<Site>",
                                    VariableName = "context",
                                    Kind = IqlExpressionKind.Variable,
                                    ReturnType = IqlType.Unknown
                                }
                            }
                        },
                        Kind = IqlExpressionKind.IsEqualTo,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "Site",
                            VariableName = "site",
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                Parameters = new List<IqlRootReferenceExpression>
                {
                    new IqlRootReferenceExpression
                    {
                        EntityTypeName = "RelationshipFilterContext<Site>",
                        VariableName = "context",
                        Kind = IqlExpressionKind.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                },
                Kind = IqlExpressionKind.Lambda,
                ReturnType = IqlType.Unknown
            };
        }

        public static IqlExpression Get2()
        {
            return new IqlLambdaExpression
            {
                Body = new IqlLambdaExpression
                {
                    Body = new IqlIsEqualToExpression
                    {
                        Left = new IqlPropertyExpression
                        {
                            PropertyName = "Name",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                EntityTypeName = "PersonLoading",
                                VariableName = "loading",
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        },
                        Right = new IqlPropertyExpression
                        {
                            PropertyName = "Title",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlPropertyExpression
                            {
                                PropertyName = "Owner",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlVariableExpression
                                {
                                    EntityTypeName = "RelationshipFilterContext<Person>",
                                    VariableName = "context",
                                    Kind = IqlExpressionKind.Variable,
                                    ReturnType = IqlType.Unknown
                                }
                            }
                        },
                        Kind = IqlExpressionKind.IsEqualTo,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "PersonLoading",
                            VariableName = "loading",
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                Parameters = new List<IqlRootReferenceExpression>
                {
                    new IqlRootReferenceExpression
                    {
                        EntityTypeName = "RelationshipFilterContext<Person>",
                        VariableName = "context",
                        Kind = IqlExpressionKind.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                },
                Kind = IqlExpressionKind.Lambda,
                ReturnType = IqlType.Unknown
            };
        }
    }
}