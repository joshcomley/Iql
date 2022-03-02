using System;

namespace Iql.Server.OData.Net.Extensions;

public static class IqlServerTypeExtensions
{
    public static Type UnwrapNullable(this Type type)
    {
        try
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
        catch
        {
            return type;
        }
    }
}