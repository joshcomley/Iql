using Iql.Entities.Services;

namespace Iql.Server.OData.Net
{
    public class IqlHttpServiceProviderProvider<TUser> : IServiceProviderProvider
        where TUser : class
    {
        public IqlHttpServiceProviderContext Context { get; }
        public IqlServiceProvider ServiceProvider { get; }

        public IqlHttpServiceProviderProvider(IqlHttpServiceProviderContext context)
        {
            Context = context;
            ServiceProvider = new IqlHttpServiceProvider<TUser>(context);
        }
    }
}