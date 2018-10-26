using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class PersonReportIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<PersonReport>().DefinePropertyValidation(s => s.Title, s => string.IsNullOrWhiteSpace(s.Title),
                "Please enter a valid report title");
            builder.EntityType<PersonReport>().DefinePropertyValidation(s => s.Title, s => !string.IsNullOrWhiteSpace(s.Title) && s.Title.Trim().Length > 5,
                "Please enter less than five characters");
        }
    }
    public class PersonReportConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<PersonReport>()
                .HasRequired(d => d.Person, (report, person) => report.PersonId == person.Id,
                    person => person.Reports);
            builder
                .EntityType<PersonReport>()
                .HasRequired(d => d.Type, (report, type) => report.TypeId == type.Id, type => type.FaultReports);
            builder
                .EntityType<PersonReport>()
                .HasRequired(d => d.Type, (report, type) => report.TypeId == type.Id, type => type.FaultReports);
        }
    }
}