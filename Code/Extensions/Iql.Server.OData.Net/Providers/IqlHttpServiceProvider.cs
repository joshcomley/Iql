using Iql.Data.Evaluation;
using Iql.Entities.Services;

namespace Iql.Server.OData.Net
{
    public class IqlHttpServiceProvider<TUser> : IqlServiceProvider
        where TUser : class
    {
        public IqlHttpServiceProviderContext Context { get; }

        public IqlHttpServiceProvider(IqlHttpServiceProviderContext context)
        {
            Context = context;
            RegisterInstance(new IqlHttpCurrentUserService<TUser>(context));
            RegisterInstance(context.DataEvaluator);
            RegisterInstance(context.CurrentUserService);
        }
    }
}