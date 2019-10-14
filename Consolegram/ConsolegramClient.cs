using System;
using System.Collections.Generic;
using System.Threading;
using Db.Logging.Abstractions;

namespace Consolegram
{
    public class ConsolegramClient
    {
        private readonly ITelegramApi api;

        private readonly ILog log;

        private readonly CancellationTokenSource mainCts;

        public ConsolegramClient(ITelegramApi api, ILog log)
        {
            this.api = api;
            this.log = log;
            mainCts = new CancellationTokenSource();
        }
        
        public void Run()
        {
            Greet();
            LogIn();
            
            foreach (var input in ReadInput(mainCts.Token))
                ParseInput(input);

            Bye();
        }

        public void Stop()
        {
            mainCts.Cancel();
            api.Dispose();
        }

        private void ParseInput(string input)
        {
            Console.WriteLine(input);
        }

        private static IEnumerable<string> ReadInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
                yield return Console.ReadLine();
        }

        private void LogIn()
        {
            Console.WriteLine("Enter phone");
            var input = Console.ReadLine();
            api.Auth(input);
        }

        private static void Greet()
        {
            var width = Console.WindowWidth;
            var greeting = "Welcome to Consolegram";
            Console.Write(new string(' ', width / 2 - greeting.Length / 2));
            Console.WriteLine(greeting);
        }

        private static void Bye()
        {
            Console.WriteLine("Bye!");
        }
    }
}