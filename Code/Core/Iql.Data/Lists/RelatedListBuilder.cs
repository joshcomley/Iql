using System;

namespace Iql.Queryable.Data.Lists
{
    public static class RelatedListBuilder
    {
        public static IRelatedList NewEmptyClone(this IRelatedList source, object newOwner)
        {
            return New(source.PropertyName, newOwner, source.OwnerType, source.TargetType);
        }

        public static IRelatedList New(string propertyName, object owner, Type ownerType, Type relationshipType)
        {
            return (IRelatedList)
                Activator.CreateInstance(typeof(RelatedList<,>).MakeGenericType(ownerType, relationshipType),
                    new object[]
                    {
                        owner,
                        propertyName,
                        null
#if TypeScript
                        , ownerType
                        , relationshipType
#endif
                    });
        }
    }
}