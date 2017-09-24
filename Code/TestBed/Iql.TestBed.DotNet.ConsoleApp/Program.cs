using System;
using System.Threading.Tasks;

namespace Iql.TestBed.DotNet.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DotNet.Main.Run();
        }
    }
}
