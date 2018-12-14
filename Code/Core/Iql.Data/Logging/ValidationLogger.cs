using System.Collections.Generic;
using System.Text;
using Iql.Data.Crud;
using Iql.Entities;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Logging
{
    public class ValidationLogger
    {
        private StringBuilder _sb;

        public ValidationLogger()
        {
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
            var config = EntityConfigurationBuilder.FindConfigurationForEntityType(
                validationResult.LocalEntity.GetType());
            _sb.AppendLine($"{config.FriendlyName}:");
            _sb.AppendLine($"Key: {config.GetCompositeKeyString(validationResult.LocalEntity)}");
            for (var i = 0; i < validationResult.EntityValidationResults.Count; i++)
            {
                ParseEntityValidationResult(validationResult.EntityValidationResults[i]);
            }
        }

        private void ParseEntityValidationResult(IEntityValidationResult validationResult)
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

        private void ParsePropertyValidationResult(IPropertyValidationResult validationResult)
        {
            if (validationResult == null)
            {
                return;
            }
            _sb.AppendLine($"Property - {validationResult.Property.PropertyName}:");
            ParseValidationFailures(
                validationResult.ValidationFailures);
        }

        private void ParseValidationFailures(IReadOnlyList<ValidationError> validationFailures)
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

        private void ParseValidationFailure(ValidationError validationFailure)
        {
            _sb.AppendLine($"{validationFailure.Key}:");
            _sb.AppendLine($"{validationFailure.Message}");
        }
    }
}