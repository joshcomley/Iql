using System.Collections.Generic;
using Iql.Entities.Functions;

namespace Iql.Entities
{
    public interface IMethodContainer
    {
        List<IqlMethod> Methods { get; set; }
    }
}