using System;

namespace IqlSampleApp.Data.Entities
{
    [Flags]
    public enum PersonSkills
    {
        Chef = 1,
        Coder = 2,
        Ninja = 4
    }
}