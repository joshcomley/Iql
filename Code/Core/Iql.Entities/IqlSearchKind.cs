using System;

namespace Iql.Entities
{
    [Flags]
    public enum IqlSearchKind
    {
        Primary = 1,
        Secondary = 2
    }
}