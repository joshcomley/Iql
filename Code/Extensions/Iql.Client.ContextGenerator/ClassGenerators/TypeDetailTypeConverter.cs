using System;
using System.Linq;
using System.Reflection;
using Brandless.ObjectSerializer;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Functions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class TypeDetailTypeConverter : ICSharpObjectSerializerConverter
    {
        public bool CanConvert(Type objectType, object @object, object propertyOwner, PropertyInfo propertyBeingAssigned)
        {
            return objectType == typeof(Type) &&

                   ((
                        propertyOwner is TypeDetail &&
                        (propertyBeingAssigned?.Name == nameof(TypeDetail.ElementType) ||
                         propertyBeingAssigned?.Name == nameof(TypeDetail.Type))
                    ) ||
                    (propertyOwner is IqlMethod &&
                     (propertyBeingAssigned?.Name == nameof(IqlMethod.ReturnType)))
                   )
                ;
        }

        public ConversionResult Convert(Type objectType, object @object, object propertyOwner, PropertyInfo propertyBeingAssigned)
        {
            var nameProperty = propertyBeingAssigned.DeclaringType.GetProperty($"{propertyBeingAssigned.Name}Name");
            var name = nameProperty.GetValue(propertyOwner) as string;
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ConversionResult(null, false);
            }

            var type = TypeName.Parse(name, true);
            return new ConversionResult(
                SyntaxFactory.TypeOfExpression(TypeSyntax(type)),
                true);
        }

        public static TypeSyntax TypeSyntax(TypeName type)
        {
            TypeSyntax nameForIntialiser;
            var genericArguments = type.Generics;
            if (genericArguments.Length > 0)
            {
                nameForIntialiser = SyntaxFactory.GenericName(
                        SyntaxFactory.Identifier(type.Name))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SeparatedList(
                                genericArguments.Select(_=>TypeSyntax(TypeName.Parse(_)))
                            )));
            }
            else
            {
                nameForIntialiser = SyntaxFactory.IdentifierName(type.Name);
            }

            return Microsoft.CodeAnalysis.SyntaxNodeExtensions.NormalizeWhitespace(nameForIntialiser);
        }
    }
}