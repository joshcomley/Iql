using System.Threading.Tasks;
using Iql.Parsing.Evaluation;

namespace Iql.Entities.Services
{
    public abstract class IqlCurrentLocationService
    {
        public abstract Task<IqlEvaluationResult<IqlPointExpression>> ResolveCurrentLocationAsync(IqlServiceProvider serviceProvider);
    }
}