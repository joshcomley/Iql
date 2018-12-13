using System.Threading.Tasks;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;

namespace Iql.Tests.Tests.Services
{
    public class TestCurrentLocationResolver : IqlCurrentLocationService
    {
        public static double? CurrentLatitude = null;
        public static double? CurrentLongitude = null;
        public override Task<IqlEvaluationResult<IqlPointExpression>> ResolveCurrentLocationAsync(IqlServiceProvider serviceProvider)
        {
            if (CurrentLongitude == null || CurrentLatitude == null)
            {
                return Task.FromResult(
                    new IqlEvaluationResult<IqlPointExpression>(false, null));
            }
            return Task.FromResult(new IqlEvaluationResult<IqlPointExpression>(true,
                new IqlPointExpression(
                    CurrentLongitude.Value,
                    CurrentLatitude.Value)));
        }
    }
}