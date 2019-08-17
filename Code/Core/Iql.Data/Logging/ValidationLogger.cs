using System.Collections.Generic;
using System.Text;
using Iql.Data.Crud;
using Iql.Entities.Extensions;
using Iql.Entities.Validation.Validation;
using Iql.Parsing.Types;

namespace Iql.Data.Logging
{
    public class ValidationLogger
    {
        public ITypeResolver TypeResolver { get; }
        private StringBuilder _sb;

        public ValidationLogger(ITypeResolver typeResolver)
        {
            TypeResolver = typeResolver;
            _sb = new StringBuilder();
        }

        public void Clear()
        {
            _sb = new StringBuilder();
        }

        public string ParseResults(IEntityCrudResult[] results)
        {
            for (var i = 0; i < results.Length; i++)
            {
                ParseValidationResult(results[i]);
            }
            var str = _sb.ToString();
            if (str == "")
            {
                str = "None";
            }
            return "Validation errors:\n" + str;
        }

        public void ParseValidationResult(IEntityCrudResult validationResult)
        {
            var config = TypeResolver.FindTypeByType(validationResult.LocalEntity.GetType());
            _sb.AppendLine($"Entity type: {config.EntityConfiguration().FriendlyName}");
            _sb.AppendLine($"Entity key: {config.EntityConfiguration().GetCompositeKeyString(validationResult.LocalEntity)}");
            foreach (var item in validationResult.EntityValidationResults)
            {
                ParseEntityValidationResult(item.Value);
            }
        }

        public void ParseEntityValidationResult(IEntityValidationResult validationResult)
        {
            if (validationResult == null)
            {
                return;
            }

            ParseValidationFailures(validationResult.ValidationFailures);

            if (validationResult.PropertyValidationResults != null)
            {
                foreach (var propertyResult in validationResult.PropertyValidationResults)
                {
                    ParsePropertyValidationResult(propertyResult);
                }
            }
            if (validationResult.RelationshipValidationResults != null)
            {
                foreach (var relationshipResult in validationResult.RelationshipValidationResults)
                {
                    ParsePropertyValidationResult(relationshipResult);
                }
            }
        }

        public void ParsePropertyValidationResult(IPropertyValidationResult validationResult)
        {
            if (validationResult == null)
            {
                return;
            }
            _sb.AppendLine($"Property - {validationResult.Property.PropertyName}:");
            ParseValidationFailures(
                validationResult.ValidationFailures);
        }

        public void ParseValidationFailures(IReadOnlyList<ValidationError> validationFailures)
        {
            if (validationFailures == null)
            {
                return;
            }
            for (var i = 0; i < validationFailures.Count; i++)
            {
                ParseValidationFailure(validationFailures[i]);
            }
        }

        public void ParseValidationFailure(ValidationError validationFailure)
        {
            _sb.AppendLine($"Validation key: {validationFailure.Key}");
            _sb.AppendLine($"Validation message:");
            _sb.AppendLine($"{validationFailure.Message}");
        }

        public string AsString()
        {
            return _sb.ToString();
        }
    }
}