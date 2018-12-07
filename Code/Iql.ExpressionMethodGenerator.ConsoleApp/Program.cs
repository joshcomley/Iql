using System;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncNonAsyncSync.Sync();
            Flatten.Generate();
            Clone.Generate();
            Replace.Generate();
        }
    }
}
