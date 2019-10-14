using System;

namespace Consolegram.Local
{
    public static class Program
    {
        public static void Main()
        {
            ConsolegramClientFactory.New().Run();
        }
    }
}