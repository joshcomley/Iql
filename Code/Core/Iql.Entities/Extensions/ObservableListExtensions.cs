using Iql.Entities.Lists;

namespace Iql.Entities.Extensions
{
    public static class ObservableListExtensions
    {
        public static T SetEnsureUnique<T>(this T list, bool ensureUnique = true)
            where T : IObservableList
        {
            list.EnsureUnique = ensureUnique;
            return list;
        }
    }
}