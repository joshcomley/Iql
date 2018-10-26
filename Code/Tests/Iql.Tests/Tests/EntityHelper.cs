using System;
using System.Text;
using Haz.App.Data.Entities;
using Iql.Data.Crud.Operations.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    public class EntityHelper
    {
        public static PersonTypeMap NewPersonTypeMap()
        {
            return new PersonTypeMap
            {
                CreatedDate = DateTimeOffset.Now,
                Description = "Some description",
                Notes = "Some notes",
                TypeId = 7,
                PersonId = 7
            };
        }
        public static PersonInspection NewPersonInspection()
        {
            return new PersonInspection
            {
                SiteInspectionId = 7,
                StartTime = DateTimeOffset.Now,
                EndTime = DateTimeOffset.Now,
                InspectionStatus = PersonInspectionStatus.PassWithObservations
            };
        }

        public static SiteInspection NewSiteInspection()
        {
            return new SiteInspection
            {
                SiteId = 1,
                StartTime = SimpleDate(),
                EndTime = SimpleDate(),
            };
        }

        public static HazClient NewHazClient()
        {
            return new HazClient
            {
                PersistenceKey = new Guid("e4a693fc-1041-4dd9-9f57-7097dd7053a3"),
                TypeId = 7,
                CreatedDate = SimpleDate(),
            };
        }

        public static DateTimeOffset SimpleDate()
        {
            return new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        public static void AssertSuccess(SaveChangesResult saveChangesResult)
        {
            Assert.IsTrue(saveChangesResult.Success, GetValidationFailures(saveChangesResult));
        }

        private static string GetValidationFailures(SaveChangesResult saveChangesResult)
        {
            if (saveChangesResult.Success)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            for (var i = 0; i < saveChangesResult.Results.Count; i++)
            {
                var result = saveChangesResult.Results[i];
                foreach (var validationResult in result.EntityValidationResults)
                {
                    foreach (var propertyValidationResult in validationResult.Value.PropertyValidationResults)
                    {
                        sb.AppendLine();
                        sb.AppendLine($"{propertyValidationResult.Property.Name}:");
                        foreach (var failure in propertyValidationResult.ValidationFailures)
                        {
                            sb.AppendLine($"{failure.Key}: {failure.Message}");
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}