using System;
using System.Threading.Tasks;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class ActionExtensions
    {
        public static Func<Task> AsAsync(this Action action)
        {
            if (action == null)
            {
                return null;
            }
            return () =>
            {
                action.Invoke();
                return Task.FromResult<object>(null);
            };
        }
    }
}