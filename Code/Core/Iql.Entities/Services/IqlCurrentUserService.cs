using System.Threading.Tasks;
using Iql.Parsing.Evaluation;

namespace Iql.Entities.Services
{
    public abstract class IqlCurrentUserService
    {
        public abstract Task<IqlObjectEvaluationResult> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider);
        public abstract Task<IqlObjectEvaluationResult> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider);
    }
}