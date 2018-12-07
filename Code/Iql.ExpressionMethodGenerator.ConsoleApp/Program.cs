using System;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Flatten.Generate();
            Clone.Generate();
            Replace.Generate();
        }
    }
}
