namespace Iql.Queryable.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryResultBaseExtensions
    {
        public static T GetRoot<T>(this T result)
            where T : IQueryResultBase
        {
            var parent = result;
            while (parent.ParentResult != null)
            {
                parent = (T)parent.ParentResult;
            }

            return parent;
        }
    }
}