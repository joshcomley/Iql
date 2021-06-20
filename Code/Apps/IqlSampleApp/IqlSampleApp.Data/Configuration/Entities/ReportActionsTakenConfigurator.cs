using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ReportActionsTakenIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<ReportActionsTaken>().DefinePropertyValidation(s => s.Notes, s => s.Notes != null || s.Notes != "",
                "Please enter some actions taken notes");
            builder.EntityType<ReportActionsTaken>().DefinePropertyValidation(s => s.Notes, s => s.Notes.Length > 5,
                "Please enter at least five characters for notes");
        }
    }
    public class ReportActionsTakenConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<ReportActionsTaken>()
                .HasRequired(d => d.PersonReport, (taken, report) => taken.FaultReportId == report.Id,
                    report => report.ActionsTaken);
        }
    }
}