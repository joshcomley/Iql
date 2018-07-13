using System;

namespace Iql.Server
{
    public static class TypeExtensions
    {
        public static GenericBaseType TryGetBaseType(this Type entityType, Type type,
            Action<GenericBaseType> onSuccess = null)
        {
            var result = entityType.TryGetBaseTypeInternal(type);
            if (result != null)
            {
                onSuccess?.Invoke(result);
            }
            return result;
        }

        private static GenericBaseType TryGetBaseTypeInternal(this Type entityType, Type type)
        {
            if (type.IsAssignableFrom(entityType) || type.IsAssignableFromGeneric(entityType))
            {
                var baseTypeDefinition = entityType;
                var baseType = entityType;
                void Update()
                {
                    if (baseTypeDefinition?.IsGenericType == true)
                    {
                        baseTypeDefinition = baseTypeDefinition.GetGenericTypeDefinition();
                    }
                }
                Update();

                while (baseType != null &&
                       baseType != type &&
                       baseTypeDefinition != null &&
                       baseTypeDefinition != type)
                {
                    foreach (var @interface in baseTypeDefinition.GetInterfaces())
                    {
                        var interfaceType =
                            @interface.TryGetBaseTypeInternal(type);
                        if (interfaceType != null)
                        {
                            return interfaceType;
                        }
                    }

                    baseTypeDefinition = baseTypeDefinition.BaseType;
                    baseType = baseType.BaseType;
                    Update();
                }

                if (baseTypeDefinition != null)
                {
                    var genericBaseType = new GenericBaseType(baseType, baseTypeDefinition);
                    return genericBaseType;
                }
            }

            return null;
        }

        public static bool IsAssignableFromGeneric(this Type genericType, Type givenType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return genericType.IsAssignableFromGeneric(baseType);
        }
    }
}