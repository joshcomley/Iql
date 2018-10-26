using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class RiskAssessmentAnswerConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<RiskAssessmentAnswer>()
                .HasRequired(d => d.Question, (answer, question) => answer.QuestionId == question.Id,
                    question => question.Answers);
        }
    }
}