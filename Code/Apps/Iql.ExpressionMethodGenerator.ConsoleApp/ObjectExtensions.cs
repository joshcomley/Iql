using System;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public static class ObjectExtensions
    {
        public static T Dump<T>(this T obj, string title = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(title + ":");
            }
            Console.WriteLine(obj.ToString());
            return obj;
        }
    }
}