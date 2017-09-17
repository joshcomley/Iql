using TypeSharp.Extensions;
#if !TypeScript
using Iql.Queryable.Data.Tracking.Cloning;
#endif

namespace Iql.Queryable.Data.Tracking
{
    [DoNotConvert]
    public static class Cloner
    {
        public static T Clone<T>(this T obj)
        {
#if TypeScript
            return obj;
#else
            return obj.Copy();
#endif
        }
    }
}